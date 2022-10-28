using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _01Contorller.Controllers
{
    public class SimpleBindController : Controller
    {
        // GET: SimpleBind
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //02-2-3 建立GET與POST的Create方法
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(string PId, string PName, int Price)
        {
            ViewBag.PId = PId;
            ViewBag.PName = PName;
            ViewBag.Price = Price;

            return View();
        }
    }
}