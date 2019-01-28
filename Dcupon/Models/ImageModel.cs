using Dcupon.Configuration;
using Dcupon.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dcupon.Models
{
    public class ImageModel : ImageDetail
    {
        public List<ImageDetail> listImageDetail = new List<ImageDetail>();
        public HttpPostedFileBase File { get; set; }
        public string FileName
        {
            get
            {
                if (File != null)
                    return File.FileName;
                else
                    return String.Empty;
            }
        }
        /// <summary>
        /// To Save Image Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(ImageDetail objimagedetails)
        {

            bool SaveStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.ImageDetails.Add(objimagedetails);
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    SaveStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.ImageUpload);
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
        public bool Update(ImageDetail objimagedetails)
        {
            bool UpdateStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {

                try
                {

                    db.Entry(objimagedetails).State = EntityState.Modified;
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    UpdateStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.ImageUpload);
                    err.Save(audit);
                }
            }
            return UpdateStatus;
        }

        public List<ImageDetail> Load()
        {

            List<ImageDetail> obj = new List<ImageDetail>();
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    obj = db.ImageDetails.OrderByDescending(c => c.CreatedOn).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.ImageUpload);
                    err.Save(audit);
                }
            }
            return obj;
        }

        /// <summary>
        /// Delete Sign-up details
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False </returns>
        public bool Delete(ImageDetail objimagedetails)
        {
            bool DeleteStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {

                try
                {

                    ImageDetail imagedetails = db.ImageDetails.Find(objimagedetails.id);
                    db.ImageDetails.Remove(imagedetails);
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    DeleteStatus = false;
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.ImageUpload);
                    err.Save(audit);

                }
            }

            return DeleteStatus;

        }

    }
}