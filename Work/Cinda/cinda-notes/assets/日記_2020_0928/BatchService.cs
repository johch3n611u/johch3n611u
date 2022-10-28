using Mxic.Framework.ServerComponent;
using Mxic.ITC.PAM.Model.Business;
using Mxic.ITC.PAM.Utility;
using Mxic.ITC.PAM.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Mxic.ITC.PAM.Repository;
using System.Web.Configuration;
using Mxic.ITC.PAM.Model.HumanResource;
using Mxic.ITC.PAM.Model;
using Autofac;
using Mxic.ITC.PAM.Interface;

using Mxic.ITC.PAM.Interface;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Sign;
using Mxic.ITC.PAM.Repository.Repository;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Newtonsoft.Json;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Data.Entity.Core.Mapping;
using NPOI.OpenXmlFormats.Wordprocessing;
using Mxic.ITC.PAM.Model.Entity.PAM;

namespace Mxic.ITC.PAM.Service
{
    public class BatchService : BaseService
    {
        StringBuilder sbMailCC = new StringBuilder();
        private readonly string ITCWebUri = string.Empty;
        int IntAutoID = 0;
        List<NOTIFICATION_TASK> vTaskList = new List<NOTIFICATION_TASK>();
        string StrMailTitle = "";
        string StrMailContent = "";
        public IHrMasterService HrMasterService { get; set; }
        public IBPMService BPMService { get; set; }
        SignRepository<List<AccountFunctionDisabledDetail>> SignListRepository;
        SignRepository<List<DeviceReturnList>> DeviceListRepository;
        SignRepository<List<AccountFunctionDisabledDetail>> Repository;
        SignRepository<PermissionDisableList> PS_Repository;
        SignRepository<List<PAM_PUSHMAIL>> PushmailActiveRepository;
        public BatchService()
        {
            // DI Container AccountFunctionDisabledRepository // 401
            SignListRepository = new SignRepository<List<AccountFunctionDisabledDetail>>(new AccountFunctionDisabledRepository());
            DeviceListRepository = new SignRepository<List<DeviceReturnList>>(new DeviceReturnListRepository());
            HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
            BPMService = new BpmService(MembershipStore);

            // DI Container AccountFunctionDisabledRepository // 404
            Repository = new SignRepository<List<AccountFunctionDisabledDetail>>(new AccountFunctionDisabledRepository());
            BPMService = new BpmService(MembershipStore);
            PS_Repository = new SignRepository<PermissionDisableList>(new ResignationDisableRepository());
            ITCWebUri = WebConfigurationManager.AppSettings["ITCWebUri"];
            PushmailActiveRepository = new SignRepository<List<PAM_PUSHMAIL>>(new PushmailActiveRepository());
        }

        #region PAM401

        /// <summary>
        /// UCPAM401: [離職會簽啟動帳號權限處理作業]
        /// 離職會簽啟動帳號權限處理作業 401 會產 三選一單 與 帳號關閉及設備繳回單
        /// </summary>
        /// <remarks>
        /// 排程功能檢查PAM資料庫表格PAM_IF_RESIGN (IF008)，
        /// 依據離職日期判斷是否可以產生離職帳號或權限停用單，
        /// 並紀錄處理紀錄於系統。
        /// </remarks>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public PageQueryResult<string> PAM401()
        {
            var response = new PageQueryResult<string>();

            try
            {
                // 確認 PAM_IF_RESIGN 近 7 天內是否有新增資料
                // Repository.Entities.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                var test = SignListRepository.Entities.PAM_IF_RESIGN.Where(x =>
                EntityFunctions.DiffDays(DateTime.Now, x.ACCOUNT_CLOSE_DATE) <= 7 &&
                EntityFunctions.DiffDays(DateTime.Now, x.ACCOUNT_CLOSE_DATE) >= -7)
                .ToList();
                if (SignListRepository.Entities.PAM_IF_RESIGN.Where(x =>
                EntityFunctions.DiffDays(DateTime.Now, x.ACCOUNT_CLOSE_DATE) <= 7 &&
                EntityFunctions.DiffDays(DateTime.Now, x.ACCOUNT_CLOSE_DATE) >= -7)
                .Any())
                {
                    CreateOneOfThree(); // 依條件產生[離職帳號或權限停用單]
                    CreateDeviceReturn(); // 依條件產生[帳號關閉及設備繳回單]
                }

            }
            catch (Exception ex)
            {
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                _logger.Error(ex.StackTrace);
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// 依條件產生[離職帳號或權限停用單]
        /// </summary>
        /// <remarks>
        /// 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.a
        /// </remarks>
        /// <returns></returns>
        public PageQueryResult<string> CreateOneOfThree()
        {
            var response = new PageQueryResult<string>();

            var NewSignFormIdEF = new Mxic.ITC.PAM.Model.Business.PageQueryResult<string>(); // Log 使用

            var SevenDays_List = (from t1 in SignListRepository.Entities.PAM_IF_RESIGN.AsNoTracking()
                                  join t2 in SignListRepository.Entities.ACCOUNT.AsNoTracking() on t1.EMP_NO equals t2.EMP_NO
                                  // into ft from t2 in ft.DefaultIfEmpty()
                                  where
                                  EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
                                  EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) >= -7
                                  select new { t1, t2 })
                        .ToList(); // 7 天內新增資料

            var ChkList = SevenDays_List.Select(x => x.t2).Distinct(new PropertyComparer<ACCOUNT>("EMP_NO")).ToList();

            SevenDays_List.Select(x => x.t2).ToList().ForEach(t2 =>
            {

                // 三選一單判斷是否有重啟條件為該 EMPNO 在 SIGN_FORM_MAIN 為單筆且 FORM_TYPE 為 DisabledList
                // 以上改為 ThreeOfOne
                if (!SignListRepository.Entities.SIGN_FORM_MAIN.Any(x => x.APPLICANTER_EMP_NO == t2.EMP_NO && x.FORM_TYPE == "ThreeOfOne"))
                {

                    var t1 = SignListRepository.Entities.PAM_IF_RESIGN.FirstOrDefault(x => x.EMP_NO == t2.EMP_NO);

                    // 不管前面過幾筆單只起一次三選一單
                    var SignFormList = SignListRepository.Entities.SIGN_FORM_MAIN.Where(y => y.APPLICANTER_EMP_NO == t1.EMP_NO).ToList();
                    var AFList = new List<PAM_AF_DISABLED>();
                    SignFormList.ForEach(x =>
                    {
                        var result = DeviceListRepository.Entities.PAM_AF_DISABLED.FirstOrDefault(z => z.SIGN_FORM_ID == x.SIGN_FORM_ID);
                        if (result != null)
                        {
                            AFList.Add(result);
                        }
                    });
                    if (AFList.Count == 0)
                    {
                        // 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.a
                        // 以下狀況，產生[離職帳號或權限停用單](如圖UCPAM402-1)，
                        // TODO : 並寄送通知：
                        // 目前依照文件 PAM_IF_RESIGN 內有 "帳號權限預計關閉日"，所以不用自建 IsHoliday 外部資料 Services
                        // 權限清單判別改以 ACCOUNT.LEABING_CONTROLS 從 PORTAL 結單時傳來的離職控管 Boolean Y/N
                        // TODO : 4.防呆：檢查當時為使用中之自有帳號或授權，若已有未結案之停用單則不列出。

                        // 3. AccountSub
                        // Personal = 0, // 員工個人帳號
                        // OnlyOne = 1, // 專用帳號（專人專用）
                        // Public = 2, // 公用帳號（多人共用）
                        // NotEmployee = 3, // 非旺宏員工帳號
                        // Maintain = 4, // 系統維運帳號
                        // System = 5, // 系統整合帳號

                        try
                        {
                            if (// 狀況
                                   t2.FUNCTION_TYPE == 1 && t2.ENABLE_AD == "Y" &&// 1. 此離職同仁工號有AD帳號 (ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true)
                                   DateTime.Now < t1.ACCOUNT_CLOSE_DATE // 2. 執行當天小於[離職日-2個工作日]
                            )
                            {
                                // TODO : 啟單資料要補
                                SignData<List<AccountFunctionDisabledDetail>> Data = new SignData<List<AccountFunctionDisabledDetail>>();
                                Data = SetSignData();

                                var objPortalService = new PortalRepository().GetPortalSystemServices(Data.Sign.ServiceCode);
                                List<PORTAL_SYSTEM_SERVICES> OEF = objPortalService.Entries.Where(x => x.SERVICE_CODE == Data.Sign.ServiceCode).ToList();
                                List<string> COC = new List<string>();
                                List<string> COCB = new List<string>();
                                foreach (var item in OEF)
                                {
                                    COC.Add(item.ORGANIZER_EMPNO);
                                    COCB.Add(item.BACKUP_ORGANIZER_EMPNO);
                                }

                                NewSignFormIdEF = SignListRepository.Create(Data, t1.EMP_NO, HrMasterService, BPMService, new Model.Signer
                                {
                                    CaseOfficerCosign = COC,
                                    CaseOfficerCosignBack = COCB
                                });

                                // 因為是 Draft 不進關卡所以要回塞 SignFormNo
                                var SignFormID = NewSignFormIdEF.Entries[0];
                                var SignFormNo = SignListRepository.GetSignNo("ThreeOfOne");
                                AccountFunctionDisabledRepository AFRepository = new AccountFunctionDisabledRepository();
                                AFRepository.SetSignFormNo(SignFormID, SignFormNo);

                                #region 設定啟單資料

                                SignData<List<AccountFunctionDisabledDetail>> SetSignData()
                                {

                                    Data.Sign = new SignFormMain();

                                    Data.FormType = "Draft"; //TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                                    Data.Sign.SignFromID = 0; // 控制啟單
                                    Data.Sign.RequiredDate = DateTime.Now;
                                    Data.Sign.FormStatus = "Draft"; // 表單狀態
                                    Data.Sign.BpmFormType = BpmFormType.ThreeOfOne; // 表單類型
                                    Data.Sign.ServiceCode = "";
                                    Data.SignButtonKey = "SignButton.ThreeOfOneWait.Send"; // 放前端按鈕的名稱
                                    Data.Sign.ApplicanterEmpNO = t1.EMP_NO; // 申請人

                                    AccountFunctionDisabledDetail newItem = new AccountFunctionDisabledDetail();
                                    newItem.CloseDate = t1.ACCOUNT_CLOSE_DATE; // 帳號權限預計關閉日
                                    newItem.Name = t1.EMP_NO; // 離職人工號
                                    Data.FormData = new List<AccountFunctionDisabledDetail>();
                                    Data.FormData.Add(newItem);

                                    return Data;
                                }

                                #endregion
                            }

                            // TODO :需確認是否每筆ACCOUNT都要LOG還是啟三選一單的要判斷即可 Log 帳號與軟體改善專案_PAM_SRS_V1.28 p.43 - 3.c


                            if (NewSignFormIdEF.Entries.Count > 0)
                            {
                                var NewSignFormId = NewSignFormIdEF.Entries[0];
                                LogOneOfThree(t1, t2, NewSignFormId);
                            }
                        }
                        catch (Exception ex)
                        {
                            throwException(ex);
                            response.StatusCode = (long)EnumStatusCode.Exception;
                            response.Message = ex.Message;
                        }

                    }
                }
            });

            return response;
        }

