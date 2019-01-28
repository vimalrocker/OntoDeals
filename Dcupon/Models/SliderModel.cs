using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dcupon.DAL;
using System.Data.Entity;
using System.Web.Mvc;
namespace Dcupon.Models
{
    public class SliderModel :Slider 
    {

        public List<Slider> listsliderdetails { get; set; }

        public bool Save(Slider obj)
        {

            bool SaveStatus = true;
            try
            {
                using (OntoDealsEntities db = new OntoDealsEntities())
                {
                    db.Sliders.Add(obj);
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
                audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Slider);
                err.Save(audit);

            }


            return SaveStatus;
        }


        public bool Update(Slider obj)
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
                audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Slider);
                err.Save(audit);
            }

            return UpdateStatus;
        }

        public List<Slider> Load()
        {

            List<Slider> obj = new List<Slider>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    obj = db.Sliders.OrderBy(c=>c.id).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Slider);
                    err.Save(audit);
                }
            }
            return obj;
        }

        public IEnumerable<SelectListItem> Image(int id)
        {


            ImageModel imagemodel = new ImageModel();
            List<ImageDetail> imageList = imagemodel.Load();
            var item = new List<SelectListItem>();
            item.Add(new SelectListItem { Value = "", Text = "Select" });
            foreach (var x in imageList)
            {
                if (x.id == id)
                {
                    item.Add(new SelectListItem { Value = Convert.ToString(x.id), Text = x.Name, Selected = true });
                }
                else
                {
                    item.Add(new SelectListItem { Value = Convert.ToString(x.id), Text = x.Name, Selected = false });
                }


            }

            return item;

        }



        public enum SliderType
        {
            DynamicSlider,
            StaticSlider 

        }
    }
}