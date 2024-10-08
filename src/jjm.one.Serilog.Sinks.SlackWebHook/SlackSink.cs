using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Serilog.Events;
using Serilog.Sinks.PeriodicBatching;
using jjm.one.Slack.Webhooks;

namespace jjm.one.Serilog.Sinks.SlackWebHook;

/// <summary>
///     This class provides functions for sending log events (serilog) to slack an implements therefor
///     <see cref="PeriodicBatchingSink" />.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public class SlackSink : PeriodicBatchingSink
{
    #region constructor

    /// <summary>
    ///     Initializes new instance of <see cref="SlackSink" />.
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
        IFormatProvider? formatProvider,
        SlackSinkActivationSwitch? statusSwitch = null,
        HttpClient? slackHttpClient = null,
        Func<LogEvent, IFormatProvider?, object, string?>? generateSlackMessageText = null,
        Func<LogEvent, IFormatProvider?, object, List<SlackAttachment>?>? generateSlackMessageAttachments = null,
        Func<LogEvent, IFormatProvider?, object, List<Block>?>? generateSlackMessageBlocks = null
    )
        : base(new SlackSerilogBatchedLogEventSink(
                slackSinkOptions,
                formatProvider,
                statusSwitch,
                slackHttpClient,
                generateSlackMessageText,
                generateSlackMessageAttachments,
                generateSlackMessageBlocks),
            new PeriodicBatchingSinkOptions
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