        /// <summary>
        /// 依條件產生[帳號關閉及設備繳回單]
        /// </summary>
        /// <remarks>
        /// 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.b
        /// </remarks>
        /// <returns></returns>
        public PageQueryResult<string> CreateDeviceReturn()
        {
            var response = new PageQueryResult<string>();

            try
            {

                var SevenDays_List = (from t1 in DeviceListRepository.Entities.PAM_IF_RESIGN.AsNoTracking()
                                      join t2 in DeviceListRepository.Entities.ACCOUNT.AsNoTracking() on t1.EMP_NO equals t2.EMP_NO
                                      // into ft from t2 in ft.DefaultIfEmpty()
                                      where
                                      EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
                                      EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) >= -7
                                      select new { t1, t2 })
                            .ToList(); // 7 天內新增資料

                SevenDays_List.Select(x => x.t2).ToList().ForEach(t2 =>
                {
                    // 三選一單判斷是否有重啟條件為該 EMPNO 在 SIGN_FORM_MAIN 為單筆且 FORM_TYPE 為 DeviceReturn
                    if (!SignListRepository.Entities.SIGN_FORM_MAIN.Any(x => x.APPLICANTER_EMP_NO == t2.EMP_NO && x.FORM_TYPE == "DeviceReturn"))
                    {

                        var t1 = DeviceListRepository.Entities.PAM_IF_RESIGN.FirstOrDefault(x => x.EMP_NO == t2.EMP_NO);

                        var SignFormList = DeviceListRepository.Entities.SIGN_FORM_MAIN.Where(y => y.APPLICANTER_EMP_NO == t1.EMP_NO).ToList();
                        var DRList = new List<PAM_DEVICE_RETURN>();
                        SignFormList.ForEach(x =>
                        {
                            var result = DeviceListRepository.Entities.PAM_DEVICE_RETURN.FirstOrDefault(z => z.SIGN_FORM_ID == x.SIGN_FORM_ID);
                            if (result != null)
                            {
                                DRList.Add(result);
                            }
                        });
                        if (DRList.Count == 0)
                        {
                            // 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.b
                            // 以下狀況，產生[帳號關閉及設備繳回單](如圖UCPAM402-2)，
                            // TODO : 並寄送通知：
                            // 狀況：

                            // 3. AccountSub
                            // Personal = 0, // 員工個人帳號
                            // OnlyOne = 1, // 專用帳號（專人專用）
                            // Public = 2, // 公用帳號（多人共用）
                            // NotEmployee = 3, // 非旺宏員工帳號
                            // Maintain = 4, // 系統維運帳號
                            // System = 5, // 系統整合帳號

                            try
                            {
                                var ShareHelper = new ShareHelper();
                                // 離職日 = 帳號權限預計關閉日 +2 個工作天
                                DateTime ResignationDay = ShareHelper.AddDays(t1.ACCOUNT_CLOSE_DATE, 2);
                                if (// 狀況
                                       t2.FUNCTION_TYPE == 1 && t2.ENABLE_AD == "Y" && // 1. 此離職同仁工號有AD帳號 (ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true)
                                       DateTime.Now < ResignationDay && DateTime.Now >= t1.ACCOUNT_CLOSE_DATE // 執行當天小於[離職日]＆大於等於[離職日 - 2個工作日]
                                )
                                {

                                    // TODO : 啟單資料要補
                                    SignData<List<DeviceReturnList>> Data = new SignData<List<DeviceReturnList>>();
                                    Data = SetSignData();

                                    var objPortalService = new PortalRepository().GetPortalSystemServices(Data.Sign.ServiceCode);
                                    List<PORTAL_SYSTEM_SERVICES> OEF = objPortalService.Entries.Where(x => x.SERVICE_CODE == Data.Sign.ServiceCode).ToList();
                                    List<string> COC = new List<string>();
                                    List<string> COCB = new List<string>();
                                    foreach (var item in OEF)
                                    {
                                        COC.Add(item.ORGANIZER_EMPNO);
                                        COCB.Add(item.BACKUP_ORGANIZER_EMPNO);
                                    }

                                    var result = DeviceListRepository.Create(Data, t1.EMP_NO, HrMasterService, BPMService, new Model.Signer
                                    {
                                        CaseOfficerCosign = COC,
                                        CaseOfficerCosignBack = COCB
                                    });


                                    // 狀態回寫已派工
                                    // 已派工 DReturnWork / 結案 DReturnEnd / 中止 DReturnStop

                                    var DR_SignFormId = result.Entries[0];

                                    var DRL = DeviceListRepository.Entities.PAM_DEVICE_RETURN.FirstOrDefault(x => x.ID == decimal.Parse(DR_SignFormId));

                                    var SFM = DeviceListRepository.Entities.SIGN_FORM_MAIN.FirstOrDefault(X => X.SIGN_FORM_ID == DRL.SIGN_FORM_ID);

                                    SFM.FORM_STATUS = "DReturnWork";

                                    DeviceListRepository.Entities.SaveChanges();

                                    #region 設定啟單資料

                                    SignData<List<DeviceReturnList>> SetSignData()
                                    {

                                        Data.Sign = new SignFormMain();

                                        Data.FormType = "Draft"; //TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                                        Data.Sign.SignFromID = 0; // 控制啟單
                                        Data.Sign.RequiredDate = DateTime.Now;
                                        Data.Sign.FormStatus = "Draft"; // 表單狀態
                                        Data.Sign.BpmFormType = BpmFormType.DeviceReturn; // 表單類型
                                        Data.SignButtonKey = "SignButton.DeviceReturn.Save"; // 放前端按鈕的名稱
                                        Data.Sign.ApplicanterEmpNO = t1.EMP_NO; // 申請人
                                        DeviceReturnList newItem = new DeviceReturnList();
                                        DateTime TimeSeparation = DateTime.Today.AddHours(13);// 當日 13:00 前後
                                        if (DateTime.Now < TimeSeparation)
                                        {
                                            // 若離職會簽單展開當下為 13:00 前，則帳號關閉及設備繳回日 = 當天，狀態＝[已派工]。
                                            newItem.ReturnDate = DateTime.Today;
                                        }
                                        else
                                        {
                                            // 若離職會簽單展開當下為 13:00 後，則帳號關閉及設備繳回日=當天+1，狀態＝[已派工]。
                                            newItem.ReturnDate = DateTime.Today.AddDays(1);
                                        }

                                        newItem.APPLICANTER_EMP_NO = t1.EMP_NO;

                                        Data.FormData = new List<DeviceReturnList>();
                                        Data.FormData.Add(newItem);

                                        return Data;
                                    }

                                    #endregion
                                }

                            }
                            catch (Exception ex)
                            {
                                throwException(ex);
                                var StatusCode = (long)EnumStatusCode.Exception;
                                var Message = ex.Message;
                            }

                        }
                    }
                });

            }
            catch (Exception ex)
            {
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 依條件產生[處理紀錄]
        /// </summary>
        /// <remarks>
        /// 帳號與軟體改善專案_PAM_SRS_V1.28 p.43 - 3.c
        /// </remarks>
        /// <returns></returns>
        public int LogOneOfThree(PAM_IF_RESIGN t1, ACCOUNT t2, string NewSignFormId)
        {
            AF_RECORD LogItem = new AF_RECORD();
            LogItem.ID = SignListRepository.Entities.AF_RECORD.Select(x => x.ID).DefaultIfEmpty(0).Max() + 1;
            var SignFormID = decimal.Parse(NewSignFormId);
            LogItem.REF_ID = SignListRepository.Entities.PAM_AF_DISABLED.FirstOrDefault(x => x.SIGN_FORM_ID == SignFormID).ID;

            var ShareHelper = new ShareHelper();
            // 離職日 = 帳號權限預計關閉日 +2 個工作天
            DateTime ResignationDay = ShareHelper.AddDays(t1.ACCOUNT_CLOSE_DATE, 2);
            // 帳號權限預計關閉日 = 離職日 -2 個工作天
            // 當日 13:00 前後
            DateTime TimeSeparation = DateTime.Today.AddHours(13);

            LogItem.CHECK_DATE = DateTime.Now;

            try
            {

                // 此紀錄後續於UCPAM410供檢視。其中檢查結果紀錄欄位請依序檢查以下狀況並分別紀錄如下：
                if (t2.FUNCTION_TYPE != 1 && t2.ENABLE_AD == null && t2.ENABLE_NOVELL == null && t2.ENABLE_NOTES == null)
                { // c1 此離職同仁工號沒有AD帳號(ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true) ：沒有AD / Novell / Notes帳號。
                    LogItem.RESULT = "此離職同仁工號沒有AD帳號：沒有AD / Novell / Notes帳號。";
                    SignListRepository.Entities.AF_RECORD.Add(LogItem);
                    SignListRepository.Entities.SaveChanges();
                }
                if (DateTime.Now >= ResignationDay)
                { // c2 執行當天大於等於離職日：會簽單產生日期大於等於離職日，關閉帳號由HR啟動。
                    LogItem.RESULT = $"執行當天大於等於離職日{ResignationDay}：會簽單產生日期大於等於離職日，關閉帳號由HR啟動。";
                    LogItem.ID++;
                    SignListRepository.Entities.AF_RECORD.Add(LogItem);
                    SignListRepository.Entities.SaveChanges();
                }
                if (DateTime.Now < t1.ACCOUNT_CLOSE_DATE)
                { // c3 執行當天小於[離職日 - 2個工作日]：會簽單展開日小於D - 2，已產生離職帳號或權限停用單。
                    LogItem.RESULT = $"執行當天小於[離職日-2個工作日]{t1.ACCOUNT_CLOSE_DATE}：會簽單展開日小於D-2，已產生離職帳號或權限停用單。";
                    LogItem.ID++;
                    SignListRepository.Entities.AF_RECORD.Add(LogItem);
                    SignListRepository.Entities.SaveChanges();
                }
                if (DateTime.Now < ResignationDay && DateTime.Now >= t1.ACCOUNT_CLOSE_DATE && DateTime.Now > TimeSeparation)
                { // c4 執行當天小於[離職日]＆大於等於[離職日 - 2個工作日] ＆　會簽單展開當下為 13:00 後：會簽單展開日大於等於D - 2，時間下午一點後，帳號關閉及設備繳回日為隔天。
                    LogItem.RESULT = $"執行當天小於[離職日]{ResignationDay}＆大於等於[離職日-2個工作日]{t1.ACCOUNT_CLOSE_DATE}＆會簽單展開當下為13:00後：會簽單展開日大於等於D-2，時間下午一點後，帳號關閉及設備繳回日為隔天。";
                    LogItem.ID++;
                    SignListRepository.Entities.AF_RECORD.Add(LogItem);
                    SignListRepository.Entities.SaveChanges();
                }
                if (DateTime.Now < ResignationDay && DateTime.Now >= t1.ACCOUNT_CLOSE_DATE && DateTime.Now < TimeSeparation)
                { // c5 執行當天小於[離職日]＆大於等於[離職日 - 2個工作日] ＆　會簽單展開當下為 13:00 前：會簽單展開日大於等於D - 2，時間下午一點前，帳號關閉及設備繳回日為當天。
                    LogItem.RESULT = $"執行當天小於[離職日]{ResignationDay}＆大於等於[離職日-2個工作日]{t1.ACCOUNT_CLOSE_DATE}＆會簽單展開當下為13:00前：會簽單展開日大於等於D-2，時間下午一點前，帳號關閉及設備繳回日為當天。";
                    LogItem.ID++;
                    SignListRepository.Entities.AF_RECORD.Add(LogItem);
                    SignListRepository.Entities.SaveChanges();
                }
                SignListRepository.Entities.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);


            }
            catch (Exception e)
            {
                throw e;
            }

            return Convert.ToInt32(LogItem.ID);
        }

