using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dcupon.DAL;
using Dcupon.Configuration;
using System.Data.Entity;
using System.Web.Mvc;
using System.Data.Entity.Validation;
namespace Dcupon.Models
{
    public class CuponDetailsModel : CuponDetail
    {

        public List<CuponDetail> listofcupoindetails { get; set; }
    
        /// <summary>
        /// To Save Cupon  Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Save(CuponDetail objCuponDetail)
        {

            bool SaveStatus = true;
            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.CuponDetails.Add(objCuponDetail);
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
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.Category);
                    err.Save(audit);

                }
            }

            return SaveStatus;
        }





        /// <summary>
        /// To Update Cupon Details 
        /// </summary>
        /// <param name="objSignUp"></param>
        /// <returns>Status : True Or False</returns>
        public bool Update(CuponDetail objCuponDetail)
        {
            bool UpdateStatus = true;

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    db.Entry(objCuponDetail).State = EntityState.Modified;
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


        /// <summary>
        /// To Load Catergory Details 
        /// </summary>
        /// <returns>list </returns>
        public List<CuponDetail> Load()
        {

            List<CuponDetail> objCuponDetail = new List<CuponDetail>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objCuponDetail = db.CuponDetails.OrderByDescending(c => c.CreatedOn).ToList();

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
            return objCuponDetail;
        }


        public CuponDetail LoadByID(int id)
        {


            CuponDetail objCuponDetail = new CuponDetail();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    objCuponDetail = db.CuponDetails.Find(id);

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.CuponsDetails);
                    err.Save(audit);

                }
            }
            return objCuponDetail;
        }


        public List<CuponDetail> LoadSearchResults(string searchtext)
        {


            List<CuponDetail> listofCuponDetails = new List<CuponDetail>();

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                   

                    listofCuponDetails = (from c in db.CuponDetails
                                          where (c.Title.Equals(searchtext) || c.Title.StartsWith(searchtext))
                                            orderby c.CreatedOn
                                            select c).Take(10).ToList();

                }

                catch (Exception ex)
                {
                    ErrorLogger err = new ErrorLogger();
                    Audit audit = new Audit();
                    audit.Details = ex.InnerException + ex.StackTrace;
                    audit.CreatedOn = DateTime.Now;
                    audit.ModuleId = Convert.ToInt16(ErrorLogger.Module.CuponsDetails);
                    err.Save(audit);

                }
            }
            return listofCuponDetails;
        }

        public IEnumerable<SelectListItem> CuponTypes
        {

            get
            {
                IEnumerable<SelectListItem> List = new[]
                {
                    new SelectListItem{Value="",Text="Select"},
                    new SelectListItem{Value="1",Text="Top Offer"},
                    new SelectListItem{Value="2",Text="Latest Offer"},
                    new SelectListItem{Value="3",Text="Deal of the Day"},
                };
                return List;

            }
        }


        public IEnumerable<SelectListItem> RedirectTypes
        {

            get
            {
                IEnumerable<SelectListItem> List = new[]
                {
                    new SelectListItem{Value="",Text="Select"},
                    new SelectListItem{Value="1",Text="POP UP"},
                    new SelectListItem{Value="2",Text="Redirect"},
           
                };
                return List;

            }
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

        public IEnumerable<SelectListItem> Category(int id)
        {

            CatergoryModel catergorymodel = new CatergoryModel();
            List<Category> list = catergorymodel.Load();
            var item = new List<SelectListItem>();
            item.Add(new SelectListItem { Value = "", Text = "Select" });
            foreach (var x in list)
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



        public IEnumerable<SelectListItem> website(int id = 0)
        {
            {

                WebsitesModel webmodel = new WebsitesModel();
                List<Website> list = webmodel.Load();
                var item = new List<SelectListItem>();
                item.Add(new SelectListItem { Value = "", Text = "Select" });
                foreach (var x in list)
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

        public IEnumerable<SelectListItem> subcategoryList(int id)
        {


            SubCategoryModel scatergoryModel = new SubCategoryModel();
            List<SubCategory> scategoryList = scatergoryModel.Load();
            var item = new List<SelectListItem>();
            item.Add(new SelectListItem { Value = "", Text = "Select" });
            foreach (var x in scategoryList)
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




    public enum CuponType
    {
        None = 0,
        TopOffer = 1,
        LatestOffer = 2,
        DealOfDay = 3
    }

    public enum RedirectType
    {
        PopUp = 1,
        Redirect = 2
    }


}