using System;
using System.Threading;
using NUnit.Framework;
using Serilog;
using Serilog.Events;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

[TestFixture]
public class SlackSinkLoggerTests
{
    [SetUp]
    public void SetUp()
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

    public const string ValidWebHook = @"https://slack.com/api/api.test";

    [Test]
    public void SingleChannel_LoggerTests_LogVerbose()
    {
        Assert.DoesNotThrow(() =>
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
        });
    }

    [Test]
    public void SingleChannel_LoggerTests_LogDebug()
    {
        Assert.DoesNotThrow(() =>
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
        });
    }

    [Test]
    public void SingleChannel_LoggerTests_LogInformation()
    {
        Assert.DoesNotThrow(() =>
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
        });
    }

    [Test]
    public void SingleChannel_LoggerTests_LogWarning()
    {
        Assert.DoesNotThrow(() =>
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
        });
    }

    [Test]
    public void SingleChannel_LoggerTests_LogError()
    {
        Assert.DoesNotThrow(() =>
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
        });
    }

    [Test]
    public void SingleChannel_LoggerTests_LogFatal()
    {
        Assert.DoesNotThrow(() =>
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
        });
    }
}