        /// <summary>
        /// 更新 Disable_AF_Detail
        /// </summary>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public bool Update_Disable_AF_Detail()
        {
            try
            {
                using (var repository = new AccountFunctionDisabledRepository())
                {

                    //TODO ID 待補
                    var requestSignFormId = 0;
                    repository.Check_AF_Detail((int)requestSignFormId);

                }

                return true;
            }
            catch (Exception ex)
            {

                throwException(ex);
                return false;
            }
        }

        #endregion PAM401

        #region PAM404

        /// <summary>
        /// UCPAM404:[離職系統啟動即時關閉帳號作業]
        /// 離職系統啟動及時關閉帳號作業 404 會產 三選一單 與 權限停用單
        /// </summary>
        /// <remarks>
        /// 此 功能定期檢查PAM資料庫表格PAM_IF_HR_TRIGGER_CLOSE是否有需即時關閉帳號的資料。
        /// 若有，將立即影響Meta EMP (IF013)將AD帳號關閉，
        /// 並即時產生[帳號關閉及設備繳回單]，若主檔有權限則另產生[權限停用單]。
        /// </remarks>
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public PageQueryResult<string> PAM404()
        {
            // TODO : 404 時間若小於 401 以 404 優先
            var response = new PageQueryResult<string>();
            try
            {
                var SevenDays_List = Repository.Entities.PAM_IF_HR_TRIGGER_CLOSE.Where(x =>
                    EntityFunctions.DiffDays(DateTime.Now, x.ADD_DATE) <= 7 &&
                    EntityFunctions.DiffDays(DateTime.Now, x.ADD_DATE) >= -7
                ).ToList();
                // 7 天內
                if (SevenDays_List.Any())
                {
                    foreach (var SDQ in SevenDays_List)
                    {

                        // 三選一單判斷是否有重啟條件為該 EMPNO 在 SIGN_FORM_MAIN 為單筆且 FORM_TYPE 為 DisabledList
                        // 以上改為 ThreeOfOne
                        if (!Repository.Entities.SIGN_FORM_MAIN.Any(x => x.APPLICANTER_EMP_NO == SDQ.EMP_NO && x.FORM_TYPE == "ThreeOfOne"))
                        {

                            // 由 PAM_IF_HR_TRIGGER_CLOSE 工號查 SIGN_FORM_MAIN SignFormId 有的話就是有啟過某種單
                            var SIGN_FORM_List = Repository.Entities.SIGN_FORM_MAIN.Where(SFM => SFM.APPLICANTER_EMP_NO == SDQ.EMP_NO).ToList();

                            // SignFormId 在查 三選一表是否有單
                            List<PAM_AF_DISABLED> AF_FORM_List = new List<PAM_AF_DISABLED>();
                            foreach (var SFL in SIGN_FORM_List)
                            {
                                AF_FORM_List = Repository.Entities.PAM_AF_DISABLED.Where(x => x.SIGN_FORM_ID == SFL.SIGN_FORM_ID).ToList();
                            }

                            // 1.檢查該離職人員為以下哪一種情況 ? 分別執行不同作業：
                            if (!AF_FORM_List.Any())
                            {
                                #region A.沒有產生過帳號關閉及設備繳回單  產生帳號關閉及設備繳回單 ＆權限停用單(執行A-1, A-2, A-3)

                                var signer = new Signer();
                                signer.CaseOfficerCosign.Add("00011");
                                signer.CaseOfficerMgrCosign.Add("00010");

                                #region 啟三選一單 A-1

                                SignData<List<AccountFunctionDisabledDetail>> Data = new SignData<List<AccountFunctionDisabledDetail>>();
                                Data.Sign = new SignFormMain();
                                Data.FormType = "ThreeOfOne"; // TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                                Data.Sign.SignFromID = 0; // 控制啟單
                                Data.Sign.RequiredDate = DateTime.Now;
                                Data.Sign.FormStatus = "Draft"; // 表單狀態
                                Data.Sign.BpmFormType = BpmFormType.ThreeOfOne; // 表單類型
                                Data.Sign.ServiceCode = "AH0001";
                                Data.SignButtonKey = "SignButton.ThreeOfOneWait.Save"; // 放前端按鈕的名稱
                                Data.Sign.ApplicanterEmpNO = SDQ.EMP_NO; // 申請人
                                Data.Sign.FillerEmpNO = SDQ.EMP_NO; // 填表人

                                AccountFunctionDisabledDetail newItem = new AccountFunctionDisabledDetail();
                                newItem.CloseDate = DateTime.Now; // 帳號權限預計關閉日 A-1
                                newItem.Name = SDQ.EMP_NO; // 離職人工號
                                Data.FormData = new List<AccountFunctionDisabledDetail>();
                                Data.FormData.Add(newItem);

                                var ThreeOfOneEF = Repository.Create(Data, SDQ.EMP_NO, HrMasterService, BPMService, signer);
                                var ThreeOfOne_SIGN_FORM_ID = decimal.Parse(ThreeOfOneEF.Entries[0]);
                                var ThreeOfOne_FORM_STATUS = Repository.Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == ThreeOfOne_SIGN_FORM_ID).FORM_STATUS;
                                ThreeOfOne_FORM_STATUS = "ThreeOfOneEnd";
                                // A-1
                                // 待主管送件 DisabledListWait / 已派工 DisabledListWork / 結案 DisabledListEnd / 中止 DisabledListStop
                                // 以上改為 ThreeOfOne
                                // ThreeOfOneEF.Entries[0] = NEW_SIGN_FORM_ID
                                #endregion

                                #region 啟權限停用單 A-2

                                // 三選一單判斷是否有重啟條件為該 EMPNO 在 SIGN_FORM_MAIN 為單筆且 FORM_TYPE 為 AccountDisableForm
                                if (!Repository.Entities.SIGN_FORM_MAIN.Any(x => x.APPLICANTER_EMP_NO == SDQ.EMP_NO && x.FORM_TYPE == "AccountDisableForm"))
                                {

                                    // TODO :
                                    // Repository.Entities.ACCOUNT.Where(t2 => t2.LEAVING_CONTROLS == 1 && (new string[] { "0" }).Contains(t2.FUNCTION_APPLY_TYPE));
                                    // 目標權限：檢查該同仁的權限主檔有在離職控管清單內的(如圖UCPAM401 - 1)，且需為自有帳號或授權, 不包含公用帳號或部門授權。
                                    // 排外：檢查以上之自有帳號或授權，若已有未結案之停用單則不列出。
                                    // 表單內容：以上同仁的目標權限扣除排外項目後所列出的權限清單，產生不同停用申請單（一項服務項目對應一張申請單，一張申請單可能有多筆授權)，發通知給不同承辦人，表單格式及站點、狀態…等，依一般權限停用單設計。

                                    // SignFormId 查 權限停用單是否有單
                                    List<PAM_PS_DISABLELIST> PS_FORM_List = new List<PAM_PS_DISABLELIST>();
                                    foreach (var SFL in SIGN_FORM_List)
                                    {
                                        PS_FORM_List = Repository.Entities.PAM_PS_DISABLELIST.Where(x => x.SIGN_FORM_ID == SFL.SIGN_FORM_ID).ToList();
                                    }


                                    SignData<PermissionDisableList> SignData_PS_List = new SignData<PermissionDisableList>();
                                    SignData_PS_List.Sign = new SignFormMain();
                                    SignData_PS_List.FormType = "AccountDisableForm"; //TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                                    SignData_PS_List.Sign.SignFromID = 0; // 控制啟單
                                    SignData_PS_List.Sign.RequiredDate = DateTime.Now;
                                    SignData_PS_List.Sign.FormStatus = "Closed"; // 表單狀態 A-2
                                    SignData_PS_List.Sign.BpmFormType = BpmFormType.AccountDisableForm;  // 表單類型
                                    SignData_PS_List.Sign.ServiceCode = "PD001";
                                    SignData_PS_List.SignButtonKey = "SignButton.AccountDisableForm.Save"; // 放前端按鈕的名稱
                                    SignData_PS_List.Sign.ApplicanterEmpNO = SDQ.EMP_NO; // 申請人
                                    SignData_PS_List.Sign.FillerEmpNO = SDQ.EMP_NO; // 填表人


                                    // 啟權限清單需要先將資料整理再送至 Create

                                    // 撈出全部上面三選一子單

                                    var SIGNER_NAME = Repository.Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.APPLICANTER_EMP_NO == SDQ.EMP_NO).APPLICANTER_NAME;

                                    PermissionDisableList PS_List = new PermissionDisableList
                                    {
                                        CloseDate = DateTime.Now, //權限關閉日 A-2
                                        SignerEmpno = SDQ.EMP_NO,
                                        DisableDesc = "",
                                        Status = 1,
                                        SignerName = SIGNER_NAME,
                                        RefId = ThreeOfOne_SIGN_FORM_ID.ToString(),
                                        CreateType = "2", // 1. 302 / 2.404
                                        Items = new List<PermissionDisableDetail>(),
                                    };

                                    var AF_DISABLED_ID = Repository.Entities.PAM_AF_DISABLED.FirstOrDefault(x => x.SIGN_FORM_ID == ThreeOfOne_SIGN_FORM_ID).ID;
                                    var ACCOUNT_List = Repository.Entities.ACCOUNT.Where(x => x.EMP_NO == SDQ.EMP_NO).ToList();

                                    // PS_Detail 是一筆筆 Account 不是 AF_Detail Group
                                    foreach (var item in ACCOUNT_List)
                                    {
                                        var FUNCTION_TYPE_NAME = Repository.Entities.FUNCTION_TYPE.FirstOrDefault(x => x.ID == item.FUNCTION_TYPE).DESCRIPTION;
                                        PermissionDisableDetail PS_Detail = new PermissionDisableDetail();
                                        PS_Detail.Item = FUNCTION_TYPE_NAME;
                                        PS_Detail.FunctionType = item.FUNCTION_TYPE.ToString();
                                        PS_Detail.UseType = item.USING_TYPE.ToString();
                                        PS_Detail.AssetId = item.MAIN_ASSET_NO;
                                        PS_Detail.Name = item.COMPUTER_NAME;// TODO : 來源不確定
                                        PS_Detail.Serial = item.COMPANY_CODE;// TODO : 來源不確定
                                        PS_Detail.Module = item.COMPANY_CODE;// TODO : 來源不確定
                                        PS_Detail.AccountId = Decimal.ToInt32(item.ID);
                                        PS_List.Items.Add(PS_Detail);
                                    }

                                    // PS_List 要以 FUNCTION_TYPE 拆表，分別起單
                                    // 丟是丟 True 的每筆 Detail 但 迴圈是跑類別

                                    SignData_PS_List.FormData = PS_List;
                                    List<PermissionDisableDetail> Detail_List = new List<PermissionDisableDetail>();
                                    Detail_List = PS_List.Items;
                                    var Group_Detail = Detail_List.GroupBy(x => x.FunctionType);
                                    List<PermissionDisableDetail> FixedItems = new List<PermissionDisableDetail>();
                                    FixedItems = PS_List.Items;
                                    foreach (var Group_FunctionType in Group_Detail)
                                    {
                                        PS_List.Items = FixedItems.Where(x => x.FunctionType == Group_FunctionType.Key).ToList();
                                        SignData_PS_List.FormData = PS_List;
                                        PS_Repository.Create(SignData_PS_List, SDQ.EMP_NO, HrMasterService, BPMService, signer);
                                    }

                                }
                                #endregion

                                #endregion

                                // TODO : A-3. 至Meta EMP系統(IF013)，找出該離職工號之設定，並將欄位：HR_disable_flag 設定為 “1”。
                            }
                            else
                            {
                                #region B.已產生過帳號關閉及設備繳回單 & 該單之帳號關閉日 > 當天   修改原單日期並Log(執行B-1, B-2, B-3)
                                foreach (var AF_FORM_ITEM in AF_FORM_List)
                                {
                                    if (AF_FORM_ITEM.CLOSE_DATE > DateTime.Now)
                                    { // 該單之帳號關閉日 > 當天

                                        // B - 1.修改原帳號關閉及設備繳回單(UCPAM402 - 2)：帳號關閉及設備繳回日 = 當天，狀態＝[結案]，
                                        // TODO:　Log = HR指定於yyyy / mm / dd 進行帳號關閉及電腦設備回收。

                                        AF_FORM_ITEM.CLOSE_DATE = DateTime.Now; // 帳號權限預計關閉日
                                        AF_FORM_ITEM.DISABLED_DATE = DateTime.Now; // 停用及設備繳回日期

                                        var FORM_STATUS = Repository.Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == AF_FORM_ITEM.SIGN_FORM_ID).FORM_STATUS;
                                        FORM_STATUS = "ThreeOfOneEnd"; // B-1

                                        // DisabledListEnd 統一改為 ThreeOfOne

                                        // TODO：B - 2.修改原權限停用單(UCPAM402 - 3)：權限關閉日 = 當天，狀態 = (請參考一般權限停用單)。
                                        // TODO：Log = HR指定於yyyy / mm / dd 進行帳號關閉及電腦設備回收。

                                        // SignFormId 查 權限停用單是否有單
                                        List<PAM_PS_DISABLELIST> PS_FORM_List = new List<PAM_PS_DISABLELIST>();
                                        foreach (var SFL in SIGN_FORM_List)
                                        {
                                            PS_FORM_List = Repository.Entities.PAM_PS_DISABLELIST.Where(x => x.SIGN_FORM_ID == SFL.SIGN_FORM_ID).ToList();
                                        }

                                        // TODO ： B-3.此功能更新 PAM資料庫表格PAM_IF_HR_TRIGGER_CLOSE (IF013)，提供資料Trigger Notes MV
                                    }
                                }
                                #endregion
                                // C.已產生過帳號關閉及設備繳回單 & 該單之帳號關閉日 <= 當天  無需處理(系統不用動作)
                            }
                        }
                    };
                    response.Message = "執行完畢";
                }
                response.Message = "無執行筆數";
            }
            catch (Exception ex)
            {
                throwException(ex);
                _logger.Error(ex.StackTrace);
                response.StatusCode = (long)EnumStatusCode.Exception;
                response.Message = ex.Message;
            }

            return response;
        }

        /// <summary>
        /// 離職停用作業共用 Function
        /// </summary>
        /// <remarks></remarks>
        /// <returns></returns>
        public class ShareHelper
        {
            /// <summary>
            /// 判斷給予日期，利用 IsHoliday 確定是否為工作日增加指定日期
            /// </summary>
            /// <remarks>"帳號預計關閉日"轉"離職日" e.g 離職日 = 帳號權限預計關閉日 +2 個工作天</remarks>
            /// <param name="Date">帳號預計關閉日</param>
            public DateTime AddDays(DateTime Date, int AddDays)
            {
                IHrMasterService HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
                var YCount = 0;
                DateTime ConputeDay = Date;
                var SaveCount = 0;
                while (YCount != AddDays)
                {

                    ConputeDay = ConputeDay.AddDays(1);
                    if (HrMasterService.IsHoliday(ConputeDay) == "Y")
                    {
                        YCount++;
                    }

                    SaveCount++;

                    if (SaveCount == 30)
                    {
                        break;
                    }

                }

                return ConputeDay;
            }

        }

        /// <summary>
        /// Distinct function 自定義
        /// </summary>
        /// <remarks>
        /// 參考 <https://dotblogs.com.tw/larrynung/2012/09/18/74901>
        /// </remarks>
        /// <returns></returns>
        public class PropertyComparer<T> : IEqualityComparer<T>
        {
            private PropertyInfo _PropertyInfo;

            /// <summary>
            /// Creates a new instance of PropertyComparer.
            /// </summary>
            /// <param name="propertyName">The name of the property on type T
            /// to perform the comparison on.</param>
            public PropertyComparer(string propertyName)
            {
                //store a reference to the property info object for use during the comparison
                _PropertyInfo = typeof(T).GetProperty(propertyName,
            BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
                if (_PropertyInfo == null)
                {
                    throw new ArgumentException(string.Format("{0} is not a property of type {1}.", propertyName, typeof(T)));
                }
            }

            #region IEqualityComparer<T> Members

            public bool Equals(T x, T y)
            {
                //get the current value of the comparison property of x and of y
                object xValue = _PropertyInfo.GetValue(x, null);
                object yValue = _PropertyInfo.GetValue(y, null);

                //if the xValue is null then we consider them equal if and only if yValue is null
                if (xValue == null)
                    return yValue == null;

                //use the default comparer for whatever type the comparison property is.
                return xValue.Equals(yValue);
            }

            public int GetHashCode(T obj)
            {
                //get the value of the comparison property out of obj
                object propertyValue = _PropertyInfo.GetValue(obj, null);

                if (propertyValue == null)
                    return 0;

                else
                    return propertyValue.GetHashCode();
            }

            #endregion
        }
        #endregion PAM404

        #region PAM301

        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public PageQueryResult<string> PAM301()
        {
            var response = new PageQueryResult<string>();

            try
            {
                using (AccountRepository resp = new AccountRepository())
                {
                    //TODO 機制
                    //帳號主檔
                    var checkCross = false;
                    HashSet<string> empList = new HashSet<string>();
                    List<PERSON_ORG_CHANGE> datalist = new List<PERSON_ORG_CHANGE>();
                    var newId = 0;
                    PageQuery<AccountRequest> request = new PageQuery<AccountRequest>();
                    var account = resp.GetAccounts(request).Entries.ToList(); ;
                    foreach (var x in account)
                    {
                        // use Account DEPT_NO
                        //(1)	判斷使用者工號所在部門(HRMaster API: getEmployee，欄位:deptNo) 是否為”R0” 開頭，若是則”中心部門”為R0000。
                        var dept = HrMasterService.GetEmployee(x.EmpNo).deptNo;
                        if (dept.StartsWith("R0"))
                        {
                            x.DetpCenter = "R0000";

                        }
                        //(2)	判斷使用者工號所在部門的Level Code (HRMaster : getDept，欄位:orgLevelCode)是否小於等於200，若是則”中心部門”為使用者工號所在部門。
                        var levelcode = HrMasterService.GetAllDept().FirstOrDefault(x1 => x1.deptNo == dept).orgLevelCode;
                        if (Convert.ToInt32(levelcode) <= 200 && !string.IsNullOrEmpty(levelcode))
                        {
                            x.DetpCenter = dept;

                        }
                        var lsit = HrMasterService.getAllManageDeptsByDeptNo(dept).ToList();
                        var manageDept = HrMasterService.getAllManageDeptsByDeptNo(dept).FirstOrDefault(x2 => Convert.ToInt32(x2.orgLevelCode) <= 200 && x2.isVirtual != "Y").deptNo;
                        // x.DetpCenter主檔中心 manageDept使用者目前所屬”中心部門”

                        //if (manageDept != x.DetpCenter)
                        //{
                        checkCross = true;
                        empList.Add(x.EmpNo);
                        //}
                        //else { checkCross = false; }


                    }
                    if (resp.Entities.PERSON_ORG_CHANGE.Select(y => y.ID).Count() > 0)
                    {
                        newId = (int)resp.Entities.PERSON_ORG_CHANGE.Select(x1 => x1.ID).Max() + 1;
                    }
                    else
                    {
                        newId = 1;
                    }
                    foreach (var x in empList)
                    {


                        if (checkCross)
                        {
                            foreach (var x2 in account.Where(x2 => x2.EmpNo == x))
                            {
                                List<string> f = new List<string>();
                                PERSON_ORG_CHANGE data = new PERSON_ORG_CHANGE();

                                data.ID = newId;
                                data.EMP_NAME = x2.EmpName;
                                data.EMP_NO = x2.EmpNo;
                                data.STATUS = 0;
                                data.DATE = DateTime.Now;
                                f.Add(x2.FunctionType.ToString());
                                data.FUNCTION_TYPE = JsonConvert.SerializeObject(f);
                                data.ACCOUNT_TYPE = 0;
                                data.OLD_DEPT = x2.DeptNo;
                                data.NEW_DEPT = x2.DeptNo;
                                datalist.Add(data);

                                newId++;
                            }


                        }
                        else
                        {
                            //TODO非跨轉 email
                        }
                    }
                    resp.Entities.PERSON_ORG_CHANGE.AddRange(datalist);
                    resp.Entities.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throwException(ex);
                response.StatusCode = (long)EnumStatusCode.Exception;
                _logger.Error(ex.StackTrace);
                response.Message = ex.Message;
            }

            return response;
        }

        #endregion

        #region PAM708
        [EnabledAnonymous(true)]
        [ExposeWebAPI(true)]
        //PAM708
        public bool PAM708()
        {
            try
            {
                using (BatchRepository resp = new BatchRepository())
                {

                    HashSet<string> EmpList = new HashSet<string>();
                    List<PAM_PUSHMAIL> GetData = new List<PAM_PUSHMAIL>();
                    var nowDate = DateTime.Now;
                    SignData<List<PAM_PUSHMAIL>> signData = new SignData<List<PAM_PUSHMAIL>>();
                    var data = resp.Entities.PAMAI_IF_NTUA_TMPDOC.ToList();
                    var newId = resp.Entities.PAM_PUSHMAIL.Select(x => x.SID).DefaultIfEmpty().Max() + 1;
                    foreach (var ele in data)
                    {
                        var EXECUTE_TIME = DateTime.Parse(ele.EXECUTE_TIME != null ? ele.EXECUTE_TIME : null);
                        if (EXECUTE_TIME != null)
                        {
                            if (!resp.Entities.PAM_PUSHMAIL.Any(x => x.EMP_NO == ele.EMP_NO) && nowDate.Subtract(EXECUTE_TIME).Days > 30)
                            {
                                EmpList.Add(ele.EMP_NO);

                                PAM_PUSHMAIL value = new PAM_PUSHMAIL();
                                value.SID = newId;
                                value.EMP_NO = ele.EMP_NO;
                                value.EMP_CHNNAME = ele.EMP_CHNNAME;
                                value.EMP_USERNAME = ele.EMP_USERNAME;
                                value.DEVICE_TYPE = ele.DEVICE_TYPE;
                                value.DEVICE_PROVIDER = ele.DEVICE_PROVIDER;
                                value.DEVICE_OS = ele.DEVICE_OS;
                                value.DEVICEDID = ele.DEVICEDID;
                                value.DEPT_NO = ele.DEPT_NO;
                                value.CREATE_DATE = nowDate;
                                GetData.Add(value);
                                newId++;
                            }
                        };


                    }
                    foreach (var empno in EmpList)
                    {
                        signData.Sign = new SignFormMain();
                        signData.FormType = "AccountDisableForm"; //TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                        signData.Sign.SignFromID = 0; // 控制啟單
                        signData.Sign.RequiredDate = DateTime.Now;
                        signData.Sign.FormStatus = "CaseOfficerCosign"; // 表單狀態 A-2
                        signData.Sign.BpmFormType = BpmFormType.PushMailActive;  // 表單類型
                        signData.Sign.ServiceCode = "PA001";
                        signData.SignButtonKey = "SignButton.DisabledList.Save"; // 放前端按鈕的名稱
                        signData.Sign.ApplicanterEmpNO = empno; // 申請人
                        signData.Sign.FillerEmpNO = empno; // 填表人



                        var signer = new Signer();
                        signer.CaseOfficerCosign.Add(empno);
                        signer.CaseOfficerCosignBack.Add(empno);
                        signData.FormData = new List<PAM_PUSHMAIL>();
                        foreach (var PAdata in GetData)
                        {
                            signData.FormData.Add(PAdata);
                        }

                        PushmailActiveRepository.Create(signData, empno, HrMasterService, BPMService, signer);





                        //Mail
                        StrMailTitle = "";
                        StrMailContent = "";
                        sbMailCC = new StringBuilder();//TODO CC
                        StrMailTitle = resp.GetMailTitle(1);
                        StrMailContent = resp.GetMailContent(1);
                        sbMailCC.Append("test" + ";"); ;//TODO CC

                        IntAutoID = (int)resp.Entities.NOTIFICATION_TASK.Select(x => x.ID).DefaultIfEmpty().Max() + 1;
                        var mail = resp.CheckMail(empno, "PAM708", IntAutoID, sbMailCC, StrMailTitle, StrMailContent);
                        vTaskList.AddRange(mail);
                    }
                    if (vTaskList.Count > 0)
                    {
                        resp.InsertTask(vTaskList);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throwException(ex);
                return false;
            }
        }
        #endregion

        #region PAM805
        [EnabledAnonymous(true)]
        [ExposeWebAPI(true)]
        public bool PAM805()
        {
            try
            {
                using (BatchRepository resp = new BatchRepository())
                {
                    HashSet<string> EmpList = new HashSet<string>();
                    AccountService service = new AccountService();
                    var request = new PageQuery<AccountRequest>
                    {
                        QueryObject = new AccountRequest
                        {
                            FunctionType = (int)EnumAccountFunctionType.HighPermission,
                            Status = (int)EnumAccountStatus.Enable
                        }
                    };
                    var account = service.GetAccounts(request).Entries;
                    StrMailTitle = "";
                    StrMailContent = "";
                    sbMailCC = new StringBuilder();//TODO CC
                    StrMailTitle = resp.GetMailTitle(2);
                    StrMailContent = resp.GetMailContent(2);
                    foreach (var ele in account)
                    {

                        // 離職
                        var quit = HrMasterService.GetEmployeeIncludeQuit(ele.EmpNo).tplvFlag;
                        // 部門異動
                        var changeDept = (ele.DeptNo == HrMasterService.GetEmployeeIncludeQuit(ele.EmpNo).deptNo);

                        //if (quit == "Y" || changeDept)
                        //{
                        EmpList.Add(ele.EmpNo);
                        //}
                    }
                    foreach (var ele in EmpList)
                    {
                        StrMailContent = "";
                        StrMailContent = resp.GetMailContent(2);
                        var tableContent = "";
                        var table = "";
                        table = "<style>table,td,th { border: 1px solid black;}</style><table><tr><th>帳號權限項目</th><th>使用部門</th><th>使用人</th><th>使用人工號</th> <th>群組</th><th>群組說明</th><th>狀態</th><th>類別</th></tr><tr>"
                                + "@content</tr></table>";
                        foreach (var ele1 in account)
                        {

                            var changeDept = (ele1.DeptNo == HrMasterService.GetEmployeeIncludeQuit(ele1.EmpNo).deptNo);
                            var quit = HrMasterService.GetEmployeeIncludeQuit(ele1.EmpNo).tplvFlag;
                            if (ele == ele1.EmpNo)
                            {
                                tableContent = $"<th>高權限</th><td>{ele1.DeptNo}</td><td>{ele1.EmpName}</td><td>{ele1.EmpNo}</td> <td>{ele1.Group}</td><td>{ele1.Group}</td> <td>{ele1.Status}</td><td>@type</td>";
                                table = table.Replace("@content", tableContent);
                            }
                            changeDept = true;
                            if (quit == "Y")
                            {
                                table = table.Replace("@type", "離職");
                            }
                            if (changeDept)
                            {
                                table = table.Replace("@type", "部門異動");
                            }

                        }
                        //mail
                        StrMailContent = StrMailContent + "<br>" + table;
                        IntAutoID = (int)resp.Entities.NOTIFICATION_TASK.Select(x => x.ID).DefaultIfEmpty().Max() + 1;
                        var mail = resp.CheckMail(ele, "PAM805", IntAutoID, sbMailCC, StrMailTitle, StrMailContent);

                        vTaskList.AddRange(mail);

                    }
                    if (vTaskList.Count > 0)
                    {
                        resp.InsertTask(vTaskList);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                throwException(ex);
                return false;

            }




        }
        #endregion

        #region PAM412
        public bool PAM412()
        {
            try
            {
                using (BatchRepository resp = new BatchRepository())
                {
                    //離職帳號或權限停用單
                    StrMailTitle = "";
                    StrMailContent = "";
                    sbMailCC = new StringBuilder();//TODO CC
                    StrMailTitle = resp.GetMailTitle(3);
                    StrMailContent = resp.GetMailContent(3);

                    var AFList = resp.Entities.PAM_AF_DISABLED.Where(x => x.FORM_STATUS == "1").ToList();
                    foreach (var ele in AFList)
                    {
                        if (ele.CLOSE_DATE == DateTime.Now)
                        {
                            var manage = HrMasterService.GetAllManagerByEmpNo(ele.SIGN_FORM_MAIN.APPLICANTER_EMP_NO, 200).ToList().FirstOrDefault();
                            StrMailTitle = StrMailTitle.Replace("@vEmp", manage.empName);
                            StrMailContent = StrMailContent.Replace("@vEmp", manage.empName);
                            IntAutoID = (int)resp.Entities.NOTIFICATION_TASK.Select(x => x.ID).DefaultIfEmpty().Max() + 1;
                            var mail = resp.CheckMail(manage.empNo, "PAM412", IntAutoID, sbMailCC, StrMailTitle, StrMailContent);
                        }


                    }
                    //帳號關閉及設備繳回單
                    //TODO 沒狀態DB
                    var DeviceList = resp.Entities.PAM_DEVICE_RETURN.Where(x => x.SIGN_FORM_MAIN.FORM_STATUS == "DReturnWork").ToList();
                    foreach (var ele in DeviceList)
                    {
                        StrMailTitle = "";
                        StrMailContent = "";
                        sbMailCC = new StringBuilder();//TODO CC
                        StrMailTitle = resp.GetMailTitle(3);
                        StrMailContent = resp.GetMailContent(3);
                        if (ele.RETURN_DATE == DateTime.Now.AddDays(-1))
                        {
                            var manage = HrMasterService.GetAllManagerByEmpNo(ele.SIGN_FORM_MAIN.APPLICANTER_EMP_NO, 200).ToList().FirstOrDefault();
                            StrMailTitle = StrMailTitle.Replace("@vEmp", manage.empName);
                            StrMailContent = StrMailContent.Replace("@vEmp", manage.empName);
                            IntAutoID = (int)resp.Entities.NOTIFICATION_TASK.Select(x => x.ID).DefaultIfEmpty().Max() + 1;
                            var mail = resp.CheckMail(manage.empNo, "PAM412", IntAutoID, sbMailCC, StrMailTitle, StrMailContent);
                        }


                    }
                    ////權限停用申請單
                    //var PSList = resp.Entities.PAM_PS_DISABLELIST.ToList();
                    //foreach (var ele in PSList)
                    //{
                    //    if (ele.CLOSE_DATE == DateTime.Now.AddDays(-1))
                    //    {
                    //        var manage = HrMasterService.GetAllManagerByEmpNo(ele.SIGN_FORM_MAIN.APPLICANTER_EMP_NO, 200).ToList().FirstOrDefault();
                    //        StrMailTitle = StrMailTitle.Replace("@vEmp", manage.empName);
                    //        StrMailContent = StrMailContent.Replace("@vEmp", manage.empName);
                    //        IntAutoID = (int)resp.Entities.NOTIFICATION_TASK.Select(x => x.ID).DefaultIfEmpty().Max() + 1;
                    //        var mail = resp.CheckMail(manage.empNo, "PAM412", IntAutoID, sbMailCC, StrMailTitle, StrMailContent);
                    //    }


                    //}
                    //設備繳回
                    //c1 狀態為(請參考一般權限停用單) & 當天=(權限關閉日)　& 服務為自動化者
                    //c2狀態為(請參考一般權限停用單) & (權限關閉日)-3個工作日＜=當天＜=(權限關閉日)　& 服務不為自動化者
                    //c3 狀態為(請參考一般權限停用單) & 當天> (權限關閉日)　& 服務不為自動化者
                    if (vTaskList.Count > 0)
                    {
                        resp.InsertTask(vTaskList);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throwException(ex);
                return false;

            }




        }
        #endregion
        [EnabledAnonymous(true)]
        [ExposeWebAPI(true)]
        #region PAM413
        public DateTime PAM413(string empNo)
        {
            empNo = "99999";
            DateTime result = new DateTime();
            try
            {
                using (BatchRepository resp = new BatchRepository())
                {
                    if (empNo != null)
                    {
                        var data = resp.Entities.PAM_IF_TIMECARD_ACCCLOSEDATE.FirstOrDefault(x => x.EMP_NO == empNo).ACCOUNT_CLOSE_DATE;
                        if (data != null)
                        {
                            result = data;
                        }


                    }
                }
            }
            catch (Exception ex) { }


            return result;
        }

        #endregion
        //[EnabledAnonymous(true)]
        //[ExposeWebAPI(true)]
        //#region PAM414
        ////IF015
        ////public PAM414 PAM414(string assetNo)
        ////{
        ////    PAM414 result = new PAM414();
        ////    try
        ////    {
        ////        using (BatchRepository resp = new BatchRepository())
        ////        {
        ////            AccountService service = new AccountService();
        ////            if (assetNo != null)
        ////            {
        ////                var data = resp.Entities.IF015.Where(x => x.MAIN_ASSET_NO == assetNo).ToList();
        ////                if (data != null)
        ////                {
        ////                    if (data.Any(x => x.FUNCTION_TYPE == (int)EnumAccountFunctionType.NB))
        ////                    {
        ////                        result.NB = true;


        ////                    }
        ////                    else
        ////                    {
        ////                        result.NB = false;
        ////                    }
        ////                    if (data.Any(x => x.FUNCTION_TYPE == (int)EnumAccountFunctionType.ComputerOthers))
        ////                    {
        ////                        result.ComputerOther = true;


        ////                    }
        ////                    else
        ////                    {
        ////                        result.ComputerOther = false;
        ////                    }
        ////                }


        ////            }
        ////        }
        ////        return result;
        ////    }
        ////    catch (Exception ex)
        ////    {

        ////        return null;
        ////    }




        //#endregion
    }
}