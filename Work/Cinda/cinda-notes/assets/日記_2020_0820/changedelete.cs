foreach (var form in Data)
            {
                try
                {
                    Account Account = new Account
                    {
                        //共用
                        FunctionType = form.FunctionType,
                        //AccountType = Data.AccountType,
                        //TODO: DetpCenter = 問來源
                    };

                    if (form.FunctionType == (int)EnumAccountFunctionType.ComputerAccount)
                    {
                        foreach (var Item in form.ComputerAccountData)
                        {
                            ACCOUNT ACCOUNT = Entities.ACCOUNT.Find(Item.Id);

                            Account.Id = Item.Id; // Update Account ID
                            Account.FunctionApplyType = form.FunctionApplyType; // ServiceCode申請子項目(依功能類型不同意義)
                            Account.UsingType = Item.UseType; // 使用時間類型
                            Account.EmpNo = Item.UserEmpNo; // 使用者工號
                            if (Item.KeeperEmpNo != "" || Item.KeeperEmpNo != null)
                            { Account.EmpNo = Item.KeeperEmpNo; } // 新使用者工號
                            Account.EmpName = Item.User; // 使用者名稱
                            if (Item.Keeper != "" || Item.Keeper != null)
                            { Account.EmpName = Item.Keeper; } // 新使用者名稱
                            Account.UpdaterEmpNo = Item.UserEmpNo; // 最後修改者工號
                            if (Item.KeeperEmpNo != "" || Item.KeeperEmpNo != null)
                            { Account.UpdaterEmpNo = Item.KeeperEmpNo; }
                            Account.AccountType = Item.OldAccountType; // 帳號類型
                            if (Item.NewAccountType != null)
                            { Account.AccountType = Item.NewAccountType; }
                            Account.LastRefSignFormId = form.SignFormId; // 來源 SignFormId
                            // 電腦帳號欄位


                            // 沿用無新資料傳遞
                            // 共用
                            Account.ManageType = ACCOUNT.MANAGE_TYPE;
                            Account.DeptNo = ACCOUNT.DEPT_NO;
                            Account.CreateDate = ACCOUNT.CREATE_DATE;
                            Account.RefType = ACCOUNT.REF_TYPE; // 來源 Portal 帳號權限異動單
                            Account.Status = ACCOUNT.STATUS;
                            Account.DisableDate = ACCOUNT.DISABLE_DATE;
                            Account.AccountCode = ACCOUNT.ACCOUNT_CODE;

                            // 電腦帳號欄位
                            Account.EnableAd = ACCOUNT.ENABLE_AD;
                            Account.EnableNovell = ACCOUNT.ENABLE_NOVELL;
                            Account.EnableNotes = ACCOUNT.ENABLE_NOTES;
                            Account.EnableExternalMail = ACCOUNT.ENABLE_EXTERNAL_MAIL;
                            Account.EnableInternet = ACCOUNT.ENABLE_INTERNET;
                            Account.EnablePrint = ACCOUNT.ENABLE_PRINT;
                            Account.EnableFax = ACCOUNT.ENABLE_FAX;
                            Account.PasswordType = ACCOUNT.PASSWORD_TYPE;
                            Account.RequireDescription = ACCOUNT.REQUIRE_DESCRIPTION;
                            Account.FirstName = ACCOUNT.FIRST_NAME;
                            Account.LastName = ACCOUNT.LAST_NAME;
                            Account.FirstNameTW = ACCOUNT.FIRST_NAME_TW;
                            Account.LastNameTW = ACCOUNT.LAST_NAME_TW;
                            Account.SystemName = ACCOUNT.SYSTEM_NAME;
                            Account.ManageMethod = ACCOUNT.MANAGE_METHOD;

                            // 2020-0820 補充欄位

                            using (var repository = new ComputerAccountRepository())
                            {
                                Account.FunctionType = (byte)EnumAccountFunctionType.ComputerAccount;
                                repository.Update(Account);
                            }

                        };

                    }
                    if (form.FunctionType == (int)EnumAccountFunctionType.ComputerOthers)
                    {
                        foreach (var Item in form.ComputerOtherData)
                        {
                            ACCOUNT ACCOUNT = Entities.ACCOUNT.Find(Item.Id);

                            Account.Id = Item.Id; // Update Account ID
                            Account.FunctionApplyType = form.FunctionApplyType; // ServiceCode申請子項目(依功能類型不同意義)
                            Account.UsingType = Item.UseType; // 使用時間類型
                            Account.EmpNo = Item.UserEmpNo; // 使用者工號
                            Account.EmpName = Item.User; // 使用者名稱
                            Account.UpdaterEmpNo = Item.UserEmpNo; // 最後修改者工號
                            Account.LastRefSignFormId = form.SignFormId; // 來源 SignFormId

                            // ComputerOthers 欄位
                            Account.MainAssetNo = Item.ComputerName.AsseetId; // 主資產編號(關聯)
                            Account.SubAssetNo = Item.ComputerName.AsseetChrildId; // 子資產編號(關聯)
                            Account.ComputerName = Item.ComputerName.PCName; // 電腦名稱(關聯)

                            // 無異動欄位
                            // 共用欄位
                            Account.ManageType = ACCOUNT.MANAGE_TYPE;
                            Account.DeptNo = ACCOUNT.DEPT_NO;
                            Account.EmpNo = ACCOUNT.EMP_NO;
                            Account.EmpName = ACCOUNT.EMP_NAME;
                            Account.UsingType = ACCOUNT.USING_TYPE;
                            Account.CreateDate = ACCOUNT.CREATE_DATE;
                            Account.UpdaterEmpNo = ACCOUNT.UPDATER_EMP_NO;
                            Account.RefType = ACCOUNT.REF_TYPE;
                            Account.Status = ACCOUNT.STATUS;
                            Account.DisableDate = ACCOUNT.DISABLE_DATE;
                            Account.AccountType = ACCOUNT.ACCOUNT_TYPE;
                            Account.AccountCode = ACCOUNT.ACCOUNT_CODE;
                            // ComputerOthers 欄位
                            Account.CompanyCode = ACCOUNT.COMPANY_CODE; // 資產COMPANYCODE(關聯)
                            Account.Policy = ACCOUNT.POLICY; // 周邊裝置權限關聯 多筆,分隔

                            using (var repository = new ComputerOthersRepository())
                            {
                                Account.FunctionType = (byte)EnumAccountFunctionType.ComputerOthers;
                                repository.Update(Account);
                            }
                        };
                    }
                    if (form.FunctionType == (int)EnumAccountFunctionType.PushMail)
                    {
                        foreach (var Item in form.PushMailData)
                        {
                            ACCOUNT ACCOUNT = Entities.ACCOUNT.Find(Item.Id);

                            Account.Id = Item.Id; // Update Account ID
                            Account.FunctionApplyType = form.FunctionApplyType; // ServiceCode申請子項目(依功能類型不同意義)
                            Account.UsingType = Item.UseType; // 使用時間類型
                            Account.EmpNo = Item.UserEmpNo; // 使用者工號
                            Account.EmpName = Item.User; // 使用者名稱
                            Account.UpdaterEmpNo = Item.UserEmpNo; // 最後修改者工號
                            Account.LastRefSignFormId = form.SignFormId; // 來源 SignFormId

                            // 無異動欄位
                            // 共用
                            Account.ManageType = ACCOUNT.MANAGE_TYPE;
                            Account.DeptNo = ACCOUNT.DEPT_NO;
                            Account.CreateDate = ACCOUNT.CREATE_DATE;
                            Account.RefType = ACCOUNT.REF_TYPE;
                            Account.Status = ACCOUNT.STATUS;
                            Account.DisableDate = ACCOUNT.DISABLE_DATE;

                            Account.AccountType = ACCOUNT.ACCOUNT_TYPE;
                            Account.AccountCode = ACCOUNT.ACCOUNT_CODE;

                            // PushMail欄位
                            Account.MobileType = ACCOUNT.MOBILE_TYPE; // 手機型號(關聯)
                            Account.MobileId = ACCOUNT.MOBILE_ID; // 行動設備ID

                            using (var repository = new PushMailRepository())
                            {
                                Account.FunctionType = (byte)EnumAccountFunctionType.PushMail;
                                repository.Update(Account);
                            }
                        };
                    }
                    if (form.FunctionType == (int)EnumAccountFunctionType.Websense)
                    {
                        foreach (var Item in form.OpenWebData)
                        {
                            ACCOUNT ACCOUNT = Entities.ACCOUNT.Find(Item.Id);

                            Account.Id = Item.Id; // Update Account ID
                            Account.FunctionApplyType = form.FunctionApplyType; // ServiceCode申請子項目(依功能類型不同意義)
                            Account.UsingType = Item.UseType; // 使用時間類型
                            Account.EmpNo = Item.UserEmpNo; // 使用者工號
                            if (Item.NewKeeperEmpNo != "" || Item.NewKeeperEmpNo != null)
                            { Account.EmpNo = Item.NewKeeperEmpNo; } // 新使用者工號
                            Account.EmpName = Item.User; // 使用者名稱
                            if (Item.NewKeeper != "" || Item.NewKeeper != null)
                            { Account.EmpName = Item.NewKeeper; } // 新使用者名稱
                            Account.UpdaterEmpNo = Item.UserEmpNo; // 最後修改者工號
                            if (Item.NewKeeperEmpNo != "" || Item.NewKeeperEmpNo != null)
                            { Account.UpdaterEmpNo = Item.NewKeeperEmpNo; }
                            Account.LastRefSignFormId = form.SignFormId; // 來源 SignFormId

                            // Websense 欄位
                            Account.SiteName = Item.WebName;
                            Account.SiteUrl = Item.WebURL;

                            // 無異動欄位
                            // 共用欄位
                            Account.ManageType = ACCOUNT.MANAGE_TYPE;
                            Account.DeptNo = ACCOUNT.DEPT_NO;
                            Account.CreateDate = ACCOUNT.CREATE_DATE;
                            Account.RefType = ACCOUNT.REF_TYPE;
                            Account.Status = ACCOUNT.STATUS;
                            Account.DisableDate = ACCOUNT.DISABLE_DATE;

                            Account.AccountType = ACCOUNT.ACCOUNT_TYPE;
                            Account.AccountCode = ACCOUNT.ACCOUNT_CODE;

                            // Websense 欄位
                            Account.EnableView = ACCOUNT.ENABLE_VIEW;
                            Account.EnableDowload = ACCOUNT.ENABLE_DOWNLOAD;
                            Account.EnableCopy = ACCOUNT.ENABLE_DOWNLOAD;
                            Account.EnableUpload = ACCOUNT.ENABLE_UPLOAD;
                            Account.SiteClass = ACCOUNT.SITE_CLASS;
                            Account.EnableInternet = ACCOUNT.ENABLE_INTERNET;

                            using (var repository = new WebsenseRepository())
                            {
                                Account.FunctionType = (byte)EnumAccountFunctionType.Websense;
                                repository.Update(Account);
                            }
                        };
                    }

                    #region 其餘帳號權限類型 (需補 Repository 的 Update)

                    //if (Data.FunctionType == (int)EnumAccountFunctionType.SslVpn)
                    //{
                    //    foreach (var Item in Data.SSLVpn)
                    //    {
                    //        //共用
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        using (var repository = new SslVpnRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.SslVpn;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.Citrix)
                    //{
                    //    foreach (var Item in Data.Citrix)
                    //    {
                    //        //共用
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // Citrix 欄位
                    //        Account.EnableCitrixNight = Item.EnableCitrixNight;
                    //        Account.Others = Item.Others;

                    //        using (var repository = new CitrixRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.Citrix;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.LocalDomain)
                    //{
                    //    foreach (var Item in Data.LocalDomain)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // LocalDomain 欄位
                    //        Account.MainAssetNo = Item.MainAssetNo;
                    //        Account.SubAssetNo = Item.SubAssetNo;
                    //        Account.ComputerName = Item.ComputerName;
                    //        Account.CompanyCode = Item.CompanyCode;

                    //        using (var repository = new LocalAdminRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.LocalDomain;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.NetworkPrinting)
                    //{
                    //    foreach (var Item in Data.NetworkPrinting)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // NetworkPrinting 欄位
                    //        Account.PrinterName = Item.PrinterName;
                    //        Account.PrinterFunction = Item.PrinterFunction;

                    //        using (var repository = new NetworkPrintingRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.NetworkPrinting;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.MailOut)
                    //{
                    //    foreach (var Item in Data.MailOut)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // MailOut 欄位
                    //        Account.MailCompany = Item.MailCompany;
                    //        Account.MailName = Item.MailName;
                    //        Account.MailAddress = Item.MailAddress;
                    //        Account.NotesMailAccount = Item.NotesMailAccount;

                    //        using (var repository = new MailOutRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.MailOut;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.NB)
                    //{
                    //    foreach (var Item in Data.NB)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // NB 欄位
                    //        Account.MainAssetNo = Item.MainAssetNo;
                    //        Account.SubAssetNo = Item.SubAssetNo;
                    //        Account.ComputerName = Item.ComputerName;
                    //        Account.CompanyCode = Item.CompanyCode;
                    //        Account.NbIdentity = Item.NbIdentity;

                    //        using (var repository = new NBRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.NB;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.ComputerOthers)
                    //{
                    //    foreach (var Item in Data.ComputerOthers)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // ComputerOthers 欄位
                    //        Account.MainAssetNo = Item.MainAssetNo;
                    //        Account.SubAssetNo = Item.SubAssetNo;
                    //        Account.ComputerName = Item.ComputerName;
                    //        Account.CompanyCode = Item.CompanyCode;
                    //        Account.Policy = Item.Policy;

                    //        using (var repository = new ComputerOthersRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.ComputerOthers;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.MobileWifi)
                    //{
                    //    foreach (var Item in Data.MobileWifi)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // MobileWifi 欄位
                    //        Account.MacAddress = Item.MacAddress;
                    //        Account.SiteType = Item.SiteType;
                    //        Account.MobileId = Item.MobileId;
                    //        Account.MobileType = Item.MobileType;

                    //        using (var repository = new MobileWifiRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.MobileWifi;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.CustomerWifi)
                    //{
                    //    foreach (var Item in Data.CustomerWifi)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        // CustomerWifi 欄位
                    //        Account.SiteType = Item.SiteType;
                    //        Account.Zone = Item.Zone;
                    //        Account.ApplyCount = Item.ApplyCount;
                    //        Account.CompanyName = Item.CompanyName;
                    //        Account.Customer = Item.Customer;

                    //        using (var repository = new CustomerWifiRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.CustomerWifi;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}
                    //if (Data.FunctionType == (int)EnumAccountFunctionType.EFax)
                    //{
                    //    foreach (var Item in Data.EFax)
                    //    {
                    //        // 共用欄位
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.ManageType = Item.ManageType;
                    //        Account.DeptNo = Item.DeptNo;
                    //        Account.EmpNo = Item.EmpNo;
                    //        Account.EmpName = Item.EmpName;
                    //        Account.UsingType = Item.UsingType;
                    //        Account.CreateDate = DateTime.Now;
                    //        Account.UpdaterEmpNo = Item.UpdaterEmpNo;
                    //        Account.RefType = (int?)Item.RefType;
                    //        Account.Status = (byte)EnumAccountStatus.Enable;
                    //        Account.DisableDate = Item.DisableDate;

                    //        Account.AccountType = Item.OldAccountType;
                    //        Account.FunctionApplyType = Item.FunctionApplyType;
                    //        Account.AccountCode = Item.AccountCode;
                    //        Account.LastRefSignFormId = Sign.SignFromID;

                    //        using (var repository = new EFaxRepository())
                    //        {
                    //            Account.FunctionType = (byte)EnumAccountFunctionType.EFax;
                    //            var autoincrementId = repository.Add(Account);
                    //        }
                    //    };
                    //}

                    #endregion

                    return true;

                }
                catch (Exception e)
                {
                    throw e;
                }
            }