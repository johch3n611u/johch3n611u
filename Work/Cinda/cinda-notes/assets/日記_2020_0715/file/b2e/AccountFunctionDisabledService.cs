
using Autofac;
using Mxic.Framework.ServerComponent;
using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Interface;
using Mxic.ITC.PAM.Model;
using Mxic.ITC.PAM.Model.BPM;
using Mxic.ITC.PAM.Model.Business;

using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Extensions;
using Mxic.ITC.PAM.Model.Sign;
using Mxic.ITC.PAM.Repository;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Mxic.ITC.PAM.Utility;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Configuration;
using Mxic.ITC.PAM.Repository;

namespace Mxic.ITC.PAM.Service
{
    [Authorization]
    public class AccountFunctionDisabledService : BaseService
    {
        public IHrMasterService HrMasterService { get; set; }
        public IBPMService BPMService { get; set; }
        SignRepository<List<AccountFunctionDisabledDetail>> Repository;
        public AccountFunctionDisabledService()
        {
            // DI Container AccountFunctionDisabledRepository
            Repository = new SignRepository<List<AccountFunctionDisabledDetail>>(new AccountFunctionDisabledRepository());
            HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
            BPMService = new BpmService(MembershipStore);
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<AccountFunctionDisabled> GetAll(PageQuery<AccountFunctionDisabled> request)
        {
            var response = new PageQueryResult<AccountFunctionDisabled>();
            //try
            //{
            using (var repository = new AccountFunctionDisabledRepository())
            {
                response = repository.GetAll(request);
            }
            //}
            //catch (Exception ex)
            //{
            //    throwException(ex);
            //    response.StatusCode = (long)EnumStatusCode.Exception;
            //    response.Message = ex.Message;

            //}

            return response;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<AccountFunctionDisabledDetail> GetDetail(decimal requestSignFormId)
        {
            var response = new PageQueryResult<AccountFunctionDisabledDetail>();
            //try
            //{
            using (var repository = new AccountFunctionDisabledRepository())
            {
                response = repository.GetDetail(requestSignFormId);
            }
            //}
            //catch (Exception ex)
            //{
            //    throwException(ex);
            //    response.StatusCode = (long)EnumStatusCode.Exception;
            //    response.Message = ex.Message;

            //}

            return response;
        }

        [ExposeWebAPI(true)]
        public bool Update(List<AccountFunctionDisabledDetail> Data)
        {
            // 以下為重新 new Repository 方法
            // 目前不確定 signform 如何新增 先改為以下方式

            bool response = false;

            using (var repository = new AccountFunctionDisabledRepository())
            {
                response = repository.Update(Data);
            }

            //return response;

            // 以下為 DI 介面方法

            //var signer = new Signer();
            //signer.CaseOfficerCosign.Add("00011");
            //signer.CaseOfficerMgrCosign.Add("00010");

            //var response = Repository.Create(Data, UserInfo.Account, HrMasterService, BPMService, signer);

            return response;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<AccountFunctionDisabled> GetNoSelected(PageQuery<AccountFunctionDisabled> request)
        {
            var response = new PageQueryResult<AccountFunctionDisabled>();
            //try
            //{
            using (var repository = new AccountFunctionDisabledRepository())
            {
                response = repository.GetNoSelected(request);
            }
            //}
            //catch (Exception ex)
            //{
            //    throwException(ex);
            //    response.StatusCode = (long)EnumStatusCode.Exception;
            //    response.Message = ex.Message;

            //}

            return response;
        }

        //[ExposeWebAPI(true)]
        //public PageQueryResult<string> Approve(SignData<List<PAMAccountChange>> Data)
        //{
        //    return Repository.Approve(Data, UserInfo.Account, HrMasterService, BPMService);
        //}
        //[ExposeWebAPI(true)]
        //public PageQueryResult<string> Rejected(SignData<List<PAMAccountChange>> Data)
        //{
        //    return Repository.Rejected(Data, UserInfo.Account, HrMasterService, BPMService);
        //}
        //[ExposeWebAPI(true)]
        //public PageQueryResult<string> Reject(SignData<List<PAMAccountChange>> Data)
        //{
        //    return Repository.Reject(Data, UserInfo.Account, HrMasterService, BPMService);
        //}



        //[ExposeWebAPI(true)]
        //public PageQueryResult<string> Closed(SignData<List<PAMAccountChange>> Data)
        //{
        //    return Repository.Close(Data, UserInfo.Account, HrMasterService, BPMService);
        //}

        //[ExposeWebAPI(true)]
        //public PageQueryResult<PAMAccountChange> GetDetails(PageQuery<PAMMailOutDomainRequest> request)
        //{
        //    var response = new PageQueryResult<PAMAccountChange>();
        //    try
        //    {
        //        using (var repository = new PAMAccountChangeRepository())
        //        {
        //            return repository.GetDetails(request);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throwException(ex);
        //        response.StatusCode = (long)EnumStatusCode.Exception;
        //        response.Message = ex.Message;

        //    }
        //    return response;
        //}



        //[ExposeWebAPI(true)]
        //public PageQueryResult<Account> GetAccounts(AccountRequest request)
        //{
        //    var response = new PageQueryResult<Account>();
        //    try
        //    {
        //        using (var repository = new AccountRepository())
        //        {
        //            return repository.GetAccountsBySignFormMain(request);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throwException(ex);
        //        response.StatusCode = (long)EnumStatusCode.Exception;
        //        response.Message = ex.Message;
        //    }
        //    return response;
        //}



    }
}
