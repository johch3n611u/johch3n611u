using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Business;

namespace Mxic.ITC.PAM.Repository
{
    public class AccountFunctionDisabledRepository : RepositoryBase, ISignRepository <List<AccountFunctionDisabled>>
    {
        public PageQueryResult<AccountFunctionDisabled> GetAll(PageQuery<AccountFunctionDisabled> request)
        {
            var result = new PageQueryResult<AccountFunctionDisabled>();

            var queryable = (from t1 in Entities.PAM_AF_DISABLED.AsNoTracking()
                             join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2 })
                             .AsQueryable();

            //if (request.QueryObject != null)
            //{
            //    var underDateTime = DateTime.Now.AddYears(-1);
            //    if (request.QueryObject.IsOneYear)
            //        queryable = queryable.Where(x => x.t2.CREATE_DATE >= underDateTime);
            //    else
            //        queryable = queryable.Where(x => x.t2.CREATE_DATE <= underDateTime);
            //}

            //var queryList = queryable.ToList()
            //    .Select(x => new PAMAccountChange
            //    {

            var queryList = queryable.ToList()
                .Select(x => new AccountFunctionDisabled {

                  Id = x.t1.ID, // 表單 PK
                  Department = x.t2.FILLER_DEPT_NO, // 填表人部門代號
                  EmpNo = x.t2.FILLER_EMP_NO, // 填表人編號
                  Name = x.t2.FILLER_NAME, // 填表人姓名
                  SignFormId = x.t1.SIGN_FORM_ID,
                  FormStatus = x.t1.F0RM_TYPE,
                  FormType = x.t1.F0RM_TYPE,
                  CloseDate = x.t1.CLOSE_DATE

    });
            //    {
            //        Id = x.t1.ID,
            //        SignFormNo = x.t2.SIGN_FORM_NO,
            //        FlowName = x.t2.FLOW_SETTING_MAIN.FLOW_NAME,
            //        FormStatus = x.t2.FORM_STATUS,
            //        SignFormId = x.t2.SIGN_FORM_ID,
            //        ApplicanterDate = x.t2.CREATE_DATE,
            //        ApplicanterName = x.t2.APPLICANTER_NAME,
            //        ApplicanterDeptNo = x.t2.APPLICANTER_DEPT_NO,
            //        SignatoryName = x.t2.SIGN_STAGE.Any(s => s.STAGE_ORDER == x.t2.NOW_STAGE_ORDER)
            //                        ? x.t2.SIGN_STAGE.Where(s => s.STAGE_ORDER == x.t2.NOW_STAGE_ORDER).OrderBy(s => s.SEQ)
            //                            .Select(s => s.SIGNATORY_NAME)
            //                            .ToList().Aggregate((current, next) => current + "/" + next)
            //                        : string.Empty,
            //        UpdateDate = x.t1.UPDATE_DATE,
            //        UpdaterEmpNo = x.t1.UPDATER_EMP_NO
            //    });

            result.Entries.AddRange(queryList);
            return result;
        }

        public bool Create(List<AccountFunctionDisabled> Datas, decimal dSignID, bool bIsNew)
        {
            //Entities.PAM_ACCOUNT_CHANGE.RemoveRange(Entities.PAM_ACCOUNT_CHANGE.Where(x => x.SIGN_FORM_ID == dSignID));

            //var autoincrementId = Entities.PAM_ACCOUNT_CHANGE.Select(x => x.ID).DefaultIfEmpty(0).Max();

            //foreach (var data in Datas)
            //{
            //    autoincrementId++;
            //    var accountChange = new PAM_ACCOUNT_CHANGE
            //    {
            //        ID = autoincrementId,
            //        DOMAIN_NAME = data.DomainName,
            //        REQUIRE_DESCRIPTION = data.RequireDescription,
            //        ATTACHMENT = data.Attachment?.Replace(base.ITCPAMRootAPIUri, ""),
            //        UPDATER_EMP_NO = base.UpdaterEmpNo,
            //        UPDATE_DATE = DateTime.Now
            //    };
            //    Entities.PAM_ACCOUNT_CHANGE.Add(accountChange);
            //}

            //Entities.SaveChanges();
            return true;
        }
        public bool Approve(List<AccountFunctionDisabled> Sign)
        {
            return true;
        }

        public string Approved(List<AccountFunctionDisabled> Datas, SignFormMain Sign)
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

        public bool Rejected(List<AccountFunctionDisabled> Sign)
        {
            return true;
        }
        public bool Close(List<AccountFunctionDisabled> Datas, SignFormMain Sign)
        {
            return true;
        }
        public void SetEntities(Entities entities)
        {
            Entities = entities;
        }

        public void SetUpdaterEmpNo(string empNo)
        {
            this.UpdaterEmpNo = empNo;
        }


        public bool Invalid(List<AccountFunctionDisabled> Datas)
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
