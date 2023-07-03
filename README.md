# jjm.one.Serilog.Sinks.SlackWebHook

A basic Slack Sink for the Serilog framework.

## Status

|                       |                       |
|----------------------:|-----------------------|
| Build & Test Status (main) | [![Build&Test](https://github.com/jjm-one/jjm.one.Serilog.Sinks.SlackWebHook/actions/workflows/dotnet.yml/badge.svg)](https://github.com/jjm-one/jjm.one.Serilog.Sinks.SlackWebHook/actions/workflows/dotnet.yml) |
| Nuget Package Version | [![Nuget Version](https://img.shields.io/nuget/v/jjm.one.Serilog.Sinks.SlackWebHook?style=flat-square)](https://www.nuget.org/packages/jjm.one.Serilog.Sinks.SlackWebHook/) |
| SonarCloudQuality Gate Status | [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=jjm-one_jjm.one.Serilog.Sinks.SlackWebHook&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=jjm-one_jjm.one.Serilog.Sinks.SlackWebHook) |

## Description

This tool provides a Serilog Sink which sends log messages to one or more channels in a Slack workspace. To send this messages this tool is using the [Slack.Webhooks](https://github.com/mrb0nj/Slack.Webhooks) project to handle the communication with Slack. Therefor you need a valid WebHook URL to use this Sink. To get such a WebHook URL please read this [article](https://slack.com/help/articles/115005265063-Incoming-Webhooks-for-Slack). The log messages will be send in batches to the slack servers. You can setup the batch size to fit your needs.

## Nuget Package

You can get the latest version of this software as a nuget package form [nuget.org](https://www.nuget.org/packages/jjm.one.Serilog.Sinks.SlackWebHook/)

### Installing the Nuget Package

| Tool                 | Command/Code |
|----------------------|--------------|
| Package Manager      | ```PM> Install-Package jjm.one.Serilog.Sinks.SlackWebHook -Version X.Y.Z``` |
| .NET CLI             | ```> dotnet add package jjm.one.Serilog.Sinks.SlackWebHook --version X.Y.Z``` |
| PackageReference     | ```<PackageReference Include="jjm.one.Serilog.Sinks.SlackWebHook" Version="X.Y.Z" />``` |
| Package CLI          | ```> paket add jjm.one.Serilog.Sinks.SlackWebHook --version X.Y.Z``` |
| Script & Interactive | ```> #r "nuget: jjm.one.Serilog.Sinks.SlackWebHook, X.Y.Z"``` |
| Cake as Addin        | ```#addin nuget:?package=jjm.one.Serilog.Sinks.SlackWebHook&version=X.Y.Z``` |
| Cake as Tool         | ```#tool nuget:?package=jjm.one.Serilog.Sinks.SlackWebHook&version=X.Y.Z``` |

## Dependencies

### Serilog related

- [Serilog](https://github.com/serilog/serilog) [Version 2.12.0](https://github.com/serilog/serilog/releases/tag/v2.12.0)
- [Serilog.Sinks.PeriodicBatching](https://github.com/serilog/serilog-sinks-periodicbatching) [Version 3.1.0](https://github.com/serilog/serilog-sinks-periodicbatching/releases/tag/v3.1.0)

### Slack related

- [Slack.Webhooks](https://github.com/mrb0nj/Slack.Webhooks) [Version 1.1.5](https://github.com/mrb0nj/Slack.Webhooks/releases/tag/v1.1.5)

## Usage

a) Absolute basic usage:

```csharp
Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Slack(
                slackWebHookUrl: "https://...",
                slackChannel: null
            )
            .CreateLogger();
```

b) Recommended usage:

```csharp
var logLevelSwitch = new LoggingLevelSwitch(initialMinimumLevel: LogEventLevel.Verbose);
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
                    sinkLevelSwitch:logLevelSwitch
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
                    sinkActivationSwitch: null
                )
                .CreateLogger();
```

## Full Documentation

The full documentation for this package can be found [here](https://jjm-one.github.io/jjm.one.Serilog.Sinks.SlackWebHook/main/doc/html/index.html).

## Repo

The associated repo for this package can be found [here](https://github.com/jjm-one/jjm.one.Serilog.Sinks.SlackWebHook).
