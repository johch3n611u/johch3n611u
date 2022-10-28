using System.Web;
using System.Web.Mvc;
using PhotoSharing.Controllers;

namespace PhotoSharing
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //3.0-2 在App_Start\FilterConfig.cs中Action Filter Class[ValueReporter]
            filters.Add(new ValueReporter());
        }
    }
}
