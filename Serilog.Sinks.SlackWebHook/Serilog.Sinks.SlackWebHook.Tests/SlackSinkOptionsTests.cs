using NUnit.Framework;
using System;

namespace Serilog.Sinks.SlackWebHook.Tests
{
    [TestFixture]
    public class SlackSinkOptionsTests
    {
        #region setup

        [SetUp]
        public void SetUp()
        {

        }

        #endregion

        #region init tests

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

        #endregion
    }
}
