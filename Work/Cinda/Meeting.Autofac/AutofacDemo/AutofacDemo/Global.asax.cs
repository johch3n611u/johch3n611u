using Autofac;
using Autofac.Integration.Mvc; // MVC 整合庫
using AutofacDemo.Controllers;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static AutofacDemo.Controllers.HomeController;

namespace AutofacDemo
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            // 註冊所有的 Controller 作為 Service
            builder.RegisterControllers(typeof(HomeController).Assembly);
            // 註冊 TextWriterLog 作為 ILog 的 Service
            #if DEBUG
            builder.RegisterType<TextWriterLog>().As<ILog>();
            #else
            builder.RegisterType<TextWriterNLog>().As<ILog>();
            #endif
            // 建立 DI Container
            var container = builder.Build();
            // 用 DI Container 作為建立 Controller 時候的 DI Resolver。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // 其他 Mvc 設定
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
