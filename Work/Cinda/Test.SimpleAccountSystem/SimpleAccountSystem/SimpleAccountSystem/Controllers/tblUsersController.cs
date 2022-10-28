using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using SimpleAccountSystem.Models;
using SimpleAccountSystem.Models.DTO;

namespace SimpleAccountSystem.Controllers
{
    public class tblUsersController : Controller
    {
        private Account db = new Account();

        // POST: tblUsers/Search
        [HttpPost, ActionName("Search")]
        public ActionResult Index(string searchString)
        {
            if (searchString != "")
            {

                var UserDetailsView = (from t1 in db.tblUser.AsNoTracking()
                                       join t2 in db.tblUserGroup.AsNoTracking() on t1.cAccount equals t2.cAccount
                                       join t3 in db.tblGroup.AsNoTracking() on t2.cGroupID equals t3.cGroupID
                                       where t1.cName.Contains(searchString) || t3.cGroupName.Contains(searchString)
                                       select new { t1, t2, t3 }
                                ).ToList();

                // Group cAccount

                var GroupAccount = UserDetailsView.GroupBy(x => new
                {
                    x.t1.cAccount,
                    x.t1.cName,
                    x.t1.cEmail,
                    x.t1.cStatus
                }).Select(x => new UserDetails
                {
                    cAccount = x.Key.cAccount,
                    cName = x.Key.cName,
                    cEmail = x.Key.cEmail,
                    cStatus = x.Key.cStatus

                }).ToList();

                // Set Response
                List<UserDetails> UserDetailsList = new List<UserDetails>();

                foreach (var User in GroupAccount)
                {
                    UserDetails UserDetail = new UserDetails();
                    UserDetail.cAccount = User.cAccount;
                    UserDetail.cName = User.cName;
                    UserDetail.cEmail = User.cEmail;
                    UserDetail.cStatus = User.cStatus;

                    // Group GroupNames

                    var ViewList = (from t1 in db.tblUser.AsNoTracking()
                                    join t2 in db.tblUserGroup.AsNoTracking() on t1.cAccount equals t2.cAccount
                                    join t3 in db.tblGroup.AsNoTracking() on t2.cGroupID equals t3.cGroupID
                                    where t1.cAccount == User.cAccount
                                    select new { t1, t2, t3 }).ToList();

                    for (var i = 0; i < ViewList.Count; i++)
                    {
                        UserDetail.cGroupNames += ViewList[i].t3.cGroupName;
                        if (i != ViewList.Count - 1)
                        {
                            UserDetail.cGroupNames += "、";
                        }

                    }

                    UserDetailsList.Add(UserDetail);
                };


                return View("Index", UserDetailsList);

            }
            else
            {

                return RedirectToAction("Index");
            }
        }

        // GET: tblUsers
        public ActionResult Index()
        {

            // LINQ
            var UserDetailsView = (
                from t1 in db.tblUser.AsNoTracking()
                join t2 in db.tblUserGroup.AsNoTracking() on t1.cAccount equals t2.cAccount
                join t3 in db.tblGroup.AsNoTracking() on t2.cGroupID equals t3.cGroupID
                select new { t1, t2, t3 }).AsQueryable();
            // Using DTO
            List<UserDetails> UserDetailsList = new List<UserDetails>();

            foreach (var User in db.tblUser)
            {
                UserDetails UserDetail = new UserDetails();
                UserDetail.cAccount = User.cAccount;
                UserDetail.cName = User.cName;
                UserDetail.cEmail = User.cEmail;
                UserDetail.cStatus = User.cStatus;

                // Group GroupNames
                var ViewList = UserDetailsView.Where(x => x.t1.cAccount == User.cAccount).ToList();
                for (var i = 0; i < ViewList.Count; i++)
                {
                    UserDetail.cGroupNames += ViewList[i].t3.cGroupName;
                    if (i != ViewList.Count - 1)
                    {
                        UserDetail.cGroupNames += "、";
                    }
                }

                UserDetailsList.Add(UserDetail);
            };

            return View(UserDetailsList);
        }

