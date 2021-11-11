using CovidNotifications.Services;
using Microsoft.AspNetCore.Mvc;

namespace CovidNotifications.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailService _service;

        public EmailController(EmailService service)
        {
            _service = service;
        }

        [HttpPost]
        public void SendEmail(string email)
        {
            _service.SendNewPatientEmail(email);
        }
    }
}
