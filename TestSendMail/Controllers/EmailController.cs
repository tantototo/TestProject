using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestSendMail.Models;
using TestSendMail.Services;

namespace TestSendMail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMailServices _mailServices;

        public EmailController(IMailServices mailServices)
        {
            _mailServices = mailServices;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromForm] MailRequest mailRequest)
        {
            try
            {
                await _mailServices.SendMailAsync(mailRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
