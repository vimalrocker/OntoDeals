using Dcupon.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcupon.Models
{
    public class SubCategoryModel : SubCategory
    {

        public List<SubCategory> listofsubcategory { get; set; }

        public bool Save(SubCategory obj)
        {

            bool SaveStatus = true;
            try
            {
                using (OntoDealsEntities db = new OntoDealsEntities())
                {
                    db.SubCategories.Add(obj);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {


                SaveStatus = false;
                ErrorLogger err = new ErrorLogger();
                Audit audit = new Audit();
                audit.Details = ex.InnerException + ex.StackTrace;
                audit.CreatedOn = DateTime.Now;
                audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.SubCategory);
                err.Save(audit);

            }


            return SaveStatus;
        }


        public bool Update(SubCategory obj)
        {
            bool UpdateStatus = true;
            try
            {
                using (OntoDealsEntities db = new OntoDealsEntities())
                {

                    db.Entry(obj).State = EntityState.Modified;
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


        public List<SubCategory> Load()
        {

            List<SubCategory> obj = new List<SubCategory>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    obj = db.SubCategories.ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.SubCategory);
                    err.Save(audit);
                }
            }
            return obj;
        }

        public List<SubCategory> LoadActive()
        {

            List<SubCategory> objsignup = new List<SubCategory>();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objsignup = db.SubCategories.OrderByDescending(c => c.CreatedOn).Where(d => d.Isactive == 0).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.SubCategory);
                    err.Save(audit);
                }
            }
            return objsignup;
        }
        public SubCategory LoadById(int id)
        {

            SubCategory obj = new SubCategory();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    obj = db.SubCategories.Find(id);

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.SubCategory);
                    err.Save(audit);

                }
            }
            return obj;
        }


        public IEnumerable<SelectListItem> categoryList(int id)
        {


            CatergoryModel catergoryModel = new CatergoryModel();
            List<Category> categoryList = catergoryModel.Load();
            var item = new List<SelectListItem>();
            item.Add(new SelectListItem { Value = "", Text = "Select" });
            foreach (var x in categoryList)
            {
                if (x.id == id)
                {
                    item.Add(new SelectListItem { Value = Convert.ToString(x.id), Text = x.Title, Selected = true });
                }
                else
                {
                    item.Add(new SelectListItem { Value = Convert.ToString(x.id), Text = x.Title, Selected = false });
                }


            }

            return item;

        }
    }
}