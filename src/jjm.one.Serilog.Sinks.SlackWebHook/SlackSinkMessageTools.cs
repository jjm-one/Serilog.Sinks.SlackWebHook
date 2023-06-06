using Serilog.Events;
using Serilog.Formatting.Display;
using Slack.Webhooks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace jjm.one.Serilog.Sinks.SlackWebHook
{
    /// <summary>
    /// Class with default tools for message generation.
    /// </summary>
    public static class SlackSinkMessageTools
    {

        /// <summary>
        /// DEFAULT GenerateSlackMessageText function.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <param name="formatProvider">A format provider</param>
        /// <param name="options">Sink Options as SlackSinkOptions.</param>
        /// <returns>The log message string.</returns>
        public static string GenerateSlackMessageText(LogEvent logEvent, IFormatProvider formatProvider, object options)
        {
            // input check
            var slackSinkOptions = (SlackSinkOptions)options ?? throw new InvalidCastException();

            // generate the log message text
            var textFormatter = new MessageTemplateTextFormatter(slackSinkOptions.SinkOutputTemplate, formatProvider);
            var stringWriter = new StringWriter();
            textFormatter.Format(logEvent, stringWriter);

            return stringWriter.ToString();
        }

        /// <summary>
        /// DEFAULT GenerateSlackMessageAttachments function.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <param name="formatProvider">A format provider</param>
        /// <param name="options">Sink Options as SlackSinkOptions.</param>
        /// <returns>The log message attachment list.</returns>
        public static List<SlackAttachment> GenerateSlackMessageAttachments(LogEvent logEvent, IFormatProvider formatProvider, object options)
        {
            // input check
            var slackSinkOptions = (SlackSinkOptions)options ?? throw new InvalidCastException();

            var attachments = new List<SlackAttachment>(2);

            #region information attachment

            // Check if the short info attachment should be added
            if (slackSinkOptions.SlackAddShortInfoAttachment && !slackSinkOptions.SlackAddExtendedInfoAttachment)
            {
                // create the attachment
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
            // Check if the extended info attachment should be added
            else if (slackSinkOptions.SlackAddExtendedInfoAttachment)
            {
                var infoFields = new List<SlackField>();
                var stringWriter = new StringWriter();

                // collect all log event information
                foreach (var property in logEvent.Properties)
                {
                    property.Value.Render(stringWriter, formatProvider: formatProvider);
                    var field = new SlackField
                    {
                        Short = slackSinkOptions.SlackDisplayExtendedInfoAttachmentShort,
                        Title = property.Key,
                        Value = stringWriter.ToString()
                    };
                    infoFields.Add(field);
                    stringWriter.GetStringBuilder().Clear();
                }

                // create the attachment
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

            #endregion

            #region exception attachment

            // Check if the exception attachment should be added & if an exception is provided with to log event
            if (slackSinkOptions.SlackAddExceptionAttachment && logEvent.Exception != null)
            {
                // create the attachment
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

            #endregion

            return attachments.Any() ? attachments : null;
        }

        /// <summary>
        /// DEFAULT GenerateSlackMessageBlocks function.
        /// </summary>
        /// <param name="logEvent">The log event.</param>
        /// <param name="formatProvider">A format provider</param>
        /// <param name="options">Sink Options as SlackSinkOptions.</param>
        /// <returns>The log message block list.</returns>
        public static List<Block> GenerateSlackMessageBlocks(LogEvent logEvent, IFormatProvider formatProvider, object options)
        {
            return null;
        }
    }
}