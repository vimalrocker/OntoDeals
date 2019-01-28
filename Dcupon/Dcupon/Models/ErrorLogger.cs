using System;
using Dcupon.DAL;
using Dcupon.Configuration;
namespace Dcupon.Models
{
    public class ErrorLogger : Audit
    {

        public bool Save(Audit objAudit)
        {

            bool SaveStatus = true;

            using (OntoDealsEntities db = new OntoDealsEntities())
            {
                try
                {

                    Audit audit = new Audit();
                    db.Audits.Add(objAudit);
                    db.SaveChanges();




                }
                catch (Exception ex)
                {
                    Email email = new Email();
                    email.Body = "<pre>" + ex.InnerException + ex.StackTrace + "</pre>";
                    email.SendErrorEmail();
                }

            }
            return SaveStatus;
        }



        public enum Module
        {
            Category = 1,
            Websites = 2,
            AdminProfile =3,
            ImageUpload = 4 ,
            CuponsDetails = 5,
            DataModel =6,
            Customer=7,
            SubCategory=8,
            Slider=9


        }

    }
}