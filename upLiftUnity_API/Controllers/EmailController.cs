using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using upLiftUnity_API.Models;
using upLiftUnity_API.Services.EmailSender;

namespace upLiftUnity_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender emailSender;
        private readonly APIDbContext _context;

        public EmailController(IEmailSender emailSender, APIDbContext context)
        {
            this.emailSender = emailSender;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            await emailSender.SendEmailAsync(email, subject, message);
            return Ok();
        }


        [HttpPost]
        [Route("sendEmailToUsers")]
        public async Task<IActionResult> SendEmailToUsers()
        {
            var emails = await _context.Users
                                       .Where(u => u.RoleId == 3)
                                       .Select(u => u.Email)
                                       .ToListAsync();

            if (emails == null || emails.Count == 0)
            {
                return BadRequest("Nuk u gjetën adresa email.");
            }

            var emailSubject = "Hapja e afatit për përzgjedhjen e orarit";
            string currentMonth = DateTime.Now.ToString("MMMM", new CultureInfo("sq-AL")); 

            // Përditëso mesazhin me muajin aktual
            var message = $"Të nderuar përdorues,\n\n"
                     + $"Ju njoftojmë se është hapur afati për përzgjedhjen e orarit dhe do të jetë i hapur deri më datë 30 {currentMonth}.\n\n"
                     + "Ju lutemi që të hyni në sistem dhe të përzgjidhni orarin tuaj për këtë periudhë. "
                     + "Për të bërë këtë, ndiqni hapat e mëposhtëm:\n"
                     + "1. Hyni në llogarinë tuaj në portalin tonë.\n"
                     + "2. Shkoni te seksioni 'Përzgjedhja e orarit'.\n"
                     + "3. Zgjidhni orarin që ju përshtatet më së miri.\n\n"
                     + "Nëse keni ndonjë pyetje ose problem, mos hezitoni të na kontaktoni në adresën tonë të emailit upLiftUnity@outlook.com ose në numrin e telefonit +311 222 345.\n\n"
                     + "Faleminderit për bashkëpunimin!\n\n"
                     + "Gjitha të mirat,\n"
                     + "UpLiftUnity";

            foreach (var email in emails)
            {
                Console.WriteLine($"{email}");
                await emailSender.SendEmailAsync(email, emailSubject, message);
            }

            return Ok("Emailat u dërguan me sukses.");
        }



    }
}
