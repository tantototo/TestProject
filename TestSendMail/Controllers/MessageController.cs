using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestSendMail.Dtos;
using TestSendMail.Models;
using TestSendMail.Services;

namespace TestSendMail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageServices _messageServices;
        private readonly IMailServices _mailServices;

        public MessageController(IMessageServices messageServices, IMailServices mailServices)
        {
            _messageServices = messageServices;
            _mailServices = mailServices;
        }

        [HttpPost("SendMessageToEmail")]
        public async Task<IActionResult> SendMessageToEmail([FromForm] MailRequestDto dto)
        {
            MailRequest mailRequest = new()
            {
                ToEmail = dto.ToEmail,
                Subject = dto.Subject,
                Attachments = dto.Attachments
            };

            try
            {
                await _messageServices.GetMessageAsync(mailRequest, _mailServices);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
