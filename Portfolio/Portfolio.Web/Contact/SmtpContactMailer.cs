using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Portfolio.Web.Contact
{
    public class SmtpContactMailer : IContactMailer
    {
        private readonly ContactOptions _options;

        public SmtpContactMailer(IOptions<ContactOptions> options)
        {
            _options = options.Value;
        }

        public bool IsAvailable => _options.IsConfigured;

        public async Task SendAsync(ContactMessage message, CancellationToken cancellationToken)
        {
            var subject = string.IsNullOrWhiteSpace(message.ContactSubject)
                ? "New message from the portfolio site"
                : message.ContactSubject;

            var mail = new MimeMessage();
            // The relay only sends from a verified sender; the visitor goes in Reply-To.
            mail.From.Add(new MailboxAddress(_options.FromName, _options.From));
            mail.To.Add(MailboxAddress.Parse(_options.To));
            mail.ReplyTo.Add(new MailboxAddress(message.ContactName, message.ContactEmail));
            mail.Subject = $"[Portfolio] {subject}";
            mail.Body = new TextPart("plain")
            {
                Text = $"From: {message.ContactName} <{message.ContactEmail}>\n\n{message.ContactMessageText}"
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(_options.SmtpHost, _options.SmtpPort, SecureSocketOptions.StartTls, cancellationToken);
            await client.AuthenticateAsync(_options.SmtpUser, _options.SmtpPassword, cancellationToken);
            await client.SendAsync(mail, cancellationToken);
            await client.DisconnectAsync(true, cancellationToken);
        }
    }
}
