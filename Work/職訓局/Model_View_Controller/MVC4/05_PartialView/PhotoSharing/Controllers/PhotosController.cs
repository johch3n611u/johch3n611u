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
                return HttpNotFound();
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

    }
}


//Partial View
//3.6.在PhotoController.cs建立[ChildActionOnly] _PhotoGallery action,用於Partial View

//3.7.建立_PhotoGallery Views
//    使用Scaffold template:Empty
//    Model Class:Photo (PhotoSharing.Models)
//    Data Context Class:PhotoSharingContext (PhotoSharing.Models)
//    "勾選" Create as a partial view核取方塊


//3.8 將_PhotoGallery.cshtml移至Shared目錄

//3.9 在_PhotoGallery.cshtml完成_PhotoGallery Partial Views
//    加上 @model IEnumerable<PhotoSharing.Models.Photo>
//    如此可使用@foreach (var item in Model){....}

//3.10 在Index View使用 _PhotoGallery Partial Views
//3.10-1  在PhotosController的Index Action==>將return View("Index", context.Photos.ToList())改成 return View()
//3.10-2  建立Index Views
//        使用Scaffold template:Empty(without model)
//        不要勾選Create as a partial view核取方塊
//        勾選Use a Layout Page核取方塊  

//3.11 完成Index View(Photo/Index),使用 _PhotoGallery Action不帶number參數
//     @Html.Action("_PhotoGallery", "Photos")

//3.12  修改View(Home/Index),使用 _PhotoGallery Action輸入number參數
//      @Html.Action("_PhotoGallery", "Photo", new { number = 2 }

//3.13 處理ValueReporter Filter會因為使用PartialView造成的重覆開啟資料連線問題
//3.13-1 在ValueReporter加上判斷是否要使用Filter的屬性IsCheck
//3.13-2 在OnActionExecuting及OnResultExecuted中加上判斷
//3.13-3 在PhotoController中的_PhotoGallery Action上標註排除使用Filter
//3.13-4 在App_Start\FilterConfig.cs中加入IsCheck=true