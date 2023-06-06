using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Slack.Webhooks;

namespace jjm.one.Serilog.Sinks.SlackWebHook
{
	public class SlackSerilogBatchedLogEventSink : IBatchedLogEventSink
    {
        #region private members

        /// <summary>
        /// <see cref="HttpClient"/> instance for the SlackClient.
        /// </summary>
        private readonly HttpClient _slackHttpClient;

        /// <summary>
        /// <see cref="SlackSink"/> instance.
        /// </summary>
        private readonly SlackClient _slackClient;

        /// <summary>
        /// <see cref="SlackSinkOptions"/> object for this Sink.
        /// </summary>
        private readonly SlackSinkOptions _slackSinkOptions;

        /// <summary>
        /// <see cref="SlackSinkActivationSwitch"/> to change the activation status of the sink on the fly.
        /// </summary>
        private readonly SlackSinkActivationSwitch _slackSinkActivationSwitch;

        /// <summary>
        /// <see cref="IFormatProvider"/> object.
        /// </summary>
        private readonly IFormatProvider _formatProvider;

        /// <summary>
        /// Function to generate the text of the slack message.
        /// </summary>
        private static Func<LogEvent, IFormatProvider, object, string> _generateSlackMessageText;

        /// <summary>
        /// Function to generate the attachments of the slack message.
        /// </summary>
        private static Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> _generateSlackMessageAttachments;

        /// <summary>
        /// Function to generate the blocks of the slack message.
        /// </summary>
        private static Func<LogEvent, IFormatProvider, object, List<Block>> _generateSlackMessageBlocks;

        #endregion

        #region constructor

        /// <summary>
        /// Initializes new instance of <see cref="SlackSink"/>.
        /// </summary>
        /// <param name="slackSinkOptions">Slack Sink Options object.</param>
        /// <param name="formatProvider">FormatProvider object.</param>
        /// <param name="slackHttpClient">HttpClient instance.</param>
        /// <param name="generateSlackMessageText">GenerateSlackMessageText function.</param>
        /// <param name="generateSlackMessageAttachments">GenerateSlackMessageAttachments function.</param>
        /// <param name="generateSlackMessageBlocks">GenerateSlackMessageBlocks function.</param>
        /// <param name="statusSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
        public SlackSerilogBatchedLogEventSink(
            SlackSinkOptions slackSinkOptions,
            IFormatProvider formatProvider,
            SlackSinkActivationSwitch statusSwitch = null,
            HttpClient slackHttpClient = null,
            Func<LogEvent, IFormatProvider, object, string> generateSlackMessageText = null,
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> generateSlackMessageAttachments = null,
            Func<LogEvent, IFormatProvider, object, List<Block>> generateSlackMessageBlocks = null
            )
        {
            _slackSinkOptions = slackSinkOptions;
            _formatProvider = formatProvider;
            _slackSinkActivationSwitch = statusSwitch ?? new SlackSinkActivationSwitch();
            _slackHttpClient = slackHttpClient ?? new HttpClient();

            // if no extern generation functions were specified, use the default ones
            if (generateSlackMessageText == null) _generateSlackMessageText = SlackSinkMessageTools.GenerateSlackMessageText;
            if (generateSlackMessageAttachments == null) _generateSlackMessageAttachments = SlackSinkMessageTools.GenerateSlackMessageAttachments;
            if (generateSlackMessageBlocks == null) _generateSlackMessageBlocks = SlackSinkMessageTools.GenerateSlackMessageBlocks;

            // start new SlackClient
            _slackClient = new SlackClient(slackSinkOptions.SlackWebHookUrl, slackSinkOptions.SlackConnectionTimeout, _slackHttpClient);
        }

        #endregion

        #region function override

        /// <summary>
        /// Implements a <see cref="Dispose"/> function  to close the <see cref="SlackClient"/> and the <see cref="HttpClient"/>.
        /// </summary>
        public void Dispose()
        {
            _slackClient.Dispose();
            _slackHttpClient.Dispose();
        }

        /// <summary>
        /// Implements <see cref="IBatchedLogEventSink.EmitBatchAsync"/> and sends the <see cref="LogEvent"/> with a <see cref="SlackClient"/> to Slack. 
        /// </summary>
        /// <param name="batch">A collection of <see cref="LogEvent"/>.</param>
        /// <returns>An Awaitable Task.</returns>
        async Task IBatchedLogEventSink.EmitBatchAsync(IEnumerable<LogEvent> batch)
        {
            // check activation status
            if (_slackSinkActivationSwitch.SlackSinkStatus == SlackSinkActivationSwitch.SlackSinkActivationStatus.InActive)
            {
                return;
            }

            foreach (LogEvent logEvent in batch)
            {
                // create new slack message
                var msg = new SlackMessage
                {
                    Attachments = _generateSlackMessageAttachments(logEvent, _formatProvider, _slackSinkOptions),
                    Blocks = _generateSlackMessageBlocks(logEvent, _formatProvider, _slackSinkOptions),
                    Channel = _slackSinkOptions.SlackChannels != null && _slackSinkOptions.SlackChannels.Any() ? _slackSinkOptions.SlackChannels[0] : null,
                    DeleteOriginal = _slackSinkOptions.SlackDeleteOriginal,
                    IconEmoji = _slackSinkOptions.SlackEmojiIcon,
                    IconUrl = _slackSinkOptions.SlackUriIcon,
                    LinkNames = _slackSinkOptions.SlackLinkNames,
                    Markdown = _slackSinkOptions.SlackMarkdown,
                    Parse = _slackSinkOptions.SlackParse,
                    ReplaceOriginal = _slackSinkOptions.SlackReplaceOriginal,
                    ResponseType = _slackSinkOptions.SlackResponseType,
                    Text = _generateSlackMessageText(logEvent, _formatProvider, _slackSinkOptions),
                    ThreadId = _slackSinkOptions.SlackThreadId,
                    Username = _slackSinkOptions.SlackUsername
                };

                // check for multi channel post
                if (_slackSinkOptions.SlackChannels != null)
                {
                    // multi channel post
                    var logMsgPosts = _slackClient.PostToChannelsAsync(msg, _slackSinkOptions.SlackChannels);

                    foreach (Task<bool> logMsgPost in logMsgPosts)
                    {
                        await logMsgPost;
                    }
                }
                else
                {
                    // single channel post
                    await _slackClient.PostAsync(msg);
                }
            }
        }

        /// <summary>
        /// Implements <see cref="IBatchedLogEventSink.OnEmptyBatchAsync"/>.
        /// </summary>
        /// <returns>An Awaitable Task.</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        async Task IBatchedLogEventSink.OnEmptyBatchAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            return;
        }

        #endregion
    }
}

