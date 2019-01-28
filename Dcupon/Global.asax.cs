using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Dcupon
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        //protected void Application_Error(object sender, EventArgs e)
        //{
        //    Exception exception = Server.GetLastError();

        //    HttpException httpException = exception as HttpException;
        //    var action = "Index";
        //    if (httpException != null)
        //    {


        //        switch (httpException.GetHttpCode())
        //        {
        //            case 404:
        //                // page not found
        //                action = "index";
        //                break;
        //            case 500:
        //                // server error
        //                action = "index";
        //                break;
        //            default:
        //                action = "index";
        //                break;
        //        }
        //        var test = String.Format("~/Home/{0}", action);
        //        Response.Redirect(String.Format("~/Home/{0}", action));
        //    }
        //    else
        //    {

        //        Response.Redirect(String.Format("~/Home/{0}", action));

        //    }



        //}
    }
}
