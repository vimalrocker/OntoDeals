using Dcupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Dcupon.Controllers
{



    public class HomeController : Controller
    {

        // GET: Home

        public ActionResult Index()
        {

            DataModel model = new DataModel();
            model.listofCupon = model.HomeDetails();
            model.listofdealsoftheday = model.LoadDealsofthedayCuponlist();
            model.listofTopOffers = model.LoadTopDealsofthedayCuponlist();
            model.listofsliders = model.LoadSliderlist();
            return View(model);

        }

        [Route("Coupons")]
        public ActionResult Coupons(string src, int? page)
        {
            DataModel model = new DataModel();
            model.listofCupon = model.LoadCuponlistbasedonwebtitle(src);
            ViewData["src"] = src;
            TempData["Page"] = "Coupons";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(model.listofCupon.ToPagedList(pageNumber, pageSize));

        }

        [Route("SCoupons")]
        public ActionResult SCoupons(int src, int? page)
        {
            DataModel model = new DataModel();
            model.listofCupon = model.LoadCuponlistbasedonscategory(src);
            ViewData["src"] = src;
            TempData["Page"] = "Coupons";
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View("Coupons", model.listofCupon.ToPagedList(pageNumber, pageSize));

        }


        [Route("Categories")]
        public ActionResult Categories(string src, int? page)
        {
            DataModel model = new DataModel();
            model.listofCupon = model.LoadCuponlistbasedoncatergory(src);
            ViewData["src"] = src;
            TempData["Page"] = "Categories";
            WebsitesModel objcd = new WebsitesModel();
            model.listofwebsite = objcd.Load();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View("Coupons", model.listofCupon.ToPagedList(pageNumber, pageSize));

        }


        public ActionResult SearchList(string term)
        {
            string Searchtext = term;
            WebsitesModel Websitesdetails = new WebsitesModel();
            List<string> listofdetails = new List<string>();
            Websitesdetails.listWebsite = Websitesdetails.LoadSearchResults(Searchtext);
            listofdetails = (from ap in Websitesdetails.listWebsite
                             select (ap.Title)).ToList();
            return Json(listofdetails, JsonRequestBehavior.AllowGet);
        }

        [Route("Cupons")]
        public ActionResult Search(string search, int? page)
        {
            DataModel model = new DataModel();
            model.listofCupon = model.LoadCuponlistbasedonwebtitle(search);
            WebsitesModel objcd = new WebsitesModel();
            model.listofwebsite = objcd.Load();
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View("Coupons", model.listofCupon.ToPagedList(pageNumber, pageSize));


        }


        public ActionResult _SideBar()
        {
            DataModel dt = new DataModel();
            WebsitesModel objcd = new WebsitesModel();
            dt.listofwebsite = objcd.LoadActive();
            return PartialView(dt);
        }
        public ActionResult LoadNavigationMenu()
        {
            DataModel dt = new DataModel();
            WebsitesModel objcd = new WebsitesModel();
            CatergoryModel objcm = new CatergoryModel();
            SubCategoryModel objscm = new SubCategoryModel();
            dt.listofCategory = objcm.LoadActive();
            dt.listofwebsite = objcd.LoadActive();
            dt.listofSubCategory = objscm.LoadActive();
            return PartialView("_NavigationPartial", dt);

        }


        public ActionResult _Register()
        {

            CustomerDetails model = new CustomerDetails();
            return PartialView("_Register", model);

        }


        [Route("about")]
        public ActionResult Aboutus()
        {

            return View();
        }
        [Route("contact")]
        public ActionResult Contactus()
        {

            return View();
        }
        [Route("privacypolicy")]
        public ActionResult privacypolicy()
        {

            return View();
        }
        [Route("termsofuse")]
        public ActionResult termsofuse()
        {

            return View();
        }
        [Route("sitemap")]
        public ActionResult sitemap()
        {

            return View();
        }
        [Route("faq")]
        public ActionResult faq()
        {

            return View();
        }
        [Route("press")]
        public ActionResult press()
        {

            return View();
        }
        [Route("whereismyrecharge")]
        public ActionResult whereismyrecharge()
        {

            return View();
        }

        [Route("rechargeoffers")]
        public ActionResult RechargeOffers()
        {

            return View();
        }

        [Route("howitworks")]
        public ActionResult howitworks()
        {

            return View();
        }
    }
}