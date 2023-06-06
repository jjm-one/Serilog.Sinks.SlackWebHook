using NUnit.Framework;
using Serilog;
using Serilog.Events;
using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace jjm.one.Serilog.Sinks.SlackWebHook.Tests
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
                        slackChannel: null,
                        slackAttachmentColorsObj: new Dictionary<LogEventLevel, string>()
                        {
                            {LogEventLevel.Verbose, ""},
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
                        slackChannels: null,
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
                            {LogEventLevel.Verbose, ""},
                        }
                    )
                    .CreateLogger();
            });
        }

        #endregion

        #region slackAttachmentFooterIconObj cast test

        [Test]
        public void SingleChannel_ConstructorTest_SlackAttachmentFooterIconObjCast()
        {

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackAttachmentFooterIconObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackAttachmentFooterIconObj: new Dictionary<LogEventLevel, string>()
                        {
                            {LogEventLevel.Verbose, ""},
                        }
                    )
                    .CreateLogger();
            });
        }

        [Test]
        public void MultiChannel_ConstructorTest_SlackAttachmentFooterIconObjCast()
        {

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackAttachmentFooterIconObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackAttachmentFooterIconObj: new Dictionary<LogEventLevel, string>()
                        {
                            {LogEventLevel.Verbose, ""},
                        }
                    )
                    .CreateLogger();
            });
        }

        #endregion

        #region slackHttpClientObj cast test

        [Test]
        public void SingleChannel_ConstructorTest_SlackHttpClientObjCast()
        {

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackHttpClientObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        slackHttpClientObj: new HttpClient()
                    )
                    .CreateLogger();
            });
        }

        [Test]
        public void MultiChannel_ConstructorTest_SlackHttpClientObjCast()
        {

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackHttpClientObj: "TestObject"
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        slackHttpClientObj: new HttpClient()
                    )
                    .CreateLogger();
            });
        }

        #endregion

        #region  generateSlackFunctions cast test

        private static string TestF1(LogEvent a, IFormatProvider b, object c)
        {
            return null;
        }
        private static List<SlackAttachment> TestF2(LogEvent a, IFormatProvider b, object c)
        {
            return null;
        }
        private static List<Block> TestF3(LogEvent a, IFormatProvider b, object c)
        {
            return null;
        }

        [Test]
        public void SingleChannel_ConstructorTest_GenerateSlackFunctionsCast()
        {
            Func<LogEvent, IFormatProvider, object, string> f1 = TestF1;
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> f2 = TestF2;
            Func<LogEvent, IFormatProvider, object, List<Block>> f3 = TestF3;

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        generateSlackFunctions: new Tuple<object, object, object>("", "", "")
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannel: null,
                        generateSlackFunctions: new Tuple<object, object, object>(f1, f2, f3)
                    )
                    .CreateLogger();
            });
        }

        [Test]
        public void MultiChannel_ConstructorTest_GenerateSlackFunctionsCast()
        {
            Func<LogEvent, IFormatProvider, object, string> f1 = TestF1;
            Func<LogEvent, IFormatProvider, object, List<SlackAttachment>> f2 = TestF2;
            Func<LogEvent, IFormatProvider, object, List<Block>> f3 = TestF3;

            Assert.Throws<InvalidCastException>(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        generateSlackFunctions: new Tuple<object, object, object>("", "", "")
                    )
                    .CreateLogger();
            });

            Assert.DoesNotThrow(() =>
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Slack(
                        slackWebHookUrl: ValidWebHook,
                        slackChannels: null,
                        generateSlackFunctions: new Tuple<object, object, object>(f1, f2, f3)
                    )
                    .CreateLogger();
            });
        }

        #endregion
    }
}
