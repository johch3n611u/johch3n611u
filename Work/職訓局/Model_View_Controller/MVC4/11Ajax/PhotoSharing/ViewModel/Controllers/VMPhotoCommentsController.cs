using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using PhotoSharing.DB_EntitySQL.Models;

namespace PhotoSharing.ViewModel.Controllers
{
    //step 1.4 建立VMPhotoCommentsController
    public class VMPhotoCommentsController : Controller
    {
        PhotoSharingEntities db = new PhotoSharingEntities();

        // GET: VMPhotoComments
        //step 2.1 建立Index Action
        public ActionResult Index(int id=1)
        {
            PhotoComments pc = new PhotoComments()
            {
                photos = db.Photos.ToList(),
                comments = db.Comments.Where(p => p.PhotoID == id).ToList()
            };

            ViewBag.PID = id;
            ViewBag.PTitle = db.Photos.Where(p => p.PhotoID == id).FirstOrDefault().Title;


            return View(pc);
        }

        //step 3.1 建立Display Action
        public ActionResult Display(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            PhotoComments pc = new PhotoComments()
            {
                photos = db.Photos.Where(p => p.PhotoID == id).ToList(),
                comments = db.Comments.Where(p => p.PhotoID == id).ToList()
            };

            ViewBag.PID = id;
            //ViewBag.PTitle = db.Photos.Where(p => p.PhotoID == id).FirstOrDefault().Title;


            return View(pc);
        }

        //step 4.1 建立CreateComment Action(Get及Post)
        //step 4.7 將CreateComment Action加入mode參數
        public ActionResult CreateComment(int? id, int mode)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.PhotoID = id;
            ViewBag.Mode = mode;
            return View();
        }
        //step 4.1 建立CreateComment Action(Get及Post)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateComment(Comments comment,int mode)
        {
            db.Comments.Add(comment);
            db.SaveChanges();

            //step 4.9 改寫CreateComment Action 口eturn時的判斷
            if (mode == 2)
                return RedirectToAction("Display", new { id = comment.PhotoID });

            return RedirectToAction("Index",new{id=comment.PhotoID });
        }


    }
}