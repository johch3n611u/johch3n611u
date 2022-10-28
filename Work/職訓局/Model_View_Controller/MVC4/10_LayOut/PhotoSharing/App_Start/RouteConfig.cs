using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PhotoSharing
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ///////////////////////////////
            //5.1-3 在\App_Start\RouteConfig.cs啟用自訂ACTION 路由==> routes.MapMvcAttributeRoutes();
            //啟用自訂Action當作路由方法
            routes.MapMvcAttributeRoutes();

            /////////////////////
            routes.MapRoute(
                name: "PhotoRoute",
                url: "photo/{id}",
                defaults: new { controller = "Photos", action = "Display" }
                //constraints: new { id=@"^[1-3]$"}
                );



            /////////////////////////
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
