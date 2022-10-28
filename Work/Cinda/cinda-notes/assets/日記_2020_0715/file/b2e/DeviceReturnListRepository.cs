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

        // public bool Create(List<DeviceReturnList> Datas, decimal dSignID, bool bIsNew) { return true; }
        public bool Create(List<DeviceReturnList> Datas, decimal dSignID, bool bIsNew)
        {
            return true;
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

                    if (queryable.Where(x => x.t2.SIGN_FORM_ID == SignFormId).ToList().Count == 1) {

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

        public string ChkBoolean(string keyword) {
            string result;
            if (keyword == "true" ) { result = "1"; } else { result = "0"; }
            return result;
        }
    }
}
