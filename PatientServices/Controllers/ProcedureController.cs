using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using Dapper;
using PatientServices.Models;
using System.Configuration;
using NLog;

namespace PatientServices.Controllers
{
    public class ProcedureController : ApiController
    {
       
        IDbConnection dbConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString);
        Logger logger = NLog.LogManager.GetCurrentClassLogger();

        [Route("Procedure")]
        [HttpGet]
        public dynamic GetProcedureList(int PatientID)
        {
            //return new { }; just to not get errors.
            //Dapper
            logger.Info("Starting Procedure List, GET");
            var procs = dbConnection.Query<Procedure>("select * from [Procedure] where PatientID ="+ PatientID);
            logger.Info("Sending Procedure List, GET");
            return procs;
            
        }

        [Route("Procedure")]
        [HttpPost]
        public dynamic PostProcedure(Procedure obj)
        {
            try
            {
                string sql = string.Format("insert into [Procedure](PatientID,SurgeryDate, SurgeryTime, SurgeonName,ProcedureName) values('{0}','{1}','{2}','{3}','{4}')", obj.PatientID, obj.SurgeryDate, obj.SurgeryTime, obj.SurgeonName, obj.ProcedureName);
                dbConnection.Execute(sql);
                var proc = dbConnection.QuerySingle<Procedure>("select * from [Procedure] where ProcedureID =(select ident_current('[Procedure]'))");
                return new { status = "ok", result = proc };
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Exception in PostProcedure");
                return new { status = "error", result = ex.Message };
                
                    
            }
        }


        //[Route("Procedure")]
        //[HttpDelete]
        //public dynamic DeleteProcedures(int PatientID) {}
        [Route("Procedure")]
        [HttpPut]
        public dynamic PutProcedure(Procedure obj)
        {
            try
            {
                string sql = string.Format("update[Procedure] set PatientID={0},SurgeryDate='{1}', SurgeryTime='{2}', SurgeonName='{3}',ProcedureName='{4}' where procedureid={5}", obj.PatientID, obj.SurgeryDate, obj.SurgeryTime, obj.SurgeonName, obj.ProcedureName,obj.ProcedureID);
                dbConnection.Execute(sql);
                return new { status = "ok"};
            }
            catch (Exception ex)
            {
                return new { status = "error", result = ex.Message };

            }
        }
    }
}
