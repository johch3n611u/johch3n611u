using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using PhotoSharing.DAL;

namespace PhotoSharing
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //1.9.在Global.asax的Application_Start方法中,建立PhotoSharingInitializer
            //Database.SetInitializer<PhotoSharingContext>(new PhotoSharingInitializer());

            //GlobalFilters.Filters.Add(new HandleErrorAttribute());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
