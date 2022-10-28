using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Member.Models;
using Member.Models.DTO;

namespace Member.Controllers
{
    public class tblSignupsController : Controller
    {
        private MemberDB db = new MemberDB();

        // GET: tblSignups
        public ActionResult Index()
        {

            var AllView = (
                 from t1 in db.tblSignup.AsNoTracking()
                 join t2 in db.tblSignupItem.AsNoTracking() on t1.cMobile equals t2.cMobile
                 join t3 in db.tblActiveItem.AsNoTracking() on t2.cItemID equals t3.cItemID
                 select new { t1, t2, t3 }).AsQueryable();

            // DTO
            List<Mix> MixList = new List<Mix>();

            foreach (var item in db.tblActiveItem)
            {
                //Group 活動名稱、報名人數 及 詳細頁
                Mix Mix = new Mix();
                Mix.cItemName = item.cItemName;

                var JoinCount = AllView.Where(x => x.t3.cItemID == item.cItemID).GroupBy(x => x.t1.cMobile).Count();
                Mix.JoinCount = JoinCount;

                Mix.cActiveDt = item.cActiveDt;

                Mix.cItemID = item.cItemID;

                MixList.Add(Mix);
            };

            return View(MixList);
        }

        // GET: tblSignups/Details/5
        public ActionResult Details(int? cItemID)
        {
            if (cItemID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var AllView = (
               from t1 in db.tblSignup.AsNoTracking()
               join t2 in db.tblSignupItem.AsNoTracking() on t1.cMobile equals t2.cMobile
               join t3 in db.tblActiveItem.AsNoTracking() on t2.cItemID equals t3.cItemID
               where t3.cItemID == cItemID
               select new { t1, t2, t3 }).ToList();

            if (AllView == null)
            {
                return HttpNotFound();
            }

            // DTO
            List<Mix> MixList = new List<Mix>();

            foreach (var item in AllView)
            {
                //Group 詳細頁 報名人員、姓名、手機、報名時間
                Mix Mix = new Mix();
                Mix.cName = item.t1.cName;
                Mix.cMobile = item.t1.cMobile;
                Mix.cCreateDT = item.t1.cCreateDT;
                Mix.cItemID = item.t3.cItemID;
                Mix.tblSignupItem_ID = item.t2.ID;

                MixList.Add(Mix);
            };
         

            ViewBag.cItemID = AllView.FirstOrDefault().t3.cItemID;

            return View(MixList);
        }

        // GET: tblSignups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblSignups/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cMobile,cName,cEmail,cCreateDT")] tblSignup tblSignup)
        {
            if (ModelState.IsValid)
            {
                db.tblSignup.Add(tblSignup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblSignup);
        }

        // GET: tblSignups/Edit/5
        // 如果 tb1SignupItem_ID = null 則報名
        public ActionResult Edit(int? tblSignupItem_ID, int cItemID)
        {
            Mix Mix = new Mix();
            if (tblSignupItem_ID == 0)
            {
                Mix = new Mix();
                Mix.cCreateDT = DateTime.Now;
                Mix.cItemID = cItemID;
                Mix.tblSignupItem_ID = 0;

                return View(Mix);
            }

            var AllView = (
               from t1 in db.tblSignup.AsNoTracking()
               join t2 in db.tblSignupItem.AsNoTracking() on t1.cMobile equals t2.cMobile
               join t3 in db.tblActiveItem.AsNoTracking() on t2.cItemID equals t3.cItemID
               where t2.ID == tblSignupItem_ID
               select new { t1, t2, t3 }).FirstOrDefault();

            if (AllView == null)
            {
                return HttpNotFound();
            }

            // DTO

            //Group 詳細頁 報名人員、姓名、手機、報名時間
            Mix = new Mix();
            Mix.cName = AllView.t1.cName;
            Mix.cMobile = AllView.t1.cMobile;
            Mix.cEmail = AllView.t1.cEmail;
            Mix.cCreateDT = AllView.t1.cCreateDT;
            Mix.cItemID = AllView.t3.cItemID;
            Mix.tblSignupItem_ID = AllView.t2.ID;

            return View(Mix);
        }

        // POST: tblSignups/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tblSignupItem_ID,cItemID,cEmail,cName,cMobile,cCreateDT")] Mix Mix)
        {
            if (ModelState.IsValid)
            {
                if (Mix.tblSignupItem_ID != 0)
                {
                    //修改
                    if (db.tblSignupItem.Any(x => x.ID == Mix.tblSignupItem_ID))
                    {
                        var db_tblSignupItem = db.tblSignupItem.FirstOrDefault(x => x.ID == Mix.tblSignupItem_ID);

                        var db_tblSignup = db.tblSignup.FirstOrDefault(x => x.cMobile == db_tblSignupItem.cMobile);
                        db_tblSignup.cName = Mix.cName;
                        db_tblSignup.cEmail = Mix.cEmail;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

                if (db.tblSignup.Any(x => x.cMobile == Mix.cMobile))
                {
                    ViewBag.Message = "<script>alert('手機重複');</script>";
                    return View(Mix);
                }
                else
                {

                    //新增

                    tblSignup new_tblSignup = new tblSignup();
                    new_tblSignup.cName = Mix.cName;
                    new_tblSignup.cMobile = Mix.cMobile;
                    new_tblSignup.cEmail = Mix.cEmail;
                    new_tblSignup.cCreateDT = DateTime.Now;
                    db.tblSignup.Add(new_tblSignup);
                    db.SaveChanges();

                    tblSignupItem new_tblSignupItem = new tblSignupItem();
                    new_tblSignupItem.cItemID = Mix.cItemID;
                    new_tblSignupItem.cMobile = Mix.cMobile;
                    db.tblSignupItem.Add(new_tblSignupItem);

                    db.SaveChanges();
                    ViewBag.Message = "<script>alert('註冊成功');</script>";
                    return RedirectToAction("Index");

                }
            }
            return View(Mix);
        }
        // GET: tblSignups/Delete/5
        public ActionResult Delete(int tblSignupItem_ID)
        {
            if (tblSignupItem_ID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (db.tblSignupItem.Any(x => x.ID == tblSignupItem_ID))
            {

                tblSignupItem tblSignupItem = db.tblSignupItem.Find(tblSignupItem_ID);
                db.tblSignupItem.Remove(tblSignupItem);
                db.SaveChanges();
            }
            else {
                return HttpNotFound();
            }

            ViewBag.Message = "<script>alert('刪除成功');</script>";
            return RedirectToAction("Index");
        }

        // POST: tblSignups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            tblSignup tblSignup = db.tblSignup.Find(id);
            db.tblSignup.Remove(tblSignup);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
