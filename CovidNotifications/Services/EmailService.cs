using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace CovidNotifications.Services
{
    public class EmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderMail;
        public EmailService(IConfiguration configuration)
        {
            var password = configuration.GetValue<string>("EmailInfo:Password");
            _senderMail = configuration.GetValue<string>("EmailInfo:Email");
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_senderMail,password),
                EnableSsl = true,
            };
        }

        public void SendNewPatientEmail(string email)
        {
            _smtpClient.Send(_senderMail, email, "Wynikt testu na covid", "Informacja o kwarantannie");
        }
    }
}