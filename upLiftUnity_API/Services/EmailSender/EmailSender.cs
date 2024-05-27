using System.Net.Mail;
using System.Net;

namespace upLiftUnity_API.Services.EmailSender
{
    public class EmailSender : IEmailSender
    {



        public Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("upLiftUnity@outlook.com", "ekipaShkaterruese!!20Qershor")
            };

            return client.SendMailAsync(
                new MailMessage(from: "upLiftUnity@outlook.com",
                                to: email,
                                subject,
                                message
                                ));
        }



    }
}
