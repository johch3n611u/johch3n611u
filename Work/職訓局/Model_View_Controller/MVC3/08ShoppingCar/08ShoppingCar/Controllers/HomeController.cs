using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//01-1-3 using _08ShoppingCar.Models
using _08ShoppingCar.Models;

namespace _08ShoppingCar.Controllers
{
    public class HomeController : Controller
    {
        //01-1-4 使用Entity建立DB物件
        dbShoppingCarEntities db = new dbShoppingCarEntities();

        //01-2-1 在HomeController裡撰寫Index Action
        public ActionResult Index()
        {
            var products = db.tProduct.ToList();

            //若Session["Member"]為空，表示會員未登入
            if (Session["Member"] == null)
            {
                //指定Index.cshtml套用_Layout.cshtml，View使用products模型
                return View("Index","_Layout",products);
            }
            //會員登入狀態,指定Index.cshtml套用_LayoutMember.cshtml，View使用products模型
            return View("Index", "_LayoutMember", products);
        }

        //03-1-1 在HomeController裡分別撰寫Get與Post Login Action
        public ActionResult Login()
        {
            return View();
        }
        //03-1-1 在HomeController裡分別撰寫Get與Post Login Action
        [HttpPost]
        public ActionResult Login(string fUserId, string fPwd)
        {
            var member = db.tMember.Where(m => m.fUserId == fUserId && m.fPwd == fPwd).FirstOrDefault();
            if(member==null)
            {
                ViewBag.Message = "帳號或密碼錯誤!!";
                return View();
            }
            Session["WelCome"] = member.fName + "真正高興地見到你!";
            Session["Member"] = member;

            return RedirectToAction("Index");
        }

        //05-1-1 在HomeController裡撰寫Logout Action
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        //04-1-1 在HomeController裡分別撰寫Get與Post Register Action
        public ActionResult Register()
        {

            return View();
        }
        //04-1-1 在HomeController裡分別撰寫Get與Post Register Action
        [HttpPost]
        public ActionResult Register(tMember Member)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }

            var member = db.tMember.Where(m => m.fUserId == Member.fUserId).FirstOrDefault();
            if(member==null)
            {
                db.tMember.Add(Member);
                db.SaveChanges();

                return RedirectToAction("Login");
            }
            ViewBag.Message = "帳號已有人使用!!";
            return View();

        }

        //06-1-1 在HomeController裡撰寫ShoppingCar Action
        public ActionResult ShoppingCar()
        {
            string fUserId = (Session["Member"] as tMember).fUserId;
            var orderDetails = db.tOrderDetail.Where(m=>m.fUserId== fUserId && m.fIsApproved=="否").ToList();

           
            return View("ShoppingCar", "_LayoutMember", orderDetails);
        }

        //06-3-1 在HomeController裡撰寫AddCar Action
        public ActionResult AddCar(string fPId)
        {
            string fUserId = (Session["Member"] as tMember).fUserId;

            //如果該商品已放在購物車中,則數量加1,若未在購物車中,則放入後預設數量為1
            var currentCar = db.tOrderDetail.Where(m => m.fUserId == fUserId && m.fIsApproved == "否" && m.fPId == fPId).FirstOrDefault();
            if(currentCar==null)
            {
                var product = db.tProduct.Where(m => m.fPId == fPId).FirstOrDefault();

                tOrderDetail orderDetail = new tOrderDetail();
                orderDetail.fUserId = fUserId;
                orderDetail.fPId = fPId;
                orderDetail.fQty = 1;
                orderDetail.fPrice = product.fPrice;
                orderDetail.fName = product.fName;
                orderDetail.fIsApproved = "否";
                db.tOrderDetail.Add(orderDetail);

            }
            else
            {
                currentCar.fQty += 1;
            }
            db.SaveChanges();


            return RedirectToAction("ShoppingCar");
        }

        //06-3-2 在HomeController裡撰寫DeleteCar Action
        public ActionResult DeleteCar(int fId)
        {

            var orderDetails = db.tOrderDetail.Where(m => m.fId == fId).FirstOrDefault();
            db.tOrderDetail.Remove(orderDetails);
            db.SaveChanges();

            return RedirectToAction("ShoppingCar");
        }

        //07-1-2 在HomeController裡撰寫Post ShoppingCar Action,將購物車狀態之商品轉成訂單
        [HttpPost]
        public ActionResult ShoppingCar(string fReceiver, string fEmail, string fAddress)
        {
            string fUserId = (Session["Member"] as tMember).fUserId;
            //string guid = Guid.NewGuid().ToString();  //產生一組隨機十六進位碼
            //以目前的日期時間加上隨機4碼數字做為訂單編號
            Random r = new Random();
            string guid = DateTime.Now.ToString().Replace("/","").Replace(":","").Replace(" ","").Replace("上午","").Replace("下午", "")+r.Next(1000,10000);

            //建立訂單主檔資料
            tOrder order = new tOrder();
            order.fOrderGuid = guid;
            order.fUserId = fUserId;
            order.fReceiver = fReceiver;
            order.fEmail = fEmail;
            order.fAddress = fAddress;
            order.fDate = DateTime.Now;
            db.tOrder.Add(order);

            var carList = db.tOrderDetail.Where(m => m.fUserId == fUserId && m.fIsApproved == "否").ToList();
            foreach(var item in carList)
            {
                item.fOrderGuid = guid;
                item.fIsApproved = "是";
            }
            db.SaveChanges();

            return RedirectToAction("OrderList");
        }

        //07-2-1 在HomeController裡撰寫OrderList Action
        public ActionResult OrderList()
        {
            string fUserId = (Session["Member"] as tMember).fUserId;
            var orders = db.tOrder.Where(m => m.fUserId == fUserId).OrderByDescending(m=>m.fDate).ToList();


            return View("OrderList", "_LayoutMember", orders);
        }

        public ActionResult OrderDetail(string fOrderGuid)
        {
           
            var orderDetails = db.tOrderDetail.Where(m => m.fOrderGuid == fOrderGuid).ToList();


            return View("OrderDetail", "_LayoutMember", orderDetails);
        }
    }
}


