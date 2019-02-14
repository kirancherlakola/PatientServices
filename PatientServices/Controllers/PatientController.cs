using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PatientServices.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;


namespace PatientServices.Controllers
{
    public class PatientController : ApiController
    {
        IDbConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
        [Route("Patient")]
        [HttpGet]
        public dynamic GetPatients()
        {
            //This is for manually creating patient data.
            //List<Patient> patients = new List<Patient>();
            //var emi= new Patient();
            //emi.FirstName = "Emiliano";
            //emi.LastName = "Sala";
            //emi.PatientID = 1;
            //patients.Add(emi);

            //var kiran = new Patient();
            //kiran.FirstName = "Kiran";
            //kiran.LastName = "Cherlakola";
            //kiran.PatientID = 2;
            //patients.Add(kiran);

            //Dapper

            //var patient =  dbConnection.QuerySingle<Patient>("select * from Patient where patientid=1");
            var patients = dbConnection.Query<Patient>("select * from Patient");
            //patients.Add(patient);
            return patients;

            
    }

        [Route("Patient")]
        [HttpPost]
        public dynamic PostPatient(Patient obj)
        {
            string sql = string.Format("insert into Patient(FirstName, LastName) values('{0}','{1}')", obj.FirstName, obj.LastName);
            dbConnection.Execute(sql);   
            return new { status = "ok" };
        }
        [Route("Patient")]
        [HttpPut]
        public dynamic PutPatient(Patient obj)
        {
            try {
                string sql = string.Format("update[Patient] set FirstName='{0}',LastName='{1}' where patientid={2}", obj.FirstName, obj.LastName, obj.PatientID);
                dbConnection.Execute(sql);
                return new { status = "ok" };

            }
            catch(Exception ex) {

                return ex;
            }
        }

        [Route("Patient/{PatientID}")]
        [HttpDelete]
        public dynamic DeletePatient(int PatientID)
        {
            string sql = string.Format("delete from Patient where PatientID={0}", PatientID);
            dbConnection.Execute(sql);
            return new { status = "ok" };
        }

    }
}
