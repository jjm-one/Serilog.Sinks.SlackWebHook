namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Unit tests for the SlackSink class.
/// </summary>
public class SlackSinkTests
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
        var sink = new SlackSink(
            options,
            null
        );

        sink.Should().NotBeNull();
    }
}