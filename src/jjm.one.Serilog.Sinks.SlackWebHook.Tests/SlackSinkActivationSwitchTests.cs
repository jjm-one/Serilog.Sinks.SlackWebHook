namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

/// <summary>
///     Unit tests for the SlackSinkActivationSwitch class.
/// </summary>
public class SlackSinkActivationSwitchTests
{
    /// <summary>
    ///     Test that the default values are set correctly.
    /// </summary>
    [Fact]
    public void DefaultValuesAreSetCorrectly()
    {
        var s = new SlackSinkActivationSwitch();

        s.SlackSinkStatus.Should().Be(SlackSinkActivationSwitch.SlackSinkActivationStatus.Active);
    }

    /// <summary>
    ///     Test that the properties can be set and retrieved correctly.
    /// </summary>
    [Fact]
    public void PropertiesCanBeSetAndRetrievedCorrectly()
    {
        var s = new SlackSinkActivationSwitch(SlackSinkActivationSwitch.SlackSinkActivationStatus.InActive);

        s.SlackSinkStatus.Should().Be(SlackSinkActivationSwitch.SlackSinkActivationStatus.InActive);
    }
}