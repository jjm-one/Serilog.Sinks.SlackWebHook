using System;
using System.Collections.Generic;
using Serilog.Events;
using Slack.Webhooks;

namespace Serilog.Sinks.SlackWebHook
{
    public class SlackSinkOptions
    {
        #region default values

        /// <summary>
        /// 
        /// </summary>
        public const string DefaultOutputTemplate = "{Message:lj}";

        /// <summary>
        /// 
        /// </summary>
        public const int DefaultBatchSizeLimit = 10;

        /// <summary>
        /// 
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// 
        /// </summary>
        public const int DefaultQueueLimit = 10000;
        
        /// <summary>
        /// 
        /// </summary>
        public const int DefaultTimeout = 1000;

        #endregion

        #region slack options

        #region slack connection options

        /// <summary>
        /// Url for the Slack WebHook.
        /// </summary>
        public string SlackWebHookUrl { get; set; }

        /// <summary>
        /// Timeout for the connection the Slack server.
        /// </summary>
        public int SlackConnectionTimeout { get; set; } = DefaultTimeout;

        #endregion

        #region generall slack & message options

        /// <summary>
        /// 
        /// </summary>
        public string SlackUsername { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public string SlackEmojiIcon { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public Uri SlackUriIcon { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public List<string> SlackChannels { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackDeleteOriginal { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackLinkNames { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackMarkdown { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public ParseMode SlackParse { get; set; } = ParseMode.None;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackReplaceOriginal { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public string SlackResponseType { get; set; } = null;

        /// <summary>
        /// 
        /// </summary>
        public string SlackThreadId { get; set; } = null;

        #endregion

        #region slack attachment options

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<LogEventLevel, string> SlackAttachmentColors { get; set; } = new Dictionary<LogEventLevel, string>
        {
            {LogEventLevel.Verbose, "#DFDFDF"},
            {LogEventLevel.Debug, "#00C9FF"},
            {LogEventLevel.Information, "#45FF00"},
            {LogEventLevel.Warning, "#FF7200"},
            {LogEventLevel.Error, "#FF0000"},
            {LogEventLevel.Fatal, "#900000"}
        };

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<LogEventLevel, string> SlackAttachmentFooterIcon { get; set; } = new Dictionary<LogEventLevel, string>
        {
            {LogEventLevel.Verbose, null},
            {LogEventLevel.Debug, Emoji.Bug},
            {LogEventLevel.Information, Emoji.InformationSource},
            {LogEventLevel.Warning, Emoji.Warning},
            {LogEventLevel.Error, Emoji.Bomb},
            {LogEventLevel.Fatal, Emoji.Fire}
        };

        /// <summary>
        /// 
        /// </summary>
        public bool SlackAddShortInfoAttachment { get; set; } = true;
        
        /// <summary>
        /// 
        /// </summary>
        public bool SlackDisplayShortInfoAttachmentShort { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackAddExtendedInfoAttachment { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackDisplayExtendedInfoAttachmentShort { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackAddExceptionAttachment { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool SlackDisplayExceptionAttachmentShort { get; set; } = true;

        #endregion

        #endregion

        #region periodic batch sink options

        /// <summary>
        /// 
        /// </summary>
        public int PeriodicBatchingSinkOptionsBatchSizeLimit { get; set; } = DefaultBatchSizeLimit;

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan PeriodicBatchingSinkOptionsPeriod { get; set; } = DefaultPeriod;

        /// <summary>
        /// 
        /// </summary>
        public int PeriodicBatchingSinkOptionsQueueLimit { get; set; } = DefaultQueueLimit;

        #endregion

        #region general sink options

        /// <summary>
        /// 
        /// </summary>
        public LogEventLevel SinkRestrictedToMinimumLevel { get; set; } = LevelAlias.Minimum;

        /// <summary>
        /// 
        /// </summary>
        public string SinkOutputTemplate { get; set; } = DefaultOutputTemplate;

        #endregion
    }
}