//00-1 利用Entity Framework建立Model(DB First)
//00-1-1 建立dbShoppingCar.mdb資料庫Model
//       在Models上按右鍵,選擇加入,新增項目,資料,ADO.NET實體資料模型,名稱輸入"dbShoppingCarModel",按新增
//       來自資料庫的EF Designer
//       連接dbShoppingCar.mdf資料庫,連線名稱不修改,按下一步按鈕
//       選擇Entity Framework 6.x, 按下一步按鈕
//       資料表全選, 按完成鈕
//       若跳出詢問方法按確定鈕
//00-1-2 在專案上按右鍵,建置
//00-2 修改Model
//00-2-1 在tMember.cs裡加入欄位名稱顯示(需using System.ComponentModel)與欄位驗證(需using System.ComponentModel.DataAnnotations)
//00-2-2 在tProduct.cs裡加入欄位名稱顯示(需using System.ComponentModel)
//00-2-3 在tOrder.cs裡加入欄位名稱顯示(需using System.ComponentModel)與欄位驗證(需using System.ComponentModel.DataAnnotations)
//00-2-4 在tOrderDetail.cs裡加入欄位名稱顯示(需using System.ComponentModel)


//01-1 建立HomeController
//01-1-1 在Controllers資料夾上按右鍵,加入,控制器,選擇 MVC5Controller-Empty
//01-1-2 指定控制器名稱為HomeController,並開啟HomeController
//01-1-3 using _08ShoppingCar.Models
//01-1-4 使用Entity建立DB物件
//01-2 製作Index頁面的顯示所有產品功能
//01-2-1 在HomeController裡撰寫Index Action
//01-2-2 在public ActionResult Index()上按右鍵,新增檢視,建立Index View
//01-2-3 進行下列設定:
//       View name:Index
//       Template:Empty(Without Model)
//       勾選Use a layout pages
//       按下Add
//01-2-4 撰寫Index View,讀出資料,並以Bootstrap排版