        // GET: tblUsers/Edit/5
        public ActionResult Edit(string id, string Message)
        {
            // Checked Update or Create
            if (id != null) { ViewBag.Mode = "編輯"; } else { ViewBag.Mode = "申請"; }

            ViewBag.Message = "<script>" + Message + "</script>";

            UserDetails UserDetails = new UserDetails();

            // SetShareData (DropDownList - cStatus String / cGroupNames )
            var GroupList = db.tblGroup.ToList();

            if (id == null) // CreatView
            {
                // cGroupNames
                List<CheckBoxListInfo> GroupInfos = new List<CheckBoxListInfo>();
                foreach (var item in GroupList)
                {
                    GroupInfos.Add(
                    new CheckBoxListInfo
                    {
                        Value = item.cGroupID,
                        DisplayText = item.cGroupName,
                        IsChecked = ""
                    });
                }

                ViewBag.GroupInfos = GroupInfos;

                // cStatus String
                List<BoolDropdownList> StatusInfos = new List<BoolDropdownList>();
                StatusInfos.Add(
                    new BoolDropdownList
                    {
                        Value = 1,
                        DisplayText = "啟用"

                    });
                StatusInfos.Add(
                    new BoolDropdownList
                    {
                        Value = 0,
                        DisplayText = "停用"
                    });

                ViewBag.StatusInfos = StatusInfos;


                return View(UserDetails);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            { // EditView

                var UserDetailsList = (
                from t1 in db.tblUser.AsNoTracking()
                join t2 in db.tblUserGroup.AsNoTracking() on t1.cAccount equals t2.cAccount into t4
                from t2 in t4.DefaultIfEmpty()
                join t3 in db.tblGroup.AsNoTracking() on t2.cGroupID equals t3.cGroupID into t5
                from t3 in t5.DefaultIfEmpty()
                where t1.cAccount == id
                select new { t1, t2, t3 }).ToList();

                if (UserDetailsList == null)
                {
                    return HttpNotFound();
                }
                else
                {

                    UserDetails = new UserDetails
                    {
                        cAccount = UserDetailsList[0].t1.cAccount,
                        cName = UserDetailsList[0].t1.cName,
                        cEmail = UserDetailsList[0].t1.cEmail
                    };


                    // cGroupNames
                    List<CheckBoxListInfo> GroupInfos = new List<CheckBoxListInfo>();
                    foreach (var item in GroupList)
                    {
                        var UserGroupList = db.tblUserGroup.Where(x => x.cAccount == UserDetails.cAccount).ToList();
                        var Filter = UserGroupList.Where(x => x.cGroupID == item.cGroupID).ToList();

                        if (Filter.Any())
                        {
                            GroupInfos.Add(
                            new CheckBoxListInfo
                            {
                                Value = item.cGroupID,
                                DisplayText = item.cGroupName,
                                IsChecked = "checked"
                            });
                        }
                        else
                        {// default - User has no Group

                            GroupInfos.Add(
                            new CheckBoxListInfo
                            {
                                Value = item.cGroupID,
                                DisplayText = item.cGroupName,
                                IsChecked = ""
                            });
                        }

                    }

                    ViewBag.GroupInfos = GroupInfos;

                    // cStatus String
                    var UseItem = UserDetailsList.FirstOrDefault();
                    List<BoolDropdownList> StatusInfos = new List<BoolDropdownList>();
                    if (UseItem.t1.cStatus == 1)
                    {
                        StatusInfos.Add(
                            new BoolDropdownList
                            {
                                Value = 1,
                                DisplayText = "啟用"
                            });
                    }
                    else if (UseItem.t1.cStatus == 0)
                    {
                        StatusInfos.Add(
                            new BoolDropdownList
                            {
                                Value = 0,
                                DisplayText = "停用"
                            });

                    }

                    ViewBag.StatusInfos = StatusInfos;

                    return View(UserDetails);
                }
            }

        }

        // POST: tblUsers/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cAccount,cName,cEmail,cStatus,cGroupNames,")] UserDetails UserDetails, string Mode)
        {
            var Message = "";

            if (ModelState.IsValid)
            {
                if (Mode == "申請")
                {
                    // Account Unique
                    var checkUnique = db.tblUser.Where(x => x.cAccount == UserDetails.cAccount).ToList();
                    if (checkUnique.Any())
                    {
                        Message = "alert('帳號重複');";
                        return RedirectToAction("Edit", new { id = UserDetails.cAccount, Message = Message });
                    }
                    else
                    {// Create Account
                        tblUser tblUser = new tblUser
                        {
                            cAccount = UserDetails.cAccount,
                            cName = UserDetails.cName,
                            cEmail = UserDetails.cEmail,
                            cStatus = UserDetails.cStatus,
                        };
                        db.tblUser.Add(tblUser);

                        if (UserDetails.cGroupNames != null)
                        {

                            var cGroupNamesList = UserDetails.cGroupNames.Split(',');
                            foreach (var cGroupName in cGroupNamesList)
                            {

                                if (cGroupName != "")
                                {
                                    tblUserGroup tblUserGroup = new tblUserGroup
                                    {
                                        cAccount = UserDetails.cAccount,
                                        cGroupID = Int32.Parse(cGroupName)
                                    };
                                    db.tblUserGroup.Add(tblUserGroup);
                                }
                            }

                        }

                        db.SaveChanges();

                        Message = "alert('註冊成功');";

                        return RedirectToAction("Edit", new { id = UserDetails.cAccount, Message = Message });
                    }
                }
                else if (Mode == "編輯")
                {// Update Account

                    var UserList = db.tblUser.Where(x => x.cAccount == UserDetails.cAccount).FirstOrDefault();
                    UserList.cName = UserDetails.cName;
                    UserList.cEmail = UserDetails.cEmail;
                    UserList.cStatus = UserDetails.cStatus;


                    var DeletetblUserGroup = db.tblUserGroup.Where(x => x.cAccount == UserDetails.cAccount).ToList();
                    db.tblUserGroup.RemoveRange(DeletetblUserGroup);
                    db.SaveChanges();

                    if (UserDetails.cGroupNames != null)
                    {
                        var cGroupNamesList = UserDetails.cGroupNames.Split(',');
                        foreach (var cGroupName in cGroupNamesList)
                        {

                            if (cGroupName != "")
                            {

                                tblUserGroup tblUserGroup = new tblUserGroup
                                {
                                    cAccount = UserDetails.cAccount,
                                    cGroupID = Int32.Parse(cGroupName)
                                };
                                db.tblUserGroup.Add(tblUserGroup);
                            }
                        }
                    }

                    db.SaveChanges();
                    Message = "alert('編輯成功');";

                    return RedirectToAction("Edit", new { id = UserDetails.cAccount, Message = Message });
                }
            }

            // SetShareData (DropDownList - cStatus String / cGroupNames )
            var GroupList = db.tblGroup.ToList();

            // cGroupNames
            List<CheckBoxListInfo> GroupInfos = new List<CheckBoxListInfo>();
            foreach (var item in GroupList)
            {
                GroupInfos.Add(
                new CheckBoxListInfo
                {
                    Value = item.cGroupID,
                    DisplayText = item.cGroupName,
                    IsChecked = ""
                });
            }

            ViewBag.GroupInfos = GroupInfos;

            // cStatus String
            List<BoolDropdownList> StatusInfos = new List<BoolDropdownList>();
            StatusInfos.Add(
                new BoolDropdownList
                {
                    Value = 1,
                    DisplayText = "啟用"

                });
            StatusInfos.Add(
                new BoolDropdownList
                {
                    Value = 0,
                    DisplayText = "停用"
                });

            ViewBag.StatusInfos = StatusInfos;

            return View(UserDetails);
        }

        // GET: tblUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tblUser = db.tblUser.Find(id);
            if (tblUser == null)
            {
                return HttpNotFound();
            }
            return View(tblUser);
        }

        // POST: tblUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var DeletetblUserGroup = db.tblUserGroup.Where(x => x.cAccount == id).ToList();
            db.tblUserGroup.RemoveRange(DeletetblUserGroup);
            db.SaveChanges();

            tblUser tblUser = db.tblUser.Find(id);
            db.tblUser.Remove(tblUser);
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
