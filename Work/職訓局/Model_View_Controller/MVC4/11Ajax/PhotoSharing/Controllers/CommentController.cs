using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharing.DAL;
using PhotoSharing.Models;


namespace PhotoSharing.Controllers
{
    public class CommentController : Controller
    {
        //7.2-1 宣告PhotoSharingContext()並在建構式建構DBContext物件
        PhotoSharingContext context = new PhotoSharingContext();

        //7.2-2 加入_CommentsForPhoto Action
        [ChildActionOnly]//無法在瀏覽器上用URL存取此action
        public ActionResult _CommentsForPhoto(int PhotoID)
        {
            var comments = from c in context.Comments
                           where c.PhotoID == PhotoID
                           select c;
            //為了在View裡取得PhotoID的值所以先存在ViewBag裡
            ViewBag.PhotoID = PhotoID;

            return PartialView(comments.ToList());
        }

        //7.5-1 加入_Create Action
        public ActionResult _Create(int PhotoID)
        {
            Comment newComment = new Comment();
            newComment.PhotoID = PhotoID;

            ViewBag.PhotoID = PhotoID;

            return PartialView("_CreateAComment");
        }

        //7.6-1 在CommentController加入_CommentsForPhoto POST Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _CommentsForPhoto(Comment comment, int PhotoID)
        {
            context.Comments.Add(comment);
            context.SaveChanges();

            var comments = from c in context.Comments
                           where c.PhotoID == PhotoID
                           select c;
            //7.6-1 在CommentController加入_CommentsForPhoto POST Action(第二個畫面)
            ViewBag.PhotoID = PhotoID;

            return PartialView("_CommentsForPhoto", comments.ToList());
        }

        //7.8-1加入CommentControl的Delete ACTION
        public ActionResult Delete(int id)
        {
            Comment comment = context.Comments.Find(id);

            context.Comments.Remove(comment);
            context.SaveChanges();
        

            return RedirectToAction("Display","Photos",new { id=comment.PhotoID});
        }


    }
}