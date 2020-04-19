using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serilog.Events;
using Serilog.Formatting.Display;
using Slack.Webhooks;

namespace Serilog.Sinks.Slack
{
    public static class SlackSinkMessageTools
    {

        public static string GenerateSlackMessageText(LogEvent logEvent, IFormatProvider formatProvider, object options)
        {
            var slackSinkOptions = (SlackSinkOptions)options ?? throw new InvalidCastException();
            var textFormatter = new MessageTemplateTextFormatter(slackSinkOptions.SinkOutputTemplate, formatProvider);
            var stringWriter = new StringWriter();
            textFormatter.Format(logEvent, stringWriter);
            return stringWriter.ToString();
        }

        public static List<SlackAttachment> GenerateSlackMessageAttachments(LogEvent logEvent, IFormatProvider formatProvider, object options)
        {
            var slackSinkOptions = (SlackSinkOptions)options ?? throw new InvalidCastException();
            var attachments = new List<SlackAttachment>(2);

            if (slackSinkOptions.SlackAddShortInfoAttachment && !slackSinkOptions.SlackAddExtendedInfoAttachment)
            {
                var shotInfoAttach = new SlackAttachment
                {
                    Actions = null,
                    AuthorIcon = null,
                    AuthorLink = null,
                    AuthorName = null,
                    CallbackId = null,
                    Color = slackSinkOptions.SlackAttachmentColors[logEvent.Level],
                    Fallback = $"{logEvent.Timestamp.ToString(formatProvider)} [{logEvent.Level}] - {logEvent.RenderMessage()}",
                    Fields = new List<SlackField>
                    {
                        new SlackField
                        {
                            Short = slackSinkOptions.SlackDisplayShortInfoAttachmentShort,
                            Title = "Level",
                            Value = logEvent.Level.ToString()
                        },
                        new SlackField
                        {
                            Short = slackSinkOptions.SlackDisplayShortInfoAttachmentShort,
                            Title = "Timestamp",
                            Value = logEvent.Timestamp.ToString(formatProvider)
                        }
                    },
                    Footer = null,
                    FooterIcon = slackSinkOptions.SlackAttachmentFooterIcon[logEvent.Level],
                    ImageUrl = null,
                    MarkdownIn = null,
                    Pretext = null,
                    Text = null,
                    ThumbUrl = null,
                    Timestamp = 0,
                    Title = "Details",
                    TitleLink = null
                };

                attachments.Add(shotInfoAttach);
            }
            else if (slackSinkOptions.SlackAddExtendedInfoAttachment)
            {
                var infoFields = new List<SlackField>();
                var stringWriter = new StringWriter();

                foreach (var logEventProperty in logEvent.Properties)
                {
                    logEventProperty.Value.Render(stringWriter, formatProvider: formatProvider);
                    var field = new SlackField
                    {
                        Short = slackSinkOptions.SlackDisplayExtendedInfoAttachmentShort,
                        Title = logEventProperty.Key,
                        Value = stringWriter.ToString()
                    };
                    infoFields.Add(field);
                    stringWriter.GetStringBuilder().Clear();
                }

                var extInfoAttach = new SlackAttachment
                {
                    Actions = null,
                    AuthorIcon = null,
                    AuthorLink = null,
                    AuthorName = null,
                    CallbackId = null,
                    Color = slackSinkOptions.SlackAttachmentColors[logEvent.Level],
                    Fallback = $"{logEvent.Timestamp.ToString(formatProvider)} [{logEvent.Level}] - {logEvent.RenderMessage()}",
                    Fields = infoFields,
                    Footer = null,
                    FooterIcon = slackSinkOptions.SlackAttachmentFooterIcon[logEvent.Level],
                    ImageUrl = null,
                    MarkdownIn = null,
                    Pretext = null,
                    Text = null,
                    ThumbUrl = null,
                    Timestamp = 0,
                    Title = "Details",
                    TitleLink = null
                };

                attachments.Add(extInfoAttach);
            }

            if (slackSinkOptions.SlackAddExceptionAttachment && logEvent.Exception != null)
            {
                var excAttach = new SlackAttachment
                {
                    Actions = null,
                    AuthorIcon = null,
                    AuthorLink = null,
                    AuthorName = null,
                    CallbackId = null,
                    Color = slackSinkOptions.SlackAttachmentColors[logEvent.Level],
                    Fallback = $"{logEvent.Timestamp.ToString(formatProvider)} Exception: {logEvent.Exception.Message} \n {logEvent.Exception.StackTrace}",
                    Fields = new List<SlackField>
                    {
                        new SlackField
                        {
                            Short = slackSinkOptions.SlackDisplayExceptionAttachmentShort,
                            Title = "Message",
                            Value = logEvent.Exception.Message
                        },
                        new SlackField
                        {
                            Short = slackSinkOptions.SlackDisplayExceptionAttachmentShort,
                            Title = "Type",
                            Value = $"`{logEvent.Exception.GetType().Name}`"
                        },
                        new SlackField
                        {
                            Short =  false,
                            Title = "Stack Trace",
                            Value = $"```{logEvent.Exception.StackTrace}```"
                        },
                        new SlackField
                        {
                            Short = false,
                            Title = "Exception",
                            Value = $"```{logEvent.Exception}```"
                        }
                    },
                    Footer = null,
                    FooterIcon = slackSinkOptions.SlackAttachmentFooterIcon[logEvent.Level],
                    ImageUrl = null,
                    MarkdownIn = new List<string>
                    {
                        "fields"
                    },
                    Pretext = null,
                    Text = null,
                    ThumbUrl = null,
                    Timestamp = 0,
                    Title = "Exception",
                    TitleLink = null
                };

                attachments.Add(excAttach);
            }

            return attachments.Any() ? attachments : null;
        }

        public static List<Block> GenerateSlackMessageBlocks(LogEvent logEvent, IFormatProvider formatProvider, object options)
        {
            //var slackSinkOptions = (SlackSinkOptions)options ?? throw new InvalidCastException();
            return null;
        }
    }
}
