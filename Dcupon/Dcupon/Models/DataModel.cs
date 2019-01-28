using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dcupon.DAL;
using Dcupon.Configuration;
using System.Data;
namespace Dcupon.Models
{
    public class DataModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CatId { get; set; }
        public int WebId { get; set; }
        public string Category { get; set; }
        public string Website { get; set; }
        public int Isactive { get; set; }

        public string Description { get; set; }
        public string RedirectUrl { get; set; }

        public string CuponCode { get; set; }
        public string ImageUrl { get; set; }

        public int RedirectType { get; set; }

        public Customer model { get; set; }
        public List<DataModel> listofCupon { get; set; }
        public List<Website> listofwebsite { get; set; }
        public List<DataModel> listofdealsoftheday { get; set; }
        public List<DataModel> listofTopOffers { get; set; }
        public List<Category> listofCategory { get; set; }
        public List<SubCategory> listofSubCategory { get; set; }


        public List<DataModel> listofsliders { get; set; }


        public List<DataModel> LoadSliderlist()
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    list = (from slider in db.Sliders
                            join images in db.ImageDetails on slider.imageid equals images.id
                            orderby slider.CreatedOn descending
                            select new DataModel
                            {
                                Id = slider.id,
                                Description = slider.description,
                                RedirectUrl = slider.redirectlink,
                                ImageUrl = images.Url
                            }).ToList();
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

