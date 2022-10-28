using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoSharing.Models;
using PhotoSharing.DAL;
using System.Net;


namespace PhotoSharing.Controllers
{
    
    public class PhotosController : Controller
    {
        //2.2建立Data Context 來自 Photo Model 的 PhotoSharingContext 
        PhotoSharingContext context = new PhotoSharingContext();

        // GET: Photos
        //2.3-1 Index()回傳Photo
      
        public ActionResult Index()
        {
            ViewBag.Date = DateTime.Now;  //用ViewBag帶入今日日期
            return View(context.Photos.ToList());
        }

        //2.3-2 Display(int id) 透過id參數回傳Photo,若找不到回傳HttpNotFound()helper
       
        public ActionResult Display(int? id)
        {
            ViewData["Date"] = DateTime.Now; //用ViewData帶入今日日期
            if (id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = context.Photos.Find(id);
            if(photo==null)
            {
                return HttpNotFound("abcde");
            }

            return View("Display", photo);
        }
        //2.3-7 建立GetImage(int id)回傳File(photo.PhotoFile, photo.ImageMimeType)
        //這不是View()Helper的ActionResult, 這是File()helper
        public FileContentResult GetImage(int id)
        {
            Photo photo = context.Photos.Find(id);
            return File(photo.PhotoFile, photo.ImageMimeType);
        }

        //2.3-3 GET:Create() 產生新增Photo作業,並回傳 new Photo()並產生CreatedDate屬性值= DateTime.Today
        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            
            return View("Create",newPhoto);
        }
        //2.3-4 Post:Create(Photo photo, HttpPostedFileBase image),使用HTTP POST,執行新增Photo回存作業,回傳RedirectToAction()helper
        //      如果ModelState.IsValid==false,回傳Photo給View, 反之執行新增Photo回存作業
        //      photo.ImageMimeType = image.ContentType;
        //      photo.PhotoFile = new byte[image.ContentLength];
        //      image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo,HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;
            if(ModelState.IsValid)
            {
                //有post照片才做照片上傳的處理
                if (image!=null)
                {
                    photo.ImageMimeType = image.ContentType;  //抓照片型態
                    photo.PhotoFile = new byte[image.ContentLength];  //取得上傳照片的大小再轉byte陣列
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }
                context.Photos.Add(photo);
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View("Create", photo);
            }


            
        }
        //2.3-5 Get:Delete(int id)產生刪除Photo作業,並回傳Photo(),若找不到回傳HttpNotFound()helper
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = context.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }

            return View("Delete", photo);
        }
        //2.3-6 Post:建立DeleteConfirmed(int id)使用[ActionName("Delete")]屬性,透過HTTP POST,執行刪除Photo回存作業.回傳RedirectToAction()helper
        [HttpPost,ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            Photo photo = context.Photos.Find(id);
            context.Photos.Remove(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //3.13-3 在PhotoController中的_PhotoGallery Action上標註排除使用Filter
        [ValueReporter(IsCheck=false)]
        //3.6.在PhotoController.cs建立[ChildActionOnly] _PhotoGallery action,用於Partial View
        public ActionResult _PhotoGallery(int number=0)
        {
            List<Photo> photos;
            if (number == 0)
            {
                //Lambda
                photos = context.Photos.OrderByDescending(p => p.CreatedDate).ThenBy(p => p.PhotoID).ToList();

                //LINQ
                //photos = (from p in context.Photos
                //          orderby p.CreatedDate descending, p.PhotoID ascending
                //          select p).ToList();

                //SQL
                //Select * from photo
                //order by CreatedDate desc,p.PhotoID
            }
            else
            {
                photos = context.Photos.OrderByDescending(p => p.CreatedDate).ThenBy(p => p.PhotoID).Take(number).ToList();

                //LINQ
                //photos = (from p in context.Photos
                //          orderby p.CreatedDate descending, p.PhotoID ascending
                //          select p).Take(number).ToList();


                //SQL
                //Select top 2 * from photo
                //order by CreatedDate desc,p.PhotoID

            }

            return PartialView("_PhotoGallery", photos);


        }

        //4.1建立ExceptionDemo Action引發錯誤
        //[HandleError(View="ExceptionError")]
        public ActionResult ExceptionDemo()
        {
            int i = 0;

            int j = 10 / i;

            return View();
        }
        //4.5建立SlideShow Action並自訂NotImplementedException錯誤
        [HandleError(View="ExceptionError")]
        public ActionResult SliderShow()
        {
            throw new NotImplementedException("這一個SliderShow的功能還沒有做好哦!!!");
        }

        //5.1-2 建立自訂路由的ACTION:DisplayByTitle(PhotoController.cs)
        [Route(@"photo/title/{title}")]
        public ActionResult DisplayByTitle(string title)
        {
            if (title == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Photo photo = context.Photos.Where(p=>p.Title==title).FirstOrDefault();

            if(photo==null)
            {
                return HttpNotFound("abcde");
            }

            return View("Display", photo);
        }

        //6.3-1 加入TestBootStrap Action
        public ActionResult TestBoostrap()
        {
            return View();
        }

    }
}



//7 用ajax的方式來實現回覆留言的功能

//7.1 加入CommentController(Mvc5 Controller-Empty)
//   在CommentController加入_Create Action
//   在_CreateAComment View使用AJAX


//7.2 加入CommentController(Mvc5 Controller-Empty)
//7.2-1 宣告PhotoSharingContext()並在建構式建構DBContext物件
//7.2-2 加入_CommentsForPhoto Action
//7.2-3 加入_CommentsForPhoto Action的Partial View
//      使用Scaffold template:Empty
//      Model Class:Comment (PhotoSharing.Models)
//      Data Context Class:PhotoSharingContext (PhotoSharing.DAL)
//      勾選Create as a partial view核取方塊
//7.2-4 將_CommentsForPhoto.cshtml移至Shared目錄

//
//7.3 在/Shared/_CommentsForPhoto.cshtml加入IEnumerable<T>及foreach (var item in Model){}

//7.4 在/Photo/Display View加入@Html.Action("_CommentsForPhoto", "Comment", new { PhotoID = Model.PhotoID })

//可先測試一下在Display中是否能正常顯示回覆留言的PartialView


// 使用NuGet安裝套件 
// 1.*Jquery(如果需要)
// 2.Microsoft.jQuery.Unobtrusive.Ajax

//7.5-1 加入_Create Action

//7.5-2 加入_Create Action的_CreateAComment View
//      使用Scaffold template:Empty
//      Model Class:Comment (PhotoSharing.Models)
//      Data Context Class:PhotoSharingContext (PhotoSharing.DAL)
//      勾選Create as a partial view核取方塊

//7.5-3 將_CreateAComment.cshtml移至Shared目錄
// GET: /Comment/_Create. A Partial View for displaying the create comment tool as a AJAX partial page update

//7.6-1 在CommentController加入_CommentsForPhoto POST Action

//7.6-2 /Shared/_CreateAComment.cshtml加入Html Helper 及 class屬性

//7.6-3 /Shared/_CommentsForPhoto.cshtml使用Ajax.BeginForm

//7.6-4 /Shared/_CommentsForPhoto.cshtml加入@Html.Action("_Create", "Comment", new { PhotoID = ViewBag.PhotoId })
//      透過@Html.Action加入_CreateAComment Partial View

//7.7 在_MainLayout.cshtml加入SCRIPT
//7.7-1 加入*jquery-3.1.1.min.js(如果需要的話)(注意:不可用jquery-1.XXX版本)

//7.7-2 *****加入jquery.unobtrusive-ajax.min.js*****這個一定要記得安裝啊!!!!!!

//到這裡可以先測試一下在Display中是否能以Ajax方式新增留言

//7.8 加入CommentControl的Delete ACTION

//7.8-1加入CommentControl的Delete ACTION

//7-8-2 在_CommentsForPhoto.cshtml加入Delete連結

//7-9 測試:http://localhost:3395/Photo/Display/1