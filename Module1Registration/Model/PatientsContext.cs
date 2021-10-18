using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module1Registration.Model
{
    public class PatientsContext : DbContext
    {
        public PatientsContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
    }
}
