using TestSendMail.Models;

namespace TestSendMail.Services
{
    public interface IMailServices
    {
        Task SendMailAsync(MailRequest mailRequest);
    }
} 