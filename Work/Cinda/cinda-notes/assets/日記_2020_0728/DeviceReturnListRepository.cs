using System.Collections.Generic;
using System.Linq;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Business;
using System;

namespace Mxic.ITC.PAM.Repository
{

    public class DeviceReturnListRepository : RepositoryBase, ISignRepository<List<DeviceReturnList>>
    {

        public DeviceReturnListRepository()
        {

        }
        public void SetEntities(Entities entities)
        {
            Entities = entities;
        }
        public PageQueryResult<DeviceReturnList> GetAll(PageQuery<DeviceReturnList> request)
        {
            var result = new PageQueryResult<DeviceReturnList>();

            var queryable = (from t1 in Entities.PAM_DEVICE_RETURN.AsNoTracking()
                             join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2 })
                             .AsQueryable();

            var queryList = queryable
                .Select(x => new DeviceReturnList
                {
                    Id = x.t1.ID,

                    ReturnDate = x.t1.RETURN_DATE,

                    DeviceReturn = x.t1.DEVICE_RETURN,

                    DeviceReturnDesc = x.t1.DEVICE_RETURN_DESC,

                    XfortReturn = x.t1.XFORT_RETURN,

                    XfortDesc = x.t1.XFORT_DESC,

                    NbBringout = x.t1.NB_BRINGOUT,

                    NbBringoutDesc = x.t1.NB_BRINGOUT_DESC,

                    SignFormId = x.t1.SIGN_FORM_ID,

                    AccountId = x.t1.ACCOUNT_ID,

                    ResignDepartment = x.t2.FILLER_DEPT_NO,

                    ResignEmpNo = x.t2.APPLICANTER_EMP_NO,

                    ResignName = x.t2.FILLER_NAME,

                    XfortAssetNo = x.t1.XFORT_ASSET_NO,

                    NbAssetNo = x.t1.NB_ASSET_NO,

                    XfortControl = x.t1.XFORT_CONTROL,

                    NbCustody = x.t1.NB_CUSTODY,

                    SignFormNo = x.t2.SIGN_FORM_NO

                }).ToList();

            result.Entries.AddRange(queryList);
            return result;
        }

        public PageQueryResult<DeviceReturnList> GetDetail(decimal requestSignFormId)
        {
            var result = new PageQueryResult<DeviceReturnList>();

            var queryable = (from t1 in Entities.PAM_DEVICE_RETURN.AsNoTracking()
                             join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2 })
                             .AsQueryable();

            if (requestSignFormId != null)
            {
                queryable = queryable.Where(x => x.t1.SIGN_FORM_ID == requestSignFormId);
            }

            var queryList = queryable
                .Select(x => new DeviceReturnList
                {
                    Id = x.t1.ID,

                    ReturnDate = x.t1.RETURN_DATE,

                    DeviceReturn = x.t1.DEVICE_RETURN,

                    DeviceReturnDesc = x.t1.DEVICE_RETURN_DESC,

                    XfortReturn = x.t1.XFORT_RETURN,

                    XfortDesc = x.t1.XFORT_DESC,

                    NbBringout = x.t1.NB_BRINGOUT,

                    NbBringoutDesc = x.t1.NB_BRINGOUT_DESC,

                    SignFormId = x.t1.SIGN_FORM_ID,

                    AccountId = x.t1.ACCOUNT_ID,

                    ResignDepartment = x.t2.FILLER_DEPT_NO,

                    ResignEmpNo = x.t2.FILLER_EMP_NO,

                    ResignName = x.t2.FILLER_NAME,

                    XfortAssetNo = x.t1.XFORT_ASSET_NO,

                    NbAssetNo = x.t1.NB_ASSET_NO,

                    XfortControl = x.t1.XFORT_CONTROL,

                    NbCustody = x.t1.NB_CUSTODY

                }).ToList();

