using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Slack.Webhooks;

namespace Serilog.Sinks.SlackWebHook
{
    /// <summary>
    /// Main Class of the SlackSink.
    /// </summary>
    public class SlackSink : PeriodicBatchingSink
    {
        #region private members

        /// <summary>
        /// HttpClient instance for the SlackClient.
        /// </summary>
        private readonly HttpClient _slackHttpClient;

        /// <summary>
        /// SlackClient.
        /// </summary>
        private readonly SlackClient _slackClient;

        /// <summary>
        /// Options for this Sink.
        /// </summary>
        private readonly SlackSinkOptions _slackSinkOptions;

        /// <summary>
        /// Switch to change the minimum LogEventLevel.
        /// </summary>
        private readonly LoggingLevelSwitch _sinkLevelSwitch;

        /// <summary>
        /// FormatProvider.
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
        /// Main constructor for the SlackSink.
        /// </summary>
        /// <param name="slackSinkOptions">Options for this Sink.</param>
        /// <param name="formatProvider">FormatProvider.</param>
        /// <param name="sinkLevelSwitch">LoggingLevelSwitch.</param>
        /// <param name="slackHttpClient">HttpClient.</param>
        /// <param name="generateSlackMessageText">GenerateSlackMessageText.</param>
        /// <param name="generateSlackMessageAttachments">GenerateSlackMessageAttachments.</param>
        /// <param name="generateSlackMessageBlocks">GenerateSlackMessageBlocks.</param>
        public SlackSink(
            SlackSinkOptions slackSinkOptions,
            IFormatProvider formatProvider,
            LoggingLevelSwitch sinkLevelSwitch = null,
            HttpClient slackHttpClient = null,
            Func<LogEvent, IFormatProvider, object, string> generateSlackMessageText = null,
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> generateSlackMessageAttachments = null,
            Func<LogEvent, IFormatProvider, object, List<Block>> generateSlackMessageBlocks = null
            )
            : base(
                slackSinkOptions.PeriodicBatchingSinkOptionsBatchSizeLimit,
                slackSinkOptions.PeriodicBatchingSinkOptionsPeriod,
                slackSinkOptions.PeriodicBatchingSinkOptionsQueueLimit)
        {
            _slackSinkOptions = slackSinkOptions;
            _formatProvider = formatProvider;
            _sinkLevelSwitch = sinkLevelSwitch ?? new LoggingLevelSwitch(LevelAlias.Minimum);
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
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            _slackClient.Dispose();
            _slackHttpClient.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                // check log level
                if (logEvent.Level < _slackSinkOptions.SinkRestrictedToMinimumLevel) continue;
                if (logEvent.Level < _sinkLevelSwitch.MinimumLevel) continue;

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

                    foreach (var logMsgPost in logMsgPosts)
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

        #endregion
    }
}
