using System;
using System.Collections.Generic;
using System.Net.Http;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Slack.Webhooks;

namespace Serilog.Sinks.SlackWebHook
{
    public static class SlackLoggerConfigurationExtensions
    {
        #region public constructors

        /// <summary>
        /// <see cref="LoggerSinkConfiguration"/> extension that provides configuration chaining.
        /// </summary>
        /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration"/> object.</param>
        /// <param name="slackWebHookUrl">Slack WebHook URL (required).</param>
        /// <param name="slackChannel">Name of the Slack channel in which the log message should be posted (recommended).</param>
        /// <param name="slackUsername">Slack username (recommended).</param>
        /// <param name="slackEmojiIcon">Slack user-icon emoji string (recommended).</param>
        /// <param name="slackUriIcon">Slack user-icon image URI (optional).</param>
        /// <param name="slackDeleteOriginal">Slack message option 'DeleteOriginal' (optional).</param>
        /// <param name="slackLinkNames">Slack message option 'LinkNames' (optional).</param>
        /// <param name="slackMarkdown">Slack message option 'Markdown' (optional).</param>
        /// <param name="slackParseObj">Slack message option 'Parse' as <see cref="ParseMode"/></param>
        /// <param name="slackReplaceOriginal">Slack message option 'ReplaceOriginal' (optional).</param>
        /// <param name="slackResponseType">Slack message option 'ResponseType' (optional).</param>
        /// <param name="slackThreadId">Slack message option 'ThreadID' (optional).</param>
        /// <param name="slackAttachmentColorsObj">Slack message attachment color list as <see cref="IDictionary{LogEventLevel, String}"/> (optional).</param>
        /// <param name="slackAttachmentFooterIconObj">Slack message attachment footer icon list as <see cref="IDictionary{LogEventLevel, String}"/> (optional).</param>
        /// <param name="slackAddShortInfoAttachment">Add the short info attachment to the log message (optional).</param>
        /// <param name="slackDisplayShortInfoAttachmentShort">Display the short info attachment in short form (optional).</param>
        /// <param name="slackAddExtendedInfoAttachment">Add the extended info attachment to the log message (optional).</param>
        /// <param name="slackDisplayExtendedInfoAttachmentShort">Display the extended info attachment in short form (optional).</param>
        /// <param name="slackAddExceptionAttachment">Add the short exception to the log message (optional).</param>
        /// <param name="slackDisplayExceptionAttachmentShort">Display the exception attachment in short form (optional).</param>
        /// <param name="slackConnectionTimeout">Timeout for the connection to the Slack servers (optional).</param>
        /// <param name="slackHttpClientObj">The <see cref="HttpClient"/> instance which the <see cref="SlackClient"/> uses.</param>
        /// <param name="generateSlackFunctions">A <see cref="Tuple{Func{LogEvent, IFormatProvider, Object, String}, Func{LogEvent, IFormatProvider, Object, List{SlackAttachment}}, Func{LogEvent, IFormatProvider, Object, List{Block}}}"/> containing custom functions [Item1 for message text generation, Item2 for message attachment list generation, Item3 for message block list generation] for the Slack message generation (optional).</param>
        /// <param name="periodicBatchingSinkOptionsBatchSizeLimit">Size of the batch of messages that get send at once to Slack (recommended).</param>
        /// <param name="periodicBatchingSinkOptionsPeriod">Time period between sending of batches of messages (recommended).</param>
        /// <param name="periodicBatchingSinkOptionsQueueLimit">Maximum size of the queue that stores the messages before the messages were send in batches to Slack (optional).</param>
        /// <param name="sinkRestrictedToMinimumLevel">The absolute minimum <see cref="LogEventLevel"/> a log message must have to be send to Slack (optional).</param>
        /// <param name="sinkOutputTemplate">The template for the output format of the log messages (optional).</param>
        /// <param name="sinkLevelSwitch">A <see cref="LoggingLevelSwitch"/> to change the minimum <see cref="LogEventLevel"/> a log message must have to be send to Slack (optional).</param>
        /// <param name="sinkFormatProvider">A format provider (optional).</param>
        /// <param name="statusSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
        /// <returns>Instance of <see cref="LoggerConfiguration"/> object.</returns>
        public static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerSinkConfiguration,

