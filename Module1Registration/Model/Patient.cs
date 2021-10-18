using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module1Registration.Model
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
        public DateTime TestDate { get; set; }
    }
}
