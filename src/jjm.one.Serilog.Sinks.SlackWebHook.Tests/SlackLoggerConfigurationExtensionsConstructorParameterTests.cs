using System;
using System.Collections.Generic;
using System.Net.Http;
using NUnit.Framework;
using Serilog;
using Serilog.Events;
using Slack.Webhooks;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

[TestFixture]
public class SlackLoggerConfigurationExtensionsConstructorParameterTests
{
    [SetUp]
    public void SetUp()
    {
    }

    private const string ValidWebHook = @"https://slack.com/api/api.test";

    [Test]
    public void SingleChannel_ConstructorTest_WebHookUrlNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    null!,
                    slackChannel: null
                )
                .CreateLogger();
        });
    }

    [Test]
    public void SingleChannel_ConstructorTest_WebHookUrlEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    "",
                    slackChannel: null
                )
                .CreateLogger();
        });
    }

    [Test]
    public void SingleChannel_ConstructorTest_WebHookUrlValid()
    {
        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_WebHookUrlNull()
    {
        Assert.Throws<ArgumentNullException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    null!,
                    slackChannel: null
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_WebHookUrlEmpty()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    "",
                    slackChannels: null
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_WebHookUrlValid()
    {
        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null
                )
                .CreateLogger();
        });
    }

    [Test]
    public void SingleChannel_ConstructorTest_SlackParseObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackParseObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackParseObj: ParseMode.None
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_SlackParseObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackParseObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackParseObj: ParseMode.None
                )
                .CreateLogger();
        });
    }

    [Test]
    public void SingleChannel_ConstructorTest_SlackAttachmentColorsObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackAttachmentColorsObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackAttachmentColorsObj: new Dictionary<LogEventLevel, string>
                    {
                        { LogEventLevel.Verbose, "" }
                    }
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_SlackAttachmentColorsObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackAttachmentColorsObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackAttachmentColorsObj: new Dictionary<LogEventLevel, string>
                    {
                        { LogEventLevel.Verbose, "" }
                    }
                )
                .CreateLogger();
        });
    }

    [Test]
    public void SingleChannel_ConstructorTest_SlackAttachmentFooterIconObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackAttachmentFooterIconObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackAttachmentFooterIconObj: new Dictionary<LogEventLevel, string>
                    {
                        { LogEventLevel.Verbose, "" }
                    }
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_SlackAttachmentFooterIconObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackAttachmentFooterIconObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackAttachmentFooterIconObj: new Dictionary<LogEventLevel, string>
                    {
                        { LogEventLevel.Verbose, "" }
                    }
                )
                .CreateLogger();
        });
    }

    [Test]
    public void SingleChannel_ConstructorTest_SlackHttpClientObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackHttpClientObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackHttpClientObj: new HttpClient()
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_SlackHttpClientObjCast()
    {
        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackHttpClientObj: "TestObject"
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackHttpClientObj: new HttpClient()
                )
                .CreateLogger();
        });
    }

    private static string? TestF1(LogEvent a, IFormatProvider b, object c)
    {
        return null;
    }

    private static List<SlackAttachment>? TestF2(LogEvent a, IFormatProvider b, object c)
    {
        return null;
    }

    private static List<Block>? TestF3(LogEvent a, IFormatProvider b, object c)
    {
        return null;
    }

    [Test]
    public void SingleChannel_ConstructorTest_GenerateSlackFunctionsCast()
    {
        var f1 = TestF1;
        var f2 = TestF2;
        var f3 = TestF3;

        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    generateSlackFunctions: new Tuple<object, object, object>("", "", "")
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    generateSlackFunctions: new Tuple<object, object, object>(f1, f2, f3)
                )
                .CreateLogger();
        });
    }

    [Test]
    public void MultiChannel_ConstructorTest_GenerateSlackFunctionsCast()
    {
        var f1 = TestF1;
        var f2 = TestF2;
        var f3 = TestF3;

        Assert.Throws<InvalidCastException>(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    generateSlackFunctions: new Tuple<object, object, object>("", "", "")
                )
                .CreateLogger();
        });

        Assert.DoesNotThrow(() =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    generateSlackFunctions: new Tuple<object, object, object>(f1, f2, f3)
                )
                .CreateLogger();
        });
    }
}