            // slack options
            string slackWebHookUrl,
            string slackChannel,
            string slackUsername = null,
            string slackEmojiIcon = null,
            Uri slackUriIcon = null,
            bool? slackDeleteOriginal = null,
            bool? slackLinkNames = null,
            bool? slackMarkdown = null,
            object slackParseObj = null,
            bool? slackReplaceOriginal = null,
            string slackResponseType = null,
            string slackThreadId = null,

            object slackAttachmentColorsObj = null,
            object slackAttachmentFooterIconObj = null,
            bool? slackAddShortInfoAttachment = null,
            bool? slackDisplayShortInfoAttachmentShort = null,
            bool? slackAddExtendedInfoAttachment = null,
            bool? slackDisplayExtendedInfoAttachmentShort = null,
            bool? slackAddExceptionAttachment = null,
            bool? slackDisplayExceptionAttachmentShort = null,

            int? slackConnectionTimeout = null,
            object slackHttpClientObj = null,

            Tuple<object, object, object> generateSlackFunctions = null,

            // periodic batch sink options
            int? periodicBatchingSinkOptionsBatchSizeLimit = null,
            TimeSpan? periodicBatchingSinkOptionsPeriod = null,
            int? periodicBatchingSinkOptionsQueueLimit = null,

            // sink options
            LogEventLevel? sinkRestrictedToMinimumLevel = null,
            string sinkOutputTemplate = null,
            LoggingLevelSwitch sinkLevelSwitch = null,
            IFormatProvider sinkFormatProvider = null,
            SlackSinkActivationSwitch statusSwitch = null
        )
        {
            return Slack(loggerSinkConfiguration, slackWebHookUrl, slackUsername, slackEmojiIcon, slackUriIcon,
                string.IsNullOrEmpty(slackChannel) ? null : new List<string> { slackChannel }, slackDeleteOriginal,
                slackLinkNames, slackMarkdown, slackParseObj, slackReplaceOriginal,
                slackResponseType, slackThreadId, slackAttachmentColorsObj, slackAttachmentFooterIconObj,
                slackAddShortInfoAttachment, slackDisplayShortInfoAttachmentShort, slackAddExtendedInfoAttachment,
                slackDisplayExtendedInfoAttachmentShort, slackAddExceptionAttachment,
                slackDisplayExceptionAttachmentShort, slackConnectionTimeout, slackHttpClientObj, generateSlackFunctions, periodicBatchingSinkOptionsBatchSizeLimit,
                periodicBatchingSinkOptionsPeriod, periodicBatchingSinkOptionsQueueLimit, sinkRestrictedToMinimumLevel,
                sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider, statusSwitch);
        }

