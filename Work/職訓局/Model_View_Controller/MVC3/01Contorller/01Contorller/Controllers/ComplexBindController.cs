using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _01Contorller.Models;

namespace _01Contorller.Controllers
{
    public class ComplexBindController : Controller
    {
        // GET: ComplexBind
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //02-3-6 建立GET與POST的Create方法
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            ViewBag.PId = p.PId;
            ViewBag.PName = p.PName;
            ViewBag.Price = p.Price;

            return View();
        }


    }
}