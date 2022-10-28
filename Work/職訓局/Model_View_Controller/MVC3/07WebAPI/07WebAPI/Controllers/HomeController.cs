using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using _07WebAPI.Models;

namespace _07WebAPI.Controllers
{
    public class HomeController : Controller
    {
        教務系統Entities db = new 教務系統Entities();
        // GET: Home
        public ActionResult Index()
        {

            return View(db.學生.ToList());
        }
    }
}