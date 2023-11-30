using System;
using NUnit.Framework;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests;

[TestFixture]
public class SlackSinkOptionsTests
{
    [SetUp]
    public void SetUp()
    {
    }

    [Test]
    public void DefaultConstructorTest()
    {
        var options = new SlackSinkOptions();
        
        Assert.That(options.SlackAttachmentColors.Count == 6);
        Assert.That(options.SlackAttachmentFooterIcon.Count == 6);

        Assert.That(options.PeriodicBatchingSinkOptionsBatchSizeLimit >= 0);
        Assert.That(!options.PeriodicBatchingSinkOptionsPeriod.Equals(TimeSpan.Zero));
        Assert.That(options.PeriodicBatchingSinkOptionsQueueLimit >= 0);
    }
}