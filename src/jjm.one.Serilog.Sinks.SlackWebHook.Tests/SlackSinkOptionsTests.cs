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

        Assert.IsTrue(options.SlackAttachmentColors.Count == 6);
        Assert.IsTrue(options.SlackAttachmentFooterIcon.Count == 6);

        Assert.IsTrue(options.PeriodicBatchingSinkOptionsBatchSizeLimit >= 0);
        Assert.IsTrue(!options.PeriodicBatchingSinkOptionsPeriod.Equals(TimeSpan.Zero));
        Assert.IsTrue(options.PeriodicBatchingSinkOptionsQueueLimit >= 0);
    }
}