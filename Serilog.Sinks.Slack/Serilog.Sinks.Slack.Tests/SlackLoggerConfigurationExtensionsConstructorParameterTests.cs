using System;
using System.Collections.Generic;
using NUnit.Framework;
using Serilog.Events;
using Slack.Webhooks;

namespace Serilog.Sinks.Slack.Tests
{
    [TestFixture]
    public class SlackLoggerConfigurationExtensionsConstructorParameterTests
    {
        #region const

        public const string ValidWebHook = @"https://slack.com/api/api.test";

        #endregion

        #region setup

        [SetUp]
        public void SetUp()
        {

        }

        #endregion

        #region WebHookUrl test

        [Test]
        public void SingleChannel_ConstructorTest_WebHookUrlNull()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: null,
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
                        slackWebHookUrl: "",
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
                        slackWebHookUrl: ValidWebHook,
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
                        slackWebHookUrl: null,
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
                        slackWebHookUrl: "",
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
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null
                    )
                    .CreateLogger();
            });
        }

        #endregion

        #region slackParseObj cast test

        [Test]
        public void SingleChannel_ConstructorTest_SlackParseObjCast()
        {
            
            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackParseObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
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
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackParseObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackParseObj: ParseMode.None
                    )
                    .CreateLogger();
            });
        }

        #endregion

        #region slackAttachmentColorsObj cast test

        [Test]
        public void SingleChannel_ConstructorTest_SlackAttachmentColorsObjCast()
        {

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackAttachmentColorsObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackAttachmentColorsObj: new Dictionary<LogEventLevel, string>()
                        {
                            {LogEventLevel.Verbose, "#DFDFDF"},
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
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackAttachmentColorsObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackAttachmentColorsObj: new Dictionary<LogEventLevel, string>()
                        {
                            {LogEventLevel.Verbose, "#DFDFDF"},
                        }
                    )
                    .CreateLogger();
            });
        }

        #endregion
    }
}
