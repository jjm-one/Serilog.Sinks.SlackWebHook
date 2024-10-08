using System;
using System.Collections.Generic;
using jjm.one.Slack.Webhooks;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Unit tests for the SlackSinkOptions class.
/// </summary>
public class SlackSinkOptionsTests
{
    /// <summary>
    ///     Test that the default values are set correctly.
    /// </summary>
    [Fact]
    public void DefaultValuesAreSetCorrectly()
    {
        var options = new SlackSinkOptions();

        options.SinkOutputTemplate.Should().Be("{Message:lj}");
        options.SlackConnectionTimeout.Should().Be(1000);
        options.SlackDeleteOriginal.Should().BeFalse();
        options.SlackLinkNames.Should().BeFalse();
        options.SlackMarkdown.Should().BeFalse();
        options.SlackParse.Should().Be(ParseMode.None);
        options.SlackReplaceOriginal.Should().BeFalse();
        options.SlackAddShortInfoAttachment.Should().BeTrue();
    }

    /// <summary>
    ///     Test that the properties can be set and retrieved correctly.
    /// </summary>
    [Fact]
    public void PropertiesCanBeSetAndRetrievedCorrectly()
    {
        var options = new SlackSinkOptions
        {
            SinkOutputTemplate = "NewTemplate",
            SlackWebHookUrl = "NewUrl",
            SlackConnectionTimeout = 2000,
            SlackUsername = "NewUsername",
            SlackEmojiIcon = "NewEmojiIcon",
            SlackUriIcon = new Uri("http://example.com/icon"),
            SlackChannels = new List<string?> { "channel1", "channel2" },
            SlackDeleteOriginal = true,
            SlackLinkNames = true,
            SlackMarkdown = true,
            SlackParse = ParseMode.Full,
            SlackReplaceOriginal = true,
            SlackResponseType = "NewResponseType",
            SlackThreadId = "NewThreadId",
            SlackAddShortInfoAttachment = false
        };

        options.SinkOutputTemplate.Should().Be("NewTemplate");
        options.SlackWebHookUrl.Should().Be("NewUrl");
        options.SlackConnectionTimeout.Should().Be(2000);
        options.SlackUsername.Should().Be("NewUsername");
        options.SlackEmojiIcon.Should().Be("NewEmojiIcon");
        options.SlackUriIcon.Should().Be(new Uri("http://example.com/icon"));
        options.SlackChannels.Should().Equal(new List<string> { "channel1", "channel2" });
        options.SlackDeleteOriginal.Should().BeTrue();
        options.SlackLinkNames.Should().BeTrue();
        options.SlackMarkdown.Should().BeTrue();
        options.SlackParse.Should().Be(ParseMode.Full);
        options.SlackReplaceOriginal.Should().BeTrue();
        options.SlackResponseType.Should().Be("NewResponseType");
        options.SlackThreadId.Should().Be("NewThreadId");
        options.SlackAddShortInfoAttachment.Should().BeFalse();
    }
}