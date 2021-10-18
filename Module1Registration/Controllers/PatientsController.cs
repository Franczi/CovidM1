using Microsoft.AspNetCore.Mvc;
using Module1Registration.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Module1Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly PatientsContext _context;

        public PatientsController(PatientsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllPatients()
        {
            return Ok(_context.Patients.ToList());
        }

        [HttpPost]
        public IActionResult CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return Created($"api/patients/{patient.Id}",patient);
        }
    }
}
