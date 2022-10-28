
using AutoMapper;
using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Model;
using Mxic.ITC.PAM.Model.Business;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Extensions;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mxic.ITC.PAM.Repository
{

    public class PAMAccountChangeRepository : RepositoryBase, ISignRepository<List<PAMAccountChange>>
    {
        private readonly IMapper _mapper;
        private MailOutDomainRepository mailOutDomainRepository { get; set; }
        public PAMAccountChangeRepository()
        {
            var config = new MapperConfiguration(
                cfg =>
                {


                    cfg.CreateMap<SignFormMain, SIGN_FORM_MAIN>()
                   .ForMember(s => s.APPLICANTER_DEPT_NO, opt => opt.MapFrom(s => s.ApplicanterDeptNO))
                   .ForMember(s => s.REQUIRED_DATE, opt => opt.MapFrom(s => s.RequiredDate))
                   .ForMember(s => s.REQUIRED_DESCRIPTION, opt => opt.MapFrom(s => s.RequiredDesc))
                   .ForMember(s => s.APPLICANTER_EMP_NO, opt => opt.MapFrom(s => s.ApplicanterEmpNO))
                   .ForMember(s => s.APPLICANTER_NAME, opt => opt.MapFrom(s => s.ApplicanterName))
                   .ForMember(s => s.CREATE_DATE, opt => opt.MapFrom(s => s.CreateDate))
                   .ForMember(s => s.FILLER_DEPT_NO, opt => opt.MapFrom(s => s.FillerDeptNO))
                   .ForMember(s => s.FILLER_EMP_NO, opt => opt.MapFrom(s => s.FillerEmpNO))
                   .ForMember(s => s.FILLER_NAME, opt => opt.MapFrom(s => s.FillerName))
                   .ForMember(s => s.FINAL_SIGN_DATE, opt => opt.MapFrom(s => s.FinalSignDate))
                   .ForMember(s => s.FLOW_ID, opt => opt.MapFrom(s => s.FlowID))
                   .ForMember(s => s.FORM_STATUS, opt => opt.MapFrom(s => s.FormStatus))
                   .ForMember(s => s.FORM_TYPE, opt => opt.MapFrom(s => s.FormType))
                   .ForMember(s => s.RELATED_MAIN, opt => opt.MapFrom(s => s.RelatedMain))
                   .ForMember(s => s.SERVICE_CODE, opt => opt.MapFrom(s => s.ServiceCode))
                   .ForMember(s => s.SIGN_FORM_NO, opt => opt.MapFrom(s => s.SignFromNo))
                   .ForMember(s => s.SIGN_FORM_ID, opt => opt.MapFrom(s => s.SignFromID));

                    cfg.CreateMap<SIGN_FORM_MAIN, SignFormMain>()
                    .ForMember(s => s.ApplicanterDeptNO, opt => opt.MapFrom(s => s.APPLICANTER_DEPT_NO))
                    .ForMember(s => s.RequiredDate, opt => opt.MapFrom(s => s.REQUIRED_DATE))
                    .ForMember(s => s.RequiredDesc, opt => opt.MapFrom(s => s.REQUIRED_DESCRIPTION))
                    .ForMember(s => s.ApplicanterEmpNO, opt => opt.MapFrom(s => s.APPLICANTER_EMP_NO))
                    .ForMember(s => s.ApplicanterName, opt => opt.MapFrom(s => s.APPLICANTER_NAME))
                    .ForMember(s => s.CreateDate, opt => opt.MapFrom(s => s.CREATE_DATE))
                    .ForMember(s => s.FillerDeptNO, opt => opt.MapFrom(s => s.FILLER_DEPT_NO))
                    .ForMember(s => s.FillerEmpNO, opt => opt.MapFrom(s => s.FILLER_EMP_NO))
                    .ForMember(s => s.FillerName, opt => opt.MapFrom(s => s.FILLER_NAME))
                    .ForMember(s => s.FinalSignDate, opt => opt.MapFrom(s => s.FINAL_SIGN_DATE))
                    .ForMember(s => s.FlowID, opt => opt.MapFrom(s => s.FLOW_ID))
                    .ForMember(s => s.FormStatus, opt => opt.MapFrom(s => s.FORM_STATUS))
                    .ForMember(s => s.FormType, opt => opt.MapFrom(s => s.FORM_TYPE))
                    .ForMember(s => s.RelatedMain, opt => opt.MapFrom(s => s.RELATED_MAIN))
                    .ForMember(s => s.ServiceCode, opt => opt.MapFrom(s => s.SERVICE_CODE))
                    .ForMember(s => s.SignFromNo, opt => opt.MapFrom(s => s.SIGN_FORM_NO))
                    .ForMember(s => s.SignFromID, opt => opt.MapFrom(s => s.SIGN_FORM_ID));



                });

            _mapper = config.CreateMapper();
            mailOutDomainRepository = new MailOutDomainRepository();
            mailOutDomainRepository.SetEntities(Entities);
        }

        public bool Create(List<PAMAccountChange> Datas, decimal dSignID, bool bIsNew)
        {
            Entities.PAM_ACCOUNT_CHANGE.RemoveRange(Entities.PAM_ACCOUNT_CHANGE.Where(x => x.SIGN_FORM_ID == dSignID));

            var autoincrementId = Entities.PAM_ACCOUNT_CHANGE.Select(x => x.ID).DefaultIfEmpty(0).Max();

            foreach (var data in Datas)
            {
                autoincrementId++;
                var accountChange = new PAM_ACCOUNT_CHANGE
                {
                    ID = autoincrementId,
                    DOMAIN_NAME = data.DomainName,
                    REQUIRE_DESCRIPTION = data.RequireDescription,
                    ATTACHMENT = data.Attachment?.Replace(base.ITCPAMRootAPIUri, ""),
                    UPDATER_EMP_NO = base.UpdaterEmpNo,
                    UPDATE_DATE = DateTime.Now
                };
                Entities.PAM_ACCOUNT_CHANGE.Add(accountChange);
            }

            Entities.SaveChanges();
            return true;
        }
        public bool Approve(List<PAMAccountChange> Sign)
        {
            return true;
        }

        public string Approved(List<PAMAccountChange> Datas, SignFormMain Sign)
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

        public bool Rejected(List<PAMAccountChange> Sign)
        {
            return true;
        }
        public bool Close(List<PAMAccountChange> Datas, SignFormMain Sign)
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


        public bool Invalid(List<PAMAccountChange> Datas)
        {
            return true;
        }

        public PageQueryResult<PAMAccountChange> GetDetails(PageQuery<PAMMailOutDomainRequest> request)
        {
            var result = new PageQueryResult<PAMAccountChange>();
            request.Sort = "Id";
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

        public PageQueryResult<PAMAccountChange> GetAll(PageQuery<PAMAccountChangeRequest> request)
        {
            var result = new PageQueryResult<PAMAccountChange>();

            var queryable = (from t1 in Entities.PAM_ACCOUNT_CHANGE.AsNoTracking()
                             join t2 in Entities.SIGN_FORM_MAIN.AsNoTracking() on t1.SIGN_FORM_ID equals t2.SIGN_FORM_ID
                             orderby t2.SIGN_FORM_ID descending
                             select new { t1, t2 })
                             .AsQueryable();

            if (request.QueryObject != null)
            {
                var underDateTime = DateTime.Now.AddYears(-1);
                if (request.QueryObject.IsOneYear)
                    queryable = queryable.Where(x => x.t2.CREATE_DATE >= underDateTime);
                else
                    queryable = queryable.Where(x => x.t2.CREATE_DATE <= underDateTime);
            }

            var queryList = queryable.ToList()
                .Select(x => new PAMAccountChange
                {
                    Id = x.t1.ID,
                    SignFormNo = x.t2.SIGN_FORM_NO,
                    FlowName = x.t2.FLOW_SETTING_MAIN.FLOW_NAME,
                    FormStatus = x.t2.FORM_STATUS,
                    SignFormId = x.t2.SIGN_FORM_ID,
                    ApplicanterDate = x.t2.CREATE_DATE,
                    ApplicanterName = x.t2.APPLICANTER_NAME,
                    ApplicanterDeptNo = x.t2.APPLICANTER_DEPT_NO,
                    SignatoryName = x.t2.SIGN_STAGE.Any(s => s.STAGE_ORDER == x.t2.NOW_STAGE_ORDER)
                                    ? x.t2.SIGN_STAGE.Where(s => s.STAGE_ORDER == x.t2.NOW_STAGE_ORDER).OrderBy(s => s.SEQ)
                                        .Select(s => s.SIGNATORY_NAME)
                                        .ToList().Aggregate((current, next) => current + "/" + next)
                                    : string.Empty,
                    UpdateDate = x.t1.UPDATE_DATE,
                    UpdaterEmpNo = x.t1.UPDATER_EMP_NO
                });

            result.Entries.AddRange(queryList);
            return result;
        }
    }
}
