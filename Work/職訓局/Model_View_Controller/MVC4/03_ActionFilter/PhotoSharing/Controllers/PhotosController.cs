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


    }
}


//2.1.加入 Empty MVC controller -> PhotoController(PhotoController.cs)
//2.2.建立Data Context 來自 Photo Model 的 PhotoSharingContext 
//2.3.建立action
//2.3-1 Index()回傳Photo
//2.3-2 Display(int id) 透過id參數回傳Photo,若找不到回傳HttpNotFound()helper
//2.3-3 Create() 產生新增Photo作業,並回傳 new Photo()並產生CreatedDate屬性值= DateTime.Today
//2.3-4 Create(Photo photo, HttpPostedFileBase image),使用HTTP POST,執行新增Photo回存作業,回傳RedirectToAction()helper
//      如果ModelState.IsValid==false,回傳Photo給View,反之執行新增Photo回存作業
//      photo.ImageMimeType = image.ContentType;
//      photo.PhotoFile = new byte[image.ContentLength];
//      image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
//2.3-5 Delete(int id)產生刪除Photo作業,並回傳Photo(),若找不到回傳HttpNotFound()helper
//2.3-6 建立DeleteConfirmed(int id)使用[ActionName("Delete")]屬性,透過HTTP POST,執行刪除Photo回存作業.回傳RedirectToAction()helper
//2.3-7 建立GetImage(int id)回傳File(photo.PhotoFile, photo.ImageMimeType)(注意:File()helper 是FileContentResult,不是 View()helper 的ActionResult)


//2.4.建立Index及Display Views
//      Model Class:Photo (PhotoSharing.Models)
//      Data Context Class:PhotoSharingContext (PhotoSharing.DAL)
//2.4-1 Index View使用Scaffold template:List
//2.4-2 Display View使用Scaffold template:Details 


//2.5. 更改Index View
//2.5-1 取出ViewBag值
//2.5-2 註解:不顯示圖片
//2.5-3 在Index View(Index.cshtml)將ActionLink的Details改成Display


//2.6 更改Display View
//2.6-1 取出ViewData值
//2.6-2 在Display View(Display.cshtml)使用GetImage Action


//2.7.建立Delete Views
//      Model Class:Photo (PhotoSharing.Models)
//      Data Context Class:PhotoSharingContext (PhotoSharing.DAL)
//2.7-1 Delete View使用Scaffold template:Delete
//2.7-2 在Delete View(Delete.cshtml)使用GetImage Action

//2.8.Controllers-->Add Class-->建立ValueReporter Class(ValueReporter.cs),加入Action Filter Type
//2.8-1 繼承System.Web.Mvc.ActionFilterAttribute(注意:命名空間 System.Web.Mvc)
//2.8-2 建立一般方法executeSql()-可傳入SQL字串來輯編資料表
//2.8-3 自訂LogValues方法,透過RouteData物件將controller及action參數透過ADO.net送至PhotoSharing資料庫的ActionLog資料表
//2.8-4 覆寫OnActionExecuting方法,執行LogValues方法,透過ActionExecutingContext.RouteData屬性傳入RouteData物件
//2.8-5 自訂RequestLog方法,取出HttpContext.Current.Request,將ServerVariable透過ADO.net送至PhotoSharing資料庫的RequestLog資料表
//2.8-6 覆寫OnActionExecuted方法,執行RequestLog方法

//2.9 在PhotoSharing資料庫中建立ActionLog與RequestLog資料表
//Create table ActionLog(
//    ActionLogSN bigint identity primary key,
//    logTime datetime default getdate() not null,
//    controllerName varchar(30) not null,
//	actionName varchar(30) not null,
//	parame varchar(10) not null
//)
//go


//create table RequestLog(
//    RequestLogSN bigint identity primary key,
//    logTime datetime default getdate() not null,
//    [ip] varchar(20) not null,
//	host varchar(30) not null,
//	browser varchar(MAX) not null,
//	requestType varchar(20) not null,
//	userHostAddress varchar(20) not null,
//	userHostName varchar(30) not null,
//	httpMethod varchar(30) not null
//)
//go

//3.0 註冊Action Filter
//3.0-1 在PhotoController class註冊Action Filter Class[ValueReporter]
//    可註冊為Action層級或Controller層級

//3.0-2 在App_Start\FilterConfig.cs中Action Filter Class[ValueReporter]

//***將PhotoSharingContext.cs裡的public PhotoSharingContext() : base("PhotoSharing")
//改為public PhotoSharingContext() : base("ConnectionString")***//  此處的值要視Web.config而定