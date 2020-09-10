using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MimeKit;
using MailKit.Net.Smtp;
using System.Linq;
using System.Net;
using System;

namespace csharp_test_hopper.Util
{
    /// <summary>
    /// Service for building and sending email. Uses MimeKit/MailKit
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// SMTP configuration object
        /// </summary>
        private readonly EmailConfig config;

        public EmailService(IOptions<EmailConfig> _config)
        {
            this.config = _config.Value;
        }

        /// <summary>
        /// Builds the email and attempts to asynchronously send via SMTP Client
        /// </summary>
        /// <param name="subject">email subject</param>
        /// <param name="body">html/text email body</param>
        /// <param name="recipients">array of recipients</param>
        /// <param name="mailFrom">email of the sender</param>
        public async Task SendEmailAsync(
            string subject,
            string body,
            string[] recipients,
            string mailFrom)
        {
            var recipientAdressess = recipients.Select(r => new MailboxAddress(string.Empty, r));

            try
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(config.FromName, mailFrom));
                emailMessage.To.AddRange(recipientAdressess);
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(
                        config.MailServerAddress,
                        Convert.ToInt32(config.MailServerPort),
                        MailKit.Security.SecureSocketOptions.StartTls).ConfigureAwait(false);

                    await client.AuthenticateAsync(new NetworkCredential(config.UserName, config.UserPassword));
                    await client.SendAsync(emailMessage).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
