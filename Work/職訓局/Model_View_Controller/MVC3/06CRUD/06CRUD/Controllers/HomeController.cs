using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//00-1-6 using _06CRUD.Models;
using _06CRUD.Models;

namespace _06CRUD.Controllers
{
    public class HomeController : Controller
    {
        //00-1-7 使用Entity建立DB物件
        dbProductEntities db = new dbProductEntities();
        // GET: Home
        //00-2-1 在HomeController裡撰寫Index的Action
        public ActionResult Index()
        {
            return View(db.tProduct.ToList());
        }

        //00-5-1 在HomeController裡撰寫GET及POST的Create Action
        public ActionResult Delete(string id)
        {
            var products = db.tProduct.Where(m => m.fId == id).FirstOrDefault();
            string fileName = products.fImg;
            System.IO.File.Delete(Server.MapPath("~/images/"+fileName));
            db.tProduct.Remove(products);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        //00-5-1 在HomeController裡撰寫GET及POST的Edit Action
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string fId, string fName, decimal fPrice, HttpPostedFileBase fImg)
        {
            try
            {
                //處理商品圖片檔
                string fileName = "";
                string subname = System.IO.Path.GetExtension(fImg.FileName);
                if (subname == ".jpg" || subname==".png")
                {
                    if(fImg.ContentLength>0)
                    {
                        fileName = System.IO.Path.GetFileName(fImg.FileName);
                        fImg.SaveAs(Server.MapPath("~/images/"+ fileName));
                    }
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


        public ActionResult Edit(string id)
        {

            return View(db.tProduct.Where(m=>m.fId==id).FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string fId, string fName, decimal fPrice, HttpPostedFileBase fImg,string oldImg)
        {
            try
            {
                
                string fileName = "";

                if (fImg != null)
                {
                    //處理商品圖片檔
                    string subname = System.IO.Path.GetExtension(fImg.FileName);
                    if (subname == ".jpg" || subname == ".png")
                    {
                        if (fImg.ContentLength > 0)
                        {
                            System.IO.File.Delete(Server.MapPath("~/images/" + oldImg));

                            fileName = System.IO.Path.GetFileName(fImg.FileName);
                            fImg.SaveAs(Server.MapPath("~/images/" + fileName));
                        }
                    }

                }
                else
                {
                    fileName = oldImg;
                }
                //tProduct product = new tProduct();
                //product.fId = fId;
                var product= db.tProduct.Where(m => m.fId == fId).FirstOrDefault();

                product.fName = fName;
                product.fPrice = fPrice;
                product.fImg = fileName;

                //db.tProduct.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");

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
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型,名稱輸入"ProductModel",按新增
//       來自資料庫的EF Designer
//       連接dbProduct.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表勾選"tProuct", 按完成鈕
//       若跳出詢問方法按確定鈕
//00-1-2 在專案上按右鍵,建置
//00-1-3 在tProduct.cs裡加入欄位名稱顯示(需using System.ComponentModel)
//00-1-4 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//00-1-5 指定控制器名稱為HomeController,並開啟HomeController
//00-1-6 using _00MVC.Models
//00-1-7 使用Entity建立DB物件


//00-2 製作Index頁面的顯示所有產品功能
//00-2-1 在HomeController裡撰寫Index的Action
//00-2-2 在public ActionResult Index()上按右鍵,新增檢視,建立Index View
//00-2-3 進行下列設定:
//       View name:Index
//       Template:List
//       Model class:tStudent(_00MVC.Models)
//       Data context class:dbProductEntities(_00MVC.Models)
//       勾選Use a layout pages
//       按下Add
//00-2-5 修改Index View,英文文字為中文
//00-2-6 修改圖片顯示處

//00-3 製作刪除功能
//00-3-1 在HomeController裡撰寫Delete的Action

//00-4 製作Create頁面及新增產品功能
//00-4-1 在HomeController裡撰寫GET及POST的Create Action
//00-4-2 在public ActionResult Create()上按右鍵,新增檢視,建立Create View
//00-4-3 進行下列設定:
//       View name:Create
//       Template:Create
//       Model class:tStudent(_00MVC.Models)
//       Data context class:dbProductEntities(_00MVC.Models)
//       勾選Use a layout pages
//       按下Add
//00-4-4 修改Index View,英文文字為中文
//00-4-5 修改form的HTML Helper為一般的from
//00-4-6 修改圖片上傳處表單
//00-4-7 加入例外訊息顯示處