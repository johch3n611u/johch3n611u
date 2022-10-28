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



//6.1   建立layout
//6.1-1 建立layout View(Shared\_MainLayout.cshtml)
//      Templete:empty(without model)
//      不要勾選Use a Layout Page
//      修改內容(可參考_Layout.cshtml)
//6.1-1-1  <title>@ViewBag.Title</title>
//6.1-1-2  製作<header> </header>裡的Navbar 
//6.1-1-3  <section>@RenderBody()</section>
//6.1-1-4  <footer>@Html.MvcSiteMap().SiteMapPath()</footer>

//6.1-2 (_ViewStart.cshtml)設定預設的Layout
//6.1-3 註解（photo/index.cshtml）原本 5.2-3的Menu及SiteMapPath

//6.2.1 *Manage NuGet Package 加入BootStrap & Jquery(已存在則省略)
//6.2.2 在layout View(Shared\_MainLayout.cshtml)使用BootStrap CSS
//      在<Head>標籤內加入
//                 1.<link href="~/Content/bootstrap.min.css" rel="stylesheet" />

//6.2.3 在layout View(Shared\_MainLayout.cshtml)使用BootStrap/Jquery JS
//      在</Body>標籤前加入
//                 1.<script src="~/Scripts/jquery-3.1.1.min.js"></script>
//                 2.<script src="~/Scripts/bootstrap.min.js"></script>

//6.2.4 將自訂css Section插入Layout(Shared\_MainLayout.cshtml)
//      在<Head>標籤內加入(6.2.2的後面)
//      @RenderSection("CSS", false)

//6.2.5 將自訂Scripts Section插入Layout(Shared\_MainLayout.cshtml)
//      在</body>標籤上一行加入
//      @RenderSection("Scripts", false)

//6.3-1 加入TestBootStrap Action
//6.3.2 在Photos裡建立Bootstrap View:TestBootStrap.cshtml,使用Bootstrap Grid System
//      Template:Empty (without model)
//      勾選Use a Layout Page
//      選擇_MainLayout.cshtml為主版
//      修改內容
//6.3.3 在TestBootStrap.cshtml View中定義Section
//      @section CSS{.......}


//6.4.1 更新Create View,使用Bootstrap Form/Button
//6.4.2 更新Create View,自訂SCRIPT,使用@section
//       @section Scripts{.......}

//6.5.1 更新(Shared\_MainLayout.cshtml),SiteMap使用Bootstrap Navbar
//6.5.2 更新(Shared\_MainLayout.cshtml),加入JQUERY, 產生<ul id="menu" class="nav navbar-nav">
//6.5.3 更新(Shared\_MainLayout.cshtml),加入JQUERY, 產生<li><form method="get" action="https://www.google.com.tw/search" class="form-inline"><input id="q" name="q" type="text" class="form-control" placeholder="Please Input Something..." /><input id="Submit1" type="submit" value="Search" class="btn btn-success" /></form></li>
