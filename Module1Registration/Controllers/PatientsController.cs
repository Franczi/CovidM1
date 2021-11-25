using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module1Registration.Model;
using Module1Registration.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Module1Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly PatientsContext _context;
        private readonly MessagesService _service;

        public PatientsController(PatientsContext context, MessagesService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK,Type=typeof(List<Patient>))]
        public IActionResult GetAllPatients()
        {
            return Ok(_context.Patients.ToList());
        }

        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, Type = typeof(Patient))]
        public async Task<IActionResult> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            await _service.sendMessage(new MessagesService.MessagePayload { EventName = "PatientCreated", PatientEmail = patient.Email });
            
            return Created($"api/patients/{patient.Id}",patient);
        }
    }
}
