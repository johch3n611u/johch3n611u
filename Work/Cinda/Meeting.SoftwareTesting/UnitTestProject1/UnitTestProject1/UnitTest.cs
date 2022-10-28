using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
//using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Mxic.Framework.Membership;
//using Mxic.ITC.PAM.Enum;
//using Mxic.ITC.PAM.Interface;
//using Mxic.ITC.PAM.Model;
//using Mxic.ITC.PAM.Model.Entity;
//using Mxic.ITC.PAM.Model.Sign;
//using Mxic.ITC.PAM.Repository;
//using Mxic.ITC.PAM.Repository.Repository;
//using Mxic.ITC.PAM.Repository.UnitOfWork;
//using Mxic.ITC.PAM.Service;
//using Mxic.ITC.PAM.Utility;

namespace UnitTestProject.UnitTest
{
    public class UnitTest
    {
        [TestClass]
        public class UnitTestDemo
        {
            [TestMethod]
            public void UnitTest() // 右鍵偵錯測試
            {
                // https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest

                #region Arrange - 初始化物件設置測試值
                var primeService = new PrimeService();
                int Test = 1;
                #endregion

                #region Act - 表示要調用的測試方法
                bool result = primeService.IsPrime(Test);
                #endregion

                #region Assert - 斷言 (判斷真假) 表示確認期望值和實際值應該相同
                Assert.IsFalse(result, "1 should not be prime");
                Assert.IsTrue(result, "");
                Assert.AreEqual(result, Test);
                Assert.Fail();
                #endregion
            }
            public class PrimeService
            {
                public bool IsPrime(int candidate)
                {
                    if (candidate == 1)
                    {
                        return false;
                    }
                    throw new NotImplementedException("Please create a test first.");
                }
            }

        }
        [TestClass]
        public class IntegrationTestDemo // 測試 -> Test Explorer
        {
            public string Email;
            public MailMessage MailMessage;
            public IntegrationTestDemo()
            {
                Email = "";
                MailMessage = new MailMessage();
            }

            [TestMethod]
            public void IsValidEmail()
            {
                #region Arrange - 初始化物件設置測試值
                Email = "zzz12345688@gmail.com";
                string ResultEmail = "";
                #endregion

                #region Act - 表示要調用的測試方法
                ResultEmail = IsValidEmail(Email);
                #endregion

                #region Assert - 斷言 (判斷真假) 表示確認期望值和實際值應該相同
                Assert.IsTrue(ResultEmail == Email);
                #endregion
            }

