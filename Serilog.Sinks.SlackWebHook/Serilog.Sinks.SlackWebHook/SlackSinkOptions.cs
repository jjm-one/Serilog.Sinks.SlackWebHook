using System;
using System.Collections.Generic;
using Serilog.Events;
using Slack.Webhooks;

namespace Serilog.Sinks.SlackWebHook
{
    /// <summary>
    /// Class to contain all relevant options of this sink.
    /// </summary>
    public class SlackSinkOptions
    {
        #region default values

        /// <summary>
        /// Default value for the OutputTemplate.
        /// </summary>
        public const string DefaultOutputTemplate = "{Message:lj}";

        /// <summary>
        /// Default value for the BatchSizeLimit.
        /// </summary>
        public const int DefaultBatchSizeLimit = 10;

        /// <summary>
        /// Default value for the Period.
        /// </summary>
        public static readonly TimeSpan DefaultPeriod = TimeSpan.FromSeconds(2);

        /// <summary>
        /// Default value for the QueueLimit.
        /// </summary>
        public const int DefaultQueueLimit = 10000;

        /// <summary>
        /// Default value for the Timeout.
        /// </summary>
        public const int DefaultTimeout = 1000;

        #endregion

        #region slack options

        #region slack connection options

        /// <summary>
        /// REQUIRED: Slack WebHook URL.
        /// </summary>
        public string SlackWebHookUrl { get; set; }

        /// <summary>
        /// OPTIONAL: Timeout for the connection to the Slack servers.
        /// </summary>
        public int SlackConnectionTimeout { get; set; } = DefaultTimeout;

        #endregion

        #region generall slack & message options

        /// <summary>
        /// RECOMMENDED: Slack username.
        /// </summary>
        public string SlackUsername { get; set; } = null;

        /// <summary>
        /// RECOMMENDED: Slack user-icon emoji string.
        /// </summary>
        public string SlackEmojiIcon { get; set; } = null;

        /// <summary>
        /// OPTIONAL: Slack user-icon image URI.
        /// </summary>
        public Uri SlackUriIcon { get; set; } = null;

        /// <summary>
        /// RECOMMENDED: A <see cref="List{String}"/> containing the name of all Slack channels in which the log message should be posted.
        /// </summary>
        public List<string> SlackChannels { get; set; } = null;

        /// <summary>
        /// OPTIONAL: Slack message option 'DeleteOriginal'.
        /// </summary>
        public bool SlackDeleteOriginal { get; set; } = false;

        /// <summary>
        /// OPTIONAL: Slack message option 'LinkNames'.
        /// </summary>
        public bool SlackLinkNames { get; set; } = false;

        /// <summary>
        /// OPTIONAL: Slack message option 'Markdown'.
        /// </summary>
        public bool SlackMarkdown { get; set; } = false;

        /// <summary>
        /// OPTIONAL: Slack message option 'Parse'.
        /// </summary>
        public ParseMode SlackParse { get; set; } = ParseMode.None;

        /// <summary>
        /// OPTIONAL: Slack message option 'ReplaceOriginal'.
        /// </summary>
        public bool SlackReplaceOriginal { get; set; } = false;

        /// <summary>
        /// OPTIONAL: Slack message option 'ResponseType'.
        /// </summary>
        public string SlackResponseType { get; set; } = null;

        /// <summary>
        /// OPTIONAL: Slack message option 'ThreadID'.
        /// </summary>
        public string SlackThreadId { get; set; } = null;

        #endregion

        #region slack attachment options

        /// <summary>
        /// OPTIONAL: Slack message attachment color list as <see cref="IDictionary{LogEventLevel,String}"/>.
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
        /// OPTIONAL: Slack message attachment footer icon list as <see cref="IDictionary{LogEventLevel,String}"/>.
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
        /// OPTIONAL: Add the short info attachment to the log message.
        /// </summary>
        public bool SlackAddShortInfoAttachment { get; set; } = true;

        /// <summary>
        /// OPTIONAL: Display the short info attachment in short form.
        /// </summary>
        public bool SlackDisplayShortInfoAttachmentShort { get; set; } = true;

        /// <summary>
        /// OPTIONAL: Add the extended info attachment to the log message.
        /// </summary>
        public bool SlackAddExtendedInfoAttachment { get; set; } = false;

        /// <summary>
        /// OPTIONAL: Display the extended info attachment in short form.
        /// </summary>
        public bool SlackDisplayExtendedInfoAttachmentShort { get; set; } = true;

        /// <summary>
        /// OPTIONAL: Add the short exception to the log message.
        /// </summary>
        public bool SlackAddExceptionAttachment { get; set; } = true;

        /// <summary>
        /// OPTIONAL: Display the exception attachment in short form.
        /// </summary>
        public bool SlackDisplayExceptionAttachmentShort { get; set; } = true;

        #endregion

        #endregion

        #region periodic batch sink options

        /// <summary>
        /// RECOMMENDED: Size of the batch of messages that get send at once to Slack.
        /// </summary>
        public int SlackPeriodicBatchingSinkOptionsBatchSizeLimit { get; set; } = DefaultBatchSizeLimit;

        /// <summary>
        /// RECOMMENDED: Time period between sending of batches of messages.
        /// </summary>
        public TimeSpan SlackPeriodicBatchingSinkOptionsPeriod { get; set; } = DefaultPeriod;

        /// <summary>
        /// OPTIONAL: Maximum size of the queue that stores the messages before the messages were send in batches to Slack.
        /// </summary>
        public int SlackPeriodicBatchingSinkOptionsQueueLimit { get; set; } = DefaultQueueLimit;

        #endregion

        #region general sink options

        /// <summary>
        /// OPTIONAL: The template for the output format of the log messages.
        /// </summary>
        public string SlackSinkOutputTemplate { get; set; } = DefaultOutputTemplate;

        #endregion
    }
}
