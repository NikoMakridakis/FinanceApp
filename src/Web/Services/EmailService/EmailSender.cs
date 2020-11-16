using MailKit.Net.Smtp;
using MimeKit;
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
                HtmlBody = string.Format($"" +
                "<div style = 'background-color:#F6F9FC; font-family:Roboto, Helvetica, Arial; padding-top:30px; padding-bottom:30px'>" +
                    "<div style = 'background-color:#ffffff; color:#525f7f; font-size: 16px; max-width: 600px; margin: 0 auto; padding: 20px 60px 20px 60px;'>" +
                        "<h1>" +
                            "<a href='https://localhost:44387/' target='_blank' rel='noopener noreferrer' style='text-decoration:none; color:#1976d2;'>" +
                                "StarBudget" +
                            "</a>" +
                        "</h1>" +
                        "<hr style='height:1px; border:none; color:#525f7f; background-color:#525f7f; opacity:0.3;' />" +
                        "<p style='font-size:16px; font-weight:400px;'>" +
                            "Hello, <br /><br />" +
                            "We received a request to reset the password for the Stripe account associated with" +
                        $"</p>{emailMessage.To}" + "<p>.</p>" +
                        "<div style='text-align:center; margin-top:30px; margin-bottom:30px;'>" +
                            $"<a href='{message.Content}' style='background-color:#1975d2; border-radius:8px; display:inline-block; cursor:pointer; color:#f6f9fc; font-size:16px; font-weight:bold; padding:13px 40px; text-decoration:none;'>" +
                                "Reset your password" +
                            "</a>" +
                        "</div>" +
                        "<p style='font-size:16px; font-weight:400px;'>" +
                            "If you didn’t request to reset your password, let us know by replying directly to this email. No changes were made to your account yet." +
                            "<br /><br />" +
                            "Thanks," +
                            "<br /><br />" +
                            "StarBudget" +
                        "</p>" +
                        "<hr style='height:1px; border:none; color:#525f7f; background-color:#525f7f; opacity:0.3;' />" +
                        "<p style='font-size:12px; color:#8898aa'>" +
                            "Copyright © StarBudget" +
                        "</p>" +
                    "</div>" +
                "</div>" +
                $"") };

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