        /// <summary>
        /// <see cref="LoggerSinkConfiguration"/> extension that provides configuration chaining.
        /// </summary>
        /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration"/> object.</param>
        /// <param name="slackWebHookUrl">Slack WebHook URL.</param>
        /// <param name="slackUsername">Slack username (recommended).</param>
        /// <param name="slackEmojiIcon">Slack user-icon emoji string (optional).</param>
        /// <param name="slackUriIcon">Slack user-icon image URI (optional).</param>
        /// <param name="slackChannels">A <see cref="List{String}"/> containing the name of all Slack channels in which the log message should be posted (recommended).</param>
        /// <param name="slackDeleteOriginal">Slack message option 'DeleteOriginal' (optional).</param>
        /// <param name="slackLinkNames">Slack message option 'LinkNames' (optional).</param>
        /// <param name="slackMarkdown">Slack message option 'Markdown' (optional).</param>
        /// <param name="slackParseObj">Slack message option 'Parse' as <see cref="ParseMode"/></param>
        /// <param name="slackReplaceOriginal">Slack message option 'ReplaceOriginal' (optional).</param>
        /// <param name="slackResponseType">Slack message option 'ResponseType' (optional).</param>
        /// <param name="slackThreadId">Slack message option 'ThreadID' (optional).</param>
        /// <param name="slackAttachmentColorsObj">Slack message attachment color list as <see cref="IDictionary{LogEventLevel,String}"/> (optional).</param>
        /// <param name="slackAttachmentFooterIconObj">Slack message attachment footer icon list as <see cref="IDictionary{LogEventLevel,String}"/> (optional).</param>
        /// <param name="slackAddShortInfoAttachment">Add the short info attachment to the log message (optional).</param>
        /// <param name="slackDisplayShortInfoAttachmentShort">Display the short info attachment in short form (optional).</param>
        /// <param name="slackAddExtendedInfoAttachment">Add the extended info attachment to the log message (optional).</param>
        /// <param name="slackDisplayExtendedInfoAttachmentShort">Display the extended info attachment in short form (optional).</param>
        /// <param name="slackAddExceptionAttachment">Add the short exception to the log message (optional).</param>
        /// <param name="slackDisplayExceptionAttachmentShort">Display the exception attachment in short form (optional).</param>
        /// <param name="slackConnectionTimeout">Timeout for the connection to the Slack servers (optional).</param>
        /// <param name="slackHttpClientObj">The <see cref="HttpClient"/> instance which the <see cref="SlackClient"/> uses.</param>
        /// <param name="generateSlackFunctions">A <see cref="Tuple{Func{LogEvent, IFormatProvider, Object, String}, Func{LogEvent, IFormatProvider, Object, List{SlackAttachment}}, Func{LogEvent, IFormatProvider, Object, List{Block}}}"/> containing custom functions [Item1 for message text generation, Item2 for message attachment list generation, Item3 for message block list generation] for the Slack message generation (optional).</param>
        /// <param name="periodicBatchingSinkOptionsBatchSizeLimit">Size of the batch of messages that get send at once to Slack (recommended).</param>
        /// <param name="periodicBatchingSinkOptionsPeriod">Time period between sending of batches of messages (recommended).</param>
        /// <param name="periodicBatchingSinkOptionsQueueLimit">Maximum size of the queue that stores the messages before the messages were send in batches to Slack (optional).</param>
        /// <param name="sinkRestrictedToMinimumLevel">The absolute minimum <see cref="LogEventLevel"/> a log message must have to be send to Slack (optional).</param>
        /// <param name="sinkOutputTemplate">The template for the output format of the log messages (optional).</param>
        /// <param name="sinkLevelSwitch">A <see cref="LoggingLevelSwitch"/> to change the minimum <see cref="LogEventLevel"/> a log message must have to be send to Slack (optional).</param>
        /// <param name="sinkFormatProvider">A format provider (optional).</param>
        /// <param name="statusSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
        /// <returns>Instance of <see cref="LoggerConfiguration"/> object.</returns>
        public static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerSinkConfiguration,

            // slack options
            string slackWebHookUrl,
            string slackUsername = null,
            string slackEmojiIcon = null,
            Uri slackUriIcon = null,
            List<string> slackChannels = null,
            bool? slackDeleteOriginal = null,
            bool? slackLinkNames = null,
            bool? slackMarkdown = null,
            object slackParseObj = null,
            bool? slackReplaceOriginal = null,
            string slackResponseType = null,
            string slackThreadId = null,

            object slackAttachmentColorsObj = null,
            object slackAttachmentFooterIconObj = null,
            bool? slackAddShortInfoAttachment = null,
            bool? slackDisplayShortInfoAttachmentShort = null,
            bool? slackAddExtendedInfoAttachment = null,
            bool? slackDisplayExtendedInfoAttachmentShort = null,
            bool? slackAddExceptionAttachment = null,
            bool? slackDisplayExceptionAttachmentShort = null,

            int? slackConnectionTimeout = null,
            object slackHttpClientObj = null,

            Tuple<object, object, object> generateSlackFunctions = null,

            // periodic batch sink options
            int? periodicBatchingSinkOptionsBatchSizeLimit = null,
            TimeSpan? periodicBatchingSinkOptionsPeriod = null,
            int? periodicBatchingSinkOptionsQueueLimit = null,

