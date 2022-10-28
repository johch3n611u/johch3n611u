using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//00-1-6 using _MVC2.Models
using MVC2.Models;
using System.IO;

namespace MVC2.Controllers
{
    public class HomeController : Controller
    {
        //00-1-7 使用Entity建立DB物件
        dbProductEntities db = new dbProductEntities();

        // GET: Home
        //00-2-1 在HomeController裡撰寫Index的Action
        public ActionResult Index()
        {
            var product = db.tProduct.ToList();
            return View(product);
        }

        //00-3-1 在HomeController裡撰寫Delete的Action
        public ActionResult Delete(string fId)
        {
            //依網址傳來的fId編號取得要刪除的產品記錄
            var product = db.tProduct.Where(m => m.fId == fId).FirstOrDefault();
            string fileName = product.fImg;//取得要刪除產品的圖檔
            System.IO.File.Delete(Server.MapPath("~/images/"+fileName));//刪除指定圖檔
            db.tProduct.Remove(product);//依編號刪除產品記錄
            db.SaveChanges();//回存結果

            //return View();
            return RedirectToAction("Index");
        }

        //00-4-1 在HomeController裡撰寫GET及POST的Create Action
        public ActionResult Create()
        {
          
            return View();
        }

        [HttpPost]
        public ActionResult Create(string fId, string fName, decimal fPrice, HttpPostedFileBase fImg)
        {
            try
            {
                string fileName = "";

                if (fImg.ContentLength > 0)
                {
                    fileName = Path.GetFileName(fImg.FileName);
                    fImg.SaveAs(Server.MapPath("~/images/" + fileName));
                }

                tProduct product = new tProduct();
                product.fId = fId;
                product.fName = fName;
                product.fPrice = fPrice;
                product.fImg = fileName;

                db.tProduct.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        //00-5-1 在HomeController裡撰寫GET及POST的Edit Action
        public ActionResult Edit(string fId)
        {
            var product = db.tProduct.Where(m => m.fId == fId).FirstOrDefault();

            return View(product);
        }


        [HttpPost]
        public ActionResult Edit(string fId, string fName, decimal fPrice, HttpPostedFileBase fImg,string oldImg)
        {
            try
            {
                string fileName = "";

                if (fImg != null)
                {
                    //檔案上傳
                    if (fImg.ContentLength > 0)
                    {
                        //取得圖檔名稱
                        fileName = Path.GetFileName(fImg.FileName);
                        fImg.SaveAs(Server.MapPath("~/images/" + fileName));
                    }
                }
                else
                {
                    fileName = oldImg; //若無上傳圖檔，則指定hidden隱藏欄位的資料
                }


                //tProduct product = new tProduct();
                var product = db.tProduct.Where(m => m.fId == fId).FirstOrDefault();
                product.fId = fId;
                product.fName = fName;
                product.fPrice = fPrice;
                product.fImg = fileName;

                db.SaveChanges();

                return RedirectToAction("Index");//導向Index的Action方法
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

    }
}


//00-1 利用Entity Framework建立Model(DB First)
//00-1-1 建立dbProduct.mdb資料庫Model
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型,名稱輸入"dbProductModel",按新增
//       來自資料庫的EF Designer
//       連接dbProduct.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表勾選"tProuct", 按完成鈕
//       若跳出詢問方法按確定鈕
//00-1-2 在專案上按右鍵,建置
//00-1-3 在tProduct.cs裡加入欄位名稱顯示(需using System.ComponentModel)
//00-1-4 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//00-1-5 指定控制器名稱為HomeController,並開啟HomeController
//00-1-6 using _MVC2.Models
//00-1-7 使用Entity建立DB物件

//00-2 製作Index頁面的顯示所有產品功能
//00-2-1 在HomeController裡撰寫Index的Action
//00-2-2 在public ActionResult Index()上按右鍵,新增檢視,建立Index View
//00-2-3 進行下列設定:
//       View name:Index
//       Template:List
//       Model class:tProduct(MVC2.Models)
//       Data context class:dbProductEntities(MVC2.Models)
//       勾選Use a layout pages
//       按下Add
//00-2-4 修改 _Layout.cshtml
//00-2-5 修改Index View,英文文字為中文
//00-2-6 修改圖片顯示處
//00-2-7 修改功能連結處 (id=>fId) 

//00-3 製作刪除功能
//00-3-1 在HomeController裡撰寫Delete的Action


//00-4 製作Create頁面及新增產品功能
//00-4-1 在HomeController裡撰寫GET及POST的Create Action
//00-4-2 在public ActionResult Create()上按右鍵,新增檢視,建立Create View
//00-4-3 進行下列設定:
//       View name:Create
//       Template:Create
//       Model class:tProduct(MVC2.Models)
//       Data context class:dbProductEntities(MVC2.Models)
//       勾選Use a layout pages
//       按下Add
//00-4-4 修改Index View,英文文字為中文
//00-4-5 修改form的HTML Helper為一般的from
//00-4-6 修改圖片上傳處表單
//00-4-7 加入例外訊息顯示處

//00-5 製作Edit頁面及修改產品功能
//00-5-1 在HomeController裡撰寫GET及POST的Edit Action
//00-5-2 在public ActionResult Edit()上按右鍵,新增檢視,建立Edit View
//00-5-3 進行下列設定:
//       View name:Edit
//       Template:Edit
//       Model class:tStudent(_00MVC.Models)
//       Data context class:dbProductEntities(_00MVC.Models)
//       勾選Use a layout pages
//       按下Add
//00-5-4 修改Edit View,英文文字為中文
//00-5-5 修改form的HTML Helper為一般的from
//00-5-6 修改圖片上傳處表單
//00-5-7 加入錯誤訊息顯示處

//00-5 執行與測試
