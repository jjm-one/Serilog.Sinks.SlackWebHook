using System;
using System.Collections.Generic;
using System.Net.Http;
using Moq;
using Serilog;
using Serilog.Events;
using jjm.one.Slack.Webhooks;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Unit tests for the SlackLoggerConfigurationExtensions class.
/// </summary>
public class SlackLoggerConfigurationExtensionsTests
{
    private const string ValidWebHook = "https://slack.com/api/api.test";

    /// <summary>
    ///     Test to ensure that the constructor throws an ArgumentNullException when the webhook URL is null.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_WebHookUrlNull()
    {
        // Arrange
        var act = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    null!,
                    slackChannel: null
                )
                .CreateLogger();
        };

        // Act & Assert
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an ArgumentException when the webhook URL is empty.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_WebHookUrlEmpty()
    {
        // Arrange
        var act = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    "",
                    slackChannel: null
                )
                .CreateLogger();
        };

        // Act & Assert
        act.Should().Throw<ArgumentException>();
    }

    /// <summary>
    ///     Test to ensure that the constructor does not throw an exception when the webhook URL is valid.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_WebHookUrlValid()
    {
        // Arrange
        var act = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null
                )
                .CreateLogger();
        };

        // Act & Assert
        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an ArgumentNullException when the webhook URL is null.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_WebHookUrlNull()
    {
        // Arrange
        var act = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    null!,
                    slackChannel: null
                )
                .CreateLogger();
        };

        // Act & Assert
        act.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an ArgumentException when the webhook URL is empty.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_WebHookUrlEmpty()
    {
        // Arrange
        var act = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    "",
                    slackChannels: null
                )
                .CreateLogger();
        };

        // Act & Assert
        act.Should().Throw<ArgumentException>();
    }

    /// <summary>
    ///     Test to ensure that the constructor does not throw an exception when the webhook URL is valid.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_WebHookUrlValid()
    {
        // Arrange
        var act = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null
                )
                .CreateLogger();
        };

        // Act & Assert
        act.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackParseObj is a string.
    ///     Also, it should not throw any exception when the slackParseObj is of type ParseMode.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_SlackParseObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackParseObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackParseObj: ParseMode.None
                )
                .CreateLogger();
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackParseObj is a string.
    ///     Also, it should not throw any exception when the slackParseObj is of type ParseMode.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_SlackParseObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackParseObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackParseObj: ParseMode.None
                )
                .CreateLogger();
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackAttachmentColorsObj is a string.
    ///     Also, it should not throw any exception when the slackAttachmentColorsObj is of type Dictionary
    ///     <LogEventLevel, string>.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_SlackAttachmentColorsObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackAttachmentColorsObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
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
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackAttachmentColorsObj is a string.
    ///     Also, it should not throw any exception when the slackAttachmentColorsObj is of type Dictionary
    ///     <LogEventLevel, string>.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_SlackAttachmentColorsObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackAttachmentColorsObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
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
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackAttachmentFooterIconObj is a
    ///     string.
    ///     Also, it should not throw any exception when the slackAttachmentFooterIconObj is of type Dictionary
    ///     <LogEventLevel, string>.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_SlackAttachmentFooterIconObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackAttachmentFooterIconObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
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
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackAttachmentFooterIconObj is a
    ///     string.
    ///     Also, it should not throw any exception when the slackAttachmentFooterIconObj is of type Dictionary
    ///     <LogEventLevel, string>.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_SlackAttachmentFooterIconObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackAttachmentFooterIconObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
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
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackHttpClientObj is a string.
    ///     Also, it should not throw any exception when the slackHttpClientObj is of type HttpClient.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_SlackHttpClientObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackHttpClientObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
        {
            var mockHttpClient = new Mock<HttpClient>();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    slackHttpClientObj: mockHttpClient.Object
                )
                .CreateLogger();
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the slackHttpClientObj is a string.
    ///     Also, it should not throw any exception when the slackHttpClientObj is of type HttpClient.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_SlackHttpClientObjCast()
    {
        // Arrange
        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackHttpClientObj: "TestObject"
                )
                .CreateLogger();
        };

        var actValidCast = () =>
        {
            var mockHttpClient = new Mock<HttpClient>();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    slackHttpClientObj: mockHttpClient.Object
                )
                .CreateLogger();
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
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

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the generateSlackFunctions is a Tuple of
    ///     strings.
    ///     Also, it should not throw any exception when the generateSlackFunctions is a Tuple of functions.
    /// </summary>
    [Fact]
    public void SingleChannel_ConstructorTest_GenerateSlackFunctionsCast()
    {
        var f1 = TestF1;
        var f2 = TestF2;
        var f3 = TestF3;

        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    generateSlackFunctions: new Tuple<object, object, object>("", "", "")
                )
                .CreateLogger();
        };

        var actValidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannel: null,
                    generateSlackFunctions: new Tuple<object, object, object>(f1, f2, f3)
                )
                .CreateLogger();
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }

    /// <summary>
    ///     Test to ensure that the constructor throws an InvalidCastException when the generateSlackFunctions is a Tuple of
    ///     strings.
    ///     Also, it should not throw any exception when the generateSlackFunctions is a Tuple of functions.
    /// </summary>
    [Fact]
    public void MultiChannel_ConstructorTest_GenerateSlackFunctionsCast()
    {
        var f1 = TestF1;
        var f2 = TestF2;
        var f3 = TestF3;

        var actInvalidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    generateSlackFunctions: new Tuple<object, object, object>("", "", "")
                )
                .CreateLogger();
        };

        var actValidCast = () =>
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                    ValidWebHook,
                    slackChannels: null,
                    generateSlackFunctions: new Tuple<object, object, object>(f1, f2, f3)
                )
                .CreateLogger();
        };

        // Act & Assert
        actInvalidCast.Should().Throw<InvalidCastException>();
        actValidCast.Should().NotThrow();
    }
}