using Dcupon.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Dcupon.Models
{
    public class ApiUploadStatus : Api_Import_Status
    {
        public List<Api_Import_Status> statuslist { get; set; }
        public ApiUploadStatus apiload { get; set; }
        public bool Save(Api_Import_Status objStatus)
        {

            bool SaveStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {
                    var Result = (from upoadapistatus in db.Api_Import_Status
                                  where upoadapistatus.api_Name == "Groupon"
                                  select upoadapistatus).ToList();
                    if (Result.Count != 0)
                    {
                        foreach (var item in Result)
                        {
                            db.Api_Import_Status.Remove(item);
                        }

                        db.SaveChanges();
                    }



                    db.Api_Import_Status.Add(objStatus);
                    db.SaveChanges();


                }
                catch (DbEntityValidationException ex)
                {

                    foreach (var eve in ex.EntityValidationErrors)
                    {

                        var test = eve;

                    }
                    SaveStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.ApiImportStatus);
                    err.Save(audit);

                }
            }

            return SaveStatus;
        }

        public List<Api_Import_Status> ListofStatus()
        {
            List<Api_Import_Status> listofapiStatus = new List<Api_Import_Status>();


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    listofapiStatus = db.Api_Import_Status.OrderByDescending(c => c.api_Name).ToList();


                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Customer);
                    err.Save(audit);
                }
            }
            return listofapiStatus;
        }


    }
}