using System;
using NUnit.Framework;

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

            Assert.IsTrue(options.SlackPeriodicBatchingSinkOptionsBatchSizeLimit >= 0);
            Assert.IsTrue(!options.SlackPeriodicBatchingSinkOptionsPeriod.Equals(TimeSpan.Zero));
            Assert.IsTrue(options.SlackPeriodicBatchingSinkOptionsQueueLimit >= 0);
        }

        #endregion
    }
}
