using Dcupon.Configuration;
using Dcupon.DAL;
using Dcupon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dcupon.Controllers
{
    public class AdminController : Controller
    {
        //Get: /Admin/Login


        [HttpGet]
        [Route("Admin")]
        public ActionResult LogIn()
        {
            AdminModel model = new AdminModel();
            return View(model);
        }

        [HttpPost]

        public ActionResult DashBoard(AdminModel model)
        {

            AdminProfile objadminprofile = new AdminProfile();
            objadminprofile.Username = model.Username;
            objadminprofile.Password = Helper.Encrypt(model.Password, true);
            if (model.ValidateUser(objadminprofile))
            {
                CatergoryModel models = new CatergoryModel();
                models.listCategory = models.Load();
                models.listofcupons = models.LatestCupon();
                Session["logedIn"] = objadminprofile.Username;
                return View(models);
            }
            else
            {
                model.NotValidated = true;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }

        }
        [HttpGet]
        public ActionResult DashBoard()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                CatergoryModel model = new CatergoryModel();
                model.listCategory = model.Load();
                model.listofcupons = model.LatestCupon();
                return View(model);
            }
            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = true;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }

        }
        public ActionResult LogFailedIn(AdminModel model)
        {
            AdminModel models = new AdminModel();
            models.NotValidated = model.NotValidated;
            return View("LogIn", models);
        }


        [HttpGet]
        public ActionResult CreateCategory()
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                CatergoryModel catergoryModel = new CatergoryModel();
                catergoryModel.listCategory = catergoryModel.Load();
                return View(catergoryModel);
            }

            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }




        }

        [HttpPost]
        public ActionResult CreateCategory(CatergoryModel catergoryModel, int id)
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                if (id == 0)
                {
                    Category category = new Category();
                    category.Title = catergoryModel.Title;
                    category.CreatedOn = DateTime.Now;
                    category.ModifiedOn = DateTime.Now;
                    catergoryModel.Save(category);
                    catergoryModel = new CatergoryModel();
                    catergoryModel.listCategory = catergoryModel.Load();
                    return View(catergoryModel);
                }

                else
                {
                    Category category = new Category();
                    category = catergoryModel.LoadById(id);
                    DateTime createddate = category.CreatedOn;
                    category = new Category();
                    category.CreatedOn = createddate;
                    category.id = id;
                    category.Title = catergoryModel.Title;
                    category.ModifiedOn = DateTime.Now;
                    catergoryModel.Update(category);
                    catergoryModel = new CatergoryModel();
                    catergoryModel.listCategory = catergoryModel.Load();
                    return View(catergoryModel);

                }
            }

            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }




        }

        [HttpGet]
        public ActionResult SubCategory()
        {
            SubCategoryModel model = new SubCategoryModel();
            model.listofsubcategory = model.Load();
            return View(model);
        }

        public ActionResult CreateSubCategory(SubCategoryModel SubCategoryModel, int id)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                if (id == 0)
                {
                    SubCategory scategory = new SubCategory();
                    scategory.Title = SubCategoryModel.Title;
                    scategory.categoriesID = SubCategoryModel.categoriesID;
                    scategory.CreatedOn = DateTime.Now;
                    scategory.ModifiedOn = DateTime.Now;
                    SubCategoryModel.Save(scategory);
                    SubCategoryModel = new SubCategoryModel();
                    SubCategoryModel.listofsubcategory = SubCategoryModel.Load();
                    return View("SubCategory", SubCategoryModel);
                }

                else
                {
                    SubCategory scategory = new SubCategory();
                    scategory = SubCategoryModel.LoadById(id);
                    DateTime createddate = scategory.CreatedOn;
                    scategory = new SubCategory();
                    scategory.CreatedOn = createddate;
                    scategory.id = id;
                    scategory.Title = SubCategoryModel.Title;
                    scategory.categoriesID = SubCategoryModel.categoriesID;
                    scategory.ModifiedOn = DateTime.Now;
                    SubCategoryModel.Update(scategory);
                    SubCategoryModel = new SubCategoryModel();
                    SubCategoryModel.listofsubcategory = SubCategoryModel.Load();
                    return View("SubCategory", SubCategoryModel);

                }
            }

            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }



        }

        [HttpPost]
        public ActionResult CreateWebsites(WebsitesModel websitesModel, int id)
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {

                if (id == 0)
                {

                    Website website = new Website();
                    website.Title = websitesModel.Title;
                    website.ModifiedOn = DateTime.Now;
                    website.CreatedOn = DateTime.Now;
                    websitesModel.Save(website);
                    websitesModel = new WebsitesModel();
                    websitesModel.listWebsite = websitesModel.Load();
                    return View("CreateWebsites", websitesModel);
                }
                else
                {

                    Website website = new Website();

                    website = websitesModel.LoadById(id);
                    DateTime createddate = website.CreatedOn;
                    website = new Website();
                    website.CreatedOn = createddate;
                    website.id = id;
                    website.Title = websitesModel.Title;
                    website.ModifiedOn = DateTime.Now;
                    websitesModel.Update(website);
                    websitesModel = new WebsitesModel();
                    websitesModel.listWebsite = websitesModel.Load();
                    return View("CreateWebsites", websitesModel);
                }
            }

            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }




        }


        [HttpGet]
        public ActionResult CreateWebsites()
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                WebsitesModel websitesModel = new WebsitesModel();
                websitesModel.listWebsite = websitesModel.Load();
                return View("CreateWebsites", websitesModel);
            }

            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }




        }

        public ActionResult DeleteWebsite(int Id)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                WebsitesModel websitesModel = new WebsitesModel();
                Website websitedetails = new Website();
                websitedetails.id = Id;
                websitesModel.Delete(websitedetails);
                websitesModel.listWebsite = websitesModel.Load();
                return View("CreateWebsites", websitesModel);
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult slider()
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                SliderModel model = new SliderModel();
                model.listsliderdetails = model.Load();
                return View(model);
            }
            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);

            }
        }


        public ActionResult UpdateSlider(SliderModel model)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                Slider obj = new Slider();
                obj.id = model.id;
                obj.IsActive = 0;
                obj.imageid = model.imageid;
                obj.description = model.description;
                obj.CreatedOn = DateTime.Now;
                obj.ModifiedOn = DateTime.Now;
                obj.redirectlink = model.redirectlink;
                model.Update(obj);
                return RedirectToAction("slider", "Admin");
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);

            }

        }
        public ActionResult CreateCupons()
        {


            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                CuponDetailsModel model = new CuponDetailsModel();

                return View(model);
            }
            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);

            }

        }

        [HttpPost]
        public ActionResult CreateCupons(CuponDetailsModel model)
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {

                CuponDetail cupondetails = new CuponDetail();
                cupondetails.CategoriesId = model.CategoriesId;
                cupondetails.WebsitesId = model.WebsitesId;
                cupondetails.Title = model.Title;
                cupondetails.ImageurlId = model.ImageurlId;
                cupondetails.Description = model.Description;
                if (!string.IsNullOrEmpty(model.CuponCode))
                {
                    cupondetails.CouponType = model.CouponType;
                }
                else { cupondetails.CouponType = 0; }
                if (!string.IsNullOrEmpty(model.CuponCode))
                { cupondetails.CuponCode = model.CuponCode; }
                else { cupondetails.CuponCode = ""; }
                cupondetails.RedirectType = model.RedirectType;
                cupondetails.SubcategoriesID = model.SubcategoriesID;
                cupondetails.RedirectUrl = model.RedirectUrl;
                cupondetails.CreatedOn = DateTime.Now;
                cupondetails.ModifiedOn = DateTime.Now;
                cupondetails.IsActive = Convert.ToInt16(Isactive.Active);
                model.Save(cupondetails);
                model = new CuponDetailsModel();
                return View(model);

            }

            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponsList()
        {
            DataModel model = new DataModel();
            model.listofCupon = model.LoadCuponlist();

            return View(model);

        }
        public ActionResult UpdateIsactiveCouponsList(int id)
        {
            CuponDetailsModel model = new CuponDetailsModel();
            CuponDetail obj = new CuponDetail();
            obj = model.LoadByID(id);
            if (obj.IsActive == 1)
            {
                obj.IsActive = 0;
            }
            else
            {
                obj.IsActive = 1;
            }
            obj.ModifiedOn = DateTime.Now;
            model.Update(obj);
            return RedirectToAction("CouponsList");



        }
        public ActionResult UpdateCupons(int id)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                CuponDetailsModel model = new CuponDetailsModel();
                CuponDetail cupondetails = new CuponDetail();
                cupondetails = model.LoadByID(id);
                model.id = model.id;
                model.Title = cupondetails.Title;
                model.CategoriesId = cupondetails.CategoriesId;
                model.WebsitesId = cupondetails.WebsitesId;
                model.Title = cupondetails.Title;
                model.ImageurlId = cupondetails.ImageurlId;
                model.SubcategoriesID = cupondetails.SubcategoriesID;
                model.Description = cupondetails.Description;
                model.CouponType = cupondetails.CouponType;
                if (!string.IsNullOrEmpty(cupondetails.CuponCode))
                { model.CuponCode = cupondetails.CuponCode; }
                else { model.CuponCode = ""; }
                model.RedirectType = cupondetails.RedirectType;
                model.RedirectUrl = cupondetails.RedirectUrl;
                model.CreatedOn = DateTime.Now;
                model.ModifiedOn = DateTime.Now;
                model.IsActive = Convert.ToInt16(Isactive.Active);
                return View(model);
            }
            else
            {
                AdminModel model = new AdminModel();
                model.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", model);
            }

        }


        public ActionResult UpdateIsactiveCategories(int id)
        {
            CatergoryModel catergoryModel = new CatergoryModel();
            Category obj = new Category();
            obj = catergoryModel.LoadById(id);
            if (obj.Isactive == 1)
            {
                obj.Isactive = 0;
            }
            else
            {
                obj.Isactive = 1;
            }
            obj.ModifiedOn = DateTime.Now;
            catergoryModel.Update(obj);
            return RedirectToAction("CreateCategory");

        }

        public ActionResult UpdateIsactiveSubCategories(int id)
        {
            SubCategoryModel subcategoryModel = new SubCategoryModel();
            SubCategory obj = new SubCategory();
            obj = subcategoryModel.LoadById(id);
            if (obj.Isactive == 1)
            {
                obj.Isactive = 0;
            }
            else
            {
                obj.Isactive = 1;
            }
            obj.ModifiedOn = DateTime.Now;
            subcategoryModel.Update(obj);
            return RedirectToAction("SubCategory");

        }

        public ActionResult UpdateIsactivewebsites(int id)
        {
            WebsitesModel websitesModel = new WebsitesModel();
            Website obj = new Website();
            obj = websitesModel.LoadById(id);
            if (obj.Isactive == 1)
            {
                obj.Isactive = 0;
            }
            else
            {
                obj.Isactive = 1;
            }
            obj.ModifiedOn = DateTime.Now;
            websitesModel.Update(obj);
            return RedirectToAction("CreateWebsites");

        }

        [HttpPost]
        public ActionResult UpdateCupons(CuponDetailsModel model)
        {


            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {

                CuponDetail cupondetails = new CuponDetail();
                cupondetails.id = model.id;
                cupondetails.CategoriesId = model.CategoriesId;
                cupondetails.WebsitesId = model.WebsitesId;
                cupondetails.Title = model.Title;
                cupondetails.ImageurlId = model.ImageurlId;
                cupondetails.Description = model.Description;
                cupondetails.SubcategoriesID = model.SubcategoriesID;
                cupondetails.CouponType = model.CouponType;
                if (!string.IsNullOrEmpty(model.CuponCode))
                { cupondetails.CuponCode = model.CuponCode; }
                else { cupondetails.CuponCode = ""; }
                cupondetails.RedirectType = model.RedirectType;
                cupondetails.RedirectUrl = model.RedirectUrl;
                cupondetails.ModifiedOn = DateTime.Now;
                cupondetails.IsActive = Convert.ToInt16(Isactive.Active);
                model.Update(cupondetails);
                model = new CuponDetailsModel();
                return View(model);
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);

            }

        }
        public ActionResult ImageUpload()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {

                return View();
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }

        [HttpPost]
        public ActionResult ImageUpload(ImageModel model)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                ImageDetail obj = new ImageDetail();
                obj.Name = model.Name;
                string filePath = Server.MapPath("~/ImageUploaded/" + model.File.FileName);
                model.File.SaveAs(filePath);
                obj.Url = Convert.ToString(HttpContext.Request.Url);
                obj.Url = obj.Url.Replace(HttpContext.Request.Url.PathAndQuery, "/ImageUploaded" + "/" + model.File.FileName);
                obj.CreatedOn = DateTime.Now;
                model.Save(obj);
                return View();
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }



        public ActionResult ImageList()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                ImageModel model = new ImageModel();
                model.listImageDetail = model.Load();
                return View(model);
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }

        public ActionResult DeleteImageList(int Id)
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                ImageModel model = new ImageModel();
                ImageDetail imgdetails = new ImageDetail();
                imgdetails.id = Id;
                model.Delete(imgdetails);
                return RedirectToAction("ImageList", "Admin");
            }
            else
            {
                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }

        public ActionResult MobileUsers()
        {

            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {

                CustomerMobileModel obj = new CustomerMobileModel();
                obj.listofmobile = obj.ListofMobile();
                return View(obj);
            }
            else
            {

                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }

        public ActionResult RegUsers()
        {
            if (!string.IsNullOrEmpty(Convert.ToString(Session["logedIn"])))
            {
                CustomerDetails obj = new CustomerDetails();
                obj.listofcustomer = obj.ListOfCustomer();
                return View(obj);
            }

            else
            {

                AdminModel models = new AdminModel();
                models.NotValidated = false;
                return RedirectToAction("LogFailedIn", "Admin", models);
            }
        }



        public JsonResult ForgotPassword(string email)
        {

            AdminModel model = new AdminModel();
            model.Username = email;
            if (model.ValidateUserEmail(model))
            {
                AdminProfile obj = new AdminProfile();
                obj = model.LoadByEmail(email);
                Email emailobj = new Email();
                var token = emailobj.GenerateToken();
                obj.PasswordVerificationToken = token;
                model.Update(obj);
                emailobj.AdminPasswordResetEmail(email, token);

                return Json(new { Authentication = "Successfull", Message = "Email sent to the Registred Email-id" });
            }
            else
            {
                return Json(new { Authentication = "UnSuccessfull", Message = "Email-id does not exist" });
            }





        }

        [Route("AdminPasswordReset")]
        public ActionResult AdminPasswordReset(string email, string token)
        {
            AdminModel model = new AdminModel();
            model.Username = email;
            model.PasswordVerificationToken = token;
            if (model.ValidateUserEmail(model))
            {
                model.Username = email;
                model.PasswordVerificationToken = token;
                return View(model);

            }
            else
            {
                return RedirectToAction("index", "Home");

            }

        }




        public JsonResult AdminPasswordReset(string email, string token, string newpassword)
        {
            AdminProfile obj = new AdminProfile();

            AdminModel model = new AdminModel();
            model.Username = email;
            model.PasswordVerificationToken = token;
            if (model.ValidateUserEmailtoken(model))
            {
                model = new AdminModel();
                obj = model.LoadByEmail(email);
                obj.Password = Helper.Encrypt(newpassword, true);

                if (model.Update(obj))
                {
                    return Json(new { Authentication = "Successfull", Message = "Your Password has been reseted successfully" });
                }
                else
                {
                    return Json(new { Authentication = "UnSuccessfull", Message = "Email Not Validated" });
                }
            }

            else
            {
                return Json(new { Authentication = "UnSuccessfull", Message = "Error updating Password Please try later" });

            }

        }

    }
}