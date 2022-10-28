using Autofac;
using Mxic.Common.Utility;
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
using System.Linq;
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
        SignRepository<List<AccountFunctionDisabledDetail>> Repository;

        public AutoResignationDisableService()
        {
            // DI Container AccountFunctionDisabledRepository
            Repository = new SignRepository<List<AccountFunctionDisabledDetail>>(new AccountFunctionDisabledRepository());
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

            // 確認 PAM_IF_RESIGN 近 7 天內是否有新增資料
            // Repository.Entities.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            if (Repository.Entities.PAM_IF_RESIGN.Where(x => (x.ACCOUNT_CLOSE_DATE.Day - DateTime.Now.Day) <= 7).Any())
            {
                // 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 2
                // TODO : PAM_IF_RESIGN 的RESIGN_DOCNO、TSEC1_URL_LINK欄位 同步 IF 009
                // TSEC1_URL_LINK欄位 : http://localhost:4202/#/pages/DisabledList/413 - 413 為 SIGN_FORM_ID

                CreateOneOfThree(); // 依條件產生[離職帳號或權限停用單]
                CreateDeviceReturn(); // 依條件產生[帳號關閉及設備繳回單]
            }

            return true;
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

            // 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.a
            // 以下狀況，產生[離職帳號或權限停用單](如圖UCPAM402-1)，
            // TODO : 並寄送通知：
            // 目前依照文件 PAM_IF_RESIGN 內有 "帳號權限預計關閉日"，所以不用自建 IsHoliday 外部資料 Services
            // 判斷條件為
            // 1.此離職同仁工號有AD帳號 (ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true)
            // 2.執行當天小於[離職日-2個工作日]
            // 3.表單內容：權限清單請檢查該同仁的權限主檔有在離職控管清單內的再出現(如圖UCPAM401 - 1)，只檢查自有帳號或授權, 不包含公用帳號或部門授權。
            // 權限清單 Appendix 包含 FUNCTION_TYPE (9001-9) 2,3,4,5,7,9 總共 15 筆
            // TODO : 4.防呆：檢查當時為使用中之自有帳號或授權，若已有未結案之停用單則不列出。

            // 3. AccountSub
            // Personal = 0, // 員工個人帳號
            // OnlyOne = 1, // 專用帳號（專人專用）
            // Public = 2, // 公用帳號（多人共用）
            // NotEmployee = 3, // 非旺宏員工帳號
            // Maintain = 4, // 系統維運帳號
            // System = 5, // 系統整合帳號

            var list = (from t1 in Repository.Entities.PAM_IF_RESIGN.AsNoTracking()
                        join t2 in Repository.Entities.ACCOUNT.AsNoTracking() on t1.EMP_NO equals t2.EMP_NO into ft
                        from t2 in ft.DefaultIfEmpty()
                        where (t1.ACCOUNT_CLOSE_DATE.Day - DateTime.Now.Day) <= 7
                        select new { t1, t2 })
                        .ToList(); // 7 天內新增資料

            list.ForEach(x =>{
                if (
                (x.t1.ACCOUNT_CLOSE_DATE.Day - DateTime.Now.Day) <= 7 && // 1.
                x.t2.FUNCTION_TYPE == 1 && (DateTime.Now < x.t1.ACCOUNT_CLOSE_DATE) && // 2.
                (new int?[] { 9001, 9002, 9003, 9004, 9005, 9006, 9007, 9008, 9009, 2, 3, 4, 5, 7, 9 }).Contains(x.t2.FUNCTION_TYPE) && // 3.
                )
                { // 啟單
                  //try
                  //{
                  //    SignData<List<AccountFunctionDisabledDetail>> Data = new SignData<List<AccountFunctionDisabledDetail>>();
                  //    var signer = new Signer();
                  //    signer.CaseOfficerCosign.Add("00011");
                  //    signer.CaseOfficerMgrCosign.Add("00010");
                  //    Repository.Create(Data, UserInfo.Account, HrMasterService, BPMService, signer);
                  //}
                  //catch (Exception ex)
                  //{
                  //    throwException(ex);
                  //    var StatusCode = (long)EnumStatusCode.Exception;
                  //    var Message = ex.Message;
                  //}

                }

                // Log 帳號與軟體改善專案_PAM_SRS_V1.28 p.43 - 3.c
                LogOneOfThree(x.t1, x.t2);

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

            // 帳號與軟體改善專案_PAM_SRS_V1.28 p.42 - 3.b
            // 以下狀況，產生[帳號關閉及設備繳回單](如圖UCPAM402-2)，
            // TODO : 並寄送通知：
            // 狀況與表單內容皆與 SIGN_FORM_MAIN 無關，至 Repository 判斷。

            //try
            //{
            //    SignData<List<AccountFunctionDisabledDetail>> Data = new SignData<List<AccountFunctionDisabledDetail>>();
            //    var signer = new Signer();
            //    signer.CaseOfficerCosign.Add("00011");
            //    signer.CaseOfficerMgrCosign.Add("00010");
            //    Repository.Create(Data, UserInfo.Account, HrMasterService, BPMService, signer);
            //}
            //catch (Exception ex)
            //{
            //    throwException(ex);
            //    var StatusCode = (long)EnumStatusCode.Exception;
            //    var Message = ex.Message;
            //}

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
        public int LogOneOfThree(PAM_IF_RESIGN t1, ACCOUNT t2)
        {
            // 離職日 = 帳號權限預計關閉日 + 2 個工作天

            // 此紀錄後續於UCPAM410供檢視。其中檢查結果紀錄欄位請依序檢查以下狀況並分別紀錄如下：
            // c - 1.此離職同仁工號沒有AD帳號(ACCOUNT FUNCTION_TYPE=1,ENABLE_AD=true) ：沒有AD / Novell / Notes帳號。
            // c - 2.執行當天大於等於離職日：會簽單產生日期大於等於離職日，關閉帳號由HR啟動。
            // c - 3.執行當天小於[離職日 - 2個工作日]：會簽單展開日小於D - 2，已產生離職帳號或權限停用單。
            // c - 4.執行當天小於[離職日]＆大於等於[離職日 - 2個工作日] ＆　會簽單展開當下為 13:00 後：會簽單展開日大於等於D - 2，時間下午一點後，帳號關閉及設備繳回日為隔天。
            // c - 5.執行當天小於[離職日]＆大於等於[離職日 - 2個工作日] ＆　會簽單展開當下為 13:00 前：會簽單展開日大於等於D - 2，時間下午一點前，帳號關閉及設備繳回日為當天。

            if (t2.FUNCTION_TYPE != 1 && t2.ENABLE_AD == null && t2.ENABLE_NOVELL == null && t2.ENABLE_NOTES == null)
            { // c1
            }
            else if ()
            { // TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.43 - 3.c2 需用到離職日可能需要寫 IsHoliday
            }
            else if (DateTime.Now < t1.ACCOUNT_CLOSE_DATE)
            { // c3
            }
            else if ()
            { // TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.43 - 3.c4 需用到離職日可能需要寫 IsHoliday
            }
            else if ()
            { // TODO : 帳號與軟體改善專案_PAM_SRS_V1.28 p.43 - 3.c5  需用到離職日可能需要寫 IsHoliday
            }

            var num = 1;
            return num;
        }

        /// <summary>
        /// 更新 Disable_AF_Detail
        /// </summary>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public bool Update_Disable_AF_Detail()
        {
            try{
                using (var repository = new AccountFunctionDisabledRepository()){

                    //TODO ID 待補
                    var requestSignFormId = 0;
                    repository.Check_AF_Detail((int)requestSignFormId);

                }

                return true;
            }
            catch (Exception ex){

                throwException(ex);
                return false;
            }
        }
    }
}
