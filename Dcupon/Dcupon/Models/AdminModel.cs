using System;
using Dcupon.DAL;
using Dcupon.Configuration;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Dcupon.Models
{
    public class AdminModel:AdminProfile
    {
        public bool NotValidated { get; set; }
        /// <summary>
        /// To Save Website Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(AdminProfile objadminprofile)
        {

            bool SaveStatus = true;
            try
            {
                using (OntoDealsEntities db = new OntoDealsEntities())
                {
                    db.AdminProfiles.Add(objadminprofile);
                    db.SaveChanges();
                }

            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {

                foreach (var failure in ex.EntityValidationErrors)
                { 
                
                }
                SaveStatus = false;
                ErrorLogger err = new ErrorLogger();
                Audit audit = new Audit();
                audit.Details = ex.InnerException + ex.StackTrace;
                audit.CreatedOn = DateTime.Now;
                audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.AdminProfile);
                err.Save(audit);

            }


            return SaveStatus;
        }


        /// <summary>
        /// To Update Website Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Update(AdminProfile objadminprofile)
        {
            bool UpdateStatus = true;
            try
            {
                using (OntoDealsEntities db = new OntoDealsEntities())
                {

                    db.Entry(objadminprofile).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {

                UpdateStatus = false;
                ErrorLogger err = new ErrorLogger();
                Audit audit = new Audit();
                audit.Details = ex.InnerException + ex.StackTrace;
                audit.CreatedOn = DateTime.Now;
                audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.AdminProfile);
                err.Save(audit);
            }

            return UpdateStatus;
        }
        public bool ValidateUser(AdminProfile objadminprofile)
        {
            int id = 0;



            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {
                    id = (from p in db.AdminProfiles
                          where p.Username == (objadminprofile.Username) && p.Password == (objadminprofile.Password)
                          select p.id).FirstOrDefault();
                    
                     
                    
                }
                catch (Exception ex)
                {


                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);
                    return false;
                 
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

        }
        public bool ValidateUserEmail(AdminProfile objadminprofile)
          {
            int id = 0;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {
                    id = (from p in db.AdminProfiles
                          where p.Username == (objadminprofile.Username)
                          select p.id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);
                    return false;
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

        }
        public bool ValidateUserEmailtoken(AdminProfile objadminprofile)
        {
            int id = 0;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {
                    id = (from p in db.AdminProfiles
                          where p.Username.ToLower() == (objadminprofile.Username) && p.PasswordVerificationToken.ToLower() == objadminprofile.PasswordVerificationToken
                          select p.id).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Websites);
                    err.Save(audit);
                    return false;
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

        }

        /// <summary>
        /// To Load Website Details 
        /// </summary>
        /// <returns>list </returns>
        public List<AdminProfile> Load()
        {

            List<AdminProfile> obj = new List<AdminProfile>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    obj = db.AdminProfiles.ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.AdminProfile);
                    err.Save(audit);
                }
            }
            return obj;
        }

        /// <summary>
        /// Load Details By passing ID
        /// </summary>
        /// <returns> single record</returns>
        public AdminProfile LoadById(int id)
        {

            AdminProfile objadminProfile = new AdminProfile();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objadminProfile = db.AdminProfiles.Find(id);

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
            return objadminProfile;
        }

        public AdminProfile LoadByEmail(string email)
        {

            AdminProfile objadminProfile = new AdminProfile();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objadminProfile = (from p in db.AdminProfiles
                                       where p.Username.ToLower().Equals(email.ToLower())
                           select p).FirstOrDefault();

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
            return objadminProfile;
        }

    }


  
    public enum Roles
    {

        SuperAdmin=1,
        Admin

    }

    public enum Isactive
    {
        Active,
        NotActive

    }


}