//02-1 修改Layout
//02-1-1 修改Views/Shared/_Layout.cshtml
//02-1-2 更改標頭與頁尾
//02-1-3 修改Brand
//02-1-4 加入選單
//02-2 建立Layout
//02-2-1 在View/Shared上按右鍵,新增檢視
//02-2-2 進行下列設定:
//       View name:_LayoutMember.cshtml
//       Template:Empty(Without Model)
//       不勾選Use a layout pages
//       按下Add
//02-2-3 將_Layout.cshtml內容複製至_LayoutMember.cshtml
//02-2-4 修改_LayoutMember.cshtml nav class內容
//02-2-5 修改Brand
//02-2-6 修改_LayoutMember.cshtml選單內容


//03-1 撰寫登入功能
//03-1-1 在HomeController裡分別撰寫Get與Post Login Action
//03-1-2 在public ActionResult Login()上按右鍵,新增檢視,建立Login View
//03-1-3 進行下列設定:
//       View name:Login
//       Template:Empty(Without Model)
//       勾選Use a layout pages
//       按下Add
//03-1-4 撰寫Login View


//04-1 撰寫會員註冊功能
//04-1-1 在HomeController裡分別撰寫Get與Post Register Action
//04-1-2 在public ActionResult Register()上按右鍵,新增檢視,建立Register View
//04-1-3 進行下列設定:
//       View name:Register
//       Template:Create
//       Model class:tMember(_08ShoppingCar.Models)
//       Data context class:dbShoppingCarEntities(_08ShoppingCar.Models)
//       勾選Use a layout pages
//       按下Add
//04-1-4 修改Register View, 改中文字, 加入@ViewBag.Message, 刪除Back to List


//05-1 撰寫會員登出功能
//05-1-1 在HomeController裡撰寫Logout Action

//06-1 撰寫購物車功能
//06-1-1 在HomeController裡撰寫ShoppingCar Action
//06-1-2 在public ActionResult ShoppingCar()上按右鍵,新增檢視,建立ShoppingCar View
//06-1-3 進行下列設定:
//       View name:ShoppingCar
//       Template:List
//       Model class:tOrderDetail(_08ShoppingCar.Models)
//       Data context class:dbShoppingCarEntities(_08ShoppingCar.Models)
//       勾選Use a layout pages
//       按下Add
//06-2 修改ShoppingCar View
//06-2-1 修改英文字為中文
//06-2-2 刪除Create連結功能
//06-2-3 修改最後一欄為刪除所選商品功能
//06-3 撰寫加入商品至購物車與從購物車刪除商品功能
//06-3-1 在HomeController裡撰寫AddCar Action
//06-3-2 在HomeController裡撰寫DeleteCar Action


//07-1 建立加入訂單功能
//07-1-1 在ShoppingCar View裡加入確認訂單的畫面
//07-1-2 在HomeController裡撰寫Post ShoppingCar Action,將購物車狀態之商品轉成訂單
//07-2 建立訂單列表功能
//07-2-1 在HomeController裡撰寫OrderList Action
//07-2-2 在public ActionResult OrderList()上按右鍵,新增檢視,建立OrderList View
//07-2-3 進行下列設定:
//       View name:OrderList
//       Template:List
//       Model class:tOrder(_08ShoppingCar.Models)
//       Data context class:dbShoppingCarEntities(_08ShoppingCar.Models)
//       勾選Use a layout pages
//       按下Add
//07-3 修改OrderList View
//07-3-1 修改英文字為中文
//07-3-2 刪除Create連結功能
//07-3-3 修改最後一欄為查看訂單明細功能
//07-4 建立查看訂單明細功能
//07-4-1 在HomeController裡撰寫OrderDetail Action
//07-4-2 在public ActionResult OrderDetail()上按右鍵,新增檢視,建立OrderDetail View
//07-4-3 進行下列設定:
//       View name:OrderDetail
//       Template:List
//       Model class:tOrderDetail(_08ShoppingCar.Models)
//       Data context class:dbShoppingCarEntities(_08ShoppingCar.Models)
//       勾選Use a layout pages
//       按下Add
//07-5 修改OrderDetail View
//07-5-1 修改英文字為中文
//07-5-2 刪除Create連結功能
//07-5-3 將最後一欄刪除