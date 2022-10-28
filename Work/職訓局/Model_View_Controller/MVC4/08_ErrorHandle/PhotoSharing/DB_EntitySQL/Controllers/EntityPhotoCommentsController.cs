using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharing.DB_EntitySQL.Models;
using System.Net;
using System.Data.Entity.Core.EntityClient;
using System.Collections;
using System.Data;

namespace PhotoSharing.DB_EntitySQL.Controllers
{
    public class EntityPhotoCommentsController : Controller
    {
        /*PhotoSharingEntities db = new PhotoSharingEntities();*/  //建立DB Context
        EntityConnection Conn = new EntityConnection("name=PhotoSharingEntities");

        // GET: EntityPhotoComments
        public ActionResult Index()
        {
            var Cmd = Conn.CreateCommand();
            Cmd.CommandText = "select p.PhotoID, p.Title, p.Description, p.CreatedDate, c.Subject, c.Body, c.UserName " +
                "from PhotoSharingEntities.photos as p inner join PhotoSharingEntities.comments as c on p.PhotoID=c.PhotoID";

            Conn.Open();
            var rd = Cmd.ExecuteReader(CommandBehavior.SequentialAccess);

            ArrayList list = new ArrayList();

            while(rd.Read())
            {
                var data = new
                {
                    PhotoID = rd["PhotoID"].ToString(),
                    Title = rd["Title"].ToString(),
                    Description = rd["Description"].ToString(),
                    CreatedDate = rd["CreatedDate"].ToString(),
                    Subject = rd["Subject"].ToString(),
                    Body = rd["Body"].ToString(),
                    UserName = rd["UserName"].ToString()
                };
                list.Add(data);

            }


            Conn.Close();

            ViewBag.Data = list;
            return View();
        }

        public ActionResult Display(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            var Cmd = Conn.CreateCommand();
            Cmd.CommandText = "select p.PhotoID, p.Title, p.Description, p.CreatedDate, c.Subject, c.Body, c.UserName " +
                "from PhotoSharingEntities.photos as p inner join PhotoSharingEntities.comments as c on p.PhotoID=c.PhotoID " +
                "where p.PhtotID=" + id;

            Conn.Open();
            var rd = Cmd.ExecuteReader(CommandBehavior.SequentialAccess);

            ArrayList list = new ArrayList();

            while (rd.Read())
            {
                var data = new
                {
                    PhotoID = rd["PhotoID"].ToString(),
                    Title = rd["Title"].ToString(),
                    Description = rd["Description"].ToString(),
                    CreatedDate = rd["CreatedDate"].ToString(),
                    Subject = rd["Subject"].ToString(),
                    Body = rd["Body"].ToString(),
                    UserName = rd["UserName"].ToString()
                };
                list.Add(data);

            }


            Conn.Close();

            ViewBag.Data = list;
            return View();
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
        public ActionResult CreateComment(Comments comment)
        {
            var Cmd = Conn.CreateCommand();
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.CommandText = "PhotoSharingEntities.Add_Comment";

    
            Cmd.Parameters.AddWithValue("username", comment.UserName);
            Cmd.Parameters.AddWithValue("subject", comment.Subject);
            Cmd.Parameters.AddWithValue("body", comment.Body);
            Cmd.Parameters.AddWithValue("photoid", comment.PhotoID);

            Conn.Open();
            Cmd.ExecuteNonQuery();
            Conn.Close();

            
            return RedirectToAction("Index");
        }
    }
}