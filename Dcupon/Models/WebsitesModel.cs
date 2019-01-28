using System;
using Dcupon.DAL;
using Dcupon.Configuration;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;

namespace Dcupon.Models
{
    public class WebsitesModel:Website
    {

        public List<Website> listWebsite = new List<Website>();
        /// <summary>
        /// To Save Website Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(Website objwebsite)
        {

            bool SaveStatus = true;

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.Websites.Add(objwebsite);
                    db.SaveChanges();


                }

                catch (DbEntityValidationException ex)
                {

                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);


                    SaveStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = fullErrorMessage + ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);

                }
            }


            return SaveStatus;
        }


        /// <summary>
        /// To Update Website Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Update(Website objwebsite)
        {
            bool UpdateStatus = true;

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {


                    db.Entry(objwebsite).State = EntityState.Modified;
                    db.SaveChanges();


                }

                catch (Exception ex)
                {

                    UpdateStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);
                }
            }
            return UpdateStatus;
        }



        /// <summary>
        /// To Load Website Details 
        /// </summary>
        /// <returns>list </returns>
        public List<Website> Load()
        {

            List<Website> objsignup = new List<Website>();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objsignup = db.Websites.OrderByDescending(c => c.CreatedOn).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);
                }
            }
            return objsignup;
        }



        public List<Website> LoadActive()
        {

            List<Website> objsignup = new List<Website>();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objsignup = db.Websites.OrderByDescending(c => c.CreatedOn).Where(d => d.Isactive == 0).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);
                }
            }
            return objsignup;
        }
        /// <summary>
        /// Load Details By passing ID
        /// </summary>
        /// <returns> single record</returns>
        public Website LoadById(int id)
        {

            Website objUserProfile = new Website();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objUserProfile = db.Websites.Find(id);

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);

                }
            }
            return objUserProfile;
        }



        public List<Website> LoadSearchResults(string searchtext)
        {


            List<Website> listofWebsiteDetails = new List<Website>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {



                    listofWebsiteDetails = (from c in db.Websites
                                          where (c.Title.ToLower().Equals(searchtext.ToLower()) || c.Title.ToLower().StartsWith(searchtext.ToLower()))
                                          orderby c.CreatedOn
                                          select c).Take(10).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);

                }
            }
            return listofWebsiteDetails;
        }
        public bool Delete(Website objWebsitedetails)
        {
            bool DeleteStatus = true;


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    Website websitedetails = db.Websites.Find(objWebsitedetails.id);
                    db.Websites.Remove(websitedetails);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    DeleteStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);

                }
            }
            return DeleteStatus;

        }
    }
}