using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _emailConfiguration;

        public EmailSender(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfiguration.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder {
                HtmlBody = string.Format(
                    "<div style='background-color: #f6f9fc;'>" +
                        "<div style='background-color: #ffffff; color: #525f7f; font-family: Roboto, Helvetica, Arial; font-size: 16px; margin: 25px auto; max-width: 600px;'>" +
                            "<h1 style='padding: 20px 40px; margin: 0px;'>" +
                                "<a href = 'https://localhost:44387/' target = '_blank' rel = 'noopener noreferrer' style='color: #1976d2; text-decoration: none;'>" +
                                    "StarBudget" +
                                "</a>" +
                            "</h1>" +
                            "<hr style='background-color: #525f7f; border: none; color: #525f7f; height: 1px; margin: 0px 40px; opacity: 0.15;'/>" +
                            "<div style='padding: 20px 40px;'>" +
                                "<p style='font-size: 16px; font-weight: 400px; margin: 0px; padding-bottom: 20px;'>" +
                                    "Hello," +
                                "</p>" +
                                "<p style='display: inline; font-size: 16px; font-weight: 400px;'>" +
                                    "We received a request to reset the password for the StarBudget account associated with " +
                                "</p>" +
                                $"{emailMessage.To}" +
                                "<p style='display: inline; font-size: 16px; font-weight: 400px;'>.</ p >" +
                            "</div>" +
                            "<div style='padding: 20px 40px; text-align: center;'>" +
                                $"<a href='{message.Content}' style='background-color: #1975d2; border-radius: 8px; color: #f6f9fc; cursor: pointer; display: inline-block; font-size: 16px; font-weight: bold; padding: 13px 40px; text-decoration: none;'>" +
                                    "Reset your password" +
                                "</a>" +
                            "</div>" +
                            "<div style='padding: 20px 40px;'>" +
                            "<p style='font-size: 16px; font-weight: 400px; margin: 0px; padding-bottom: 20px;'>" +
                                "If you didn’t request to reset your password, let us know by replying directly to this email. No changes were made to your account yet." +
                            "</p>" +
                            "<p style='font-size: 16px; font-weight: 400px; margin: 0px; padding-bottom: 20px;'>" +
                                "Thanks," +
                            "</p>" +
                            "<p style='font-size: 16px; font-weight: 400px; margin: 0px;'>" +
                                "StarBudget" +
                            "</p>" +
                        "</div>" +
                        "<hr style='background-color: #525f7f; border: none; color: #525f7f; height: 1px; margin: 0px 40px; opacity: 0.15;'/>" +
                        "<p style='color: #8898aa; font-size: 12px; margin: 0px; padding: 20px 40px;'>" +
                            "Copyright © StarBudget " + $"{DateTime.Now.Year}" +
                        "</p>" +
                    "</div>" +
                "</div>"
                ) };

            emailMessage.Body = bodyBuilder.ToMessageBody();
            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfiguration.UserName, _emailConfiguration.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfiguration.UserName, _emailConfiguration.Password);

                    await client.SendAsync(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception, or both.
                    throw;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }
        }
    }
}
