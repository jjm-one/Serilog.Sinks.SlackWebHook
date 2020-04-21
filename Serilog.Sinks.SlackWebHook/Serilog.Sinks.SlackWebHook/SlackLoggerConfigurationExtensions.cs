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
        /// <param name="slackWebHookUrl"></param>
        /// <param name="slackUsername"></param>
        /// <param name="slackEmojiIcon"></param>
        /// <param name="slackUriIcon"></param>
        /// <param name="slackChannel"></param>
        /// <param name="slackDeleteOriginal"></param>
        /// <param name="slackLinkNames"></param>
        /// <param name="slackMarkdown"></param>
        /// <param name="slackParseObj"></param>
        /// <param name="slackReplaceOriginal"></param>
        /// <param name="slackResponseType"></param>
        /// <param name="slackThreadId"></param>
        /// <param name="slackAttachmentColorsObj"></param>
        /// <param name="slackAttachmentFooterIconObj"></param>
        /// <param name="slackAddShortInfoAttachment"></param>
        /// <param name="slackDisplayShortInfoAttachmentShort"></param>
        /// <param name="slackAddExtendedInfoAttachment"></param>
        /// <param name="slackDisplayExtendedInfoAttachmentShort"></param>
        /// <param name="slackAddExceptionAttachment"></param>
        /// <param name="slackDisplayExceptionAttachmentShort"></param>
        /// <param name="slackConnectionTimeout"></param>
        /// <param name="slackHttpClientObj"></param>
        /// <param name="generateSlackFunctions"></param>
        /// <param name="periodicBatchingSinkOptionsBatchSizeLimit"></param>
        /// <param name="periodicBatchingSinkOptionsPeriod"></param>
        /// <param name="periodicBatchingSinkOptionsQueueLimit"></param>
        /// <param name="sinkRestrictedToMinimumLevel"></param>
        /// <param name="sinkOutputTemplate"></param>
        /// <param name="sinkLevelSwitch"></param>
        /// <param name="sinkFormatProvider"></param>
        /// <returns>Instance of <see cref="LoggerConfiguration"/> object.</returns>
        public static LoggerConfiguration Slack(
            this LoggerSinkConfiguration loggerSinkConfiguration,

            // slack options
            string slackWebHookUrl,
            string slackUsername = null,
            string slackEmojiIcon = null,
            Uri slackUriIcon = null,
            string slackChannel = null,
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
            IFormatProvider sinkFormatProvider = null
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
                sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider);
        }

        /// <summary>
        /// <see cref="LoggerSinkConfiguration"/> extension that provides configuration chaining.
        /// </summary>
        /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration"/> object.</param>
        /// <param name="slackWebHookUrl"></param>
        /// <param name="slackUsername"></param>
        /// <param name="slackEmojiIcon"></param>
        /// <param name="slackUriIcon"></param>
        /// <param name="slackChannels"></param>
        /// <param name="slackDeleteOriginal"></param>
        /// <param name="slackLinkNames"></param>
        /// <param name="slackMarkdown"></param>
        /// <param name="slackParseObj"></param>
        /// <param name="slackReplaceOriginal"></param>
        /// <param name="slackResponseType"></param>
        /// <param name="slackThreadId"></param>
        /// <param name="slackAttachmentColorsObj"></param>
        /// <param name="slackAttachmentFooterIconObj"></param>
        /// <param name="slackAddShortInfoAttachment"></param>
        /// <param name="slackDisplayShortInfoAttachmentShort"></param>
        /// <param name="slackAddExtendedInfoAttachment"></param>
        /// <param name="slackDisplayExtendedInfoAttachmentShort"></param>
        /// <param name="slackAddExceptionAttachment"></param>
        /// <param name="slackDisplayExceptionAttachmentShort"></param>
        /// <param name="slackConnectionTimeout"></param>
        /// <param name="slackHttpClientObj"></param>
        /// <param name="generateSlackFunctions"></param>
        /// <param name="periodicBatchingSinkOptionsBatchSizeLimit"></param>
        /// <param name="periodicBatchingSinkOptionsPeriod"></param>
        /// <param name="periodicBatchingSinkOptionsQueueLimit"></param>
        /// <param name="sinkRestrictedToMinimumLevel"></param>
        /// <param name="sinkOutputTemplate"></param>
        /// <param name="sinkLevelSwitch"></param>
        /// <param name="sinkFormatProvider"></param>
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
            IFormatProvider sinkFormatProvider = null
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
                generateSlackMessageText =
                    (Func<LogEvent, IFormatProvider, object, string>)generateSlackFunctions.Item1;
                generateSlackMessageAttachments =
                    (Func<LogEvent, IFormatProvider, object, List<SlackAttachment>>)generateSlackFunctions.Item2;
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
                sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider);
        }

        #endregion

        #region private constructors

        /// <summary>
        /// <see cref="LoggerSinkConfiguration"/> extension that provides configuration chaining.
        /// </summary>
        /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration"/> object.</param>
        /// <param name="slackWebHookUrl"></param>
        /// <param name="slackUsername"></param>
        /// <param name="slackEmojiIcon"></param>
        /// <param name="slackUriIcon"></param>
        /// <param name="slackChannels"></param>
        /// <param name="slackDeleteOriginal"></param>
        /// <param name="slackLinkNames"></param>
        /// <param name="slackMarkdown"></param>
        /// <param name="slackParse"></param>
        /// <param name="slackReplaceOriginal"></param>
        /// <param name="slackResponseType"></param>
        /// <param name="slackThreadId"></param>
        /// <param name="slackAttachmentColors"></param>
        /// <param name="slackAttachmentFooterIcon"></param>
        /// <param name="slackAddShortInfoAttachment"></param>
        /// <param name="slackDisplayShortInfoAttachmentShort"></param>
        /// <param name="slackAddExtendedInfoAttachment"></param>
        /// <param name="slackDisplayExtendedInfoAttachmentShort"></param>
        /// <param name="slackAddExceptionAttachment"></param>
        /// <param name="slackDisplayExceptionAttachmentShort"></param>
        /// <param name="slackConnectionTimeout"></param>
        /// <param name="slackHttpClient"></param>
        /// <param name="generateSlackMessageText"></param>
        /// <param name="generateSlackMessageAttachments"></param>
        /// <param name="generateSlackMessageBlocks"></param>
        /// <param name="periodicBatchingSinkOptionsBatchSizeLimit"></param>
        /// <param name="periodicBatchingSinkOptionsPeriod"></param>
        /// <param name="periodicBatchingSinkOptionsQueueLimit"></param>
        /// <param name="sinkRestrictedToMinimumLevel"></param>
        /// <param name="sinkOutputTemplate"></param>
        /// <param name="sinkLevelSwitch"></param>
        /// <param name="sinkFormatProvider"></param>
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
            IFormatProvider sinkFormatProvider = null
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

            if (periodicBatchingSinkOptionsBatchSizeLimit != null) slackSinkOptions.PeriodicBatchingSinkOptionsBatchSizeLimit = (int)periodicBatchingSinkOptionsBatchSizeLimit;
            if (periodicBatchingSinkOptionsPeriod != null) slackSinkOptions.PeriodicBatchingSinkOptionsPeriod = (TimeSpan)periodicBatchingSinkOptionsPeriod;
            if (periodicBatchingSinkOptionsQueueLimit != null) slackSinkOptions.PeriodicBatchingSinkOptionsQueueLimit = (int)periodicBatchingSinkOptionsQueueLimit;

            if (sinkRestrictedToMinimumLevel != null) slackSinkOptions.SinkRestrictedToMinimumLevel = (LogEventLevel)sinkRestrictedToMinimumLevel;
            if (sinkOutputTemplate != null) slackSinkOptions.SinkOutputTemplate = sinkOutputTemplate;

            return loggerSinkConfiguration.Sink(new SlackSink(slackSinkOptions, sinkFormatProvider, sinkLevelSwitch, slackHttpClient,
                generateSlackMessageText, generateSlackMessageAttachments, generateSlackMessageBlocks));
        }

        #endregion
    }
}
