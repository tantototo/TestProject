using TestSendMail.Models;

namespace TestSendMail.Services
{
    public interface IMessageServices
    {
        Task GetMessageAsync(MailRequest mailRequest, IMailServices mailServices);
        Task PublishMessage(string message);
    }
}