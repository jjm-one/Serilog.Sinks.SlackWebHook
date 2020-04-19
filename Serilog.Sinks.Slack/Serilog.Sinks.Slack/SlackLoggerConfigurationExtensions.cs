using System;
using System.Collections.Generic;
using System.Net.Http;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Slack.Webhooks;

namespace Serilog.Sinks.Slack
{
    public static class SlackLoggerConfigurationExtensions
    {
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

            object slackAttachmentColorsObj = default,
            object slackAttachmentFooterIconObj = default,
            bool slackAddShortInfoAttachment = true,
            bool slackDisplayShortInfoAttachmentShort = true,
            bool slackAddExtendedInfoAttachment = false,
            bool slackDisplayExtendedInfoAttachmentShort = true,
            bool slackAddExceptionAttachment = true,
            bool slackDisplayExceptionAttachmentShort = true,

            int? slackConnectionTimeout = null,
            object slackHttpClientObj = null,

            Tuple<object, object, object> generateSlackFunctions = null,

            // periodic batch sink options
            int? periodicBatchingSinkOptionsBatchSizeLimit = null,
            TimeSpan? periodicBatchingSinkOptionsPeriod = null,
            int? periodicBatchingSinkOptionsQueueLimit = null,

            // sink options
            LogEventLevel sinkRestrictedToMinimumLevel = LevelAlias.Minimum,
            string sinkOutputTemplate = null,
            LoggingLevelSwitch sinkLevelSwitch = null,
            IFormatProvider sinkFormatProvider = null
        )
        {
            return Slack(loggerSinkConfiguration, slackWebHookUrl, slackUsername, slackEmojiIcon, slackUriIcon,
                new List<string> { slackChannel }, slackDeleteOriginal, slackLinkNames, slackMarkdown, slackParseObj, slackReplaceOriginal,
                slackResponseType, slackThreadId, slackAttachmentColorsObj, slackAttachmentFooterIconObj,
                slackAddShortInfoAttachment, slackDisplayShortInfoAttachmentShort, slackAddExtendedInfoAttachment,
                slackDisplayExtendedInfoAttachmentShort, slackAddExceptionAttachment,
                slackDisplayExceptionAttachmentShort, slackConnectionTimeout, slackHttpClientObj, generateSlackFunctions, periodicBatchingSinkOptionsBatchSizeLimit,
                periodicBatchingSinkOptionsPeriod, periodicBatchingSinkOptionsQueueLimit, sinkRestrictedToMinimumLevel,
                sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider);
        }

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

            object slackAttachmentColorsObj = default,
            object slackAttachmentFooterIconObj = default,
            bool slackAddShortInfoAttachment = true,
            bool slackDisplayShortInfoAttachmentShort = true,
            bool slackAddExtendedInfoAttachment = false,
            bool slackDisplayExtendedInfoAttachmentShort = true,
            bool slackAddExceptionAttachment = true,
            bool slackDisplayExceptionAttachmentShort = true,

            int? slackConnectionTimeout = null,
            object slackHttpClientObj = null,

            Tuple<object, object, object> generateSlackFunctions = null,

            // periodic batch sink options
            int? periodicBatchingSinkOptionsBatchSizeLimit = null,
            TimeSpan? periodicBatchingSinkOptionsPeriod = null,
            int? periodicBatchingSinkOptionsQueueLimit = null,

            // sink options
            LogEventLevel sinkRestrictedToMinimumLevel = LevelAlias.Minimum,
            string sinkOutputTemplate = null,
            LoggingLevelSwitch sinkLevelSwitch = null,
            IFormatProvider sinkFormatProvider = null
        )
        {
            var slackParse = (ParseMode)slackParseObj;
            var slackAttachmentColors = (IDictionary<LogEventLevel, string>)slackAttachmentColorsObj;
            var slackAttachmentFooterIcon = (IDictionary<LogEventLevel, string>)slackAttachmentFooterIconObj;
            var slackHttpClient = (HttpClient)slackHttpClientObj;

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

            IDictionary<LogEventLevel, string> slackAttachmentColors = default,
            IDictionary<LogEventLevel, string> slackAttachmentFooterIcon = default,
            bool slackAddShortInfoAttachment = true,
            bool slackDisplayShortInfoAttachmentShort = true,
            bool slackAddExtendedInfoAttachment = false,
            bool slackDisplayExtendedInfoAttachmentShort = true,
            bool slackAddExceptionAttachment = true,
            bool slackDisplayExceptionAttachmentShort = true,

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
            LogEventLevel sinkRestrictedToMinimumLevel = LevelAlias.Minimum,
            string sinkOutputTemplate = null,
            LoggingLevelSwitch sinkLevelSwitch = null,
            IFormatProvider sinkFormatProvider = null
        )
        {
            if (string.IsNullOrEmpty(slackWebHookUrl)) throw new ArgumentNullException(nameof(slackWebHookUrl));

            var slackSinkOptions = new SlackSinkOptions
            {
                SlackWebHookUrl = slackWebHookUrl,
                SlackUsername = slackUsername,
                SlackEmojiIcon = slackEmojiIcon,
                SlackUriIcon = slackUriIcon,
                SlackChannels = slackChannels,
                SlackDeleteOriginal = slackDeleteOriginal,
                SlackLinkNames = slackLinkNames,
                SlackMarkdown = slackMarkdown,
                SlackParse = slackParse,
                SlackReplaceOriginal = slackReplaceOriginal,
                SlackResponseType = slackResponseType,
                SlackThreadId = slackThreadId,

                SlackAddShortInfoAttachment = slackAddShortInfoAttachment,
                SlackDisplayShortInfoAttachmentShort = slackDisplayShortInfoAttachmentShort,
                SlackAddExtendedInfoAttachment = slackAddExtendedInfoAttachment,
                SlackDisplayExtendedInfoAttachmentShort = slackDisplayExtendedInfoAttachmentShort,
                SlackAddExceptionAttachment = slackAddExceptionAttachment,
                SlackDisplayExceptionAttachmentShort = slackDisplayExceptionAttachmentShort,

                SlackConnectionTimeout = slackConnectionTimeout,

                PeriodicBatchingSinkOptionsBatchSizeLimit = periodicBatchingSinkOptionsBatchSizeLimit,
                PeriodicBatchingSinkOptionsPeriod = periodicBatchingSinkOptionsPeriod,
                PeriodicBatchingSinkOptionsQueueLimit = periodicBatchingSinkOptionsQueueLimit,

                SinkRestrictedToMinimumLevel = sinkRestrictedToMinimumLevel,
            };
            if (slackAttachmentColors != default) slackSinkOptions.SlackAttachmentColors = slackAttachmentColors;
            if (slackAttachmentFooterIcon != default)
                slackSinkOptions.SlackAttachmentFooterIcon = slackAttachmentFooterIcon;
            if (sinkOutputTemplate != null) slackSinkOptions.SinkOutputTemplate = sinkOutputTemplate;

            return loggerSinkConfiguration.Sink(new SlackSink(slackSinkOptions, sinkFormatProvider, sinkLevelSwitch, slackHttpClient,
                generateSlackMessageText, generateSlackMessageAttachments, generateSlackMessageBlocks));
        }
    }
}
