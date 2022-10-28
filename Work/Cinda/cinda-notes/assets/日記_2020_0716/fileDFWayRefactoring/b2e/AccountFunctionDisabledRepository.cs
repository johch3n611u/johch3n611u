using System.Collections.Generic;
using System.Linq;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Business;
using AutoMapper;
using System.Runtime.InteropServices.ComTypes;
using System;

namespace Mxic.ITC.PAM.Repository
{

    public class AccountFunctionDisabledRepository : RepositoryBase, ISignRepository<List<AccountFunctionDisabledDetail>>
    {

        public AccountFunctionDisabledRepository()
        {

        }
        public void SetEntities(Entities entities)
        {
            Entities = entities;
        }
        public PageQueryResult<AccountFunctionDisabled> GetAll(PageQuery<AccountFunctionDisabled> request)
        {
            var result = new PageQueryResult<AccountFunctionDisabled>();
            var queryable = (from t1 in Entities.PAM_AF_DISABLED.AsNoTracking()
                             join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2 })
                                    .AsQueryable().ToList();
            // 排除 未選擇 F0RM_TYPE 0

            if (request.QueryObject.FormStatus != "-1")
            {
                queryable = queryable.Where(x => x.t1.FORM_STATUS == request.QueryObject.FormStatus).ToList();
                if (request.QueryObject.FormStatus != "4") { queryable = queryable.Where(x => x.t1.F0RM_TYPE != "0").ToList(); }
            }



            var queryList = queryable
                .Select(x => new AccountFunctionDisabled
                {

                    Id = x.t1.ID, // 表單 PK
                    Department = x.t2.APPLICANTER_DEPT_NO, // 申請人部門代號
                    EmpNo = x.t2.APPLICANTER_EMP_NO, // 申請人工號
                    Name = x.t2.APPLICANTER_NAME, // 申請人姓名
                    SignFormId = x.t1.SIGN_FORM_ID, // 主表單編號
                    FormStatus = x.t1.FORM_STATUS, // 文件處理狀態
                    FormType = x.t1.F0RM_TYPE, // 離職帳號或權限停用單狀態
                    CloseDate = x.t1.CLOSE_DATE // 帳號權限預計關閉日

                }).ToList();

            result.Entries.AddRange(queryList);
            return result;
        }

        public PageQueryResult<AccountFunctionDisabledDetail> GetDetail(decimal requestSignFormId)
        {
            var result = new PageQueryResult<AccountFunctionDisabledDetail>();

            var queryable = (from t3 in Entities.SIGN_FORM_MAIN.AsNoTracking()
                             join t2 in Entities.PAM_AF_DISABLED.AsNoTracking() on  t3.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             join t1 in Entities.PAM_AF_DISABLED_DETAIL.AsNoTracking() on t2.ID equals t1.AF_DISABLED_ID into add from t1 in add.DefaultIfEmpty()
                             join t4 in Entities.FUNCTION_TYPE.AsNoTracking() on t1.SERVICE_NAME equals t4.ID into ft from t4 in ft.DefaultIfEmpty()
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2, t3, t4 })
                             .AsQueryable();

                             //var queryable = (from t1 in Entities.PAM_AF_DISABLED_DETAIL.AsNoTracking()
                             //                 join t2 in Entities.PAM_AF_DISABLED.AsNoTracking() on t1.AF_DISABLED_ID equals t2.ID
                             //                 join t3 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t2.SIGN_FORM_ID equals t3.SIGN_FORM_ID
                             //                 join t4 in Entities.FUNCTION_TYPE.AsNoTracking() on t1.SERVICE_NAME equals t4.ID 
                             //                 orderby t2.SIGN_FORM_ID descending
                             //                 select new { t1, t2, t3,t4 })
                             //                 .AsQueryable();

            if (requestSignFormId != null)
            {
                queryable = queryable.Where(x => x.t3.SIGN_FORM_ID == requestSignFormId);
            }

            var queryList = queryable
                .Select(x => new AccountFunctionDisabledDetail
                {
                    Id = x.t1.ID, // 子表 ID
                    SignFormId = x.t2.SIGN_FORM_ID,
                    Name = x.t3.APPLICANTER_NAME, // 申請人姓名
                    CloseDate = x.t2.CLOSE_DATE, // 帳號權限預計關閉日
                    FormType = x.t2.F0RM_TYPE, // 選擇項目
                    DisabledDate = x.t2.DISABLED_DATE, // 停用及設備繳回日期 ( 選擇項目為 AD/Note/Novell帳號提前停用及設備提前繳回 才會有值
                    ServiceName = x.t4.DESCRIPTION, // 權限名稱
                    Disabled = x.t1.DISABLED, // 權限是否停用
                    PrecloseDate = x.t1.PRECLOSE_DATE, // 權限預計停用日期
                    Status = x.t1.STATUS // 權限處理狀態

                }).ToList();

