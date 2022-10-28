using Autofac;
using Autofac.Integration.Mvc; // MVC ��X�w
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
            // ���U�Ҧ��� Controller �@�� Service
            builder.RegisterControllers(typeof(HomeController).Assembly);
            // ���U TextWriterLog �@�� ILog �� Service
            #if DEBUG
            builder.RegisterType<TextWriterLog>().As<ILog>();
            #else
            builder.RegisterType<TextWriterNLog>().As<ILog>();
            #endif
            // �إ� DI Container
            var container = builder.Build();
            // �� DI Container �@���إ� Controller �ɭԪ� DI Resolver�C
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // ��L Mvc �]�w
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
