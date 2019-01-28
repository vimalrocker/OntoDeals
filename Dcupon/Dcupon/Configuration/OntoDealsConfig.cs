using Dcupon.Models;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Web;


namespace Dcupon.Configuration
{
    public class OntoDealsConfig
    {

        private static string dbconnection { get; set; }


        /// <summary>
        /// Load DB Configure Details 
        /// </summary>
        public static string DBConection
        {

            get
            {
                if (dbconnection == null)
                {
                    var json = "";
                    using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath(@"~/Configuration/Config.json")))
                    {
                        json = reader.ReadToEnd();
                    }
                    JToken token = JToken.Parse(json);
                    //dbconnection = Helper.Decrypt(Convert.ToString(token.Root["DBconnection"].First["Test"]), true);
                    dbconnection = Convert.ToString(token.Root["DBconnection"].First["Production"]);
                }
                return dbconnection;
            }
        }
    }
}