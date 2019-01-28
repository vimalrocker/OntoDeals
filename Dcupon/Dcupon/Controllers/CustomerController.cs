using Dcupon.DAL;
using Dcupon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Dcupon.Controllers
{
    public class CustomerController : Controller
    {

        private string Authentication { get; set; }
        private string Message { get; set; }
        // GET: Customer
        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            CustomerDetails model = new CustomerDetails();
            model.Email = email;
            model.Password = password;
            CustomerDetails customerDetails = new CustomerDetails();
            Customer obj = new Customer();
            obj = customerDetails.ValidateUser(model);
            if (!string.IsNullOrEmpty(obj.Name))
            {
                Session["Username"] = obj.Name;
                return Json(new { Authentication = "Successfull", Message = "" });
            }
            else
            {
                Session["Loginstatus"] = false;
                return Json(new { Authentication = "UnSuccessfull", Message = "Incorrect Email/Password Combination" });
            }




        }

        public ActionResult DashBoard()
        {


            if (string.IsNullOrEmpty(Convert.ToString(Session["Username"])))
            {
                Session["Loginstatus"] = false;
                return RedirectToAction("index", "Home");

            }
            else
            {
                return View();
            }
        }

        public JsonResult Register(string name, string email, string password)
        {

            Customer model = new Customer();
            model.Name = name;
            model.Email = email;
            model.Password = password;
            CustomerDetails customerDetails = new CustomerDetails();

            if (customerDetails.CheckDuplicates(model.Email))
            {
                model.Password = Helper.Encrypt(model.Password, true);
                model.passwordVerificationToken = "";
                if (customerDetails.Save(model))
                {
                    Session["Username"] = model.Name;
                    return Json(new { Authentication = "Successfull", Message = "" });
                }
                else
                {
                    Session["Loginstatus"] = false;
                    return Json(new { Authentication = "UnSuccessfull", Message = "" });
                }
            }
            else
            {
                Session["Loginstatus"] = false;
                return Json(new { Authentication = "UnSuccessfull", Message = "Email Already Exsit" });

            }




        }

        public ActionResult GetRecharge(CustomerDetails model)
        {
            CustomerMobileModel customermob = new CustomerMobileModel();
            if (customermob.CheckDuplicatesMobile(model.custmobile.Mobile))
            {
                CustomerMobile obj = new CustomerMobile();
                obj.Name = model.custmobile.Name;
                obj.Mobile = model.custmobile.Mobile;
                obj.MobileOperator = model.custmobile.MobileOperator;
                obj.ConnectionType = model.custmobile.ConnectionType;
                if (customermob.Save(obj))
                {

                    return RedirectToAction("index", "Home");
                }
                else
                {

                    return RedirectToAction("index", "Home");
                }
            }
            else
            {
                return RedirectToAction("index", "Home");
            }
        }
        public ActionResult LogOut()
        {

            Session["Username"] = null;
            Session["Error"] = null;
            return RedirectToAction("index", "Home");

        }

        public JsonResult ForgotPassword(string email)
        {
            Customer model = new Customer();
            model.Email = email;

            CustomerDetails obj = new CustomerDetails();
            if (obj.ValidateUserEmail(model))
            {

                model = obj.Load(model);
                Email emailobj = new Email();
                var token = emailobj.GenerateToken();
                model.passwordVerificationToken = token;
                obj.Update(model);

                emailobj.PasswordResetEmail(email, token);

                return Json(new { Authentication = "Successfull", Message = "Email sent to the Registred Email-id" });
            }
            else
            {
                return Json(new { Authentication = "UnSuccessfull", Message = "Email-id does not exist" });
            }





        }

        [Route("PasswordReset")]
        public ActionResult PasswordReset(string email, string token)
        {
            CustomerDetails obj = new CustomerDetails();
            Customer model = new Customer();
            model.Email = email;
            model.passwordVerificationToken = token;
            if (obj.ValidateUserToken(model))
            {
                obj.Email = email;
                obj.passwordVerificationToken = token;
                return View(obj);

            }
            else
            {
                return RedirectToAction("index", "Home");

            }

        }




        public JsonResult UserPasswordReset(string email, string token, string newpassword)
        {
            CustomerDetails obj = new CustomerDetails();

            Customer model = new Customer();
            model.Email = email;
            model.passwordVerificationToken = token;
            if (obj.ValidateUserToken(model))
            {
                model = new Customer();
                model.Email = email;
                model = obj.Load(model);
                model.Password = Helper.Encrypt(newpassword, true);

                if (obj.Update(model))
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