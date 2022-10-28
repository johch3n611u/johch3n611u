
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

namespace Mxic.ITC.PAM.Service
{
    [Authorization]
    public class PAMAccountChangeService : BaseService
    {
        public IHrMasterService HrMasterService { get; set; }
        public IBPMService BPMService { get; set; }
        SignRepository<List<PAMAccountChange>> Repository;
        public PAMAccountChangeService()
        {
            Repository = new SignRepository<List<PAMAccountChange>>(new PAMAccountChangeRepository());
            HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
            BPMService = new BpmService(MembershipStore);            
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> Approve(SignData<List<PAMAccountChange>> Data)
        {
            return Repository.Approve(Data, UserInfo.Account, HrMasterService, BPMService);
        }
        [ExposeWebAPI(true)]
        public PageQueryResult<string> Rejected(SignData<List<PAMAccountChange>> Data)
        {
            return Repository.Rejected(Data, UserInfo.Account, HrMasterService, BPMService);
        }
        [ExposeWebAPI(true)]
        public PageQueryResult<string> Reject(SignData<List<PAMAccountChange>> Data)
        {
            return Repository.Reject(Data, UserInfo.Account, HrMasterService, BPMService);
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> Create(SignData<List<PAMAccountChange>> Data)
        {
            Data.Sign.ApplicanterEmpNO = UserInfo.Account;
            Data.Sign.FillerEmpNO = UserInfo.Account;

            var signer = new Signer();
            signer.CaseOfficerCosign.Add("00011");
            signer.CaseOfficerMgrCosign.Add("00010");
            return Repository.Create(Data, UserInfo.Account, HrMasterService, BPMService, signer);
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> Closed(SignData<List<PAMAccountChange>> Data)
        {
            return Repository.Close(Data, UserInfo.Account, HrMasterService, BPMService);
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<PAMAccountChange> GetDetails(PageQuery<PAMMailOutDomainRequest> request)
        {
            var response = new PageQueryResult<PAMAccountChange>();
            try
            {
                using (var repository = new PAMAccountChangeRepository())
                {
                    return repository.GetDetails(request);
                }
            }
            catch (Exception ex)
            {
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message;

            }
            return response;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<PAMAccountChange> GetAll(PageQuery<PAMAccountChangeRequest> request)
        {
            var response = new PageQueryResult<PAMAccountChange>();
            try
            {
                using (var repository = new PAMAccountChangeRepository())
                {
                    return repository.GetAll(request);
                }
            }
            catch (Exception ex)
            {
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message;

            }
            return response;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<Account> GetAccounts(AccountRequest request)
        {
            var response = new PageQueryResult<Account>();
            try
            {
                using (var repository = new AccountRepository())
                {
                    return repository.GetAccountsBySignFormMain(request);
                }
            }
            catch (Exception ex)
            {
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message;
            }
            return response;
        }


      
    }
}
