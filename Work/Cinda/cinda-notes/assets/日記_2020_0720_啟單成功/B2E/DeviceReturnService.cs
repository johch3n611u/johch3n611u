
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
using System.Linq;
using System.Web.Razor.Generator;

namespace Mxic.ITC.PAM.Service
{
    [Authorization]
    public class DeviceReturnService : BaseService
    {
        public IHrMasterService HrMasterService { get; set; }
        public IBPMService BPMService { get; set; }
        SignRepository<List<DeviceReturnList>> Repository;
        public DeviceReturnService()
        {
            // DI Container DeviceReturnListRepository
            Repository = new SignRepository<List<DeviceReturnList>>(new DeviceReturnListRepository());
            HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
            BPMService = new BpmService(MembershipStore);
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<DeviceReturnList> GetAll(PageQuery<DeviceReturnList> request)
        {
            var response = new PageQueryResult<DeviceReturnList>();
            //try
            //{
            using (var repository = new DeviceReturnListRepository())
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
        public PageQueryResult<DeviceReturnList> GetDetail(decimal requestSignFormId)
        {
            var response = new PageQueryResult<DeviceReturnList>();
            //try
            //{
            using (var repository = new DeviceReturnListRepository())
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
        public bool Update(DeviceReturnList Data)
        {
            // 以下為重新 new Repository 方法
            // 目前不確定 signform 如何新增 先改為以下方式

            bool response = false;

            using (var repository = new DeviceReturnListRepository())
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
        public PageQueryResult<string> Create(SignData<List<DeviceReturnList>> Data)
        {

            PageQueryResult<string> response = new PageQueryResult<string>();

            // if (AutoIncrement == 0) --> SignRepository.cs line 209 
            // 當 Data.Sign.SignFromID 等於 0 時啟單，所以前端或此處必須先
            // 依據 DB Account Function Disabled 與 Device Return 之關聯
            // 判斷是否重複決定是否啟單


            using (var repository = new DeviceReturnListRepository())
            {

                var repeatCount = 0;

                if (Data.FormData.FirstOrDefault().DeviceReturnId != null)
                {
                    var ParseString = Decimal.Parse(Data.FormData.FirstOrDefault().DeviceReturnId);
                    repeatCount = repository.Entities.PAM_DEVICE_RETURN.Where(x => x.ID == ParseString).ToList().Count;
                }

                // 無重複筆數
                if (repeatCount == 0)
                {
                    Data.Sign.SignFromID = 0;
                }
                else {
                    response.Message = "此單重複";
                    return response;
                }
                    var signer = new Signer();
                    signer.CaseOfficerCosign.Add("00011");
                    signer.CaseOfficerMgrCosign.Add("00010");
                // SignRepository.cs 會根據傳入的 Data.Sign.SignFromID 判斷是否重複
                response = Repository.Create(Data, UserInfo.Account, HrMasterService, BPMService, signer);

            }

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
