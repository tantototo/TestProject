using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
using TestSendMail.Models;
using TestSendMail.Settings;
using Microsoft.Extensions.Options;
using MailKit.Security;

namespace TestSendMail.Services
{
    public class MailServices : IMailServices
    {
        private readonly MailSettings _mailSettings;

        public MailServices(IOptions<MailSettings> options)
        {
            _mailSettings = options.Value;
        }

        public async Task SendMailAsync(MailRequest mailRequest)
        {
            var message = new MimeMessage();
            message.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            message.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            
            message.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if(mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var attachment in mailRequest.Attachments)
                {
                    if (attachment.Length > 0)
                    {
                        using(var stream = new MemoryStream())
                        {
                            attachment.CopyTo(stream);
                            fileBytes = stream.ToArray();
                        }
                        builder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            message.Body = builder.ToMessageBody();
            
            using var client = new SmtpClient();
            client.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            client.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await client.SendAsync(message);
            client.Disconnect(true);
            client.Dispose();
        }
    }
}
