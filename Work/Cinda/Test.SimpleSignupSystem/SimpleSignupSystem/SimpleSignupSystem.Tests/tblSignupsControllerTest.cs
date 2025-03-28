// <copyright file="tblSignupsControllerTest.cs">Copyright (C)  2020</copyright>
using System;
using System.Web.Mvc;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleSignupSystem.Controllers;
using SimpleSignupSystem.Models.DTO;
using SimpleSignupSystem.Models.Entity;

namespace SimpleSignupSystem.Controllers.Tests
{
    /// <summary>此類別包含 tblSignupsController 的參數化單元測試</summary>
    [PexClass(typeof(tblSignupsController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class tblSignupsControllerTest
    {
        /// <summary>.ctor() 的測試虛設常式</summary>
        [PexMethod]
        public tblSignupsController ConstructorTest()
        {
            tblSignupsController target = new tblSignupsController();
            return target;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.ConstructorTest()
        }

        /// <summary>Create(tblSignup) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult CreateTest(
            [PexAssumeUnderTest]tblSignupsController target,
            tblSignup tblSignup
        )
        {
            ActionResult result = target.Create(tblSignup);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.CreateTest(tblSignupsController, tblSignup)
        }

        /// <summary>DeleteConfirmed(String) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult DeleteConfirmedTest([PexAssumeUnderTest]tblSignupsController target, string id)
        {
            ActionResult result = target.DeleteConfirmed(id);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.DeleteConfirmedTest(tblSignupsController, String)
        }

        /// <summary>Delete(Int32) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult DeleteTest(
            [PexAssumeUnderTest]tblSignupsController target,
            int tblSignupItem_ID
        )
        {
            ActionResult result = target.Delete(tblSignupItem_ID);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.DeleteTest(tblSignupsController, Int32)
        }

        /// <summary>Details(Nullable`1&lt;Int32&gt;) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult DetailsTest(
            [PexAssumeUnderTest]tblSignupsController target,
            int? cItemID
        )
        {
            ActionResult result = target.Details(cItemID);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.DetailsTest(tblSignupsController, Nullable`1<Int32>)
        }

        /// <summary>Edit(Nullable`1&lt;Int32&gt;, Int32) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult EditTest(
            [PexAssumeUnderTest]tblSignupsController target,
            int? tblSignupItem_ID,
            int cItemID
        )
        {
            ActionResult result = target.Edit(tblSignupItem_ID, cItemID);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.EditTest(tblSignupsController, Nullable`1<Int32>, Int32)
        }

        /// <summary>Edit(Mix) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult EditTest01([PexAssumeUnderTest]tblSignupsController target, Mix Mix)
        {
            ActionResult result = target.Edit(Mix);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.EditTest01(tblSignupsController, Mix)
        }

        /// <summary>Index(String, String, Nullable`1&lt;Int32&gt;) 的測試虛設常式</summary>
        [PexMethod]
        public ActionResult IndexTest(
            [PexAssumeUnderTest]tblSignupsController target,
            string Message,
            string Search,
            int? SelectId
        )
        {
            ActionResult result = target.Index(Message, Search, SelectId);
            return result;
            // TODO: 將判斷提示加入 方法 tblSignupsControllerTest.IndexTest(tblSignupsController, String, String, Nullable`1<Int32>)
        }
    }
}