            return list;

        }

        public List<DataModel> LoadCuponlist()
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    list = (from cupons in db.CuponDetails
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join websites in db.Websites on cupons.WebsitesId equals websites.id
                            where websites.Isactive == 0 && categories.Isactive == 0
                            orderby cupons.ModifiedOn descending
                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Category = categories.Title,
                                Website = websites.Title,
                                Isactive = cupons.IsActive
                            }).ToList();



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

            return list;

        }
        public List<DataModel> HomeDetails()
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    list = (from websites in db.Websites

                            join cupons in db.CuponDetails on websites.id equals cupons.WebsitesId
                            join image in db.ImageDetails on cupons.ImageurlId equals image.id
                            where cupons.IsActive == 0
                            orderby websites.CreatedOn descending
                            select new DataModel
                            {

                                ImageUrl = image.Url,
                                WebId = websites.id,
                                Title = websites.Title

                            }).Distinct().Take(10).ToList();




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



            return list;

        }
        public int CuponCountweb(int id)
        {
            int count = 0;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {


                    count = db.CuponDetails.Where(o => o.WebsitesId == id && o.IsActive == 0).Count();

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
        public List<DataModel> LoadDealsofthedayCuponlist()
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    var ctype = Convert.ToInt16(CuponType.DealOfDay);

                    list = (from cupons in db.CuponDetails 
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join websites in db.Websites on cupons.WebsitesId equals websites.id
                            join images in db.ImageDetails on cupons.ImageurlId equals images.id
                            where cupons.CouponType.Equals(ctype) && cupons.IsActive == 0 && websites.Isactive == 0 && categories.Isactive == 0
                            orderby cupons.ModifiedOn descending
                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Website = websites.Title,
                                Isactive = cupons.IsActive,
                                ImageUrl = images.Url,
                                RedirectType = cupons.RedirectType,
                                RedirectUrl = cupons.RedirectUrl,
                                CuponCode = cupons.CuponCode,
                                Description = cupons.Description
                            }).ToList();




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

            return list;

        }
        public List<DataModel> LoadTopDealsofthedayCuponlist()
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    var ctype = Convert.ToInt16(CuponType.TopOffer);

                    list = (from cupons in db.CuponDetails 
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join websites in db.Websites on cupons.WebsitesId equals websites.id
                            join images in db.ImageDetails on cupons.ImageurlId equals images.id
                            where cupons.CouponType.Equals(ctype) && cupons.IsActive == 0 && websites.Isactive == 0 && categories.Isactive == 0 
                            orderby cupons.ModifiedOn descending
                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Website = websites.Title,
                                Isactive = cupons.IsActive,
                                ImageUrl = images.Url,
                                RedirectType = cupons.RedirectType,
                                RedirectUrl = cupons.RedirectUrl,
                                CuponCode = cupons.CuponCode,
                                Description = cupons.Description

                            }).ToList();




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

            return list;

        }
        public List<DataModel> LoadCuponlistbasedonwebid(int id)
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    var ctype = Convert.ToInt16(CuponType.DealOfDay);

                    list = (from cupons in db.CuponDetails
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join websites in db.Websites on cupons.WebsitesId equals websites.id
                            join images in db.ImageDetails on cupons.ImageurlId equals images.id
                            where cupons.WebsitesId.Equals(id) && cupons.IsActive == 0 && websites.Isactive == 0 && categories.Isactive == 0
                            orderby cupons.CreatedOn descending
                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Website = websites.Title,
                                Isactive = cupons.IsActive,
                                ImageUrl = images.Url,
                                Description = cupons.Description,
                                RedirectUrl = cupons.RedirectUrl,
                                RedirectType = cupons.RedirectType

                            }).ToList();




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

            return list;

        }
        public List<DataModel> LoadCuponlistbasedonwebtitle(string title)
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    list = (from websites in db.Websites
                            join cupons in db.CuponDetails on websites.id equals cupons.WebsitesId
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join images in db.ImageDetails on cupons.ImageurlId equals images.id
                            join subcat in db.SubCategories on cupons.SubcategoriesID equals subcat.id
                            where (websites.Title.ToUpper().StartsWith(title.ToUpper()) || websites.Title.ToUpper().Contains(title.ToUpper()) || websites.Title.ToUpper().Equals(title.ToUpper())
                            || subcat.Title.ToUpper().StartsWith(title.ToUpper()) || subcat.Title.ToUpper().Contains(title.ToUpper()) || subcat.Title.ToUpper().Equals(title.ToUpper())
                            && (cupons.IsActive == 0 && websites.Isactive == 0 && categories.Isactive == 0))
                            orderby websites.ModifiedOn descending
                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Website = websites.Title,
                                Isactive = cupons.IsActive,
                                ImageUrl = images.Url,
                                Description = cupons.Description,
                                RedirectUrl = cupons.RedirectUrl,
                                RedirectType = cupons.RedirectType,
                                CuponCode = cupons.CuponCode

                            }).ToList();
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

            return list;

        }

        public List<DataModel> LoadCuponlistbasedonscategory(int id)
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    list = (from websites in db.Websites
                            join cupons in db.CuponDetails on websites.id equals cupons.WebsitesId
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join images in db.ImageDetails on cupons.ImageurlId equals images.id
                            join scategory in db.SubCategories on cupons.SubcategoriesID equals scategory.id
                            where scategory.id.Equals(id) && cupons.IsActive == 0 && websites.Isactive == 0 && categories.Isactive == 0
                            orderby websites.ModifiedOn descending
                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Website = websites.Title,
                                Isactive = cupons.IsActive,
                                ImageUrl = images.Url,
                                Description = cupons.Description,
                                RedirectUrl = cupons.RedirectUrl,
                                RedirectType = cupons.RedirectType,
                                CuponCode = cupons.CuponCode

                            }).ToList();
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

            return list;

        }
        public List<DataModel> LoadCuponlistbasedoncatergory(string title)
        {
            List<DataModel> list = new List<DataModel>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    var ctype = Convert.ToInt16(CuponType.DealOfDay);

                    list = (from cupons in db.CuponDetails
                            join categories in db.Categories on cupons.CategoriesId equals categories.id
                            join websites in db.Websites on cupons.WebsitesId equals websites.id
                            join images in db.ImageDetails on cupons.ImageurlId equals images.id
                            where categories.Title.Equals(title) && cupons.IsActive == 0 && websites.Isactive == 0 && categories.Isactive == 0
                            orderby cupons.ModifiedOn descending

                            select new DataModel
                            {
                                Id = cupons.id,
                                Title = cupons.Title,
                                CatId = cupons.CategoriesId,
                                WebId = cupons.WebsitesId,
                                Website = websites.Title,
                                Isactive = cupons.IsActive,
                                ImageUrl = images.Url,
                                Description = cupons.Description,
                                RedirectUrl = cupons.RedirectUrl,
                                RedirectType = cupons.RedirectType,
                                CuponCode = cupons.CuponCode

                            }).ToList();




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

            return list;

        }

        public List<SubCategory> LoadlistofSubCategory(int id)
        {
            List<SubCategory> list = new List<SubCategory>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    var ctype = Convert.ToInt16(CuponType.DealOfDay);

                    list = (from subcat in db.SubCategories
                            where subcat.Isactive == 0 && subcat.categoriesID == id
                            orderby subcat.ModifiedOn descending
                            select subcat).ToList();

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

            return list;

        }
    }
}