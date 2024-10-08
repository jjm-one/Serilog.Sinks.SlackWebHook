﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using jjm.one.Slack.Webhooks;

namespace jjm.one.Serilog.Sinks.SlackWebHook;

/// <summary>
///     This class contains the extension functions to add the Slack logger configuration to a existing logger
///     configuration.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
public static class SlackLoggerConfigurationExtensions
{
    #region private constructors

#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute
    /// <summary>
    ///     <see cref="LoggerSinkConfiguration" /> extension that provides configuration chaining.
    /// </summary>
    /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration" /> object.</param>
    /// <param name="slackWebHookUrl">Slack WebHook URL.</param>
    /// <param name="slackUsername">Slack username (recommended).</param>
    /// <param name="slackEmojiIcon">Slack user-icon emoji string (optional).</param>
    /// <param name="slackUriIcon">Slack user-icon image URI (optional).</param>
    /// <param name="slackChannels">
    ///     A <see cref="List{String}" /> containing the name of all Slack channels in which the log
    ///     message should be posted (recommended).
    /// </param>
    /// <param name="slackDeleteOriginal">Slack message option 'DeleteOriginal' (optional).</param>
    /// <param name="slackLinkNames">Slack message option 'LinkNames' (optional).</param>
    /// <param name="slackMarkdown">Slack message option 'Markdown' (optional).</param>
    /// <param name="slackParse">Slack message option 'Parse' as <see cref="ParseMode" /></param>
    /// <param name="slackReplaceOriginal">Slack message option 'ReplaceOriginal' (optional).</param>
    /// <param name="slackResponseType">Slack message option 'ResponseType' (optional).</param>
    /// <param name="slackThreadId">Slack message option 'ThreadID' (optional).</param>
    /// <param name="slackAttachmentColors">
    ///     Slack message attachment color list as
    ///     <see cref="IDictionary{LogEventLevel,String}" /> (optional).
    /// </param>
    /// <param name="slackAttachmentFooterIcon">
    ///     Slack message attachment footer icon list as
    ///     <see cref="IDictionary{LogEventLevel,String}" /> (optional).
    /// </param>
    /// <param name="slackAddShortInfoAttachment">Add the short info attachment to the log message (optional).</param>
    /// <param name="slackDisplayShortInfoAttachmentShort">Display the short info attachment in short form (optional).</param>
    /// <param name="slackAddExtendedInfoAttachment">Add the extended info attachment to the log message (optional).</param>
    /// <param name="slackDisplayExtendedInfoAttachmentShort">Display the extended info attachment in short form (optional).</param>
    /// <param name="slackAddExceptionAttachment">Add the short exception to the log message (optional).</param>
    /// <param name="slackDisplayExceptionAttachmentShort">Display the exception attachment in short form (optional).</param>
    /// <param name="slackConnectionTimeout">Timeout for the connection to the Slack servers (optional).</param>
    /// <param name="slackHttpClient">The <see cref="HttpClient" /> instance which the <see cref="SlackClient" /> uses.</param>
    /// <param name="generateSlackMessageText">
    ///     A <see cref="Func{LogEvent, IFormatProvider, Object, String}" /> for message
    ///     text generation (optional).
    /// </param>
    /// <param name="generateSlackMessageAttachments">
    ///     A
    ///     <see cref="Func{LogEvent, IFormatProvider, Object, List{SlackAttachment}}" /> message attachment list generation
    ///     (optional).
    /// </param>
    /// <param name="generateSlackMessageBlocks">
    ///     A <see cref="Func{LogEvent, IFormatProvider, Object, List{Block}}" /> for
    ///     message block list generation (optional).
    /// </param>
    /// <param name="periodicBatchingSinkOptionsBatchSizeLimit">
    ///     Size of the batch of messages that get send at once to Slack
    ///     (recommended).
    /// </param>
    /// <param name="periodicBatchingSinkOptionsPeriod">Time period between sending of batches of messages (recommended).</param>
    /// <param name="periodicBatchingSinkOptionsQueueLimit">
    ///     Maximum size of the queue that stores the messages before the
    ///     messages were send in batches to Slack (optional).
    /// </param>
    /// <param name="sinkRestrictedToMinimumLevel">
    ///     The absolute minimum <see cref="LogEventLevel" /> a log message must have to
    ///     be send to Slack (optional).
    /// </param>
    /// <param name="sinkOutputTemplate">The template for the output format of the log messages (optional).</param>
    /// <param name="sinkLevelSwitch">
    ///     A <see cref="LoggingLevelSwitch" /> to change the minimum <see cref="LogEventLevel" /> a
    ///     log message must have to be send to Slack (optional).
    /// </param>
    /// <param name="sinkFormatProvider">A format provider (optional).</param>
    /// <param name="sinkActivationSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
    /// <returns>Instance of <see cref="LoggerConfiguration" /> object.</returns>
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
    private static LoggerConfiguration Slack(
        this LoggerSinkConfiguration loggerSinkConfiguration,

        // slack options
        string slackWebHookUrl,
        string? slackUsername = null,
        string? slackEmojiIcon = null,
        Uri? slackUriIcon = null,
        List<string?>? slackChannels = null,
        bool? slackDeleteOriginal = null,
        bool? slackLinkNames = null,
        bool? slackMarkdown = null,
        ParseMode? slackParse = null,
        bool? slackReplaceOriginal = null,
        string? slackResponseType = null,
        string? slackThreadId = null,
        IDictionary<LogEventLevel, string?>? slackAttachmentColors = null,
        IDictionary<LogEventLevel, string?>? slackAttachmentFooterIcon = null,
        bool? slackAddShortInfoAttachment = null,
        bool? slackDisplayShortInfoAttachmentShort = null,
        bool? slackAddExtendedInfoAttachment = null,
        bool? slackDisplayExtendedInfoAttachmentShort = null,
        bool? slackAddExceptionAttachment = null,
        bool? slackDisplayExceptionAttachmentShort = null,
        int? slackConnectionTimeout = null,
        HttpClient? slackHttpClient = null,
        Func<LogEvent, IFormatProvider?, object, string?>? generateSlackMessageText = null,
        Func<LogEvent, IFormatProvider?, object, List<SlackAttachment>?>? generateSlackMessageAttachments = null,
        Func<LogEvent, IFormatProvider?, object, List<Block>?>? generateSlackMessageBlocks = null,

        // periodic batch sink options
        int? periodicBatchingSinkOptionsBatchSizeLimit = null,
        TimeSpan? periodicBatchingSinkOptionsPeriod = null,
        int? periodicBatchingSinkOptionsQueueLimit = null,

        // sink options
        LogEventLevel? sinkRestrictedToMinimumLevel = null,
        string? sinkOutputTemplate = null,
        LoggingLevelSwitch? sinkLevelSwitch = null,
        IFormatProvider? sinkFormatProvider = null,
        SlackSinkActivationSwitch? sinkActivationSwitch = null
    )
    {
        if (slackWebHookUrl == null)
            throw new ArgumentNullException(nameof(slackWebHookUrl), "The Slack WebHook can't be null!");
        if (string.IsNullOrEmpty(slackWebHookUrl))
            throw new ArgumentException("The Slack WebHook can't be empty!", nameof(slackWebHookUrl));

        var slackSinkOptions = new SlackSinkOptions
        {
            SlackWebHookUrl = slackWebHookUrl
        };

        if (slackUsername is not null) slackSinkOptions.SlackUsername = slackUsername;
        if (slackEmojiIcon is not null) slackSinkOptions.SlackEmojiIcon = slackEmojiIcon;
        if (slackUriIcon is not null) slackSinkOptions.SlackUriIcon = slackUriIcon;
        if (slackChannels is not null) slackSinkOptions.SlackChannels = slackChannels;

        if (slackDeleteOriginal is not null) slackSinkOptions.SlackDeleteOriginal = (bool)slackDeleteOriginal;
        if (slackLinkNames is not null) slackSinkOptions.SlackLinkNames = (bool)slackLinkNames;
        if (slackMarkdown is not null) slackSinkOptions.SlackMarkdown = (bool)slackMarkdown;
        if (slackParse is not null) slackSinkOptions.SlackParse = (ParseMode)slackParse;
        if (slackReplaceOriginal is not null) slackSinkOptions.SlackReplaceOriginal = (bool)slackReplaceOriginal;
        if (slackResponseType is not null) slackSinkOptions.SlackResponseType = slackResponseType;
        if (slackThreadId is not null) slackSinkOptions.SlackThreadId = slackThreadId;

        if (slackAttachmentColors is not null) slackSinkOptions.SlackAttachmentColors = slackAttachmentColors;
        if (slackAttachmentFooterIcon is not null)
            slackSinkOptions.SlackAttachmentFooterIcon = slackAttachmentFooterIcon;
        if (slackAddShortInfoAttachment is not null)
            slackSinkOptions.SlackAddShortInfoAttachment = (bool)slackAddShortInfoAttachment;
        if (slackDisplayShortInfoAttachmentShort is not null)
            slackSinkOptions.SlackDisplayShortInfoAttachmentShort = (bool)slackDisplayShortInfoAttachmentShort;
        if (slackAddExtendedInfoAttachment is not null)
            slackSinkOptions.SlackAddExtendedInfoAttachment = (bool)slackAddExtendedInfoAttachment;
        if (slackDisplayExtendedInfoAttachmentShort is not null)
            slackSinkOptions.SlackDisplayExtendedInfoAttachmentShort = (bool)slackDisplayExtendedInfoAttachmentShort;
        if (slackAddExceptionAttachment is not null)
            slackSinkOptions.SlackAddExceptionAttachment = (bool)slackAddExceptionAttachment;
        if (slackDisplayExceptionAttachmentShort is not null)
            slackSinkOptions.SlackDisplayExceptionAttachmentShort = (bool)slackDisplayExceptionAttachmentShort;

        if (slackConnectionTimeout is not null) slackSinkOptions.SlackConnectionTimeout = (int)slackConnectionTimeout;

        if (periodicBatchingSinkOptionsBatchSizeLimit is not null)
            slackSinkOptions.PeriodicBatchingSinkOptionsBatchSizeLimit = (int)periodicBatchingSinkOptionsBatchSizeLimit;
        if (periodicBatchingSinkOptionsPeriod is not null)
            slackSinkOptions.PeriodicBatchingSinkOptionsPeriod = (TimeSpan)periodicBatchingSinkOptionsPeriod;
        if (periodicBatchingSinkOptionsQueueLimit is not null)
            slackSinkOptions.PeriodicBatchingSinkOptionsQueueLimit = (int)periodicBatchingSinkOptionsQueueLimit;

        if (sinkOutputTemplate is not null) slackSinkOptions.SinkOutputTemplate = sinkOutputTemplate;

        return loggerSinkConfiguration.Sink(
            new SlackSink(slackSinkOptions, sinkFormatProvider, sinkActivationSwitch, slackHttpClient,
                generateSlackMessageText, generateSlackMessageAttachments, generateSlackMessageBlocks),
            sinkRestrictedToMinimumLevel ?? LevelAlias.Minimum,
            sinkLevelSwitch);
    }

