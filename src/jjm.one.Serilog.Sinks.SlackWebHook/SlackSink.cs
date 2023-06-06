using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace jjm.one.Serilog.Sinks.SlackWebHook
{
    /// <summary>
    /// This class provides functions for sending log events (serilog) to slack an implements therefor <see cref="PeriodicBatchingSink"/>.
    /// </summary>
    public class SlackSink : PeriodicBatchingSink
    {
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
        public SlackSink(
            SlackSinkOptions slackSinkOptions,
            IFormatProvider formatProvider,
            SlackSinkActivationSwitch statusSwitch = null,
            HttpClient slackHttpClient = null,
            Func<LogEvent, IFormatProvider, object, string> generateSlackMessageText = null,
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> generateSlackMessageAttachments = null,
            Func<LogEvent, IFormatProvider, object, List<Block>> generateSlackMessageBlocks = null
            )
            : base(new SlackSerilogBatchedLogEventSink(
                        slackSinkOptions: slackSinkOptions,
                        formatProvider: formatProvider,
                        statusSwitch: statusSwitch,
                        slackHttpClient: slackHttpClient,
                        generateSlackMessageText: generateSlackMessageText,
                        generateSlackMessageAttachments: generateSlackMessageAttachments,
                        generateSlackMessageBlocks: generateSlackMessageBlocks),
                   new PeriodicBatchingSinkOptions()
                   {
                       BatchSizeLimit = slackSinkOptions.PeriodicBatchingSinkOptionsBatchSizeLimit,
                       Period = slackSinkOptions.PeriodicBatchingSinkOptionsPeriod,
                       QueueLimit = slackSinkOptions.PeriodicBatchingSinkOptionsQueueLimit
                   }
                   )
        {
        }

        #endregion
    }
}
