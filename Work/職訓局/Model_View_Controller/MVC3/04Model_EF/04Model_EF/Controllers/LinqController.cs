using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//04-2-6 using _04EF.Models
using _04Model_EF.Models;

namespace _04Model_EF.Controllers
{
    public class LinqController : Controller
    {
        //04-2-7 於LinqController建立DB物件
        NorthwindEntities db = new NorthwindEntities();

        //04-2-10 建立一般方法Index() Action-查詢所有員工記錄
        public ActionResult Index()
        {
            //SQL DML
            //select * from 員工
            //Linq查詢運算式
            //var result = from E in db.員工
            //             select E;

            string show = "";
            //Linq擴充方法
            //用Lambda表示法撰寫
            var result = db.員工.ToList();


            return View(result);
        }

        //04-2-8 建立一般方法ShowEmployee()-查詢所有員工記錄
        public string ShowEmployee()
        {
            //SQL DML
            //select * from 員工
            //Linq查詢運算式
            //var result = from E in db.員工
            //             select E;

            string show = "";
            //Linq擴充方法
            //用Lambda表示法撰寫
            var result = db.員工;


            foreach (var E in result)
            {
                show += "工號:" + E.員工編號 + " 姓名:" + E.姓名 + " 職稱:" + E.職稱 + "<br />";
            }

            return show;
        }

    }
}