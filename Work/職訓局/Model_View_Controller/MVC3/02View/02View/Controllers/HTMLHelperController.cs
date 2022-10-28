using _02View.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _02View.Controllers
{
    public class HTMLHelperController : Controller
    {
        // GET: HTMLHelper
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //03-6-6 建立GET與POST的Create方法
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Member member)
        {
            string msg = "註冊資料:<hr />帳號:"+member.UserId+"<br />姓名:"+member.Name+"<br />密碼:"+member.Pwd+"<br />信箱:"+member.Email+"<br />生日:"+member.Birthday+"<br />";
            ViewBag.Msg = msg;
            return View();
        }

    }
}