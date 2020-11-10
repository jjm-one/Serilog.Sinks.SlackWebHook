# serilog-sinks-slackwebhook

A basic Slack Sink for the Serilog framework.

## Status

|                       |                       |
|----------------------:|-----------------------|
| Nuget Package Version | [![Nuget Version](https://img.shields.io/nuget/v/Serilog.Sinks.SlackWebHook?style=flat-square)](https://www.nuget.org/packages/Serilog.Sinks.SlackWebHook/) |
| nuget.org Deployment  | [![nuget.org Deployment](https://img.shields.io/azure-devops/release/jonas-merkle/09454b63-c969-4591-aa24-ea8867d031bd/1/2?style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_release?view=all&_a=releases&definitionId=1) |
| Build Status Master   | [![Build Status Master](https://img.shields.io/azure-devops/build/jonas-merkle/serilog-sinks-slackwebhook/3/master?style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_build/latest?definitionId=3) |
| Build Status Develop  | [![Build status Develop](https://img.shields.io/azure-devops/build/jonas-merkle/serilog-sinks-slackwebhook/4/develop?style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_build/latest?definitionId=4) |
| Test Status Master    | [![Test Status Master](https://img.shields.io/azure-devops/tests/jonas-merkle/serilog-sinks-slackwebhook/3?compact_message&style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_test/analytics?definitionId=3&contextType=build) |
| Test Status Develop   | [![Test Status Master](https://img.shields.io/azure-devops/tests/jonas-merkle/serilog-sinks-slackwebhook/4?compact_message&style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_test/analytics?definitionId=4&contextType=build) |
| Sonar Code Quality    | [![Sonar Code Quality](https://img.shields.io/sonar/quality_gate/jonas-merkle_serilog-sinks-slackwebhook?server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=jonas-merkle_serilog-sinks-slackwebhook) |
| Maintenance Status    | ![Sonar Code Quality](https://img.shields.io/maintenance/yes/2020?style=flat-square) |

## Description

This tool provides a Serilog Sink which sends log messages to one or more channels in a Slack workspace. To send this messages this tool is using the [Slack.Webhooks](https://github.com/mrb0nj/Slack.Webhooks) project to handle the communication with Slack. Therefor you need a valid WebHook URL to use this Sink. To get such a WebHook URL please read this [article](https://slack.com/help/articles/115005265063-Incoming-Webhooks-for-Slack). The log messages will be send in batches to the slack servers. You can setup the batch size to fit your needs.

## Nuget Package

You can get the latest version of this software as a nuget package form [nuget.org](https://www.nuget.org/packages/Serilog.Sinks.SlackWebHook/)

### Installing the Nuget Package

| Tool             | Command/Code |
|------------------|--------------|
|Package Manager   | ```PM> Install-Package Serilog.Sinks.SlackWebHook -Version X.Y.Z``` |
|.NET CLI          | ```> dotnet add package Serilog.Sinks.SlackWebHook --version X.Y.Z``` |
| PackageReference | ```<PackageReference Include="Serilog.Sinks.SlackWebHook" Version="X.Y.Z" />``` |
| Package CLI      | ```> paket add Serilog.Sinks.SlackWebHook --version X.Y.Z``` |

## Dependencies

### Serilog related

- [Serilog](https://github.com/serilog/serilog) [Version 2.10.0](https://github.com/serilog/serilog/releases/tag/v2.10.0)
- [Serilog.Sinks.PeriodicBatching](https://github.com/serilog/serilog-sinks-periodicbatching) [Version 2.3.0](https://github.com/serilog/serilog-sinks-periodicbatching/releases/tag/v2.3.0)

### Slack related

- [Slack.Webhooks](https://github.com/mrb0nj/Slack.Webhooks) [Version 1.1.4](https://github.com/mrb0nj/Slack.Webhooks/releases/tag/v1.1.4)

## Usage

a) Absolute basic usage:

```csharp
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Slack(
                slackWebHookUrl: "https://...",
                slackChannel: null
            .CreateLogger();
```

b) Recommended usage:

```csharp
var logLevelSwitch = new LoggingLevelSwitch(initialMinimumLevel: LogEventLevel.Verbos);
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Slack(
                    slackWebHookUrl: "https://...",
                    slackChannel: "log-output-channel",
                    slackUsername: "Serilog Slack Sink Bot",
                    slackEmojiIcon: ":monkey_face:",

                    periodicBatchingSinkOptionsBatchSizeLimit: 1,
                    periodicBatchingSinkOptionsPeriod: TimeSpan.FromMilliseconds(1000),
                    periodicBatchingSinkOptionsQueueLimit: 10000,

                    sinkRestrictedToMinimumLevel: LogEventLevel.Verbose,
                    sinkLevelSwitch:logLevelSwitch)
                )
                .CreateLogger();
```

c) Advanced usage (all available option exposed):

```csharp
Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Slack(
                    slackWebHookUrl: "https://...",
                    slackUsername: null,
                    slackEmojiIcon: null,
                    slackUriIcon: null,
                    slackChannels: null,
                    slackDeleteOriginal: null,
                    slackLinkNames: null,
                    slackMarkdown: null,
                    slackParseObj: null,
                    slackReplaceOriginal: null,
                    slackResponseType: null,
                    slackThreadId: null,
                    slackAttachmentColorsObj: null,
                    slackAttachmentFooterIconObj: null,
                    slackAddShortInfoAttachment: null,
                    slackDisplayShortInfoAttachmentShort: null,
                    slackAddExtendedInfoAttachment: null,
                    slackDisplayExtendedInfoAttachmentShort: null,
                    slackAddExceptionAttachment: null,
                    slackDisplayExceptionAttachmentShort: null,
                    slackConnectionTimeout: null,
                    slackHttpClientObj: null,
                    generateSlackFunctions: null,
                    periodicBatchingSinkOptionsBatchSizeLimit: null,
                    periodicBatchingSinkOptionsPeriod: null,
                    periodicBatchingSinkOptionsQueueLimit: null,
                    sinkRestrictedToMinimumLevel: null,
                    sinkOutputTemplate: null,
                    sinkLevelSwitch: null,
                    sinkFormatProvider: null,
                    statusSwitch: null
                )
                .CreateLogger();
```
