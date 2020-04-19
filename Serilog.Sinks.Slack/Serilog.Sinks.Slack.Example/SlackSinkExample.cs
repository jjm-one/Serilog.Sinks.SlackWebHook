using System;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Sinks.Slack.Example
{
    public class SlackSinkExample
    {
        public static void Main(string[] args)
        {
            string slackChannel = null;
            var slackUsername = "Serilog Slack Sink Example";
            var slackUserIcon = ":monkey_face:";

            Console.WriteLine("####################################################################################################");
            Console.WriteLine("Serilog Slack Sink Example (ver. " + typeof(SlackSinkExample).Assembly.GetName().Version + ")");
            Console.WriteLine("####################################################################################################");
            Console.WriteLine();

            Console.Write("Slack WebHook Url: ");
            var input = Console.ReadLine();
            string slackWebHookUrl;
            if (string.IsNullOrEmpty(input)) throw new ArgumentException("No valid WebHook Url!");
            else slackWebHookUrl = input;
            Console.WriteLine();

            Console.Write("Slack Channel (optional): ");
            input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input)) slackChannel = input;
            Console.WriteLine();

            Console.Write("Slack Username (optional): ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) slackUsername = input;
            Console.WriteLine();

            Console.Write("Slack User Icon (optional): ");
            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) slackUserIcon = input;
            Console.WriteLine();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Slack(
                            slackWebHookUrl: slackWebHookUrl,
                            slackChannel: slackChannel,
                            slackUsername: slackUsername,
                            slackEmojiIcon: slackUserIcon,

                            periodicBatchingSinkOptionsBatchSizeLimit: 1,
                            periodicBatchingSinkOptionsPeriod: TimeSpan.FromMilliseconds(10),
                            periodicBatchingSinkOptionsQueueLimit: 100,

                            sinkRestrictedToMinimumLevel: LogEventLevel.Verbose,
                            sinkOutputTemplate: "{message}",
                            sinkLevelSwitch:new LoggingLevelSwitch(initialMinimumLevel: LogEventLevel.Verbose)
                )
                .CreateLogger();

            Log.Write(LogEventLevel.Verbose, "Verbose Logging Message");
            Log.Write(LogEventLevel.Debug, "Debug Logging Message");
            Log.Write(LogEventLevel.Information, "Information Logging Message");
            Log.Write(LogEventLevel.Warning, "Warning Logging Message");
            Log.Write(LogEventLevel.Error, "Error Logging Message");
            Log.Write(LogEventLevel.Fatal, "Fatal Logging Message");

            try
            {
                throw new Exception("TEST EXCEPTION!");
            }
            catch (Exception e)
            {
                Log.Write(LogEventLevel.Fatal, e, "Fatal Logging Message with exception");
            }

            System.Threading.Thread.Sleep(10000);

            Console.WriteLine("Press any key to finish...");
            Console.ReadKey();

            Environment.Exit(0);
        }
    }
}
