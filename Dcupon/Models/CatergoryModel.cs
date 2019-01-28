using System;
using Dcupon.DAL;
using Dcupon.Configuration;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
namespace Dcupon.Models
{
    public class CatergoryModel:Category
    {

        public List<Category> listCategory = new List<Category>();

        public List<CuponDetail> listofcupons = new List<CuponDetail>();




        public List<CuponDetail> LatestCupon()
        {
            List<CuponDetail> listofcupons = new List<CuponDetail>();


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    listofcupons = db.CuponDetails.OrderByDescending(c => c.CreatedOn).Take(10).ToList();


                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.DataModel);
                    err.Save(audit);
                }
            }
            return listofcupons; 
        }
        /// <summary>
        /// To Save Catergory Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(Category objCategory)
        {

            bool SaveStatus = true;


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.Categories.Add(objCategory);
                    db.SaveChanges();
                }


                catch (Exception ex)
                {
                    SaveStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Category);
                    err.Save(audit);

                }
            }

            return SaveStatus;
        }


        /// <summary>
        /// To Update Catergory Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Update(Category objCategory)
        {
            bool UpdateStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.Entry(objCategory).State = EntityState.Modified;
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    UpdateStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Category);
                    err.Save(audit);
                }
            }

            return UpdateStatus;
        }

        public  int CuponCountCat(int id)
        {
            int count = 0;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {


                    count = db.CuponDetails.Where(o => o.CategoriesId == id && o.IsActive == 0).Count();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.DataModel);
                    err.Save(audit);
                }
            }
            return count;
        }

     

        /// <summary>
        /// To Load Catergory Details 
        /// </summary>
        /// <returns>list </returns>
        public List<Category> Load()
        {

            List<Category> objsignup = new List<Category>();


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objsignup = db.Categories.OrderByDescending(c => c.CreatedOn).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Category);
                    err.Save(audit);
                }
            }
            return objsignup;
        }

        public List<Category> LoadActive()
        {

            List<Category> objsignup = new List<Category>();


            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objsignup = db.Categories.OrderBy(c => c.CreatedOn).Where(d => d.Isactive == 0).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Category);
                    err.Save(audit);
                }
            }
            return objsignup;
        }

        /// <summary>
        /// Load Details By passing ID
        /// </summary>
        /// <returns> single record</returns>
        public Category LoadById(int id)
        {

            Category objUserProfile = new Category();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objUserProfile = db.Categories.Find(id);

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Category);
                    err.Save(audit);

                }
            }
            return objUserProfile;
        }

    }
}