using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//04-3-8 using _04EF.Models
using _04Model_EF.Models;

namespace _04Model_EF.Controllers
{
    public class ValidationController : Controller
    {
        //04-3-9 於VilidationController建立DB物件,並撰寫Index、Create、Delete的Action
        dbStudentEntities db = new dbStudentEntities();

        // GET: Validation
        public ActionResult Index()
        {
            var student = db.tStudent.ToList();
            return View(student);
        }

        public ActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult Create(tStudent stu)
        {
            if (ModelState.IsValid)
            {
                db.tStudent.Add(stu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stu);
        }

        public ActionResult Delete(string id)
        {
            var stuID = db.tStudent.Where(s=>s.fStuId==id).FirstOrDefault();
            db.tStudent.Remove(stuID);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}