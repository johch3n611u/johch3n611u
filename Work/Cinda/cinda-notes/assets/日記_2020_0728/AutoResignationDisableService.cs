using Autofac;
using Mxic.Common.Utility;
using Mxic.Framework.Membership;
using Mxic.Framework.ServerComponent;
using Mxic.ITC.PAM.Enum;
using Mxic.ITC.PAM.Interface;
using Mxic.ITC.PAM.Model;
using Mxic.ITC.PAM.Model.Entity;
using Mxic.ITC.PAM.Model.Sign;
using Mxic.ITC.PAM.Repository;
using Mxic.ITC.PAM.Repository.UnitOfWork;
using Mxic.ITC.PAM.Srv;
using Mxic.ITC.PAM.Utility;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Mxic.ITC.PAM.Service
{
    /// <summary>
    /// 離職停用作業
    /// </summary>
    /// <returns></returns>
    class AutoResignationDisableService : BaseService
    {
        public IHrMasterService HrMasterService { get; set; }
        public IBPMService BPMService { get; set; }
        SignRepository<List<AccountFunctionDisabledDetail>> SignListRepository;
        SignRepository<List<DeviceReturnList>> DeviceListRepository;

        public AutoResignationDisableService()
        {
            // DI Container AccountFunctionDisabledRepository
            SignListRepository = new SignRepository<List<AccountFunctionDisabledDetail>>(new AccountFunctionDisabledRepository());
            DeviceListRepository = new SignRepository<List<DeviceReturnList>>(new DeviceReturnListRepository());
            HrMasterService = AutofacResolverHelper.Current.Container.Resolve<IHrMasterService>();
            BPMService = new BpmService(MembershipStore);
        }



        /// <summary>
        /// UCPAM401: [離職會簽啟動帳號權限處理作業]
        /// </summary>
        /// <remarks>
        /// 排程功能檢查PAM資料庫表格PAM_IF_RESIGN (IF008)，
        /// 依據離職日期判斷是否可以產生離職帳號或權限停用單，
        /// 並紀錄處理紀錄於系統。
        /// </remarks>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public bool AutoCreate()
        {
            try
            {
                // 確認 PAM_IF_RESIGN 近 7 天內是否有新增資料
                // Repository.Entities.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
                if (SignListRepository.Entities.PAM_IF_RESIGN.Where(x =>
                EntityFunctions.DiffDays(DateTime.Now, x.ACCOUNT_CLOSE_DATE) <= 7 &&
                EntityFunctions.DiffDays(DateTime.Now, x.ACCOUNT_CLOSE_DATE) >= 0)
                .Any())
                {
                    CreateOneOfThree(); // 依條件產生[離職帳號或權限停用單]
                    CreateDeviceReturn(); // 依條件產生[帳號關閉及設備繳回單]
                }
                return true;
            }
            catch (Exception ex)
            {
                throwException(ex);
                return false;
            }
        }

        /// <summary>
        /// 依條件產生[離職帳號或權限停用單]
        /// </summary>
        /// <remarks>
        /// 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.a
        /// </remarks>
        /// <returns></returns>
        public int CreateOneOfThree()
        {
            var NewSignFormIdEF = new Mxic.ITC.PAM.Model.Business.PageQueryResult<string>(); // Log 使用

            var SevenDays_List = (from t1 in SignListRepository.Entities.PAM_IF_RESIGN.AsNoTracking()
                                  join t2 in SignListRepository.Entities.ACCOUNT.AsNoTracking() on t1.EMP_NO equals t2.EMP_NO
                                  // into ft from t2 in ft.DefaultIfEmpty()
                                  where
                                  EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
                                  EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) >= 0
                                  select new { t1, t2 })
                        .ToList(); // 7 天內新增資料

            SevenDays_List.Select(x => x.t2).Distinct(new PropertyComparer<ACCOUNT>("EMP_NO")).ToList().ForEach(t2 =>
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
                               t2.FUNCTION_TYPE == 1 && t2.ENABLE_AD == "1" &&// 1. 此離職同仁工號有AD帳號 (ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true)
                               DateTime.Now < t1.ACCOUNT_CLOSE_DATE && // 2. 執行當天小於[離職日-2個工作日] 
                               t2.LEAVING_CONTROLS == 1 && (new string[] { "0" }).Contains(t2.FUNCTION_APPLY_TYPE)
                        // 表單內容：權限清單請檢查該同仁的權限主檔有在離職控管清單內的再出現(如圖UCPAM401 - 1)，只檢查自有帳號或授權, 不包含公用帳號或部門授權。
                        )
                        {
                            // TODO : 啟單資料要補
                            SignData<List<AccountFunctionDisabledDetail>> Data = new SignData<List<AccountFunctionDisabledDetail>>();
                            Data = SetSignData();
                            var signer = new Signer();
                            signer.CaseOfficerCosign.Add("00011");
                            signer.CaseOfficerMgrCosign.Add("00010");

                            NewSignFormIdEF = SignListRepository.Create(Data, t1.EMP_NO, HrMasterService, BPMService, signer);

                            #region 設定啟單資料

                            SignData<List<AccountFunctionDisabledDetail>> SetSignData()
                            {

                                Data.Sign = new SignFormMain();

                                Data.FormType = "Draft"; //TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                                Data.Sign.SignFromID = 0; // 控制啟單
                                Data.Sign.RequiredDate = DateTime.Now;
                                Data.Sign.FormStatus = "Draft"; // 表單狀態
                                Data.Sign.FormType = "SAMAuth"; // 表單類型
                                Data.Sign.ServiceCode = "AH0001";
                                Data.SignButtonKey = "SignButton.DisabledList.Save"; // 放前端按鈕的名稱
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
                        var NewSignFormId = NewSignFormIdEF.Entries[0];
                        LogOneOfThree(t1, t2, NewSignFormId);
                    }
                    catch (Exception ex)
                    {
                        throwException(ex);
                        var StatusCode = (long)EnumStatusCode.Exception;
                        var Message = ex.Message;
                    }

                }
            });

            var num = 1;
            return num;
        }

        /// <summary>
        /// 依條件產生[帳號關閉及設備繳回單]
        /// </summary>
        /// <remarks>
        /// 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.b
        /// </remarks>
        /// <returns></returns>
        public int CreateDeviceReturn()
        {
            try
            {

                var SevenDays_List = (from t1 in DeviceListRepository.Entities.PAM_IF_RESIGN.AsNoTracking()
                                      join t2 in DeviceListRepository.Entities.ACCOUNT.AsNoTracking() on t1.EMP_NO equals t2.EMP_NO
                                      // into ft from t2 in ft.DefaultIfEmpty()
                                      where
                                      EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) <= 7 &&
                                      EntityFunctions.DiffDays(DateTime.Now, t1.ACCOUNT_CLOSE_DATE) >= 0
                                      select new { t1, t2 })
                            .ToList(); // 7 天內新增資料

                SevenDays_List.Select(x => x.t2).Distinct(new PropertyComparer<ACCOUNT>("EMP_NO")).ToList().ForEach(t2 =>
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
                                   t2.FUNCTION_TYPE == 1 && t2.ENABLE_AD == "1" && // 1. 此離職同仁工號有AD帳號 (ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true)
                                   DateTime.Now < ResignationDay && DateTime.Now >= t1.ACCOUNT_CLOSE_DATE // 執行當天小於[離職日]＆大於等於[離職日 - 2個工作日]
                            )
                            {

                            // TODO : 啟單資料要補
                            SignData<List<DeviceReturnList>> Data = new SignData<List<DeviceReturnList>>();
                                Data = SetSignData();
                                var signer = new Signer();
                                signer.CaseOfficerCosign.Add("00011");
                                signer.CaseOfficerMgrCosign.Add("00010");

                                DeviceListRepository.Create(Data, t1.EMP_NO, HrMasterService, BPMService, signer);

                            #region 設定啟單資料

                            SignData<List<DeviceReturnList>> SetSignData()
                                {

                                    Data.Sign = new SignFormMain();

                                    Data.FormType = "Draft"; //TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.44 參考畫面 待確認 控制按鈕
                                Data.Sign.SignFromID = 0; // 控制啟單
                                Data.Sign.RequiredDate = DateTime.Now;
                                    Data.Sign.FormStatus = "Draft"; // 表單狀態
                                Data.Sign.FormType = "SAMAuth"; // 表單類型
                                Data.Sign.ServiceCode = "AH0001";
                                    Data.SignButtonKey = "SignButton.DisabledList.Save"; // 放前端按鈕的名稱
                                Data.Sign.ApplicanterEmpNO = t1.EMP_NO; // 申請人
                                DeviceReturnList newItem = new DeviceReturnList();
                                    DateTime TimeSeparation = DateTime.Today.AddHours(13);// 當日 13:00 前後
                                if (DateTime.Now < TimeSeparation)
                                    {
                                    // TODO :  若離職會簽單展開當下為 13:00 前，則帳號關閉及設備繳回日 = 當天，狀態＝[已派工]。
                                    newItem.ReturnDate = DateTime.Today;
                                    }
                                    else
                                    {
                                    // TODO :  若離職會簽單展開當下為 13:00 後，則帳號關閉及設備繳回日=當天+1，狀態＝[已派工]。
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
                });

            }
            catch (Exception e)
            {
                throw e;
            }

            var num = 1;
            return num;
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
            LogItem.ID = SignListRepository.Entities.AF_RECORD.Max(x => x.ID)+1;
            var SignFormID = decimal.Parse(NewSignFormId);
            LogItem.AF_ID = SignListRepository.Entities.PAM_AF_DISABLED.FirstOrDefault(x => x.SIGN_FORM_ID == SignFormID).ID;

            var ShareHelper = new ShareHelper();
            // 離職日 = 帳號權限預計關閉日 +2 個工作天
            DateTime ResignationDay = ShareHelper.AddDays(t1.ACCOUNT_CLOSE_DATE, 2);
            // 帳號權限預計關閉日 = 離職日 -2 個工作天
            // 當日 13:00 前後
            DateTime TimeSeparation = DateTime.Today.AddHours(13);

            LogItem.CHECK_DATE = DateTime.Now;

            try {

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
            catch (Exception e) {
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
            while (YCount != AddDays)
            {

                ConputeDay = ConputeDay.AddDays(1);
                if (HrMasterService.IsHoliday(ConputeDay) == "Y")
                {
                    YCount++;
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
}
