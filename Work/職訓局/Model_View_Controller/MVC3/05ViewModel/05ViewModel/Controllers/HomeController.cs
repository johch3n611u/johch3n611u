using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//06-2-3 using _06ViewModel.Models及using _06ViewModel.ViewModel
using _05ViewModel.Models;
using _05ViewModel.ViewModel;

namespace _05ViewModel.Controllers
{
    public class HomeController : Controller
    {
        //06-2-4 於HomeController建立DB物件
        dbEmployeeEntities db = new dbEmployeeEntities();

        // GET: Home
        //06-2-5 編輯ActionResult Index()的內容
        public ActionResult Index(int id=1)
        {
            CVM cvm = new CVM()
            {
                department = db.tDepartment.ToList(),
                employee = db.tEmployee.Where(m => m.fDepId == id).ToList()
            };


            return View(cvm);
        }

        //06-2-6 於HomeController建立GET與POST的Create Action
        public ActionResult Create()
        {

            return View(db.tDepartment.ToList());
        }
      
        [HttpPost]
        public ActionResult Create(tEmployee emp)
        {
            db.tEmployee.Add(emp);
            db.SaveChanges();


            return RedirectToAction("Index",new { id=emp.fDepId});
        }

        public ActionResult Delete(string id)
        {
            var emp = db.tEmployee.Where(m=>m.fEmpId==id).FirstOrDefault();

            db.tEmployee.Remove(emp);
            db.SaveChanges();


            return RedirectToAction("Index", new { id = emp.fDepId });
        }

    }
}

//06-1 建立ViewModel
//06-1-1 建立dbEmployee.mdb資料庫Model
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型
//       來自資料庫的EF Designer
//       連接dbEmployee.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表"全選", 按完成鈕
//       若跳出詢問方法按確定鈕
//06-1-2 在專案上按右鍵,建置
//06-1-3 加入ViewModel資料夾
//06-1-4 using _06ViewModel.Models
//06-1-5 建立tDepartment和tEmployee的List物件

//06-2 建立HomeController
//06-2-1 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//06-2-2 指定控制器名稱為HomeController,並開啟HomeController
//06-2-3 using _06ViewModel.Models及using _06ViewModel.ViewModel
//06-2-4 於HomeController建立DB物件
//06-2-5 編輯ActionResult Index()的內容
//06-2-6 於HomeController建立GET與POST的Create Action


//06-3 建立各個View
//06-3-1 在public ActionResult Index()上按右鍵,新增檢視,建立Index View
//06-3-2 進行下列設定:
//       View name:Index
//       Template:Empty (Without model)
//       勾選Use a layout pages
//       按下Add
//06-3-3 在最上方加上@model _06ViewModel.ViewModel.CVMDepEmp

//06-3-5 在Index View中撰寫顯示畫面
//06-3-6 執行及測試
//06-3-7 在public ActionResult Create()上按右鍵,新增檢視,建立CreateView
//06-3-8 進行下列設定:
//       View name:Create
//       Template:Empty (Without model)
//       勾選Use a layout pages
//       按下Add
//06-3-9 加入給下拉選單用的資料
//06-3-10 將英文字改為中文字
//06-3-11 建立員工資料新增表單
//06-3-12 執行及測試