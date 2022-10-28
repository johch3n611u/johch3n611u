using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;

namespace PhotoSharing.DB_ADOnet.Controllers
{
    public class PhotoCommentsController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        SqlCommand Cmd = new SqlCommand();

        private DataTable querySql(string sql)
        {
            SqlDataAdapter adp = new SqlDataAdapter(sql,Conn);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            return ds.Tables[0];
        }

        private void executeSql(string sql)
        {
            Conn.Open();
            Cmd.Connection = Conn;
            Cmd.CommandText = sql;
            Cmd.ExecuteNonQuery();
            Conn.Close();
          
        }

        // GET: PhotoComments
        public ActionResult Index()
        {
            var sql = "select p.PhotoID, p.Title, p.Description, p.CreatedDate, c.Subject, c.Body, c.UserName " +
                "from photos as p inner join comments as c on p.PhotoID=c.PhotoID";

            DataTable dt = querySql(sql);

            return View(dt);
        }


        public ActionResult Display(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var sql = "select p.PhotoID, p.Title, p.Description, p.CreatedDate, c.Subject, c.Body, c.UserName " +
                "from photos as p inner join comments as c on p.PhotoID=c.PhotoID where p.Photoid="+id;

            DataTable dt = querySql(sql);

            return View(dt);
        }


        public ActionResult CreateComment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.PhotoID = id;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(string username, string subject, string body, int photoid)
        {

            var sql = "Add_Comment";
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@username", username);
            Cmd.Parameters.AddWithValue("@subject", subject);
            Cmd.Parameters.AddWithValue("@body", body);
            Cmd.Parameters.AddWithValue("@photoid", photoid);

            executeSql(sql);

            return RedirectToAction("Index");
        }



    }
}