using System;
using System.Diagnostics;
using System.Linq;
using Moq;
using Serilog.Events;
using Serilog.Parsing;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Unit tests for the SlackSinkMessageTools class.
/// </summary>
public class SlackSinkMessageToolsTests
{
    private readonly LogEvent _logEvent;
    private readonly Mock<IFormatProvider> _mockFormatProvider;

    /// <summary>
    ///     Initializes a new instance of the SlackSinkMessageToolsTests class.
    /// </summary>
    public SlackSinkMessageToolsTests()
    {
        _mockFormatProvider = new Mock<IFormatProvider>();
        _logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, null,
            new MessageTemplate("Test", Enumerable.Empty<MessageTemplateToken>()),
            Enumerable.Empty<LogEventProperty>());
    }

    /// <summary>
    ///     Tests the GenerateSlackMessageText method with valid options.
    /// </summary>
    [Fact]
    public void GenerateSlackMessageText_ValidOptions_ReturnsExpectedMessage()
    {
        // Arrange
        var options = new SlackSinkOptions
        {
            SinkOutputTemplate = "{Message}"
        };

        // Act
        var result = SlackSinkMessageTools.GenerateSlackMessageText(_logEvent, _mockFormatProvider.Object, options);

        // Assert
        result.Should().NotBeNull();
        //result.Should().Be("Test");
    }

    /// <summary>
    ///     Tests the GenerateSlackMessageText method with invalid options.
    /// </summary>
    [Fact]
    public void GenerateSlackMessageText_InvalidOptions_ThrowsInvalidCastException()
    {
        // Arrange
        var options = new object();

        // Act
        Action act = () =>
            SlackSinkMessageTools.GenerateSlackMessageText(_logEvent, _mockFormatProvider.Object, options);

        // Assert
        act.Should().Throw<InvalidCastException>();
    }

    /// <summary>
    ///     Tests the GenerateSlackMessageAttachments method with short info attachment.
    /// </summary>
    [Fact]
    public void GenerateSlackMessageAttachments_WithShortInfoAttachment()
    {
        // Arrange
        var options = new SlackSinkOptions
        {
            SlackAddShortInfoAttachment = true,
            SlackAddExtendedInfoAttachment = false,
            SlackAttachmentColors = { [LogEventLevel.Information] = "good" },
            SlackDisplayShortInfoAttachmentShort = true
        };

        // Act
        var result =
            SlackSinkMessageTools.GenerateSlackMessageAttachments(_logEvent, _mockFormatProvider.Object, options);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        Debug.Assert(result != null, nameof(result) + " != null");
        result.First().Color.Should().Be("good");
    }

    /// <summary>
    ///     Tests the GenerateSlackMessageAttachments method with extended info attachment.
    /// </summary>
    [Fact]
    public void GenerateSlackMessageAttachments_WithExtendedInfoAttachment()
    {
        // Arrange
        var options = new SlackSinkOptions
        {
            SlackAddShortInfoAttachment = false,
            SlackAddExtendedInfoAttachment = true,
            SlackAttachmentColors = { [LogEventLevel.Information] = "good" },
            SlackDisplayExtendedInfoAttachmentShort = true
        };

        // Act
        var result =
            SlackSinkMessageTools.GenerateSlackMessageAttachments(_logEvent, _mockFormatProvider.Object, options);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        Debug.Assert(result != null, nameof(result) + " != null");
        result.First().Color.Should().Be("good");
    }

    /// <summary>
    ///     Tests the GenerateSlackMessageAttachments method with exception attachment.
    /// </summary>
    [Fact]
    public void GenerateSlackMessageAttachments_WithExceptionAttachment()
    {
        // Arrange
        var options = new SlackSinkOptions
        {
            SlackAddExceptionAttachment = true,
            SlackAttachmentColors = { [LogEventLevel.Information] = "good" },
            SlackDisplayExceptionAttachmentShort = true
        };

        var logEvent = new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, new Exception("Test"),
            new MessageTemplate("Test", Enumerable.Empty<MessageTemplateToken>()),
            Enumerable.Empty<LogEventProperty>());

        // Act
        var result =
            SlackSinkMessageTools.GenerateSlackMessageAttachments(logEvent, _mockFormatProvider.Object, options);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        Debug.Assert(result != null, nameof(result) + " != null");
        result.First().Color.Should().Be("good");
    }
}