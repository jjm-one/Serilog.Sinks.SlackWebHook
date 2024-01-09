using System;
using System.Threading;
using Serilog;
using Serilog.Events;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Tests for SlackSinkLogger.
/// </summary>
public class SlackSinkLoggerTests
{
    private const string ValidWebHook = @"https://slack.com/api/api.test";

    public SlackSinkLoggerTests()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Slack(
                ValidWebHook,
                slackChannel: null,
                periodicBatchingSinkOptionsBatchSizeLimit: 1,
                periodicBatchingSinkOptionsPeriod: TimeSpan.FromTicks(1)
            )
            .CreateLogger();
    }

    /// <summary>
    ///     Test to ensure that the logger does not throw an exception when logging a verbose message.
    /// </summary>
    [Fact]
    public void SingleChannel_LoggerTests_LogVerbose()
    {
        var act = () =>
        {
            try
            {
                throw new Exception("Test Exception!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Verbose, e, "Verbose Log Message");
            }

            Thread.Sleep(500);
        };

        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the logger does not throw an exception when logging a debug message.
    /// </summary>
    [Fact]
    public void SingleChannel_LoggerTests_LogDebug()
    {
        var act = () =>
        {
            try
            {
                throw new Exception("Test Exception!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Debug, e, "Debug Log Message");
            }

            Thread.Sleep(500);
        };

        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the logger does not throw an exception when logging an information message.
    /// </summary>
    [Fact]
    public void SingleChannel_LoggerTests_LogInformation()
    {
        var act = () =>
        {
            try
            {
                throw new Exception("Test Exception!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Information, e, "Information Log Message");
            }

            Thread.Sleep(500);
        };

        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the logger does not throw an exception when logging a warning message.
    /// </summary>
    [Fact]
    public void SingleChannel_LoggerTests_LogWarning()
    {
        var act = () =>
        {
            try
            {
                throw new Exception("Test Exception!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Warning, e, "Warning Log Message");
            }

            Thread.Sleep(500);
        };

        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the logger does not throw an exception when logging an error message.
    /// </summary>
    [Fact]
    public void SingleChannel_LoggerTests_LogError()
    {
        var act = () =>
        {
            try
            {
                throw new Exception("Test Exception!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Error, e, "Error Log Message");
            }

            Thread.Sleep(500);
        };

        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the logger does not throw an exception when logging a fatal message.
    /// </summary>
    [Fact]
    public void SingleChannel_LoggerTests_LogFatal()
    {
        var act = () =>
        {
            try
            {
                throw new Exception("Test Exception!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Fatal, e, "Fatal Log Message");
            }

            Thread.Sleep(500);
        };

        act.Should().NotThrow();
    }
}