            result.Entries.AddRange(queryList);
            return result;
        }

        public bool Update(DeviceReturnList Data)
        {

            if (!String.IsNullOrEmpty(Data.Id.ToString()))
            {
                // 判斷 PAM_DEVICE_RETURN 有無此單，如無此單則退回不保存

                decimal? SignFormId = 0;

                try
                {
                    SignFormId = Data.SignFormId;

                    var queryable = (from t1 in Entities.PAM_DEVICE_RETURN.AsNoTracking()
                                     join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                                     orderby t2.SIGN_FORM_ID descending
                                     select new { t1, t2 })
                    .AsQueryable();

                    if (queryable.Where(x => x.t2.SIGN_FORM_ID == SignFormId).ToList().Count == 1)
                    {

                        // 更新 PAM_DEVICE_RETURN

                        var EF_PAM_DEVICE_RETURN = Entities.PAM_DEVICE_RETURN.FirstOrDefault(x => x.SIGN_FORM_ID == SignFormId);

                        EF_PAM_DEVICE_RETURN.DEVICE_RETURN = ChkBoolean(Data.DeviceReturn);

                        EF_PAM_DEVICE_RETURN.DEVICE_RETURN_DESC = Data.DeviceReturnDesc;

                        EF_PAM_DEVICE_RETURN.NB_BRINGOUT = ChkBoolean(Data.NbBringout);

                        EF_PAM_DEVICE_RETURN.NB_BRINGOUT_DESC = Data.NbBringoutDesc;

                        EF_PAM_DEVICE_RETURN.XFORT_DESC = Data.XfortDesc;

                        EF_PAM_DEVICE_RETURN.XFORT_RETURN = ChkBoolean(Data.XfortReturn);

                        Entities.SaveChanges();

                    }

                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;
        }

        // public bool Create(List<DeviceReturnList> Datas, decimal dSignID, bool bIsNew) { return true; }
        public bool Create(List<DeviceReturnList> Datas, decimal dSignID, bool bIsNew)
        {
            try
            {
                var AF_SignFormId = Datas[0].SignFormId; // 離職帳號或權限停用單 signformid

                PAM_AF_DISABLED EF =
                Entities.PAM_AF_DISABLED.FirstOrDefault(x =>
                x.SIGN_FORM_ID == AF_SignFormId);

                var SerialId = (from t1 in Entities.PAM_DEVICE_RETURN.AsNoTracking()
                                select new { t1.ID }
                 ).Max(v => v.ID) + 1; // 流水號

                // 401 使用
                string CREATE_TYPE = "2"; // 1. 401 啟單 / 2.三選一啟單
                if (EF == null) {
                    EF = new PAM_AF_DISABLED();
                    EF.DEVICE_RETURN_ID = null;
                    EF.DISABLED_DATE = Datas[0].ReturnDate;
                    CREATE_TYPE = "1";
                }

                // 判斷是否重複如重複則跳過不起單
                if (EF.DEVICE_RETURN_ID == null)
                {

                    #region 啟 DeviceReturnList 子單

                    //HSAMRepository hsamRepository = new HSAMRepository();
                    //var HardwareList = hsamRepository.GetAssetMasterByEmp(Datas.FirstOrDefault().APPLICANTER_EMP_NO);
                    //// TODO : 表單內容：依離職人員找到其保管的硬體資產，並檢查該同仁的權限主檔，
                    //// 帶入有無Xfort（DVD, USB,Skype),電腦周邊權限及有無NB攜出Flag。
                        string EMP_NO = Datas[0].APPLICANTER_EMP_NO;
                        List<ACCOUNT> AC_EF = Entities.ACCOUNT.Where(x => x.EMP_NO == EMP_NO).ToList();

                        string XFORT_ASSET_NO = ""; // XFORT 資產編號
                        string XFORT_CONTROL = ""; // 是否有Xfort管控?
                                                   // 電腦周邊權限 Xfort 11
                        var AC_EF_Xfort = AC_EF.Where(x => x.FUNCTION_TYPE == 11).ToList();
                        if (AC_EF_Xfort.Any())
                        {
                            AC_EF_Xfort.ForEach(x => XFORT_ASSET_NO += (x.POLICY + ";"));
                            XFORT_CONTROL = "1";
                        }

                        string NB_ASSET_NO = ""; // NB 資產編號
                        string NB_CUSTODY = ""; // 是否有NB攜出保管證?
                                                // NB攜出保管證 9
                        var AC_EF_NB = AC_EF.Where(x => x.FUNCTION_TYPE == 9).ToList();
                        if (AC_EF_NB.Any())
                        {
                            AC_EF_NB.ForEach(x => NB_ASSET_NO += (x.NB_IDENTITY + ";"));
                            NB_CUSTODY = "1";
                        }

                    var newList = new PAM_DEVICE_RETURN
                    {
                        ID = SerialId,
                        SIGN_FORM_ID = dSignID,
                        RETURN_DATE = EF.DISABLED_DATE,
                        // IF008 離職人 EMPNO 查來的 ACCOUNT 資料
                        XFORT_ASSET_NO = XFORT_ASSET_NO,
                        XFORT_CONTROL = XFORT_CONTROL,
                        NB_ASSET_NO = NB_ASSET_NO,
                        NB_CUSTODY = NB_CUSTODY,
                        CREATE_TYPE = CREATE_TYPE,
                    };

                    Entities.PAM_DEVICE_RETURN.Add(newList);

                    // 離職帳號或權限停用單 補 FK
                    EF.DEVICE_RETURN_ID = SerialId;

                    #endregion

                    ////////////////////////////////////////////////////////////////////////////////
                    Entities.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                    Entities.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool Approve(List<DeviceReturnList> Sign)
        {
            return true;
        }

        public string Approved(List<DeviceReturnList> Datas, SignFormMain Sign)
        {
            //var details = Entities.PAM_MAIL_OUT_DOMAIN_DETAIL.Where(x => x.PAM_MAIL_OUT_DOMAIN.SIGN_FORM_ID == Sign.SignFromID).ToList();
            //foreach (var data in details)
            //{
            //    if (data.ACTION_TYPE == (byte)EnumAccountActionType.Delete)
            //    {
            //        mailOutDomainRepository.Remove(new Account
            //        {
            //            Id = (decimal)data.DELETE_REF,
            //            UpdaterEmpNo = data.UPDATER_EMP_NO,
            //            UpdateDate = DateTime.Now,
            //            LastRefSignFormId = Sign.SignFromID,
            //            Status = (byte)EnumAccountStatus.Disable
            //        });
            //    }
            //    else
            //    {
            //        mailOutDomainRepository.Add(new Account
            //        {
            //            FunctionType = (byte)EnumAccountFunctionType.MailOutDomain,
            //            EmpNo = string.Empty,
            //            EmpName = string.Empty,
            //            UsingType = (byte)EnumAccountUsingType.Long,
            //            UpdateDate = DateTime.Now,
            //            UpdaterEmpNo = data.UPDATER_EMP_NO,
            //            LastRefSignFormId = Sign.SignFromID,
            //            RequireDescription = data.REQUIRE_DESCRIPTION,
            //            DomainName = data.DOMAIN_NAME,
            //            Attachment = data.ATTACHMENT,
            //            Status = (byte)EnumAccountStatus.Enable
            //        });
            //    }
            //}
            return string.Empty;
        }

        public bool Rejected(List<DeviceReturnList> Sign)
        {
            return true;
        }
        public bool Close(List<DeviceReturnList> Datas, SignFormMain Sign)
        {
            return true;
        }

        public void SetUpdaterEmpNo(string empNo)
        {
            this.UpdaterEmpNo = empNo;
        }


        public bool Invalid(List<DeviceReturnList> Datas)
        {
            return true;
        }

        public PageQueryResult<AccountFunctionDisabled> GetDetails(PageQuery<AccountFunctionDisabled> request)
        {
            var result = new PageQueryResult<AccountFunctionDisabled>();
            //request.Sort = "Id";
            //var queryable = Entities.PAM_MAIL_OUT_DOMAIN_DETAIL
            //    .Where(x => x.PAM_MAIL_OUT_DOMAIN.SIGN_FORM_ID == request.QueryObject.SignFormId)
            //    .ToList()
            //    .Select(x => new PAMAccountChange
            //    {
            //        Guid = Guid.NewGuid(),
            //        Id = x.ID,
            //        PAMMailOotDomainId = x.PAM_MAIL_OUT_DOMAIN_ID,
            //        ApplyType = x.APPLY_TYPE,
            //        ActionType = x.ACTION_TYPE,
            //        DomainName = x.DOMAIN_NAME,
            //        RequireDescription = x.REQUIRE_DESCRIPTION,
            //        Attachment = string.IsNullOrEmpty(x.ATTACHMENT) ? string.Empty : this.ITCPAMRootAPIUri + x.ATTACHMENT,
            //        DeleteRef = x.DELETE_REF,
            //        UpdaterEmpNo = x.UPDATER_EMP_NO,
            //        UpdateDate = x.UPDATE_DATE

            //    });
            //result.Entries.AddRange(queryable);
            return result;
        }

        public string ChkBoolean(string keyword)
        {
            string result;
            if (keyword == "true") { result = "1"; } else { result = "0"; }
            return result;
        }

   
    }
}
