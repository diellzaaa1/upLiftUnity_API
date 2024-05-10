using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upLiftUnity_API.Services.EmailSender;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender emailSender;

        public EmailController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> sendEmail(string email, string subject, string message)
        {
            await emailSender.SendEmailAsync(email, subject, message);
            return Ok();
        }
    }
    }
