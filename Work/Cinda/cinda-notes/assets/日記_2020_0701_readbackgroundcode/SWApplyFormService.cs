using Autofac;
using Mxic.Framework.ServerComponent;
using Mxic.QEC.ICMS.Interface;
using Mxic.ITC.Portal.Model.Business;
using Mxic.ITC.Portal.Model.Sign;
using Mxic.ITC.Portal.Repository;
using Mxic.ITC.Portal.Repository.Repository;
using Mxic.ITC.Portal.Repository.UnitOfWork;
using Mxic.ITC.Portal.Utility;
using System.Collections.Generic;
using Mxic.ITC.Portal.Model.Entity;
using System.Linq;
using Mxic.ITC.Portal.Enum;
using System.Threading.Tasks;

namespace Mxic.ITC.Portal.Service
{
    [Authorization]
    public class SWApplyFormService : BaseService
    {
        SignRepository<SwapplyForm> Repository;
        public IHrMasterService HrMasterService { get; set; }
        public IBPMService BPMService { get; set; }
        public SWApplyFormService()
        {
            Repository = new SignRepository<SwapplyForm>(new SWApplyFormRepository());
            HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
            BPMService = AutofacResolverHelper.Current.Container.Resolve<IBPMService>();
        }
        [ExposeWebAPI(true)]
        public PageQueryResult<string> Approve(SignData<SwapplyForm> Data)
        {
            BPMService = new BpmService(MembershipStore);
            var result = Repository.Approve(Data, UserInfo.Account, HrMasterService, BPMService);
            Task.Run(() =>
            {
                var batchService = new BatchService();
                batchService.FlowNotification();
            });
            return result;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> Close(SignData<SwapplyForm> Data)
        {
            BPMService = new BpmService(MembershipStore);
            var result = Repository.Close(Data, UserInfo.Account, HrMasterService, BPMService);
            Task.Run(() =>
            {
                var batchService = new BatchService();
                batchService.FlowNotification();
            });
            return result;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> ChangeApprover(SignData<SwapplyForm> Data)
        {
            BPMService = new BpmService(MembershipStore);
            var result = Repository.ChangeApprover(Data, UserInfo.Account, HrMasterService, BPMService);
            Task.Run(() =>
            {
                var batchService = new BatchService();
                batchService.FlowNotification();
            });
            return result;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> Invalid(SignData<SwapplyForm> Data)
        {
            BPMService = new BpmService(MembershipStore);
            var result = Repository.Invalid(Data, UserInfo.Account, HrMasterService, BPMService);
            Task.Run(() =>
            {
                var batchService = new BatchService();
                batchService.FlowNotification();
            });
            return result;
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<string> Rejected(SignData<SwapplyForm> Data)
        {
            BPMService = new BpmService(MembershipStore);
            var result = Repository.Rejected(Data, UserInfo.Account, HrMasterService, BPMService);
            Task.Run(() =>
            {
                var batchService = new BatchService();
                batchService.FlowNotification();
            });
            return result;
        }
        [ExposeWebAPI(true)]
        public PageQueryResult<string> Reject(SignData<SwapplyForm> Data)
        {
            BPMService = new BpmService(MembershipStore);
            var result = Repository.Reject(Data, UserInfo.Account, HrMasterService, BPMService);
            Task.Run(() =>
            {
                var batchService = new BatchService();
                batchService.FlowNotification();
            });
            return result;
        }

        [ExposeWebAPI(true)] // 允許 API 被訪問 <https://softwareengineering.stackexchange.com/questions/203844/what-does-it-mean-to-expose-something>
        public PageQueryResult<string> Create(SignData<SwapplyForm> Data)
        // 反傳值為 PageQueryResult 物件 string 型別
        // SignData 型別 SwapplyForm 參考型別 <https://stackoverflow.com/questions/4737970/what-does-where-t-class-new-mean>
        {
            BPMService = new BpmService(MembershipStore); // Business Process Management ? 審核檢閱關卡配置 Service ???...
            Data.Sign.BpmFormType = BpmFormType.SAMSwapplyForm; // SAM SW 申請表格
            using (var repository = new SAMRepository()) // SAMRepository : RepositoryBase 繼承資料庫型別的類別，目前看都是一些取資料的方法。
            {
                List<SoftwareList> softList = repository.GetSoftwareList(new PageQuery<int> { PageSize = 9999, PageNum = 1 }).Entries;
                // List 物件 SoftwareList 型別
                var checkSoftwareList = (from t1 in softList
                                         join t2 in Data.FormData.SoftwareForm on t1.Id equals t2.SoftwareId
                                         // equals 等於 <https://docs.microsoft.com/zh-tw/dotnet/csharp/language-reference/keywords/equals>
                                         select new SoftwareList // System.Linq 方法
                                         {
                                             Type = t1.Type,
                                             PaymentSAMNo = t1.PaymentSAMNo,
                                             PaymentSAMNoBack = t1.PaymentSAMNoBack,
                                             FreeSAMNo = t1.FreeSAMNo,
                                             FreeSAMNoBack = t1.FreeSAMNoBack,
                                         }
                                         ).ToList(); // 將查詢轉為列表
                var checkType = "";
                var samNo = "";
                var samNoBack = "";
                List<string> signer = new List<string>();
                List<string> signerBack = new List<string>();
                if (Data.FormData.EndDate<Data.FormData.StartDate)
                {
                    return new PageQueryResult<string>
                    {
                        Message = "[tw]請確認試用起迄日期[/tw][en]Please confirm the trial date[/en]",
                        StatusCode = (long)Enum.EnumStatusCode.Fail // 錯誤狀態碼
                    };
                }// 目前不確定為何能轉換中英，斷點查看是在 new PageQueryResult 類別環節轉換...
                foreach (var item in checkSoftwareList)
                {

                        checkType = item.Type;
                        samNo = item.Type == "付費" ? item.PaymentSAMNo : item.FreeSAMNo;
                        samNoBack = item.Type == "付費" ? item.PaymentSAMNoBack : item.FreeSAMNoBack;
                        signer.Add(samNo);
                        signerBack.Add(samNoBack);

                }// 利用列表特性 foreach 做某些判斷
                signer = signer.Distinct().ToList();
                signerBack = signerBack.Distinct().ToList();// Distinct() linq 方法，剔除重複資料
                if (signer.Count == 0 || signer.Count >1)
                {
                    return new PageQueryResult<string>
                    {
                        Message = "",
                        StatusCode = (long)Enum.EnumStatusCode.FormSamCheck // 表格檢查
                    };
                }
                if (signerBack.Count > 1)
                {
                    return new PageQueryResult<string>
                    {
                        Message = "",
                        StatusCode = (long)Enum.EnumStatusCode.FormSamCheck // 表格檢查
                    };
                }

                var result = Repository.Create(Data, UserInfo.Account, HrMasterService, BPMService, new Model.Signer
                {
                    CaseOfficerCosign = signer,
                    CaseOfficerCosignBack = signerBack,
                });

                // 以上操作 SignRepository<SwapplyForm> Repository; 資料庫類別物件的方法 Create
                // 詳細內容補於 Repository.UnitOfWork.SignRepository.cs Creat 實作

                Task.Run(() =>
                {
                    var batchService = new BatchService();
                    batchService.FlowNotification(); // 流程通知
                });

                // 調用非同步方法 Task 執行批次 batchService

                return result;
            }

        }
        [ExposeWebAPI(true)]
        public List<FlowDiagram> GetFlowDiagram(SignData<SwapplyForm> Data)
        {
            return Repository.GetFlowDiagram(Data);

        }

        [ExposeWebAPI(true)]
        public IEnumerable<ApplyItem> GetApplyItems(string pStrLang)
        {
            SWApplyFormRepository objSF = new SWApplyFormRepository();
            return objSF.GetApplyItems(pStrLang);
        }


        [ExposeWebAPI(true)]
        public PageQueryResult<ServiceQuery> GetServiceList(PageQuery<int> query)
        {
            SWApplyFormRepository objSF = new SWApplyFormRepository();
            return objSF.GetServiceListD(query, 0, UserInfo.Account, IsPortalAdm(UserInfo.Account), HrMasterService);
        }

        [ExposeWebAPI(true)]
        public PageQueryResult<ServiceQuery> GetServiceList2(PageQuery<int> query)
        {
            SWApplyFormRepository objSF = new SWApplyFormRepository();
            return objSF.GetServiceListD(query, 1, UserInfo.Account, IsPortalAdm(UserInfo.Account), HrMasterService);
        }

        [ExposeWebAPI(true)]
        public SignData<SwapplyForm> GetSignByID(int pIntID)
        {
            SWApplyFormRepository objSF = new SWApplyFormRepository();
            return objSF.GetSignByID(pIntID);
        }

        [ExposeWebAPI(true)]
        public SignHeader GetSignform(int pIntID)
        {
            SignBaseRepository objSF = new SignBaseRepository();
            return objSF.GetSignform(pIntID, UserInfo.Account, HrMasterService);
        }
    }
}
