using TestProjectWebApi.Models;

namespace TestProjectWebApi.Services
{
    public interface IMailServices
    {
        Task SendMailAsync(MailRequest mailRequest);
    }
} 