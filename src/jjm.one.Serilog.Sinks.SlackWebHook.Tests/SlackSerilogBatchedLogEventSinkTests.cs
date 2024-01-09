using System;
using System.Collections.Generic;
using Serilog.Events;
using Serilog.Parsing;
using Serilog.Sinks.PeriodicBatching;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Unit tests for the SlackSerilogBatchedLogEventSink class.
/// </summary>
public class SlackSerilogBatchedLogEventSinkTests
{
    /// <summary>
    ///     Test that the constructor works correctly.
    /// </summary>
    [Fact]
    public void ConstructorWorksCorrectly()
    {
        var options = new SlackSinkOptions
        {
            SlackWebHookUrl = @"https://slack.com/api/api.test"
        };
        var sink = new SlackSerilogBatchedLogEventSink(options, null);

        sink.Should().NotBeNull();
    }

    /// <summary>
    ///     Test that the EmitBatchAsync function works correctly.
    /// </summary>
    [Fact]
    public async void EmitBatchAsyncWorksCorrectly()
    {
        var options = new SlackSinkOptions
        {
            SlackWebHookUrl = @"https://slack.com/api/api.test"
        };
        var sink = new SlackSerilogBatchedLogEventSink(options, null);
        var logEvents = new List<LogEvent>
        {
            new(DateTimeOffset.Now, LogEventLevel.Information, null,
                new MessageTemplate("Test message", new List<MessageTemplateToken>()), new List<LogEventProperty>())
        };

        await ((IBatchedLogEventSink)sink).EmitBatchAsync(logEvents);
    }

    /// <summary>
    ///     Test that the Dispose function works correctly.
    /// </summary>
    [Fact]
    public void DisposeWorksCorrectly()
    {
        var options = new SlackSinkOptions
        {
            SlackWebHookUrl = @"https://slack.com/api/api.test"
        };
        var sink = new SlackSerilogBatchedLogEventSink(options, null);

        sink.Dispose();
    }
}