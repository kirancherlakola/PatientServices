using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientServices.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}