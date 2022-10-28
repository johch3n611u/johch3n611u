using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Routing;

namespace PhotoSharing.Controllers
{
    //2.8-1 繼承System.Web.Mvc.ActionFilterAttribute(注意:命名空間 System.Web.Mvc)
    public class ValueReporter:ActionFilterAttribute
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand cmd = new SqlCommand();
        //2.8-2 建立一般方法executeSql()-可傳入SQL字串來輯編資料表
        void executeSql(string sql)
        {
            Conn.Open();

            cmd.Connection = Conn;
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();

            Conn.Close();
        }
        //2.8-4 覆寫OnActionExecuting方法,執行LogValues方法,透過ActionExecutingContext.RouteData屬性傳入RouteData物件
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //base.OnActionExecuting(filterContext);
            LogValues(filterContext.RouteData);
        }
        //2.8-3 自訂LogValues方法,透過RouteData物件將controller及action參數透過ADO.net送至PhotoSharing資料庫的ActionLog資料表
        void LogValues(RouteData routeData)
        {
            cmd.Parameters.Clear();
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var parame = routeData.Values["id"] == null ? "N/A" : routeData.Values["id"];

            string sql = "insert into actionLog(controllerName,actionName,parame) values(@controllerName,@actionName,@parame)";
            cmd.Parameters.AddWithValue("@controllerName", controllerName);
            cmd.Parameters.AddWithValue("@actionName", actionName);
            cmd.Parameters.AddWithValue("@parame", parame);

            executeSql(sql);
        }

        //2.8-5 自訂RequestLog方法,取出HttpContext.Current.Request,將ServerVariable透過ADO.net送至PhotoSharing資料庫的RequestLog資料表
        void RequestLog()
        {
            cmd.Parameters.Clear();
            var ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            var host = HttpContext.Current.Request.ServerVariables["REMOTE_HOST"];
            var browser = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"];

            var requestType = HttpContext.Current.Request.RequestType;
            var userHostAddress = HttpContext.Current.Request.UserHostAddress;
            var userHostName = HttpContext.Current.Request.UserHostName;
            var httpMethod = HttpContext.Current.Request.HttpMethod;

            string sql = "insert into RequestLog(ip,host,browser,requestType,userHostAddress,userHostName,httpMethod) ";
            sql+= "values(@ip,@host,@browser,@requestType,@userHostAddress,@userHostName,@httpMethod)";
            cmd.Parameters.AddWithValue("@ip", ip);
            cmd.Parameters.AddWithValue("@host", host);
            cmd.Parameters.AddWithValue("@browser", browser);
            cmd.Parameters.AddWithValue("@requestType", requestType);
            cmd.Parameters.AddWithValue("@userHostAddress", userHostAddress);
            cmd.Parameters.AddWithValue("@userHostName", userHostName);
            cmd.Parameters.AddWithValue("@httpMethod", httpMethod);
    

            executeSql(sql);
        }
        //2.8-6 覆寫OnActionExecuted方法,執行RequestLog方法
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RequestLog();
        }
      
    }
}