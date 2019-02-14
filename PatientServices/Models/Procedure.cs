using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientServices.Models
{
    public class Procedure
    {
        public int ProcedureID { get; set; }
        public int PatientID { get; set; }
        public string SurgeryDate { get; set; }
        public DateTime SurgeryTime { get; set; }
        public string SurgeonName { get; set; }
        public string ProcedureName { get; set; }
    }
}