            public string IsValidEmail(string Email)
            {
                var addr = new MailAddress(Email);

                if (addr.Address == Email)
                {
                    return Email;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            [TestMethod]
            public void SetEmail()
            {
                #region Arrange - 初始化物件設置測試值
                Email = "zzz12345688@gmail.com";
                MailMessage = new MailMessage();
                #endregion

                #region Act - 表示要調用的測試方法
                MailMessage ResultMailMessage = SetEmail("測試發信標題", "測試發信內容", new List<string> { Email }, new List<string>());
                #endregion

                #region Assert - 斷言 (判斷真假) 表示確認期望值和實際值應該相同
                Assert.IsTrue(MailMessage != ResultMailMessage);
                #endregion
            }

            public static MailMessage SetEmail(string Subject, string Content, List<string> MailList, List<string> CCMailList)
            {
                MailMessage MailMessage = new MailMessage();
                MailMessage.From = new MailAddress("allen.liu@shinda.com.tw");
                foreach (var Mail in MailList)
                {
                    MailMessage.To.Add(Mail);
                }
                foreach (var Mail in CCMailList)
                {
                    MailMessage.CC.Add(Mail);
                }
                MailMessage.Subject = Subject;
                MailMessage.Body = Content;
                MailMessage.IsBodyHtml = true;

                return MailMessage;
            }

            [TestMethod]
            public void SendEmail()
            {
                #region Arrange - 初始化物件設置測試值
                Email = "zzz12345688@gmail.com";
                MailMessage = new MailMessage();
                #endregion

                #region Act - 表示要調用的測試方法
                MailMessage = SetEmail("測試發信標題", "測試發信內容", new List<string> { Email }, new List<string>());
                var result = SendEmail(MailMessage);
                #endregion

                #region Assert - 斷言 (判斷真假) 表示確認期望值和實際值應該相同
                Assert.IsTrue(result);
                #endregion
            }

            public static bool SendEmail(MailMessage MailMessage)
            {
                try
                {
                    using (var SmtpClient = new SmtpClient("smtp.gmail.com", 587))
                    {
                        SmtpClient.Credentials = new NetworkCredential(Account, PassWord);
                        SmtpClient.EnableSsl = true;
                        SmtpClient.Send(MailMessage);
                        MailMessage.Dispose();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            [TestMethod]
            public void IntegrationTest()
            {
                #region Arrange - 初始化物件設置測試值
                Email = "zzz12345688@gmail.com";
                MailMessage = new MailMessage();
                #endregion

                #region Act - 表示要調用的測試方法
                // Step 1
                Email = IsValidEmail(Email);
                // Step 2
                MailMessage = SetEmail("測試發信標題", "測試發信內容", new List<string> { Email }, new List<string>());
                // Step 3
                var result = SendEmail(MailMessage);
                #endregion

                #region Assert - 斷言 (判斷真假) 表示確認期望值和實際值應該相同
                Assert.IsTrue(result);
                #endregion
            }
        }

        //[TestClass]
        //public class IntegrationTest
        //{
        //    [TestMethod]
        //    public void TestSendMail()
        //    {
        //        MailHelper.SendAutomatedEmail("測試發信標題", "測試發信內容", new List<string> { "" }, new List<string>());
        //    }

        //    [TestMethod]
        //    public void TestFormCreat()
        //    {
        //        #region Arrange - 初始化物件設置測試值
        //        SignData<AccountChangeForm> SignData = new SignData<AccountChangeForm>();
        //        SignData.IsSelf = true;
        //        AccountChangeForm AccountChangeForm = new AccountChangeForm();
        //        AccountChangeForm.ApplyItem = "PushMail使用權限異動";
        //        AccountChangeForm.ApplyNo = 0;
        //        AccountChangeForm.FunctionApplyType = "AC02_002";
        //        AccountChangeForm.FunctionType = 2;
        //        List<ChangePushMail> PushMailData = new List<ChangePushMail>();
        //        ChangePushMail ChangePushMail = new ChangePushMail();
        //        ChangePushMail.ApplyType = "AA02_001";
        //        ChangePushMail.Id = 6;
        //        Phone NewPhone = new Phone();
        //        NewPhone.Id = 0;
        //        ChangePushMail.NewPhone = NewPhone;
        //        Phone OldPhone = new Phone();
        //        OldPhone.DeviceID = "IBM_IOS_12345678901234567890123456789099";
        //        OldPhone.Id = 1;
        //        OldPhone.Module = "IPhone8 plus";
        //        ChangePushMail.OldPhone = OldPhone;
        //        ChangePushMail.UseType = 1;
        //        ChangePushMail.User = "李先生(部3)";
        //        ChangePushMail.UserEmpNo = "00011";
        //        PushMailData.Add(ChangePushMail);
        //        AccountChangeForm.PushMailData = PushMailData;
        //        AccountChangeForm.SignFormId = 0;
        //        SignData.FormData = AccountChangeForm;
        //        SignFormMain SignFormMain = new SignFormMain();
        //        SignFormMain.ApplicanterDeptNO = "MR530";
        //        SignFormMain.ApplicanterEmpNO = "00011";
        //        SignFormMain.ApplicanterName = "李先生(部3)";
        //        SignFormMain.CreateDate = DateTime.Now;
        //        SignFormMain.FillerDeptNO = "MR530";
        //        SignFormMain.FillerEmpNO = "00011";
        //        SignFormMain.FlowID = 4;
        //        SignFormMain.FormStatus = "SignOff";
        //        SignFormMain.FormType = "AccountChangeForm";
        //        SignFormMain.RequiredDate = DateTime.Now;
        //        SignFormMain.RequiredDesc = "123456789";
        //        SignFormMain.ServiceCode = "AC02_002";
        //        SignFormMain.SignFormNo = "MA_2010_00049";
        //        SignFormMain.SignFromID = 0;
        //        SignFormMain.BpmFormType = BpmFormType.AccountChangeForm;
        //        SignData.Sign = SignFormMain;
        //        #endregion

        //        #region Act - 表示要調用的測試方法
        //        ExternalService ExternalService = new ExternalService();
        //        AccountChangeFormService ACFS = new AccountChangeFormService();
        //        var objPortalService = new PortalRepository().GetPortalSystemServices("AC02_002");
        //        List<PORTAL_SYSTEM_SERVICES> OEF = objPortalService.Entries.Where(x => x.SERVICE_CODE == "AC02_002").ToList();
        //        List<string> COC = new List<string>();
        //        List<string> COCB = new List<string>();
        //        foreach (var item in OEF)
        //        {
        //            COC.Add(item.ORGANIZER_EMPNO);
        //            COCB.Add(item.BACKUP_ORGANIZER_EMPNO);
        //        }
        //        SignRepository<AccountChangeForm> Repository = new SignRepository<AccountChangeForm>(new AccounChangeFormRepository());
        //        IHrMasterService HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
        //        Model.Business.PageQueryResult<string> result = new Model.Business.PageQueryResult<string>();
        //        result = ACFS.Create(SignData);
        //        #endregion

        //        #region Assert - 斷言 (判斷真假) 表示確認期望值和實際值應該相同
        //        Assert.IsFalse(result != null);
        //        #endregion
        //    }
        //}

        public static string Account = "allen.liu@shinda.com.tw";
        public static string PassWord = "";
    }
}

