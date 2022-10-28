
using Mxic.Framework.ServerComponent;
using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Model;
using Mxic.ITC.PAM.Model.BPM;
using Mxic.ITC.PAM.Model.Business;
using System.Linq;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Repository;
using Mxic.ITC.PAM.Repository.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Mxic.ITC.PAM.Service
{
    [Authorization]
    public class TestApplyAccountService : ServerComponentBase
    {
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        /// <summary>
        /// 測試 ACCOUNT 接口使用
        /// </summary>
        public void TestApplyAccount()
        {

            using (var Repository = new PAMapplyRepository())
            {
                List<PAM_ACCOUNT_APPLYFORM> AccountApplyForm_List = Repository.Entities.PAM_ACCOUNT_APPLYFORM.ToList();
                foreach (var item in AccountApplyForm_List)
                {
                    AccountApplyForm AccountApplyForm = new AccountApplyForm
                    {
                        ApplyNo = (int)item.APPLY_NO,
                        AccountType = (int)item.ACCOUNT_TYPE,
                        ApplyItem = item.APPLY_ITEM,
                        FunctionType = (int)item.FUNCTION_TYPE, // 必須從主單查找 帳號類型
                        SignFormId = (int)item.SIGN_FORM_ID,
                    };

                    List<PAM_ACCOUNT_APPLY> PamAccountApply_List = Repository.Entities.PAM_ACCOUNT_APPLY.Where(x => x.APPLY_NO == item.APPLY_NO).ToList();

                    switch ((int)item.FUNCTION_TYPE)
                    {
                        case (int)EnumAccountFunctionType.ComputerAccount:

                            foreach(var AccountApply in PamAccountApply_List)

                            AccountApplyForm.ComputerAccount = new ComputerAccount[]
                            {

                            };

                            break;
                        case (int)EnumAccountFunctionType.PushMail:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.SslVpn:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.Citrix:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.Websense:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.LocalDomain:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.NetworkPrinting:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.MailOut:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.NB:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.FTP:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.ComputerOthers:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.MobileWifi:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.CustomerWifi:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.EFax:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.MailOutDomain:
                            Console.WriteLine("Case 1");
                            break;
                        case (int)EnumAccountFunctionType.FTPFolder:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)EnumAccountFunctionType.HighPermission:
                            Console.WriteLine("Case 2");
                            break;

                    }



                    SignFormMain Sign = new SignFormMain
                    {
                    };

                    Repository.Approved(Data, Sign);
                }



            }

            //Account Account = new Account
            //{
            //    AccountType = 1,
            //    FunctionApplyType ="1",
            //    AccountCode = "1",
            //    ManageType =1,
            //    DeptNo = "1",
            //    EmpNo = "1",
            //    EmpName = "1",
            //    UsingType = 1,
            //    CreateDate = DateTime.Now,
            //    UpdateDate = DateTime.Now,
            //    UpdaterEmpNo = "1",
            //    RefType = 1,
            //    LastRefSignFormId = 1,
            //    UsingEndDate = DateTime.Now,
            //    Status = 1,
            //    DisableDate = DateTime.Now,
            //    RequireDescription = "1",
            //    DomainName = "1",
            //    Attachment = "1",
            //    EnableCitrixNight = "1",
            //    EnableAd = "1",
            //    EnableNovell = "1",
            //    EnableNotes ="1",
            //    EnableExternalMail ="1",
            //    EnableInternet ="1",
            //    EnablePrint ="1",
            //    EnableFax ="1",
            //    MobileType ="1",
            //    MobileId ="1",
            //    Others ="1",
            //    SiteName ="1",
            //    SiteUrl ="1",
            //    EnableView ="1",
            //    EnableDowload ="1",
            //    EnableCopy ="1",
            //    EnableUpload ="1",
            //    SiteClass ="1",
            //    MainAssetNo ="1",
            //    SubAssetNo ="1",
            //    ComputerName ="1",
            //    CompanyCode ="1",
            //    PrinterName ="1",
            //    PrinterFunction =1,
            //    MailCompany ="1",
            //    MailName ="1",
            //    MailAddress ="1",
            //    NotesMailAccount ="1",
            //    NbIdentity ="1",
            //    Policy ="1",
            //    MacAddress ="1",
            //    SiteType ="1",
            //    ApplyCount =1,
            //    CompanyName ="1",
            //    Customer ="1",
            //    Zone ="1",
            //    Group ="1",
            //    SystemName ="1",
            //    CompanyNameEn ="1",
            //    ContactName ="1",
            //    ContactMail ="1",
            //    ContactIp ="1",
            //    // FtpPermission = ,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    PasswordType = 1,
            //    FirstName ="1",
            //    LastName ="1",
            //    FirstNameTW ="1",
            //    LastNameTW ="1",
            //    ManageMethod ="1",
            //    SignFormId = 1,
            //    SignFormNo ="1",
            //    FlowName ="1",
            //    FormStatus ="1",
            //    ApplicanterDate =DateTime.Now,
            //    ApplicanterEmpNo ="1",
            //    ApplicanterName ="1",
            //    ApplicanterDeptNo ="1",
            //    FinalSignDate =DateTime.Now,
            //    ServiceProject ="1",
            //    GroupDesc ="1",
            //    Safe ="1",
            //};

            //Account NewAccount = new Account
            //{
            //    AccountType = 2,
            //    FunctionApplyType = "2",
            //    AccountCode = "2",
            //    ManageType = 2,
            //    DeptNo = "2",
            //    EmpNo = "2",
            //    EmpName = "2",
            //    UsingType = 2,
            //    CreateDate = DateTime.Now,
            //    UpdateDate = DateTime.Now,
            //    UpdaterEmpNo = "2",
            //    RefType = 2,
            //    LastRefSignFormId = 2,
            //    UsingEndDate = DateTime.Now,
            //    Status = 2,
            //    DisableDate = DateTime.Now,
            //    RequireDescription = "2",
            //    DomainName = "2",
            //    Attachment = "2",
            //    EnableCitrixNight = "2",
            //    EnableAd = "2",
            //    EnableNovell = "2",
            //    EnableNotes = "2",
            //    EnableExternalMail = "2",
            //    EnableInternet = "2",
            //    EnablePrint = "2",
            //    EnableFax = "2",
            //    MobileType = "2",
            //    MobileId = "2",
            //    Others = "2",
            //    SiteName = "2",
            //    SiteUrl = "2",
            //    EnableView = "2",
            //    EnableDowload = "2",
            //    EnableCopy = "2",
            //    EnableUpload = "2",
            //    SiteClass = "2",
            //    MainAssetNo = "2",
            //    SubAssetNo = "2",
            //    ComputerName = "2",
            //    CompanyCode = "2",
            //    PrinterName = "2",
            //    PrinterFunction = 2,
            //    MailCompany = "2",
            //    MailName = "2",
            //    MailAddress = "2",
            //    NotesMailAccount = "2",
            //    NbIdentity = "2",
            //    Policy = "2",
            //    MacAddress = "2",
            //    SiteType = "2",
            //    ApplyCount = 2,
            //    CompanyName = "2",
            //    Customer = "2",
            //    Zone = "2",
            //    Group = "2",
            //    SystemName = "2",
            //    CompanyNameEn = "2",
            //    ContactName = "2",
            //    ContactMail = "2",
            //    ContactIp = "2",
            //    // FtpPermission = ,
            //    StartDate = DateTime.Now,
            //    EndDate = DateTime.Now,
            //    PasswordType = 2,
            //    FirstName = "2",
            //    LastName = "2",
            //    FirstNameTW = "2",
            //    LastNameTW = "2",
            //    ManageMethod = "2",
            //    SignFormId = 2,
            //    SignFormNo = "2",
            //    FlowName = "2",
            //    FormStatus = "2",
            //    ApplicanterDate = DateTime.Now,
            //    ApplicanterEmpNo = "2",
            //    ApplicanterName = "2",
            //    ApplicanterDeptNo = "2",
            //    FinalSignDate = DateTime.Now,
            //    ServiceProject = "2",
            //    GroupDesc = "2",
            //    Safe = "2",
            //};




            //try
            //{
            //    using (var repository = new ComputerAccountRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.ComputerAccount;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            //try
            //{
            //    using (var repository = new PushMailRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.PushMail;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            //try
            //{
            //    using (var repository = new SslVpnRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.SslVpn;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            //try
            //{
            //    using (var repository = new CitrixRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.Citrix;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            //try
            //{
            //    using (var repository = new WebsenseRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.Websense;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            //try
            //{
            //    using (var repository = new NetworkPrintingRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.NetworkPrinting;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}

            //try
            //{
            //    using (var repository = new FTPRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.FTP;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //try
            //{
            //    using (var repository = new MailOutRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.MailOut;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //try
            //{
            //    using (var repository = new MobileWifiRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.MobileWifi;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //try
            //{
            //    using (var repository = new CustomerWifiRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.CustomerWifi;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
            //try
            //{
            //    using (var repository = new EFaxRepository())
            //    {
            //        Account.FunctionType = (byte)EnumAccountFunctionType.EFax;
            //        var autoincrementId = repository.Add(Account);

            //        NewAccount.Id = decimal.Parse(autoincrementId);
            //        repository.Update(NewAccount);

            //        Account.Id = decimal.Parse(autoincrementId);
            //        repository.Remove(Account);
            //    }
            //}
            //catch (Exception e)
            //{
            //    throw e;
            //}
        }

    }
}
