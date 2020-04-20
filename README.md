# serilog-sinks-slackwebhook
A basic Slack Sink for the Serilog framwork.

## Status
|                       | |
|----------------------:|-|
| Nuget Package Version | [![Nuget Version](https://img.shields.io/nuget/v/Serilog.Sinks.SlackWebHook?style=flat-square)](https://www.nuget.org/packages/Serilog.Sinks.SlackWebHook/) |
| nuget.org Deployment  | [![nuget.org Deployment](https://img.shields.io/azure-devops/release/jonas-merkle/09454b63-c969-4591-aa24-ea8867d031bd/1/2?style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_release?view=all&_a=releases&definitionId=1)
| Build Status Master   | [![Build Status Master](https://img.shields.io/azure-devops/build/jonas-merkle/serilog-sinks-slackwebhook/3/master?style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_build/latest?definitionId=3) |
| Build Status Develop  | [![Build status Develop](https://img.shields.io/azure-devops/build/jonas-merkle/serilog-sinks-slackwebhook/4/develop?style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_build/latest?definitionId=4) |
| Test Status Master    | [![Test Status Master](https://img.shields.io/azure-devops/tests/jonas-merkle/serilog-sinks-slackwebhook/3?compact_message&style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_test/analytics?definitionId=3&contextType=build) |
| Test Status Develop   | [![Test Status Master](https://img.shields.io/azure-devops/tests/jonas-merkle/serilog-sinks-slackwebhook/4?compact_message&style=flat-square)](https://dev.azure.com/jonas-merkle/serilog-sinks-slackwebhook/_test/analytics?definitionId=4&contextType=build) |
| Sonar Code Quality    | [![Sonar Code Quality](https://img.shields.io/sonar/quality_gate/jonas-merkle_serilog-sinks-slackwebhook?server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=jonas-merkle_serilog-sinks-slackwebhook) |
| Maintenance Status    | [![Sonar Code Quality](https://img.shields.io/maintenance/yes/2020?style=flat-square)]() |

## Nuget Package
You can get the latest version of this software as a nuget package form [nuget.org](https://www.nuget.org/packages/Serilog.Sinks.SlackWebHook/)

### Installing the Nuget Package
| Tool | Command/Code |
|------|--------------|
|Package Manager | ```PM> Install-Package Serilog.Sinks.SlackWebHook -Version X.Y.Z``` |
|.NET CLI | ```> dotnet add package Serilog.Sinks.SlackWebHook --version X.Y.Z```|
| PackageReference | ```<PackageReference Include="Serilog.Sinks.SlackWebHook" Version="X.Y.Z" />```|
| Package CLI | ```> paket add Serilog.Sinks.SlackWebHook --version X.Y.Z```|

## Dependencies

### Serilog related
- [Serilog](https://github.com/serilog/serilog) [Version 2.9.0](https://github.com/serilog/serilog/releases/tag/v2.9.0)
- [Serilog.Sinks.PeriodicBatching](https://github.com/serilog/serilog-sinks-periodicbatching) [Version 2.9.0](https://github.com/serilog/serilog-sinks-periodicbatching/releases/tag/v2.3.0)

### Slack related
- [Slack.Webhooks](https://github.com/mrb0nj/Slack.Webhooks) [Version 1.1.3](https://github.com/mrb0nj/Slack.Webhooks/releases/tag/v1.1.3)

## Usage