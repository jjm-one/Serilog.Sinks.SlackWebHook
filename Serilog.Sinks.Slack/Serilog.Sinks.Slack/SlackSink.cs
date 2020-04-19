using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Slack.Webhooks;

namespace Serilog.Sinks.Slack
{
    public class SlackSink : PeriodicBatchingSink
    {
        private static HttpClient _slackHttpClient;

        private readonly SlackClient _slackClient;

        private readonly SlackSinkOptions _slackSinkOptions;

        private readonly LoggingLevelSwitch _sinkLevelSwitch;

        private readonly IFormatProvider _formatProvider;

        private static Func<LogEvent, IFormatProvider, object, string> _generateSlackMessageText;
        private static Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> _generateSlackMessageAttachments;
        private static Func<LogEvent, IFormatProvider, object, List<Block>> _generateSlackMessageBlocks;

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
                slackSinkOptions.PeriodicBatchingSinkOptionsBatchSizeLimit ?? default,
                slackSinkOptions.PeriodicBatchingSinkOptionsPeriod ?? default,
                slackSinkOptions.PeriodicBatchingSinkOptionsQueueLimit ?? default)
        {
            _slackSinkOptions = slackSinkOptions;
            _formatProvider = formatProvider;
            _sinkLevelSwitch = sinkLevelSwitch ?? new LoggingLevelSwitch(LevelAlias.Minimum);
            _slackHttpClient = slackHttpClient ?? new HttpClient();
            if (generateSlackMessageText == null) _generateSlackMessageText = SlackSinkMessageTools.GenerateSlackMessageText;
            if (generateSlackMessageAttachments == null) _generateSlackMessageAttachments = SlackSinkMessageTools.GenerateSlackMessageAttachments;
            if (generateSlackMessageBlocks == null) _generateSlackMessageBlocks = SlackSinkMessageTools.GenerateSlackMessageBlocks;


            _slackClient = new SlackClient(slackSinkOptions.SlackWebHookUrl, slackSinkOptions.SlackConnectionTimeout ?? default, _slackHttpClient);
        }

        protected override void Dispose(bool disposing)
        {
            _slackClient.Dispose();
            _slackHttpClient.Dispose();
            base.Dispose(disposing);
        }

        protected override async Task EmitBatchAsync(IEnumerable<LogEvent> events)
        {
            foreach (var logEvent in events)
            {
                if (logEvent.Level < _slackSinkOptions.SinkRestrictedToMinimumLevel) continue;
                if (logEvent.Level < _sinkLevelSwitch.MinimumLevel) continue;

                var msg = new SlackMessage()
                {
                    Attachments = _generateSlackMessageAttachments(logEvent, _formatProvider, _slackSinkOptions),
                    Blocks = _generateSlackMessageBlocks(logEvent, _formatProvider, _slackSinkOptions),
                    Channel = _slackSinkOptions.SlackChannels != null && _slackSinkOptions.SlackChannels.Any() ? _slackSinkOptions.SlackChannels[0] : null,
                    DeleteOriginal = _slackSinkOptions.SlackDeleteOriginal ?? default,
                    IconEmoji = _slackSinkOptions.SlackEmojiIcon,
                    IconUrl = _slackSinkOptions.SlackUriIcon,
                    LinkNames = _slackSinkOptions.SlackLinkNames ?? default,
                    Markdown = _slackSinkOptions.SlackMarkdown ?? default,
                    Parse = _slackSinkOptions.SlackParse ?? default,
                    ReplaceOriginal = _slackSinkOptions.SlackReplaceOriginal ?? default,
                    ResponseType = _slackSinkOptions.SlackResponseType,
                    Text = _generateSlackMessageText(logEvent, _formatProvider, _slackSinkOptions),
                    ThreadId = _slackSinkOptions.SlackThreadId,
                    Username = _slackSinkOptions.SlackUsername
                };

                if (_slackSinkOptions.SlackChannels != null)
                {
                    var logMsgPosts = _slackClient.PostToChannelsAsync(msg, _slackSinkOptions.SlackChannels);

                    foreach (var logMsgPost in logMsgPosts)
                    {
                        await logMsgPost;
                    }
                }
                else
                {
                    await _slackClient.PostAsync(msg);
                }
            }
        }
    }
}
