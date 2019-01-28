using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dcupon.DAL;
namespace Dcupon.Models
{
    public class CustomerMobileModel : CustomerMobile
    {


        public List<CustomerMobile> listofmobile { get; set; }
        /// <summary>
        /// To Save Customer  Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(CustomerMobile objCustomer)
        {

            bool SaveStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.CustomerMobiles.Add(objCustomer);
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    SaveStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Customer);
                    err.Save(audit);

                }
            }

            return SaveStatus;
        }


        public bool CheckDuplicatesMobile(string phoneno)
        {
            bool isduplicate = true;
            CustomerMobile obj = new CustomerMobile();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {
                    obj = (from p in db.CustomerMobiles
                           where p.Mobile.Equals(phoneno)
                           select p).FirstOrDefault();


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
            if (obj != null)
            {
                isduplicate = false;
            }
            return isduplicate;

        }


        public List<CustomerMobile> ListofMobile()
        {
            List<CustomerMobile> listofmobile = new List<CustomerMobile>();


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    listofmobile = db.CustomerMobiles.OrderByDescending(c => c.Name).ToList();


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
            return listofmobile;
        }
 
    }
}