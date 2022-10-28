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


    }
}


//4.1建立ExceptionDemo Action引發錯誤
//4.2替ExceptionDemo Action建立View並測試
//4.3在Web.Config啟用自訂錯誤 <customErrors mode = "On" />
//4.4再測試一次ExceptionDemo View(會把錯誤導向Error.cshtml)

//4.5 建立SlideShow Action並自訂NotImplementedException錯誤
//4.6 替SlideShow Action建立View並測試(仍會把錯誤導向Error.cshtml)

//4.7 在Shared目錄建立ExceptionError View
//    在ExceptionError.cshtml使用System.Web.Mvc.HandleErrorInfo 取出ControllerName,ActionName及Exception.Message屬性

//4.8 在global.asax註冊使用GlobalFilterCollection(Web Application層級)
//4.8-1其實在FilterConfig.cs裡已經被註冊使用GlobalFilterCollection，因此預預都會導到Error.cshtml
//在global.asax裡若把GlobalFilters.Filters.Add(new HandleErrorAttribute() { View = "ExceptionError" })
//寫法GlobalFilterCollection之前，則會用GlobalFilterCollection，若寫在之後，則會用
//GlobalFilters.Filters.Add(new HandleErrorAttribute() { View = "ExceptionError" })

//4.9 改寫在Shared目錄裡的Error View(已存在,可直接修改內容)
//4.9-1 在Error.cshtml使用System.Web.Mvc.HandleErrorInfo 取出ControllerName,ActionName及Exception.Message屬性
//4.9-2 使用System.Web.Mvc.HandleErrorInfo
//      @model System.Web.Mvc.HandleErrorInfo
//4.9-3取出ControllerName,ActionName及Exception.Message屬性*@
//      <fieldset>
//          <legend>HandleErrorInfo</legend>
//           Controller: @Model.ControllerName<br />
//           Action: @Model.ActionName<br />
//           Message: @Model.Exception.Message
//      </fieldset>


//4.10 在PhotoController加入 [HandleError(View = "Error")]屬性(Controller層級,會覆蓋Global層級)
// 若只加[HandleError],沒指定View的話,不管Global設定什麼,都會導到預設

//4.11 在Web.config設計400與404錯誤的重新導向

//<httpErrors errorMode = "Custom" >
//     <remove statusCode="400"/>
//     <remove statusCode = "404" />

//     <error statusCode="400" path="/400.html" responseMode="ExecuteURL"/>
//     <error statusCode = "404" path="/404.html" responseMode="ExecuteURL"/>
//</httpErrors>