            // sink options
            LogEventLevel? sinkRestrictedToMinimumLevel = null,
            string sinkOutputTemplate = null,
            LoggingLevelSwitch sinkLevelSwitch = null,
            IFormatProvider sinkFormatProvider = null,
            SlackSinkActivationSwitch statusSwitch = null
        )
        {
            ParseMode? slackParse = null;
            if (slackParseObj != null) slackParse = (ParseMode)slackParseObj;

            IDictionary<LogEventLevel, string> slackAttachmentColors = null;
            if (slackAttachmentColorsObj != null) slackAttachmentColors = (IDictionary<LogEventLevel, string>)slackAttachmentColorsObj;

            IDictionary<LogEventLevel, string> slackAttachmentFooterIcon = null;
            if (slackAttachmentFooterIconObj != null) slackAttachmentFooterIcon = (IDictionary<LogEventLevel, string>)slackAttachmentFooterIconObj;

            HttpClient slackHttpClient = null;
            if (slackHttpClientObj != null) slackHttpClient = (HttpClient)slackHttpClientObj;

            Func<LogEvent, IFormatProvider, object, string> generateSlackMessageText = null;
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> generateSlackMessageAttachments = null;
            Func<LogEvent, IFormatProvider, object, List<Block>> generateSlackMessageBlocks = null;

            if (generateSlackFunctions != null)
            {
                if (generateSlackFunctions.Item1 != null)
                    generateSlackMessageText =
                        (Func<LogEvent, IFormatProvider, object, string>)generateSlackFunctions.Item1;
                if (generateSlackFunctions.Item2 != null)
                    generateSlackMessageAttachments =
                        (Func<LogEvent, IFormatProvider, object, List<SlackAttachment>>)generateSlackFunctions.Item2;
                if (generateSlackFunctions.Item3 != null)
                    generateSlackMessageBlocks =
                        (Func<LogEvent, IFormatProvider, object, List<Block>>)generateSlackFunctions.Item3;
            }

            return Slack(loggerSinkConfiguration, slackWebHookUrl, slackUsername, slackEmojiIcon, slackUriIcon,
                slackChannels, slackDeleteOriginal, slackLinkNames, slackMarkdown, slackParse, slackReplaceOriginal,
                slackResponseType, slackThreadId, slackAttachmentColors, slackAttachmentFooterIcon,
                slackAddShortInfoAttachment, slackDisplayShortInfoAttachmentShort, slackAddExtendedInfoAttachment,
                slackDisplayExtendedInfoAttachmentShort, slackAddExceptionAttachment,
                slackDisplayExceptionAttachmentShort, slackConnectionTimeout, slackHttpClient, generateSlackMessageText,
                generateSlackMessageAttachments, generateSlackMessageBlocks, periodicBatchingSinkOptionsBatchSizeLimit,
                periodicBatchingSinkOptionsPeriod, periodicBatchingSinkOptionsQueueLimit, sinkRestrictedToMinimumLevel,
                sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider, statusSwitch);
        }

        #endregion

        #region private constructors

        /// <summary>
        /// <see cref="LoggerSinkConfiguration"/> extension that provides configuration chaining.
        /// </summary>
        /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration"/> object.</param>
        /// <param name="slackWebHookUrl">Slack WebHook URL.</param>
        /// <param name="slackUsername">Slack username (recommended).</param>
        /// <param name="slackEmojiIcon">Slack user-icon emoji string (optional).</param>
        /// <param name="slackUriIcon">Slack user-icon image URI (optional).</param>
        /// <param name="slackChannels">A <see cref="List{String}"/> containing the name of all Slack channels in which the log message should be posted (recommended).</param>
        /// <param name="slackDeleteOriginal">Slack message option 'DeleteOriginal' (optional).</param>
        /// <param name="slackLinkNames">Slack message option 'LinkNames' (optional).</param>
        /// <param name="slackMarkdown">Slack message option 'Markdown' (optional).</param>
        /// <param name="slackParse">Slack message option 'Parse' as <see cref="ParseMode"/></param>
        /// <param name="slackReplaceOriginal">Slack message option 'ReplaceOriginal' (optional).</param>
        /// <param name="slackResponseType">Slack message option 'ResponseType' (optional).</param>
        /// <param name="slackThreadId">Slack message option 'ThreadID' (optional).</param>
        /// <param name="slackAttachmentColors">Slack message attachment color list as <see cref="IDictionary{LogEventLevel,String}"/> (optional).</param>
        /// <param name="slackAttachmentFooterIcon">Slack message attachment footer icon list as <see cref="IDictionary{LogEventLevel,String}"/> (optional).</param>
        /// <param name="slackAddShortInfoAttachment">Add the short info attachment to the log message (optional).</param>
        /// <param name="slackDisplayShortInfoAttachmentShort">Display the short info attachment in short form (optional).</param>
        /// <param name="slackAddExtendedInfoAttachment">Add the extended info attachment to the log message (optional).</param>
        /// <param name="slackDisplayExtendedInfoAttachmentShort">Display the extended info attachment in short form (optional).</param>
        /// <param name="slackAddExceptionAttachment">Add the short exception to the log message (optional).</param>
        /// <param name="slackDisplayExceptionAttachmentShort">Display the exception attachment in short form (optional).</param>
        /// <param name="slackConnectionTimeout">Timeout for the connection to the Slack servers (optional).</param>
        /// <param name="slackHttpClient">The <see cref="HttpClient"/> instance which the <see cref="SlackClient"/> uses.</param>
        /// <param name="generateSlackMessageText">A <see cref="Func{LogEvent, IFormatProvider, Object, String}"/> for message text generation (optional).</param>
        /// <param name="generateSlackMessageAttachments">A <see cref="Func{LogEvent, IFormatProvider, Object, List{SlackAttachment}}"/> message attachment list generation (optional).</param>
        /// <param name="generateSlackMessageBlocks">A <see cref="Func{LogEvent, IFormatProvider, Object, List{Block}}"/> for message block list generation (optional).</param>
        /// <param name="periodicBatchingSinkOptionsBatchSizeLimit">Size of the batch of messages that get send at once to Slack (recommended).</param>
        /// <param name="periodicBatchingSinkOptionsPeriod">Time period between sending of batches of messages (recommended).</param>
        /// <param name="periodicBatchingSinkOptionsQueueLimit">Maximum size of the queue that stores the messages before the messages were send in batches to Slack (optional).</param>
        /// <param name="sinkRestrictedToMinimumLevel">The absolute minimum <see cref="LogEventLevel"/> a log message must have to be send to Slack (optional).</param>
        /// <param name="sinkOutputTemplate">The template for the output format of the log messages (optional).</param>
        /// <param name="sinkLevelSwitch">A <see cref="LoggingLevelSwitch"/> to change the minimum <see cref="LogEventLevel"/> a log message must have to be send to Slack (optional).</param>
        /// <param name="sinkFormatProvider">A format provider (optional).</param>
        /// <param name="statusSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
        /// <returns>Instance of <see cref="LoggerConfiguration"/> object.</returns>
        private static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerSinkConfiguration,

            // slack options
            string slackWebHookUrl,
            string slackUsername = null,
            string slackEmojiIcon = null,
            Uri slackUriIcon = null,
            List<string> slackChannels = null,
            bool? slackDeleteOriginal = null,
            bool? slackLinkNames = null,
            bool? slackMarkdown = null,
            ParseMode? slackParse = null,
            bool? slackReplaceOriginal = null,
            string slackResponseType = null,
            string slackThreadId = null,

            IDictionary<LogEventLevel, string> slackAttachmentColors = null,
            IDictionary<LogEventLevel, string> slackAttachmentFooterIcon = null,
            bool? slackAddShortInfoAttachment = null,
            bool? slackDisplayShortInfoAttachmentShort = null,
            bool? slackAddExtendedInfoAttachment = null,
            bool? slackDisplayExtendedInfoAttachmentShort = null,
            bool? slackAddExceptionAttachment = null,
            bool? slackDisplayExceptionAttachmentShort = null,

            int? slackConnectionTimeout = null,
            HttpClient slackHttpClient = null,

            Func<LogEvent, IFormatProvider, object, string> generateSlackMessageText = null,
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> generateSlackMessageAttachments = null,
            Func<LogEvent, IFormatProvider, object, List<Block>> generateSlackMessageBlocks = null,

            // periodic batch sink options
            int? periodicBatchingSinkOptionsBatchSizeLimit = null,
            TimeSpan? periodicBatchingSinkOptionsPeriod = null,
            int? periodicBatchingSinkOptionsQueueLimit = null,

            // sink options
            LogEventLevel? sinkRestrictedToMinimumLevel = null,
            string sinkOutputTemplate = null,
            LoggingLevelSwitch sinkLevelSwitch = null,
            IFormatProvider sinkFormatProvider = null,
            SlackSinkActivationSwitch statusSwitch = null
        )
        {
            if (slackWebHookUrl == null) throw new ArgumentNullException(nameof(slackWebHookUrl), "The Slack WebHook can't be null!");
            if (string.IsNullOrEmpty(slackWebHookUrl)) throw new ArgumentException("The Slack WebHook can't be empty!", nameof(slackWebHookUrl));

            var slackSinkOptions = new SlackSinkOptions
            {
                SlackWebHookUrl = slackWebHookUrl
            };

            if (slackUsername != null) slackSinkOptions.SlackUsername = slackUsername;
            if (slackEmojiIcon != null) slackSinkOptions.SlackEmojiIcon = slackEmojiIcon;
            if (slackUriIcon != null) slackSinkOptions.SlackUriIcon = slackUriIcon;
            if (slackChannels != null) slackSinkOptions.SlackChannels = slackChannels;

            if (slackDeleteOriginal != null) slackSinkOptions.SlackDeleteOriginal = (bool)slackDeleteOriginal;
            if (slackLinkNames != null) slackSinkOptions.SlackLinkNames = (bool)slackLinkNames;
            if (slackMarkdown != null) slackSinkOptions.SlackMarkdown = (bool)slackMarkdown;
            if (slackParse != null) slackSinkOptions.SlackParse = (ParseMode)slackParse;
            if (slackReplaceOriginal != null) slackSinkOptions.SlackReplaceOriginal = (bool)slackReplaceOriginal;
            if (slackResponseType != null) slackSinkOptions.SlackResponseType = slackResponseType;
            if (slackThreadId != null) slackSinkOptions.SlackThreadId = slackThreadId;

            if (slackAttachmentColors != null) slackSinkOptions.SlackAttachmentColors = slackAttachmentColors;
            if (slackAttachmentFooterIcon != null) slackSinkOptions.SlackAttachmentFooterIcon = slackAttachmentFooterIcon;
            if (slackAddShortInfoAttachment != null) slackSinkOptions.SlackAddShortInfoAttachment = (bool)slackAddShortInfoAttachment;
            if (slackDisplayShortInfoAttachmentShort != null) slackSinkOptions.SlackDisplayShortInfoAttachmentShort = (bool)slackDisplayShortInfoAttachmentShort;
            if (slackAddExtendedInfoAttachment != null) slackSinkOptions.SlackAddExtendedInfoAttachment = (bool)slackAddExtendedInfoAttachment;
            if (slackDisplayExtendedInfoAttachmentShort != null) slackSinkOptions.SlackDisplayExtendedInfoAttachmentShort = (bool)slackDisplayExtendedInfoAttachmentShort;
            if (slackAddExceptionAttachment != null) slackSinkOptions.SlackAddExceptionAttachment = (bool)slackAddExceptionAttachment;
            if (slackDisplayExceptionAttachmentShort != null) slackSinkOptions.SlackDisplayExceptionAttachmentShort = (bool)slackDisplayExceptionAttachmentShort;

            if (slackConnectionTimeout != null) slackSinkOptions.SlackConnectionTimeout = (int)slackConnectionTimeout;

            if (periodicBatchingSinkOptionsBatchSizeLimit != null) slackSinkOptions.SlackPeriodicBatchingSinkOptionsBatchSizeLimit = (int)periodicBatchingSinkOptionsBatchSizeLimit;
            if (periodicBatchingSinkOptionsPeriod != null) slackSinkOptions.SlackPeriodicBatchingSinkOptionsPeriod = (TimeSpan)periodicBatchingSinkOptionsPeriod;
            if (periodicBatchingSinkOptionsQueueLimit != null) slackSinkOptions.SlackPeriodicBatchingSinkOptionsQueueLimit = (int)periodicBatchingSinkOptionsQueueLimit;

            if (sinkOutputTemplate != null) slackSinkOptions.SlackSinkOutputTemplate = sinkOutputTemplate;

            return loggerSinkConfiguration.Sink(
                logEventSink: new SlackSink(slackSinkOptions, sinkFormatProvider, statusSwitch, slackHttpClient,
                generateSlackMessageText, generateSlackMessageAttachments, generateSlackMessageBlocks),
                restrictedToMinimumLevel: sinkRestrictedToMinimumLevel ?? LevelAlias.Minimum,
                levelSwitch: sinkLevelSwitch);
        }

        #endregion
    }
}