    #endregion

    #region public constructors

#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute
    /// <summary>
    ///     <see cref="LoggerSinkConfiguration" /> extension that provides configuration chaining.
    /// </summary>
    /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration" /> object.</param>
    /// <param name="slackWebHookUrl">Slack WebHook URL (required).</param>
    /// <param name="slackChannel">Name of the Slack channel in which the log message should be posted (recommended).</param>
    /// <param name="slackUsername">Slack username (recommended).</param>
    /// <param name="slackEmojiIcon">Slack user-icon emoji string (recommended).</param>
    /// <param name="slackUriIcon">Slack user-icon image URI (optional).</param>
    /// <param name="slackDeleteOriginal">Slack message option 'DeleteOriginal' (optional).</param>
    /// <param name="slackLinkNames">Slack message option 'LinkNames' (optional).</param>
    /// <param name="slackMarkdown">Slack message option 'Markdown' (optional).</param>
    /// <param name="slackParseObj">Slack message option 'Parse' as <see cref="ParseMode" /></param>
    /// <param name="slackReplaceOriginal">Slack message option 'ReplaceOriginal' (optional).</param>
    /// <param name="slackResponseType">Slack message option 'ResponseType' (optional).</param>
    /// <param name="slackThreadId">Slack message option 'ThreadID' (optional).</param>
    /// <param name="slackAttachmentColorsObj">
    ///     Slack message attachment color list as
    ///     <see cref="IDictionary{LogEventLevel, String}" /> (optional).
    /// </param>
    /// <param name="slackAttachmentFooterIconObj">
    ///     Slack message attachment footer icon list as
    ///     <see cref="IDictionary{LogEventLevel, String}" /> (optional).
    /// </param>
    /// <param name="slackAddShortInfoAttachment">Add the short info attachment to the log message (optional).</param>
    /// <param name="slackDisplayShortInfoAttachmentShort">Display the short info attachment in short form (optional).</param>
    /// <param name="slackAddExtendedInfoAttachment">Add the extended info attachment to the log message (optional).</param>
    /// <param name="slackDisplayExtendedInfoAttachmentShort">Display the extended info attachment in short form (optional).</param>
    /// <param name="slackAddExceptionAttachment">Add the short exception to the log message (optional).</param>
    /// <param name="slackDisplayExceptionAttachmentShort">Display the exception attachment in short form (optional).</param>
    /// <param name="slackConnectionTimeout">Timeout for the connection to the Slack servers (optional).</param>
    /// <param name="slackHttpClientObj">The <see cref="HttpClient" /> instance which the <see cref="SlackClient" /> uses.</param>
    /// <param name="generateSlackFunctions">
    ///     A
    ///     <see
    ///         cref="Tuple{Func{LogEvent, IFormatProvider, Object, String}, Func{LogEvent, IFormatProvider, Object, List{SlackAttachment}}, Func{LogEvent, IFormatProvider, Object, List{Block}}}" />
    ///     containing custom functions [Item1 for message text generation, Item2 for message attachment list generation, Item3
    ///     for message block list generation] for the Slack message generation (optional).
    /// </param>
    /// <param name="periodicBatchingSinkOptionsBatchSizeLimit">
    ///     Size of the batch of messages that get send at once to Slack
    ///     (recommended).
    /// </param>
    /// <param name="periodicBatchingSinkOptionsPeriod">Time period between sending of batches of messages (recommended).</param>
    /// <param name="periodicBatchingSinkOptionsQueueLimit">
    ///     Maximum size of the queue that stores the messages before the
    ///     messages were send in batches to Slack (optional).
    /// </param>
    /// <param name="sinkRestrictedToMinimumLevel">
    ///     The absolute minimum <see cref="LogEventLevel" /> a log message must have to
    ///     be send to Slack (optional).
    /// </param>
    /// <param name="sinkOutputTemplate">The template for the output format of the log messages (optional).</param>
    /// <param name="sinkLevelSwitch">
    ///     A <see cref="LoggingLevelSwitch" /> to change the minimum <see cref="LogEventLevel" /> a
    ///     log message must have to be send to Slack (optional).
    /// </param>
    /// <param name="sinkFormatProvider">A format provider (optional).</param>
    /// <param name="sinkActivationSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
    /// <returns>Instance of <see cref="LoggerConfiguration" /> object.</returns>
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
    public static LoggerConfiguration Slack(
        this LoggerSinkConfiguration loggerSinkConfiguration,

        // slack options
        string slackWebHookUrl,
        string? slackChannel,
        string? slackUsername = null,
        string? slackEmojiIcon = null,
        Uri? slackUriIcon = null,
        bool? slackDeleteOriginal = null,
        bool? slackLinkNames = null,
        bool? slackMarkdown = null,
        object? slackParseObj = null,
        bool? slackReplaceOriginal = null,
        string? slackResponseType = null,
        string? slackThreadId = null,
        object? slackAttachmentColorsObj = null,
        object? slackAttachmentFooterIconObj = null,
        bool? slackAddShortInfoAttachment = null,
        bool? slackDisplayShortInfoAttachmentShort = null,
        bool? slackAddExtendedInfoAttachment = null,
        bool? slackDisplayExtendedInfoAttachmentShort = null,
        bool? slackAddExceptionAttachment = null,
        bool? slackDisplayExceptionAttachmentShort = null,
        int? slackConnectionTimeout = null,
        object? slackHttpClientObj = null,
        Tuple<object, object, object>? generateSlackFunctions = null,

        // periodic batch sink options
        int? periodicBatchingSinkOptionsBatchSizeLimit = null,
        TimeSpan? periodicBatchingSinkOptionsPeriod = null,
        int? periodicBatchingSinkOptionsQueueLimit = null,

        // sink options
        LogEventLevel? sinkRestrictedToMinimumLevel = null,
        string? sinkOutputTemplate = null,
        LoggingLevelSwitch? sinkLevelSwitch = null,
        IFormatProvider? sinkFormatProvider = null,
        SlackSinkActivationSwitch? sinkActivationSwitch = null
    )
    {
        return Slack(loggerSinkConfiguration, slackWebHookUrl, slackUsername, slackEmojiIcon, slackUriIcon,
            string.IsNullOrEmpty(slackChannel) ? null : [slackChannel], slackDeleteOriginal,
            slackLinkNames, slackMarkdown, slackParseObj, slackReplaceOriginal,
            slackResponseType, slackThreadId, slackAttachmentColorsObj, slackAttachmentFooterIconObj,
            slackAddShortInfoAttachment, slackDisplayShortInfoAttachmentShort, slackAddExtendedInfoAttachment,
            slackDisplayExtendedInfoAttachmentShort, slackAddExceptionAttachment,
            slackDisplayExceptionAttachmentShort, slackConnectionTimeout, slackHttpClientObj, generateSlackFunctions,
            periodicBatchingSinkOptionsBatchSizeLimit,
            periodicBatchingSinkOptionsPeriod, periodicBatchingSinkOptionsQueueLimit, sinkRestrictedToMinimumLevel,
            sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider, sinkActivationSwitch);
    }

#pragma warning disable CS1584 // XML comment has syntactically incorrect cref attribute
    /// <summary>
    ///     <see cref="LoggerSinkConfiguration" /> extension that provides configuration chaining.
    /// </summary>
    /// <param name="loggerSinkConfiguration">Instance of <see cref="LoggerSinkConfiguration" /> object.</param>
    /// <param name="slackWebHookUrl">Slack WebHook URL.</param>
    /// <param name="slackUsername">Slack username (recommended).</param>
    /// <param name="slackEmojiIcon">Slack user-icon emoji string (optional).</param>
    /// <param name="slackUriIcon">Slack user-icon image URI (optional).</param>
    /// <param name="slackChannels">
    ///     A <see cref="List{String}" /> containing the name of all Slack channels in which the log
    ///     message should be posted (recommended).
    /// </param>
    /// <param name="slackDeleteOriginal">Slack message option 'DeleteOriginal' (optional).</param>
    /// <param name="slackLinkNames">Slack message option 'LinkNames' (optional).</param>
    /// <param name="slackMarkdown">Slack message option 'Markdown' (optional).</param>
    /// <param name="slackParseObj">Slack message option 'Parse' as <see cref="ParseMode" /></param>
    /// <param name="slackReplaceOriginal">Slack message option 'ReplaceOriginal' (optional).</param>
    /// <param name="slackResponseType">Slack message option 'ResponseType' (optional).</param>
    /// <param name="slackThreadId">Slack message option 'ThreadID' (optional).</param>
    /// <param name="slackAttachmentColorsObj">
    ///     Slack message attachment color list as
    ///     <see cref="IDictionary{LogEventLevel,String}" /> (optional).
    /// </param>
    /// <param name="slackAttachmentFooterIconObj">
    ///     Slack message attachment footer icon list as
    ///     <see cref="IDictionary{LogEventLevel,String}" /> (optional).
    /// </param>
    /// <param name="slackAddShortInfoAttachment">Add the short info attachment to the log message (optional).</param>
    /// <param name="slackDisplayShortInfoAttachmentShort">Display the short info attachment in short form (optional).</param>
    /// <param name="slackAddExtendedInfoAttachment">Add the extended info attachment to the log message (optional).</param>
    /// <param name="slackDisplayExtendedInfoAttachmentShort">Display the extended info attachment in short form (optional).</param>
    /// <param name="slackAddExceptionAttachment">Add the short exception to the log message (optional).</param>
    /// <param name="slackDisplayExceptionAttachmentShort">Display the exception attachment in short form (optional).</param>
    /// <param name="slackConnectionTimeout">Timeout for the connection to the Slack servers (optional).</param>
    /// <param name="slackHttpClientObj">The <see cref="HttpClient" /> instance which the <see cref="SlackClient" /> uses.</param>
    /// <param name="generateSlackFunctions">
    ///     A
    ///     <see
    ///         cref="Tuple{Func{LogEvent, IFormatProvider, Object, String}, Func{LogEvent, IFormatProvider, Object, List{SlackAttachment}}, Func{LogEvent, IFormatProvider, Object, List{Block}}}" />
    ///     containing custom functions [Item1 for message text generation, Item2 for message attachment list generation, Item3
    ///     for message block list generation] for the Slack message generation (optional).
    /// </param>
    /// <param name="periodicBatchingSinkOptionsBatchSizeLimit">
    ///     Size of the batch of messages that get send at once to Slack
    ///     (recommended).
    /// </param>
    /// <param name="periodicBatchingSinkOptionsPeriod">Time period between sending of batches of messages (recommended).</param>
    /// <param name="periodicBatchingSinkOptionsQueueLimit">
    ///     Maximum size of the queue that stores the messages before the
    ///     messages were send in batches to Slack (optional).
    /// </param>
    /// <param name="sinkRestrictedToMinimumLevel">
    ///     The absolute minimum <see cref="LogEventLevel" /> a log message must have to
    ///     be send to Slack (optional).
    /// </param>
    /// <param name="sinkOutputTemplate">The template for the output format of the log messages (optional).</param>
    /// <param name="sinkLevelSwitch">
    ///     A <see cref="LoggingLevelSwitch" /> to change the minimum <see cref="LogEventLevel" /> a
    ///     log message must have to be send to Slack (optional).
    /// </param>
    /// <param name="sinkFormatProvider">A format provider (optional).</param>
    /// <param name="sinkActivationSwitch">A Switch to change the activation status of the sink on the fly (optional).</param>
    /// <returns>Instance of <see cref="LoggerConfiguration" /> object.</returns>
#pragma warning restore CS1584 // XML comment has syntactically incorrect cref attribute
    public static LoggerConfiguration Slack(
        this LoggerSinkConfiguration loggerSinkConfiguration,

        // slack options
        string slackWebHookUrl,
        string? slackUsername = null,
        string? slackEmojiIcon = null,
        Uri? slackUriIcon = null,
        List<string?>? slackChannels = null,
        bool? slackDeleteOriginal = null,
        bool? slackLinkNames = null,
        bool? slackMarkdown = null,
        object? slackParseObj = null,
        bool? slackReplaceOriginal = null,
        string? slackResponseType = null,
        string? slackThreadId = null,
        object? slackAttachmentColorsObj = null,
        object? slackAttachmentFooterIconObj = null,
        bool? slackAddShortInfoAttachment = null,
        bool? slackDisplayShortInfoAttachmentShort = null,
        bool? slackAddExtendedInfoAttachment = null,
        bool? slackDisplayExtendedInfoAttachmentShort = null,
        bool? slackAddExceptionAttachment = null,
        bool? slackDisplayExceptionAttachmentShort = null,
        int? slackConnectionTimeout = null,
        object? slackHttpClientObj = null,
        Tuple<object, object, object>? generateSlackFunctions = null,

        // periodic batch sink options
        int? periodicBatchingSinkOptionsBatchSizeLimit = null,
        TimeSpan? periodicBatchingSinkOptionsPeriod = null,
        int? periodicBatchingSinkOptionsQueueLimit = null,

        // sink options
        LogEventLevel? sinkRestrictedToMinimumLevel = null,
        string? sinkOutputTemplate = null,
        LoggingLevelSwitch? sinkLevelSwitch = null,
        IFormatProvider? sinkFormatProvider = null,
        SlackSinkActivationSwitch? sinkActivationSwitch = null
    )
    {
        ParseMode? slackParse = null;
        if (slackParseObj is not null) slackParse = (ParseMode)slackParseObj;

        IDictionary<LogEventLevel, string?>? slackAttachmentColors = null;
        if (slackAttachmentColorsObj is not null)
            slackAttachmentColors = (IDictionary<LogEventLevel, string?>)slackAttachmentColorsObj;

        IDictionary<LogEventLevel, string?>? slackAttachmentFooterIcon = null;
        if (slackAttachmentFooterIconObj is not null)
            slackAttachmentFooterIcon = (IDictionary<LogEventLevel, string?>)slackAttachmentFooterIconObj;

        HttpClient? slackHttpClient = null;
        if (slackHttpClientObj is not null) slackHttpClient = (HttpClient)slackHttpClientObj;

        Func<LogEvent, IFormatProvider?, object, string?>? generateSlackMessageText = null;
        Func<LogEvent, IFormatProvider?, object, List<SlackAttachment>?>? generateSlackMessageAttachments = null;
        Func<LogEvent, IFormatProvider?, object, List<Block>?>? generateSlackMessageBlocks = null;

        if (generateSlackFunctions is not null)
        {
            generateSlackMessageText =
                (Func<LogEvent, IFormatProvider?, object, string?>)generateSlackFunctions.Item1;
            generateSlackMessageAttachments =
                (Func<LogEvent, IFormatProvider?, object, List<SlackAttachment>?>)generateSlackFunctions.Item2;
            generateSlackMessageBlocks =
                (Func<LogEvent, IFormatProvider?, object, List<Block>?>)generateSlackFunctions.Item3;
        }

        return Slack(loggerSinkConfiguration, slackWebHookUrl, slackUsername, slackEmojiIcon, slackUriIcon,
            slackChannels, slackDeleteOriginal, slackLinkNames, slackMarkdown, slackParse, slackReplaceOriginal,
            slackResponseType, slackThreadId, slackAttachmentColors, slackAttachmentFooterIcon,
            slackAddShortInfoAttachment, slackDisplayShortInfoAttachmentShort, slackAddExtendedInfoAttachment,
            slackDisplayExtendedInfoAttachmentShort, slackAddExceptionAttachment,
            slackDisplayExceptionAttachmentShort, slackConnectionTimeout, slackHttpClient, generateSlackMessageText,
            generateSlackMessageAttachments, generateSlackMessageBlocks, periodicBatchingSinkOptionsBatchSizeLimit,
            periodicBatchingSinkOptionsPeriod, periodicBatchingSinkOptionsQueueLimit, sinkRestrictedToMinimumLevel,
            sinkOutputTemplate, sinkLevelSwitch, sinkFormatProvider, sinkActivationSwitch);
    }

    #endregion
}