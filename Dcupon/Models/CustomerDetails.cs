using Dcupon.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcupon.Models
{
    public class CustomerDetails : Customer
    {
        public CustomerMobileModel custmobile { get; set; }

        public List<Customer> listofcustomer { get; set; }
        /// <summary>
        /// To Save Customer  Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(Customer objCustomer)
        {

            bool SaveStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.Customers.Add(objCustomer);
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


        public bool Update(Customer objCustomer)
        {
            bool UpdateStatus = true;

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.Entry(objCustomer).State = EntityState.Modified;
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    UpdateStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Customer);
                    err.Save(audit);
                }
            }
            return UpdateStatus;
        }


        public Customer Load(Customer objCustomer)
        {

            Customer obj = new Customer();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {

              
                try
                {
                    obj = (from p in db.Customers
                           where p.Email.ToLower().Equals(objCustomer.Email.ToLower())
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
            if (obj == null)
            {
                obj = new Customer();
            }
            return obj;

        }
        public bool CheckDuplicates(string email, string password)
        {
            bool isduplicate = true;
            Customer obj = new Customer();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {
                    obj = (from p in db.Customers
                           where p.Email.ToLower() == email.ToLower()
                           select p).FirstOrDefault();



                    if (obj != null)
                    {
                        var pwd = Helper.Decrypt(obj.Password, true);
                        if (pwd == "" && password == "")
                        {
                            obj = null;

                        }
                        else if (pwd != "" && password == "")
                        {
                            obj = null;
                        }
                        else if (pwd != "" && password != "")
                        {

                        }

                        else
                        {
                            obj.Password = Helper.Encrypt(password, true);
                            db.SaveChanges();
                            obj = null;
                        }
                    }

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

   
        public Customer ValidateUser(Customer objCustomer)
        {

            Customer obj = new Customer();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {

                var pass = Helper.Encrypt(objCustomer.Password, true);
                try
                {
                    obj = (from p in db.Customers
                           where p.Email.Equals(objCustomer.Email) && p.Password.Equals(pass)
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
            if (obj == null)
            {
                obj = new Customer();
            }
            return obj;

        }

        public bool ValidateUserEmail(Customer objCustomer)
        {

            var id = 0;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {

                try
                {
                    id = (from p in db.Customers
                           where p.Email.ToLower().Equals(objCustomer.Email.ToLower())
                           select p.id).FirstOrDefault();


                }
                catch (Exception ex)
                {


                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Customer);
                    err.Save(audit);
                    return false;

                }


            }
            if (id > 0)
            {
                return true ;
            }
            else
            {

                return false;
            }

        }


        public bool ValidateUserToken(Customer objCustomer)
        {

            var id = 0;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {

                try
                {
                    id = (from p in db.Customers
                          where p.passwordVerificationToken.ToLower().Equals(objCustomer.passwordVerificationToken.ToLower()) && p.Email.ToLower().Equals(objCustomer.Email.ToLower())
                          select p.id).FirstOrDefault();


                }
                catch (Exception ex)
                {


                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Customer);
                    err.Save(audit);
                    return false;

                }


            }
            if (id > 0)
            {
                return true;
            }
            else
            {

                return false;
            }

        }
        public List<Customer> ListOfCustomer()
        {
            List<Customer> listofcust = new List<Customer>();


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    listofcust = db.Customers.OrderByDescending(c => c.Name).ToList();


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
            return listofcust;
        }
 


        public enum MobileOperator
        {
            Airtel = 1,
            Aircel = 2,
            BSNL = 3,
            Vodafone = 4,
            Docomo = 5,
            Idea = 6,
            RelianceGSM = 7,
            RelianceCDMA = 8,
            Uninor = 9,
            Loop = 10,
            MTNL = 11,
            Indicom = 12,
            MTS = 13,
            Videocon = 14,
            VirginCDMA = 15,
            VirginGSM = 16,
            DocomoCDMA = 17

        }

        public IEnumerable<SelectListItem> MobileOperators
        {

            get
            {
                IEnumerable<SelectListItem> List = new[]
                {
                    new SelectListItem{Value="",Text="Select"},
                    new SelectListItem{Value="1",Text="Airtel"},
                    new SelectListItem{Value="2",Text="Aircel"},
                    new SelectListItem{Value="3",Text="BSNL"},
                    new SelectListItem{Value="4",Text="Vodafone"},
                    new SelectListItem{Value="5",Text="Docomo"},
                    new SelectListItem{Value="6",Text="Idea"},
                    new SelectListItem{Value="7",Text="RelianceGSM"},
                    new SelectListItem{Value="8",Text="RelianceCDMA"},
                    new SelectListItem{Value="9",Text="Uninor"},
                    new SelectListItem{Value="10",Text="Loop"},
                    new SelectListItem{Value="11",Text="MTNL"},
                    new SelectListItem{Value="12",Text="MTS"},
                    new SelectListItem{Value="13",Text="Indicom"},
                    new SelectListItem{Value="14",Text="Videocon"},
                     new SelectListItem{Value="15",Text="VirginCDMA"},
                    new SelectListItem{Value="16",Text="VirginGSM"},
                    new SelectListItem{Value="17",Text="DocomoCDMA"},
           
                };
                return List;

            }
        }
    }



}