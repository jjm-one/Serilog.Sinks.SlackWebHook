using System;
using System.Collections.Generic;
using Serilog.Events;
using Slack.Webhooks;

namespace Serilog.Sinks.Slack
{
    public class SlackSinkOptions
    {
        // defaults
        public const string DefaultOutputTemplate = "{Message:lj}";
        public const int DefaultBatchSizeLimit = 10;
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);
        public const int DefaultQueueLimit = 10000;
        public const int DefaultTimeout = 1000;


        // Slack
        public string SlackWebHookUrl { get; set; }
        public string SlackUsername { get; set; } = null;
        public string SlackEmojiIcon { get; set; } = null;
        public Uri SlackUriIcon { get; set; } = null;
        public List<string> SlackChannels { get; set; } = null;
        public bool? SlackDeleteOriginal { get; set; } = null;
        public bool? SlackLinkNames { get; set; } = null;
        public bool? SlackMarkdown { get; set; } = null;
        public ParseMode? SlackParse { get; set; } = null;
        public bool? SlackReplaceOriginal { get; set; } = null;
        public string SlackResponseType { get; set; } = null;
        public string SlackThreadId { get; set; } = null;

        // slack attachment settings
        public IDictionary<LogEventLevel, string> SlackAttachmentColors { get; set; } = new Dictionary<LogEventLevel, string>
        {
            {LogEventLevel.Verbose, "#DFDFDF"},
            {LogEventLevel.Debug, "#00C9FF"},
            {LogEventLevel.Information, "#45FF00"},
            {LogEventLevel.Warning, "#FF7200"},
            {LogEventLevel.Error, "#FF0000"},
            {LogEventLevel.Fatal, "#900000"}
        };
        public IDictionary<LogEventLevel, string> SlackAttachmentFooterIcon { get; set; } = new Dictionary<LogEventLevel, string>
        {
            {LogEventLevel.Verbose, null},
            {LogEventLevel.Debug, Emoji.Bug},
            {LogEventLevel.Information, Emoji.InformationSource},
            {LogEventLevel.Warning, Emoji.Warning},
            {LogEventLevel.Error, Emoji.Bomb},
            {LogEventLevel.Fatal, Emoji.Fire}
        };

        public bool SlackAddShortInfoAttachment { get; set; } = true;
        public bool SlackDisplayShortInfoAttachmentShort { get; set; } = true;
        public bool SlackAddExtendedInfoAttachment { get; set; } = false;
        public bool SlackDisplayExtendedInfoAttachmentShort { get; set; } = true;
        public bool SlackAddExceptionAttachment { get; set; } = true;
        public bool SlackDisplayExceptionAttachmentShort { get; set; } = true;

        // slack connection
        public int? SlackConnectionTimeout { get; set; } = DefaultTimeout;


        // Periodic Batch Sink
        public int? PeriodicBatchingSinkOptionsBatchSizeLimit { get; set; } = DefaultBatchSizeLimit;
        public TimeSpan? PeriodicBatchingSinkOptionsPeriod { get; set; } = DefaultPeriod;
        public int? PeriodicBatchingSinkOptionsQueueLimit { get; set; } = DefaultQueueLimit;


        // Sink
        public LogEventLevel SinkRestrictedToMinimumLevel { get; set; } = LevelAlias.Minimum;
        public string SinkOutputTemplate { get; set; } = DefaultOutputTemplate;
    }
}