            result.Entries.AddRange(queryList);
            return result;
        }

        // public bool Create(List<AccountFunctionDisabledDetail> Datas, decimal dSignID, bool bIsNew) { return true; }
        public bool Create(List<AccountFunctionDisabledDetail> Datas, decimal dSignID, bool bIsNew)
        {
            return true;
        }

        public bool Update(List<AccountFunctionDisabledDetail> Datas)
        {
            if (Datas.Count > 0)
            {
                // 判斷 PAM_AF_DISABLED 有無此單，如無此單則退回不保存

                decimal? SignFormId = 0;

                try
                {
                    var Item = Datas[0];
                    SignFormId = Item.SignFormId;

                    var queryable = (from t1 in Entities.PAM_AF_DISABLED.AsNoTracking()
                                     join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                                     orderby t2.SIGN_FORM_ID descending
                                     select new { t1, t2 })
                    .AsQueryable();

                    var length = queryable.Where(x => x.t2.SIGN_FORM_ID == SignFormId).ToList().Count;

                    if (length > 0)
                    {
                        // 更新 PAM_AF_DISABLED

                        var EF_PAM_AF_DISABLED = Entities.PAM_AF_DISABLED.FirstOrDefault(x => x.SIGN_FORM_ID == SignFormId);

                        EF_PAM_AF_DISABLED.F0RM_TYPE = Item.FormType;
                        if (Item.FormType == Convert.ToString(2))
                        {
                            EF_PAM_AF_DISABLED.DISABLED_DATE = Item.DisabledDate;
                        }

                        // 未選擇儲存
                        if (Item.FormType == "0")
                        {
                            EF_PAM_AF_DISABLED.FORM_STATUS = "4";
                        }

                        Entities.SaveChanges();

                        // 更新 PAM_AF_DISABLED_DETAIL

                        foreach (var Data in Datas)
                        {

                            var EF_PAM_AF_DISABLED_DETAIL = Entities.PAM_AF_DISABLED_DETAIL.FirstOrDefault(x => x.ID == Data.Id);

                            if (Data.Disabled == "true") { Data.Disabled = "1"; } else { Data.Disabled = "0"; }

                            EF_PAM_AF_DISABLED_DETAIL.DISABLED = Data.Disabled;
                            EF_PAM_AF_DISABLED_DETAIL.PRECLOSE_DATE = Data.PrecloseDate;

                            Entities.SaveChanges();

                        }
                    }
                    else
                    {
                        return false;
                    }

                }
                catch (Exception ex)
                {
                    return false;
                }

            }

            return true;
        }

        public PageQueryResult<AccountFunctionDisabled> GetNoSelected(PageQuery<AccountFunctionDisabled> request)
        {
            var result = new PageQueryResult<AccountFunctionDisabled>();

            var queryable = (from t1 in Entities.PAM_AF_DISABLED.AsNoTracking()
                             join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2 })
                             .AsQueryable();

            // 查詢未選擇

            queryable = queryable.Where(x => x.t1.F0RM_TYPE == "0");

            var queryList = queryable
                .Select(x => new AccountFunctionDisabled
                {

                    Id = x.t1.ID, // 表單 PK
                    Department = x.t2.APPLICANTER_DEPT_NO, // 申請人部門代號
                    EmpNo = x.t2.APPLICANTER_EMP_NO, // 申請人工號
                    Name = x.t2.APPLICANTER_NAME, // 申請人姓名
                    SignFormId = x.t1.SIGN_FORM_ID, // 主表單編號
                    FormStatus = x.t1.FORM_STATUS, // 文件處理狀態
                    FormType = x.t1.F0RM_TYPE, // 離職帳號或權限停用單狀態
                    CloseDate = x.t1.CLOSE_DATE // 帳號權限預計關閉日

                }).ToList();

            result.Entries.AddRange(queryList);
            return result;
        }

        public bool Approve(List<AccountFunctionDisabledDetail> Sign)
        {
            return true;
        }

        public string Approved(List<AccountFunctionDisabledDetail> Datas, SignFormMain Sign)
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

        public bool Rejected(List<AccountFunctionDisabledDetail> Sign)
        {
            return true;
        }
        public bool Close(List<AccountFunctionDisabledDetail> Datas, SignFormMain Sign)
        {
            return true;
        }

        public void SetUpdaterEmpNo(string empNo)
        {
            this.UpdaterEmpNo = empNo;
        }


        public bool Invalid(List<AccountFunctionDisabledDetail> Datas)
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

    }
}
