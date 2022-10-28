using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mxic.Common.Bpm;
using Mxic.Common.Base;
using Mxic.Common.Test.TestVO;
using System.Collections.Generic;
using Mxic.Common.Bpm.VO;
using Mxic.Soa.Bpm.VO;
using NLog;

namespace Mxic.Common.Test
{
	[TestClass]
	public class BpmServiceTest
	{
		BpmEifService _srv = null;
		//BpmApproveLogService _logSrv = new BpmApproveLogService("CONTROLPLAN");
		//BpmApproveLogService _logSrv = new BpmApproveLogService("ESH");
		BpmApproveLogService _logSrv = new BpmApproveLogService("AUDITINFO");
		private static Logger _log = NLog.LogManager.GetCurrentClassLogger();

		public BpmServiceTest()
		{
			//_srv = new BpmEifService("ESH", "http://172.17.71.38/BpmHost/api/",MxPlatformConst.ESH); // QAS Test
            //_srv = new BpmEifService("ESH", "http://172.17.71.207/BpmHost/api/", MxPlatformConst.ESH); // Coding
			//_srv = new BpmEifService("http://172.17.71.38/BpmHost/api/");
			//_srv = new BpmEifService("AP_FRAMEWORK", "http://172.17.71.207/BpmHost/api/", MxPlatformConst.ESH); // Coding
			_srv = new BpmEifService("http://172.17.71.5/BpmHost/api/"); // Coding
			//_srv = new BpmEifService("http://172.17.71.207/BpmHost/api/");
            //_srv = new BpmEifService("ESH", "http://172.17.20.71/BpmHost/api/", MxPlatformConst.ESH); // PROD
            //_srv = new BpmEifService("ESH", "http://172.17.71.204/BpmHost/api/", MxPlatformConst.ESH); // Coding
		}

        #region 元祖測試

        #region 表單CRUD

        /// <summary>
        /// 
        /// </summary>
        //[TestMethod]
        //public void CreateQECARUsingMxDictionary()
        //{
        //    QEC_TechReview_AR form = new QEC_TechReview_AR()
        //    {
        //        requisitionId = Guid.NewGuid().ToString(),
        //        applicantDept = "MR220SecA",
        //        applicantDeptName = "MR220",
        //        applicantID = "jamieliu",
        //        applicantName = "jamieliu",
        //        diagramId = "QEC_TechReview_Master_P0",
        //        docNo = "CM-2015001",
        //        fillerId = "jamieliu",
        //        fillerName = "jamieliu",
        //        identify = "QEC_TechReview_Master",
        //        AROwner = "osirisatom",
        //        ARReviewer = "beck",
        //        formName = "QECAR",
        //        mailSubject = "fasdf",
        //        showSubject = "adsfasdfadsfafasd",
        //        projectLeader = "zoehsu"
        //    };
        //    MxDictionary mxDic = new MxDictionary();
        //    mxDic.SetMxDictionary(form);
        //    ReturnResult result = _srv.CreateForm(mxDic);
        //    Assert.IsTrue(result.ReturnCode == 0);
        //}


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreateDeclareApplyUsingMxDictionary_Test()
        {
            TestForm t = new TestForm() 
            {
                requisitionId = Guid.NewGuid().ToString(),
                applicantDept = "MR220SecA",
                applicantDeptName = "MR220",
                applicantID = "jamieliu",
                applicantName = "jamieliu",
                diagramId = "ESH_DECLARE_APPLY_P0",
                docNo = "CM-2015001",
                fillerId = "jamieliu",
                fillerName = "jamieliu",
                identify = "ESH_DECLARE_APPLY",
                cosigner = "pollycheng,beckcheng,hcchou",
                deptLevel = "-300",
                isAuthorize = "Y",
                isOutGoing = "Y",
                isRemark = "N"
            };
            MxDictionary mxDic = new MxDictionary();
            mxDic.SetMxDictionary(t);
            ReturnResult result = _srv.CreateForm(mxDic);
            Assert.IsTrue(result.ReturnCode == 0);
        }


        /// <summary>
        /// 利用 MxDictionary 新增證照註冊申請單(啟單)
        /// </summary>
        [TestMethod]
        public void CreateLicenseRegisterUsingMxDictionary()
        {
            LicenseRegister lic = MakeLicenseRegister();
            MxDictionary mxDic = new MxDictionary();
            mxDic.SetMxDictionary(lic);
            ReturnResult result = _srv.CreateForm(mxDic);
            Assert.IsTrue(result.ReturnCode == 0);
        }

        /// <summary>
        /// 利用 MxDictionary 新增證照廠區變更單(啟單)
        /// </summary>
        [TestMethod]
        public void CreateLicensePlantModifyUsingMxDictionary()
        {
            LicensePlantModify lic = MakeLicensePlantModify();
            MxDictionary mxDic = new MxDictionary();
            mxDic.SetMxDictionary(lic);
            ReturnResult result = _srv.CreateForm(mxDic);
            Assert.IsTrue(result.ReturnCode == 0);
        }

        /// <summary>
        /// 利用 MxDictionary 新增證照管制表(啟單)
        /// </summary>
        [TestMethod]
        public void CreateLicenseControlRegisterUsingMxDictionary()
        {
            LicenseControlRegister lic = MakeLicenseControlRegister();
            MxDictionary mxDic = new MxDictionary();
            mxDic.SetMxDictionary(lic);
            ReturnResult result = _srv.CreateForm(mxDic);
            Assert.IsTrue(result.ReturnCode == 0);
        }

        /// <summary>
        /// 利用 MxDictionary 新增證照追蹤單(啟單)
        /// </summary>
        [TestMethod]
        public void CreateLicenseTrackingUsingMxDictionary()
        {
            LicenseTracking lic = MakeLicenseTracking();
            MxDictionary mxDic = new MxDictionary();
            mxDic.SetMxDictionary(lic);
            ReturnResult result = _srv.CreateForm(mxDic);
            Assert.IsTrue(result.ReturnCode == 0);
        }

        /// <summary>
        /// 利用 MxDictionary 新增申報申請單(啟單)
        /// </summary>
        [TestMethod]
        public void CreateDeclareApplyUsingMxDictionary()
        {
            DeclareApply lic = MakeDeclareApply();
            MxDictionary mxDic = new MxDictionary();
            mxDic.SetMxDictionary(lic);
            ReturnResult result = _srv.CreateForm(mxDic);
            Assert.IsTrue(result.ReturnCode == 0);
        }

        /// <summary>
        /// 利用 VO 新增證照註冊申請單(啟單)
        /// </summary>
        //[TestMethod]
        //public void CreateLicenseRegister()
        //{
        //    LicenseRegister lic = MakeLicenseRegister();
        //    ReturnResult result = _srv.CreateForm(lic);
        //    Assert.IsTrue(result.ReturnCode == 0);
        //}

        /// <summary>
        /// 利用 VO 新增證照廠區變更單(啟單)
        /// </summary>
        //[TestMethod]
        //public void CreateLicensePlantModify()
        //{
        //    LicensePlantModify lic = MakeLicensePlantModify();
        //    ReturnResult result = _srv.Create(lic);
        //    Assert.IsTrue(result.ReturnCode == 0);
        //}

        /// <summary>
        /// 利用 VO 新增證照管制表(啟單)
        /// </summary>
        [TestMethod]
        public void CreateLicenseControlRegister()
        {
            LicenseControlRegister lic = MakeLicenseControlRegister();
            ReturnResult result = _srv.CreateForm(lic);
            Assert.IsTrue(result.ReturnCode == 0);

			// 重覆啟單測試
			result = _srv.CreateForm(lic);
			Assert.IsTrue(result.ReturnCode == 1);
        }

        /// <summary>
        /// 利用 VO 新增新增證照追蹤單(啟單)
        /// </summary>
        [TestMethod]
        public void CreateLicenseTracking()
        {
            LicenseTracking lic = MakeLicenseTracking();
            ReturnResult result = _srv.CreateForm(lic);
            Assert.IsTrue(result.ReturnCode == 0);
        }

        /// <summary>
        /// 利用 VO 新增申報申請單(啟單)
        /// </summary>
        //[TestMethod]
        //public void CreateDeclareApply()
        //{
        //    DeclareApply lic = MakeDeclareApply();
        //    ReturnResult result = _srv.Create(lic);
        //    Assert.IsTrue(result.ReturnCode == 0);
        //}

        /// <summary>
        /// 利用 VO 新增申報申請單(啟單)
        /// </summary>
        [TestMethod]
		public void CreateControlPlanApply()
        {
			string requisitionId = Guid.NewGuid().ToString();
			ControlPlanApply form = new ControlPlanApply()
			{
				applicantID = "sandyyeh",
				applicantName = "葉純妤",
				applicantDept = "MR210SecA",
				applicantDeptName = "MR210",
				fillerId = "sandyyeh",
				fillerName = "葉純妤",
				requisitionId = requisitionId,
				docNo = "CP2017010061",
				diagramId = "PEC_ControlPlan_Revision_P0",
				identify = "PEC_ControlPlan_Revision",
				applicantDateTime = DateTime.Now,
				peMgr = "lisatsai,vickysu",
				qreMgr = "",
				formName = "ControlPlan",
				mailSubject = "mailsubject",
				showSubject = "showsubject"
			};
            ReturnResult result = _srv.CreateForm(form);
            Assert.IsTrue(result.ReturnCode == 0);
        }

		[TestMethod]
		public void CreateDeclareApplyByFormVOObject()
		{
			DeclareApply lic = MakeDeclareApply();
			ReturnResult result = _srv.CreateForm(lic);
			Assert.IsTrue(result.ReturnCode == 0);
		}
		/// <summary>
		/// 
		/// </summary>
		[TestMethod]
		public void CreateQecTechReviewFormApplyUsingMxDictionary_Test()
		{
			_srv = new BpmEifService("http://172.17.71.207/BpmHost/api/"); // Coding
			QecTechReviewForm TESTVO = new QecTechReviewForm();
			TESTVO.applicantDept = "MR230SecB";
			TESTVO.applicantID = "mkwang";
			TESTVO.applicantName = "王木坤";
			TESTVO.applicantDeptName = "";
			TESTVO.fillerId = "mkwang";
			TESTVO.fillerName = "王木坤";
			TESTVO.requisitionId = Guid.NewGuid().ToString();
			TESTVO.docNo = "tttt1112222";
			//TESTVO.diagramId = "ESH_LIC_PlantModify_P0";
			//TESTVO.identify = "ESH_LIC_PlantModify";
			TESTVO.diagramId = "QEC_TechReview_Master_P0";
			TESTVO.identify = "QEC_TechReview_Master";
			TESTVO.applicantDateTime = DateTime.Now;
			TESTVO.draftFlag = "";
			TESTVO.flowActivated = "";
			TESTVO.priority = "";
			TESTVO.formName = "releaseform";
			TESTVO.mailSubject = "1";
			TESTVO.showSubject = "2";
			TESTVO.EPCGroup = "millychuang";
			MxDictionary mxDic = new MxDictionary();
			mxDic.SetMxDictionary(TESTVO);
			ReturnResult result = _srv.CreateForm(mxDic);
			Assert.IsTrue(result.ReturnCode == 0);
		}

		[TestMethod]
		public void StringHelperTest()
		{
			// 正常情況測試
			Assert.IsTrue("MR510/beckcheng" == StringHelper.ConvertBpmAccount("beckcheng@MR510SecC"));
			Assert.IsTrue("MR510/beckcheng" == StringHelper.ConvertBpmAccount("beckcheng@MR510"));
			Assert.IsTrue("MR510/beckcheng,MR510/tinawen" == StringHelper.ConvertBpmAccount("beckcheng@MR510SecC,tinawen@MR510SecC"));

			// 例外情境測試
			// 由於是以 ; 分隔, 故會產生此結果
			Assert.IsTrue("MR510/beckcheng" == StringHelper.ConvertBpmAccount("beckcheng@MR510SecC;tinawen@MR510SecC"));
			Assert.IsTrue("" == StringHelper.ConvertBpmAccount(""));
			Assert.IsTrue("" == StringHelper.ConvertBpmAccount(null));
			Assert.IsTrue("MR51/beckcheng" == StringHelper.ConvertBpmAccount("beckcheng@MR51"));
		}
        // 測試 Update M

        #endregion 表單CRUD

        #region 簽核測試

        /// <summary>
        /// 核准
        /// </summary>
        [TestMethod]
        public void Agree()
        {
            // 先啟單, 再同意
            //DeclareApply form = MakeDeclareApply();
            LicenseTracking form = MakeLicenseTracking();
            ReturnResult result = _srv.CreateForm(form);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);     
            //ReturnResult result = new ReturnResult();
            //List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo("76d21469-59d8-435a-8f11-aff7dfbc7c46");   
            
            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                string oriSigner = data.ApproverId;

                // 先測非簽核人 => 有 Exception 才是正確
                ApproveActionInfo approveInfo = new ApproveActionInfo();
                approveInfo.ExecutorId = "test";
                approveInfo.RequisitionId = data.IdNo;
                try
                {
                    result = _srv.ApproveAction(approveInfo);
                    Assert.IsTrue(false);
                }
                catch (Exception ex)
                {
                    Assert.IsTrue(true);
                }

                // 測簽核人同意
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",
                    Comment = "API單元測試:同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                List<MxNextApprover> after = _logSrv.GetWaitForApproveByIdNo(data.IdNo);

                if (after != null)
                    Assert.IsTrue(oriSigner != after[0].ApproverId);
            }
        }

        /// <summary>
        /// 駁回至申請人
        /// </summary>
        [TestMethod]
        public void Reject()
        {
            // 先啟單, 再退件
            LicenseTracking form = MakeLicenseTracking();
            ReturnResult result = _srv.CreateForm(form);
            Assert.IsTrue(result.ReturnCode == 0);

            MxNextApprover data = _srv.GetNextApprover(form.requisitionId);
            ApproveActionInfo approveInfo = new ApproveActionInfo()
            {
                RequisitionId = data.IdNo,
                ActionName = "不同意",
                Comment = "API單元測試:不同意",
                ExecutorId = data.ApproverId,
                ExecutorName = data.ApproverName,
                ExecutorDept = data.ApproverDept
            };
            result = _srv.Reject(approveInfo);
            Assert.IsTrue(result.ReturnCode == 0);

            // 檢查是否退回起點
            String currentStop = _srv.GetNextApprover(form.requisitionId).CurrProcessName.ToLower();
            if (currentStop.IndexOf("start") != -1)
                Assert.IsTrue(true);
            else
                Assert.IsTrue(false);
        }

        /// <summary>
        /// 駁回前一站
        /// </summary>
        [TestMethod]
        public void RejectToPrevious()
        {
            // 先啟單, 簽核後退回前站
            DeclareApply form = MakeDeclareApply();
            ReturnResult result = _srv.CreateForm(form);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                string oriSigner = data.ApproverId;
                ApproveActionInfo approveInfo = new ApproveActionInfo();

                // 測簽核人同意
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",
                    Comment = "API單元測試:同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                List<MxNextApprover> after = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

                if (after != null)
                    Assert.IsTrue(oriSigner != after[0].ApproverId);

                // 測第二站簽核人駁回前一站
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "駁回前站",
                    Comment = "API單元測試:駁回前站",
                    ExecutorId = after[0].ApproverId,
                    ExecutorName = after[0].ApproverName,
                    ExecutorDept = after[0].ApproverDept
                };

                result = _srv.RejectToPrevious(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                List<MxNextApprover> rejectTo = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

                if (after != null)
                    Assert.IsTrue(oriSigner == rejectTo[0].ApproverId); // 是否退回前一簽核者
            }
        }

        /// <summary>
        /// 變更簽核人(BPM增額代理方式，詳請詢問BPM設計者)
        /// </summary>
        [TestMethod]
		public void ShiftApprove()
        {
            // 測試前可調整參數
            string newSigner = "beckcheng";  // 要變更成何人，請改這裡
            string newSignerDept = "MR510"; // 要變更成何人，其部門請改這裡
            string executorId = "shirleychen";  // 變更簽核人動作執行人帳號，請改這裡
            string executorDept = "KG110";      // 變更簽核人動作執行人部門，請改這裡
            string executorName = "陳O玲";      // 變更簽核人動作執行人姓名，請改這裡



            // 先啟單, 再變更簽核人
            DeclareApply form = MakeDeclareApply();
            ReturnResult rtnResult = _srv.CreateForm(form);
            MxNextApprover nextApprover = _srv.GetNextApprover(form.requisitionId);
            

            if (rtnResult.ReturnCode == 0)
            {

                ShiftApproverInfo approveInfo = new ShiftApproverInfo()
                {
                    RequisitionId = form.requisitionId,
                    ActionName = "變更簽核",
                    Reason = "API單元測試:變更簽核",
                    // 設定由其他單位(管理單位)人員進行變更簽核
                    ExecutorId = executorId,
                    ExecutorDept = executorDept,
                    ExecutorName = executorName,
                    ShiftId = newSigner,
                    ShiftDept = newSignerDept
                };


                // 檢查是否更換成新簽核人
                ReturnResult result = _srv.ShiftApprover(approveInfo);
                if (result.ReturnCode == 0)
                {
                    nextApprover = _srv.GetNextApprover(form.requisitionId);
                    if (nextApprover.ApproverId == newSigner) 
                        Assert.IsTrue(true);
                    else
                        Assert.IsTrue(false);
                }
                else
                {
                    Assert.IsTrue(false);
                }
                Assert.IsTrue(result.ReturnCode == 0);

                // 檢查Log中是否紀錄的是執行動作者(而不是有權限者)
                int count = 0;
                List<MxSignLog> logs = _logSrv.GetApproveLog(form.requisitionId);
                foreach (MxSignLog log in logs) 
                {
                    if (log.ApproveAction == "變更簽核") 
                    {
                        if (log.ApproverId == executorId)
                            Assert.IsTrue(true);
                        else
                            Assert.IsTrue(false);
                        count++;
                    }
                }

                if (count == 0)
                    Assert.IsTrue(false); // 找不到變更簽核人的簽核紀錄

                _logSrv.Dispose();
                _logSrv = null;
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        /// <summary>
        /// 作廢
        /// </summary>
        [TestMethod]
        public void WithDraw()
        {
            // 測試前可調整參數
            string executorId = "shirleychen";  // 作廢動作執行人帳號，請改這裡
            string executorDept = "KG110";      // 作廢動作執行人部門，請改這裡
            string executorName = "陳O玲";      // 作廢動作執行人姓名，請改這裡

            // 先啟單, 再撤單
            DeclareApply form = MakeDeclareApply();
            ReturnResult rtnResult = _srv.CreateForm(form);
            MxNextApprover nextApprover = _srv.GetNextApprover(form.requisitionId);

            if (rtnResult.ReturnCode == 0)
            {

                WithdrawFormInfo approveInfo = new WithdrawFormInfo()
                {
                    RequisitionId = form.requisitionId,
                    ActionName = "作廢",
                    Reason = "API單元測試:作廢",
                    // 設定由其他單位(管理單位)人員進行作廢
                    ExecutorId = executorId,
                    ExecutorDept = executorDept,
                    ExecutorName = executorName,
                };

                ReturnResult result = _srv.WithdrawForm(approveInfo);
                if (result.ReturnCode == 0)
                {
                    nextApprover = _srv.GetNextApprover(form.requisitionId);
                    if (nextApprover == null) // 沒有下一站(已結束)
                        Assert.IsTrue(true);
                    else
                        Assert.IsTrue(false);
                }
                else
                {
                    Assert.IsTrue(false);
                }
                Assert.IsTrue(result.ReturnCode == 0);


                // 檢查Log中是否紀錄的是執行動作者(而不是有權限者)
                int count = 0;
                List<MxSignLog> logs = _logSrv.GetApproveLog(form.requisitionId);
                foreach (MxSignLog log in logs)
                {
                    if (log.ApproveAction == "作廢")
                    {
                        if (log.ApproverId == executorId)
                            Assert.IsTrue(true);
                        else
                            Assert.IsTrue(false);
                        count++;
                    }
                }

                if (count == 0)
                    Assert.IsTrue(false); // 找不到變更簽核人的簽核紀錄



                _logSrv.Dispose();
                _logSrv = null;
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        /// <summary>
        /// 變更簽核站
        /// </summary>
        [TestMethod]
        public void ChangeStop()
        {
            // 測試前可調整參數
            string executorId = "shirleychen";  // 變更簽核站動作執行人帳號，請改這裡
            string executorDept = "KG110";      // 變更簽核站動作執行人部門，請改這裡
            string executorName = "陳O玲";      // 變更簽核站動作執行人姓名，請改這裡

            // 先建立申請單
            LicenseRegister dForm = MakeLicenseRegister();
            // 第一站起單
            ReturnResult result = _srv.CreateForm(dForm);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(dForm.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                string targetStopSigner = data.ApplicantId;
                ApproveActionInfo approveInfo = new ApproveActionInfo();
                ShiftStopInfo shiftInfo = new ShiftStopInfo();

                // 測第二站簽核人同意
                String secondSigner = data.ApproverId;
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",
                    Comment = "API單元測試:變更簽核站使用同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                List<MxNextApprover> after = _logSrv.GetWaitForApproveByIdNo(dForm.requisitionId);
                if (after != null)
                    Assert.IsTrue(secondSigner != after[0].ApproverId);

                //取得此流程的第二站資訊:簽完後才能取得
                List<String> list = _srv.GetSignedStops(data.IdNo, data.Identify);
                String firstStop = (list[0].ToString().Split(','))[1];

                //測第三站簽核人指定退回Start站
                shiftInfo = new ShiftStopInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "變更簽核站",
                    Comment = "API單元測試:變更簽核站",
                    // 設定由其他單位(管理單位)人員進行變更簽核
                    ExecutorId = executorId,
                    ExecutorDept = executorDept,
                    ExecutorName = executorName,
                    targetStop = firstStop
                };

                result = _srv.ShiftApproveStop(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                //取得當前簽核資訊
                List<MxNextApprover> check = _logSrv.GetWaitForApproveByIdNo(data.IdNo);
                // 檢查是否成功變更簽核站(Start站)
                Assert.IsTrue(check[0].ApproverId == targetStopSigner);
            }

        }

        /// <summary>
        /// 取得已簽核過的站點
        /// </summary>
        [TestMethod]
        public void GetSignedStops()
        {
            // 先啟單, 再同意
            DeclareApply form = MakeDeclareApply();
            ReturnResult result = _srv.CreateForm(form);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                string oriSigner = data.ApproverId;
                ApproveActionInfo approveInfo = new ApproveActionInfo();

                // 測簽核人同意
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "核准",
                    Comment = "同意 - 測試 only",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                List<MxNextApprover> after = _logSrv.GetWaitForApproveByIdNo(data.IdNo);
                if (after != null)
                    Assert.IsTrue(oriSigner != after[0].ApproverId);

                List<String> list = _srv.GetSignedStops(form.requisitionId, form.identify);
                Assert.IsTrue(list.Count == 2);
            }
        }

		/// <summary>
		///		依 IdNo 進行 Grouping 
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		private Dictionary<string, List<MxNextApprover>> GroupCosignApprover(List<MxNextApprover> list)
		{
			Dictionary<string, List<MxNextApprover>> dict = new Dictionary<string, List<MxNextApprover>>();
			List<MxNextApprover> cosignList = new List<MxNextApprover>();
			foreach (MxNextApprover approver in list)
			{

				if (dict.ContainsKey(approver.IdNo))
					cosignList = dict[approver.IdNo];
				else
					cosignList = new List<MxNextApprover>();

				cosignList.Add(approver);
				dict.Add(approver.IdNo, cosignList);
			}
			return dict;
		}

		/// <summary>
		///		回傳平行會簽的 NextApprover 資料
		/// </summary>
		/// <returns></returns>
		private List<MxNextApprover> GetCosginApprover()
		{
			List<MxNextApprover> list = _logSrv.GetWaitForApprove();
			List<MxNextApprover> cosignList = new List<MxNextApprover>();
			string[] cosignProcessNames = new string[] { "AplBos", "SpcRol", "DsntList" };
			Dictionary<string, List<MxNextApprover>> dict = new Dictionary<string, List<MxNextApprover>>();

			// 找出符合的會簽中 NextApprover
			foreach (MxNextApprover approver in list)
			{
				foreach (string cosignProcessName in cosignProcessNames)
				{
					if (approver.ProcessId.StartsWith(cosignProcessName))
					{
						cosignList.Add(approver);
					}
				}
			}
			return cosignList;
		}

		[TestMethod]
		public void ShiftApproveStop()
		{
			// 先啟單, 再同意
			DeclareApply form = MakeDeclareApply();
			ReturnResult result = _srv.CreateForm(form);
			List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

			if (forms.Count > 0)
			{
				MxNextApprover data = forms[0];
				string oriSigner = data.ApproverId;
				ApproveActionInfo approveInfo = new ApproveActionInfo();

				// 測簽核人同意
				approveInfo = new ApproveActionInfo()
				{
					RequisitionId = data.IdNo,
					ActionName = "核准",
					Comment = "同意 - 測試 only",
					ExecutorId = data.ApproverId,
					ExecutorName = data.ApproverName,
					ExecutorDept = data.ApproverDept
				};
				result = _srv.ApproveAction(approveInfo);
				Assert.IsTrue(result.ReturnCode == 0);

				// 變更站點
				ShiftStopInfo shiftInfo = new ShiftStopInfo()
				{
					RequisitionId = data.IdNo,
					ActionName = "變更簽核站",
					Comment = "API單元測試:變更簽核站",
					// 設定由其他單位(管理單位)人員進行變更簽核
					ExecutorId = "jameshsiao",
					ExecutorDept = "MR210",
					ExecutorName = "蕭詠霖",
					targetStop = "Start01"
				};

				result = _srv.ShiftApproveStop(shiftInfo);
				Assert.IsTrue(result.ReturnCode == 0);

				List<MxNextApprover> after = _logSrv.GetWaitForApproveByIdNo(data.IdNo);
				Assert.IsTrue(after[0].ProcessId == "Start01");
			}
		}

		[TestMethod]
		public void AppendExtraAgent()
		{
			LicenseTracking form = MakeLicenseTracking();
			ReturnResult result = _srv.CreateForm(form);
			if (result.ReturnCode == 0)
			{
				//測第三站簽核人指定退回Start站
				AppendExtraAgentInfo shiftInfo = new AppendExtraAgentInfo()
				{
					RequisitionId = form.requisitionId,
					ActionName = "新增簽核代理人",
					ExecutorId = "jameshsiao",
					ExecutorDept = "MR210",
					ExecutorName = "蕭詠霖",
					Reason = "Test",
					AgentDept = "MR510SecB",
					AgentId = "beckcheng"
				};

				result = _srv.AppendExtraAgent(shiftInfo);
				Assert.IsTrue(result.ReturnCode == 0);

				List<MxNextApprover> list = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				Assert.IsTrue(list.Count == 2);

				// 擇一進行簽核同意
				MxNextApprover approver = list[1];
				ApproveActionInfo approveInfo = new ApproveActionInfo()
				{
					RequisitionId = approver.IdNo,
					ActionName = "同意",
					Comment = "API單元測試:同意",
					ExecutorId = "beckcheng",
					ExecutorName = "Beck Cheng",
					ExecutorDept = "MR510SecB"
				};
				result = _srv.ApproveAction(approveInfo);
				Assert.IsTrue(result.ReturnCode == 0);

				list = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				Assert.IsTrue(list.Count == 1);
			}
		}

		[TestMethod]
		public void AuditInfoTest()
		{
			//List<MxSignLog> list = _logSrv.GetApproveLog("test");
			//Assert.IsTrue(list.Count == 0);

			// 先啟單, 再同意
			//DeclareApply form = MakeDeclareApply();
			BpmARApply bpmARApply = new BpmARApply()
			{
				applicantDept = "MR510",
				applicantID = "beckcheng",
				applicantName = "beckcheng",
				applicantDeptName = "MR510",
				fillerId = "beckcheng",
				fillerName = "beckcheng",
				requisitionId = "beckcheng0031d4122111",
				docNo = "beckcheng_docno",
				diagramId = "QEC_AuditInfo_AR_P0",
				identify = "QEC_AuditInfo_AR",
				applicantDateTime = DateTime.Now,
				assigner = "beckcheng",
				auditor = "beckcheng",
				formName = "AuditInfo",
				mailSubject = "mailsubject",
				showSubject = "showsubject"
			};
			
			ReturnResult result = _srv.CreateForm(bpmARApply);
			Assert.IsTrue(result.ReturnCode == 0);

		}

        #endregion

        #region 管理功能測試

        /// <summary>
        /// 變更申請人
        /// </summary>
        [TestMethod]
        public void ShiftApplicantSingle()
        {
            // 測試前可調整參數
            string newAccountId = "beckcheng";  // 新申請人帳號，請改這裡
            string newAccountName = "鄭O村";    // 新申請人姓名，請改這裡
            string newDeptId = "MR510SecC";     // 新申請人單位，請改這裡
            string newDeptName = "MR510";       // 新申請人部門名稱，請改這裡

            // 先啟單
            //DeclareApply form = MakeDeclareApply();
            LicenseRegister form = MakeLicenseRegister();
            ReturnResult result = _srv.CreateForm(form);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];

				ShiftApplicantInfo shiftInfo = new ShiftApplicantInfo()
                {
                    Identify = data.Identify,
                    RequisitionId = data.IdNo,
                    oAccountId = data.ApplicantId,
                    oDeptId = data.ApproverDept,
                    nAccountId = newAccountId,
                    nDeptId = newDeptId
                };
                // 變更申請人Osiris => Beck
                result = _srv.ShiftApplicant(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                // 檢查申請人是否更換
                List<ProcessInfo> curProcess = _srv.AskCurrentStopInfo(data.IdNo, data.Identify);
                if (curProcess[0] == null)
                    Assert.IsTrue(false);
                else
                    if (curProcess[0].applicantId == newAccountId)
                        Assert.IsTrue(true); // 申請人已更換 Osiris => Beck
                    else
                        Assert.IsTrue(false);
            }
        }

        /// <summary>
        /// 退件後變更申請人重新送件
        /// </summary>
        [TestMethod]
        public void ApproveAfterRejectAndShiftApplicant()
        {
            // 測試前可調整參數
            string newAccountId = "yclai01";  // 新申請人帳號，請改這裡
            string newAccountName = "賴螢潔";    // 新申請人姓名，請改這裡
            string newDeptId = "B0100";     // 新申請人單位，請改這裡
            string newDeptName = "B0100";       // 新申請人部門名稱，請改這裡
            //string newAccountId = "epheseyang";  // 新申請人帳號，請改這裡
            //string newAccountName = "楊O明";    // 新申請人姓名，請改這裡
            //string newDeptId = "MR510";     // 新申請人單位，請改這裡
            //string newDeptName = "MR510";       // 新申請人部門名稱，請改這裡

            // 先啟單
            DeclareApply form = MakeDeclareApply();
            //LicenseRegister form = MakeLicenseRegister();
            ReturnResult result = _srv.CreateForm(form);
			Assert.IsTrue(result.ReturnCode == 0);

            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
            ApproveActionInfo approveInfo = new ApproveActionInfo();

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];

                // 測簽核人同意
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",
                    Comment = "API單元測試:同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                // 測退件
                data = _srv.GetNextApprover(form.requisitionId);
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "不同意",
                    Comment = "API單元測試:不同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.Reject(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                // 檢查是否退回起點
                String currentStop = _srv.GetNextApprover(form.requisitionId).CurrProcessName.ToLower();
                if (currentStop.IndexOf("start") != -1)
                    Assert.IsTrue(true);
                else
                    Assert.IsTrue(false);

                // 變更申請人
                ShiftApplicantInfo shiftInfo = new ShiftApplicantInfo()
                {
                    Identify = data.Identify,
                    RequisitionId = data.IdNo,
                    oAccountId = data.ApplicantId,
                    oDeptId = data.ApproverDept,
                    nAccountId = newAccountId,
                    nDeptId = newDeptId,
					ProcessId = data.ProcessId,
					ProcessName = data.CurrProcessName,
					Reason = "測試變更申請人"
                };
                result = _srv.ShiftApplicant(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                // 檢查申請人是否更換
                List<ProcessInfo> curProcess = _srv.AskCurrentStopInfo(data.IdNo, data.Identify);
                if (curProcess[0] == null)
                    Assert.IsTrue(false);
                else
                    if (curProcess[0].applicantId == newAccountId)
                        Assert.IsTrue(true);
                    else
                        Assert.IsTrue(false);

                // 檢查申請人簽核權限是否更換
                if (curProcess[0] == null)
                    Assert.IsTrue(false);
                else
                    if (curProcess[0].approverId == newAccountId)
                        Assert.IsTrue(true);
                    else
                        Assert.IsTrue(false);

                // 再同意
                data = _srv.GetNextApprover(form.requisitionId);
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",
                    Comment = "API單元測試:同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);     
            }
        }

        /// <summary>
        /// 變更填表人
        /// </summary>
        [TestMethod]
        public void ShiftFillerSingle()
        {
            // 測試前可調整參數
            string newAccountId = "tinawen";  // 新申請人帳號，請改這裡
            string newAccountName = "溫O婷";    // 新申請人姓名，請改這裡

            // 先啟單
            DeclareApply form = MakeDeclareApply();
            ReturnResult result = _srv.CreateForm(form);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                MxBpmShiftApplicantInfo shiftInfo;

                shiftInfo = new MxBpmShiftApplicantInfo()
                {
                    identify = data.Identify,
                    requisitionId = data.IdNo,
                    oAccountId = data.ApplicantId,
                    nAccountId = newAccountId,
                    nMemberName = newAccountName
                };
                // 變更填表人Osiris => Beck
                result = _srv.ShiftFiller(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);
            }
        }

        /// <summary>
        /// 批次變更申請人
        /// </summary>
        [TestMethod]
        public void ShiftApplicantInSameForm()
        {
            // 測試前可調整參數
            string newAccountId = "beckcheng";  // 新申請人帳號，請改這裡
            string newAccountName = "鄭O村";    // 新申請人姓名，請改這裡
            string newDeptId = "MR510SecC";     // 新申請人單位，請改這裡
            string newDeptName = "MR510";       // 新申請人部門名稱，請改這裡

            // 先啟單 3 張
            List<DeclareApply> testForms = new List<DeclareApply>();
            DeclareApply form = MakeDeclareApply();
            ReturnResult result = new ReturnResult();
            testForms.Add(form);
            testForms.Add(MakeDeclareApply());
            testForms.Add(MakeDeclareApply());
            foreach (DeclareApply x in testForms)
                result = _srv.CreateForm(x);

            // 變更第1張的申請人
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                MxBpmShiftApplicantInfo shiftInfo;                

                shiftInfo = new MxBpmShiftApplicantInfo()
                {
                    identify = data.Identify,
                    processingOnly = 1,
                    appendHistory = 0,
                    requisitionId = data.IdNo,
                    oAccountId = data.ApplicantId,
                    oDeptId = data.ApplicantDept,
                    nAccountId = newAccountId,
                    nDeptId = newDeptId,
                    nMemberName = newAccountName,
                    nDeptName = newDeptName,
                    sysId = "tinawen",
                    sysName = "溫O婷",
                    uniqueId = 0,
                    resultPrompt = "",
                    comment = "test"

                };
                // 變更申請人Jamie => Beck
                result = _srv.ShiftApplicantInSameForm(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                // 檢查申請人是否全部更換
                foreach (DeclareApply x in testForms)
                {
                    List<ProcessInfo> curProcess = _srv.AskCurrentStopInfo(x.requisitionId, x.identify);
                    if (curProcess[0] == null)
                        Assert.IsTrue(false);
                    else
                        if (curProcess[0].applicantId != x.applicantID)
                            Assert.IsTrue(true); // 申請人已更換 Jamie => Beck
                        else
                            Assert.IsTrue(false);
                }
            }
        }

        /// <summary>
        /// 批次變更填表人
        /// </summary>
        [TestMethod]
        public void ShiftFillerInSameForm()
        {
            // 測試前可調整參數
            string newAccountId = "beckcheng";  // 新申請人帳號，請改這裡
            string newAccountName = "鄭O村";    // 新申請人姓名，請改這裡

            // 先啟單 3 張
            List<DeclareApply> testForms = new List<DeclareApply>();
            DeclareApply form = MakeDeclareApply();
            ReturnResult result = new ReturnResult();
            testForms.Add(form);
            testForms.Add(MakeDeclareApply());
            testForms.Add(MakeDeclareApply());
            foreach (DeclareApply x in testForms)
                result = _srv.CreateForm(x);

            // 變更第1張的填表人
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                MxBpmShiftApplicantInfo shiftInfo;

                shiftInfo = new MxBpmShiftApplicantInfo()
                {
                    identify = data.Identify,
                    processingOnly = 1,
                    appendHistory = 0,
                    requisitionId = data.IdNo,
                    oAccountId = data.ApplicantId,
                    nAccountId = newAccountId,
                    nMemberName = newAccountName,
                    sysId = "ESH",
                    sysName = "ESH",
                    uniqueId = 0,
                    resultPrompt = "",
                    comment = "test"

                };
                // 變更填表人TinaWen => Beck
                result = _srv.ShiftFillerInSameForm(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);

            }
        }


        /// <summary>
        /// 測試驗證伺服器與 BPM Engine 狀態
        /// </summary>
        [TestMethod]
        public void CheckServerAndBpmEngine()
        {
            String result = _srv.CheckServerAndEngine();
            Assert.IsTrue(result == "OK");
        }

        /// <summary>
        /// 測試同步BPM簽核人
        /// </summary>
        [TestMethod]
        public void TestSynchronizeBPMApprover()
        {
            // 啟單

            // 簽核

            // 刪除簽核紀錄

            // 同步

            // 檢查紀錄

            string id = "323410de-6587-49e8-ae0e-67d46995f02e";
            string identify = "ESH_LIC_ApplyModify";
            string reason = "elaineliang to tinawen";
            ReturnResult result = _srv.SynchronizeBPMApprover(id, identify, reason);
            Assert.IsTrue(result.ReturnCode == 0);
        }

		[TestMethod]
		public void TestSynchronizeBPMApproverBatchTechReview()
		{
			ReturnResult result = _srv.SynchronizeBPMApproverBatch();
			Assert.IsTrue(result.ReturnCode == 0);
		}

        /// <summary>
        /// 測試批次同步BPM簽核人(For Proxy)
        /// </summary>
        [TestMethod]
        public void TestSynchronizeBPMApproverBatch()
        {
            ReturnResult result = new ReturnResult();
            List<String> allFormIds = new List<String>();
            List<String> allFormIdentifies = new List<String>();
            List<ProcessInfo> processInfos = new List<ProcessInfo>();
            List<MxNextApprover> nextApprovers = new List<MxNextApprover>();
            ProcessInfo processInfo = new ProcessInfo();

            // 啟單並偷換AP資料庫的值
            DeclareApply declareForm = MakeDeclareApply();
			result = _srv.CreateForm(declareForm);
            Assert.IsTrue(result.ReturnCode == 0);
            allFormIds.Add(declareForm.requisitionId);
            allFormIdentifies.Add(declareForm.identify);
            processInfos.AddRange(_srv.AskCurrentStopInfo(declareForm.requisitionId, declareForm.identify));
            
            foreach (ProcessInfo data in processInfos)
            {
                // 模擬代理人情況
                data.isAgent = "1";
				data.originalApprover = data.applicantId;
				data.approverId = "nobody";
            }
            _srv.UpdateProcessInfo(processInfos);

            processInfos.Clear();
            LicenseRegister registerForm = MakeLicenseRegister();
            Assert.IsTrue(_srv.CreateForm(registerForm).ReturnCode == 0);
            allFormIds.Add(registerForm.requisitionId);
            allFormIdentifies.Add(registerForm.identify);
            processInfos.AddRange(_srv.AskCurrentStopInfo(registerForm.requisitionId, registerForm.identify));

            foreach (ProcessInfo data in processInfos)
            {
                // 模擬代理人情況
                data.isAgent = "1";
				data.originalApprover = data.applicantId;
                data.approverId = "nobody";
            }
            _srv.UpdateProcessInfo(processInfos);

            processInfos.Clear();
            LicenseControlRegister controlForm = MakeLicenseControlRegister();
            Assert.IsTrue(_srv.CreateForm(controlForm).ReturnCode == 0);
            allFormIds.Add(controlForm.requisitionId);
            allFormIdentifies.Add(controlForm.identify);
            processInfos.AddRange(_srv.AskCurrentStopInfo(controlForm.requisitionId, controlForm.identify));

            foreach (ProcessInfo data in processInfos)
            {
                // 模擬代理人情況
                data.isAgent = "1";
				data.originalApprover = data.applicantId;
                data.approverId = "nobody";
            }
            _srv.UpdateProcessInfo(processInfos);

            processInfos.Clear();
            LicensePlantModify plantModifyForm = MakeLicensePlantModify();
            Assert.IsTrue(_srv.CreateForm(plantModifyForm).ReturnCode == 0);
            allFormIds.Add(plantModifyForm.requisitionId);
            allFormIdentifies.Add(plantModifyForm.identify);
            processInfos.AddRange(_srv.AskCurrentStopInfo(plantModifyForm.requisitionId, plantModifyForm.identify));

            foreach (ProcessInfo data in processInfos)
            {
                // 模擬代理人情況
                data.isAgent = "1";
				data.originalApprover = data.applicantId;
                data.approverId = "nobody";
            }
            _srv.UpdateProcessInfo(processInfos);

            processInfos.Clear();
            LicenseTracking trackingForm = MakeLicenseTracking();
            Assert.IsTrue(_srv.CreateForm(trackingForm).ReturnCode == 0);
            allFormIds.Add(trackingForm.requisitionId);
            allFormIdentifies.Add(trackingForm.identify);
            processInfos.AddRange(_srv.AskCurrentStopInfo(trackingForm.requisitionId, trackingForm.identify));

            foreach (ProcessInfo data in processInfos)
            {
                // 模擬代理人情況
                data.isAgent = "1";
				data.originalApprover = data.applicantId;
                data.approverId = "nobody";
            }
            _srv.UpdateProcessInfo(processInfos);

            // 檢查偷換結果
            for (int i = 0; i < allFormIds.Count; i++)
                nextApprovers.AddRange(_logSrv.GetWaitForApproveByIdNo(allFormIds[i]));

            foreach (MxNextApprover data in nextApprovers)
                Assert.IsTrue(data.ApproverId == "nobody");

            // 同步
            result = _srv.SynchronizeBPMApproverBatch();
            Assert.IsTrue(result.ReturnCode == 0);

            // 檢查同步結果
            nextApprovers.Clear();
            for (int i = 0; i < allFormIds.Count; i++)
                nextApprovers.AddRange(_logSrv.GetWaitForApproveByIdNo(allFormIds[i]));
            foreach (MxNextApprover data in nextApprovers)
                Assert.IsTrue(data.ApproverId != "nobody");
        }

        /// <summary>
        /// 取得所有國定休假日
        /// </summary>
        [TestMethod]
        public void GetHolidays()
        {
            List<String> allDateTime = _srv.GetAllHolidays();
            Assert.IsTrue(allDateTime.Count>0);
        }

        #endregion

        #region 待簽箱功能

        /// <summary>
        /// 取得所有待簽資料
        /// </summary>
        [TestMethod]
        public void GetWaitForApproveList()
        {
            List<MxNextApprover> list = _logSrv.GetWaitForApprove();
            Assert.IsTrue(list.Count > 0);
        }

        /// <summary>
        /// 取得特定工號待簽資料
        /// </summary>
        [TestMethod]
        public void GetMyOnGoingFormList()
        {
			List<string> approvers = _logSrv.GetWaitForApproveId();
			if (approvers.Count > 0)
			{
				List<MxNextApprover> list = _logSrv.GetMyOnGoingForm(approvers[0]);
				Assert.IsTrue(list.Count > 0);
			}
        }

        /// <summary>
        /// 取得所有待簽資料之工號
        /// </summary>
        [TestMethod]
        public void GetWaitForApproveId()
        {
            List<string> list = _logSrv.GetWaitForApproveId();
            Assert.IsTrue(list.Count > 0);
        }

        /// <summary>
        /// 取得特定Id之待簽資料
        /// </summary>
        //[TestMethod]
        //public void GetWaitForApproveById()
        //{
        //    List<MxNextApprover> list = _logSrv.GetWaitForApproveByIdNo("M-2016-0083");
        //    Assert.IsTrue(list.Count > 0);
        //}
        
        #endregion

        #endregion 元祖測試

        #region 邏輯測試

        #region 表單CRUD

        // 測試重複建單

        #endregion

        #region 簽核測試
        
        /// <summary>
        /// 測試退件後重新送件
        /// </summary>
        [TestMethod]
        public void AgreeAfterRejectToStart()
        {
            // 先啟單, 再同意
            LicenseTracking form = MakeLicenseTracking();
            ReturnResult result = _srv.CreateForm(form);
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);

            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                string oriSigner = data.ApproverId;
                ApproveActionInfo approveInfo = new ApproveActionInfo();

                // 測簽核人同意
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",
                    Comment = "API單元測試:同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                List<MxNextApprover> after = _logSrv.GetWaitForApproveByIdNo(data.IdNo);
                if (after != null)
                    Assert.IsTrue(oriSigner != after[0].ApproverId);

                // 測退件
                data = _srv.GetNextApprover(form.requisitionId);
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "不同意",
                    Comment = "API單元測試:不同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.Reject(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                // 檢查是否退回起點
                String currentStop = _srv.GetNextApprover(form.requisitionId).CurrProcessName.ToLower();
                if (currentStop.IndexOf("start") != -1)
                    Assert.IsTrue(true);
                else
                    Assert.IsTrue(false);

                // 再同意
                data = _srv.GetNextApprover(form.requisitionId);
                approveInfo = new ApproveActionInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "同意",                    
                    Comment = "API單元測試:同意",
                    ExecutorId = data.ApproverId,
                    ExecutorName = data.ApproverName,
                    ExecutorDept = data.ApproverDept
                };
                result = _srv.ApproveAction(approveInfo);
                Assert.IsTrue(result.ReturnCode == 0);
            }
        }
		#endregion

		#region BPM API 測試
		/// <summary>
		///		代理人轉回測試
		/// </summary>
		[TestMethod]
		public void TestProxy()
		{
			FormVOBase form = new DelegateForm()
			{
				applicantID = "beckcheng",
				applicantDept = "MR510",
				applicantName = "Beck Cheng",
				diagramId = "DelegateTest_P0",
				identify = "DelegateTest",
				ApproverList1 = "beckcheng,jeffhsu",
				ApproverList2 = "rlwang",
				requisitionId = Guid.NewGuid().ToString()
			};
			ReturnResult result = _srv.CreateForm(form);
			Assert.IsTrue(result.ReturnCode == 0);
		}

		[TestMethod]
		public void TestUpdateProxyInfo()
		{
			List<string> approverLog = new List<string>();
			int approverCnt = 0;

			_srv = new BpmEifService("http://172.17.71.207/BpmHost/api/");

			// 建立表單
			FormVOBase form = new FormVOBase()
			{
				applicantDept = "MR510SecB",
				applicantID = "jeffhsu",
				applicantName = "許詩浩",
				diagramId = "Add_Del_Approver_P0",
				identify = "Add_Del_Approver",
				requisitionId = Guid.NewGuid().ToString()
			};
			ReturnResult result = _srv.CreateForm(form);
			Assert.IsTrue(result.ReturnCode == 0);

			// 取得 BPM 上的 ProcessInfo
			List<ProcessInfo> list = _srv.AskCurrentStopInfo(form.requisitionId, form.identify);

			Assert.IsTrue(result.ReturnCode == 0);
		}

		/// <summary>
		///		平行會簽流程測試 - 包含啟單/新增會簽人/刪除會簽人等測試
		/// </summary>
		[TestMethod]
		public void TestCosignProcess()
		{
			List<string> approverLog = new List<string>();
			int approverCnt = 0;
			// 建立表單
			FormVOBase form = new FormVOBase()
			{
				applicantDept = "MR510",
				applicantID = "beckcheng",
				applicantName = "鄭國村",
				diagramId = "Add_Del_Approver_P0",
				identify = "Add_Del_Approver",
				requisitionId = Guid.NewGuid().ToString()
			};
			ReturnResult result = _srv.CreateForm(form);
			Assert.IsTrue(result.ReturnCode == 0);

			List<MxNextApprover> orginalApprovers = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
			approverCnt = orginalApprovers.Count;
			Assert.IsTrue(orginalApprovers.Count > 0);
			approverLog.Add(GetApproverInfo("原始會簽人", orginalApprovers));
			_log.Debug("[TestCosignProcess] RequisitionId: " + form.requisitionId);
			if (orginalApprovers.Count > 0)
			{
				ApproveActionInfo action = new ApproveActionInfo()
				{
					ActionName = "新增會簽人",
					Comment = "測試新增會簽人",
					RequisitionId = orginalApprovers[0].IdNo,
					ExecutorDept = orginalApprovers[0].ApproverDept,
					ExecutorId = orginalApprovers[0].ApproverId,
					ExecutorName = orginalApprovers[0].ApproverName
				};
				approverCnt += 2;
				result = _srv.AddDynamicApprover(action, "beckcheng@MR510,jeffhsu@MR510SecB", "測試新增會簽人");
				Assert.IsTrue(result.ReturnCode == 0);

				// 檢查新增會簽人是否成功
				List<MxNextApprover> approvers = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				approverLog.Add(GetApproverInfo("新增會簽人*2", approvers));
				Assert.IsTrue(approvers.Count == approverCnt);

				// 測試移除會簽人功能
				action = new ApproveActionInfo()
				{
					ActionName = "移除會簽人",
					Comment = "測試移除會簽人",
					RequisitionId = approvers[0].IdNo,
					ExecutorDept = approvers[0].ApproverDept,
					ExecutorId = approvers[0].ApproverId,
					ExecutorName = approvers[0].ApproverName
				};
				result = _srv.RemoveApprover(action, "beckcheng@MR510,jeffhsu@MR510SecB", "測試移除會簽人");
				Assert.IsTrue(result.ReturnCode == 0);

				approverCnt -= 2;
				approvers = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				Assert.IsTrue(approvers.Count == approverCnt);
				approverLog.Add(GetApproverInfo("移除會簽人*1", approvers));

				result = _srv.AddDynamicApprover(action, "jeffcho@MR220SecA", "測試新增會簽人");
				Assert.IsTrue(result.ReturnCode == 0);
				approverCnt += 1;
				approvers = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				Assert.IsTrue(approvers.Count == approverCnt);
				approverLog.Add(GetApproverInfo("新增會簽人*1", approvers));

				action = new ApproveActionInfo()
				{
					ActionName = "同意",
					Comment = "測試同意",
					RequisitionId = approvers[0].IdNo,
					ExecutorDept = approvers[0].ApproverDept,
					ExecutorId = approvers[0].ApproverId,
					ExecutorName = approvers[0].ApproverName
				};
				result = _srv.ApproveAction(action);
				Assert.IsTrue(result.ReturnCode == 0);
				approverCnt -= 1;
				approvers = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				Assert.IsTrue(approvers.Count == approverCnt);

				// 測試退件
				action = new ApproveActionInfo()
				{
					ActionName = "退件",
					Comment = "測試退件",
					RequisitionId = approvers[0].IdNo,
					ExecutorDept = approvers[0].ApproverDept,
					ExecutorId = approvers[0].ApproverId,
					ExecutorName = approvers[0].ApproverName
				};
				result = _srv.Reject(action);
				Assert.IsTrue(result.ReturnCode == 0);
				approvers = _logSrv.GetWaitForApproveByIdNo(form.requisitionId);
				Assert.IsTrue(approvers.Count == 1);

				// 寫 Debug Log
				foreach (string log in approverLog)
					_log.Debug(log);
			}

		}

		/// <summary>
        /// 測試變更會簽的簽核人
        /// </summary>
        [TestMethod]
        public void ShiftCosigner()
        {
            // 測試前可調整參數
            string newSigner = "jeffhsu";  // 要變更成何人，請改這裡
            string newSignerDept = "MR510SecB"; // 要變更成何人，其部門請改這裡
            string executorId = "shirleychen";  // 變更簽核人動作執行人帳號，請改這裡
            string executorDept = "KG110";      // 變更簽核人動作執行人部門，請改這裡
            string executorName = "陳O玲";      // 變更簽核人動作執行人姓名，請改這裡

            // 先啟單, 簽至會簽站後，再變更簽核人
            // 注意！目前(20151126)只有申報申請單第二站是會簽，要測試請用申報申請單
            DeclareApply form = MakeDeclareApply();
            ReturnResult rtnResult = _srv.CreateForm(form);
            MxNextApprover nextApprover;
            List<ProcessInfo> curStopInfo = new List<ProcessInfo>();
            string stopFlag = "start";
            int countLoop = 0;

            if (rtnResult.ReturnCode == 0)
            {
                // 執行簽核第一站:申請人主管
                // 因為不曉得簽到幾層，這裡用迴圈跑 Agree
                do
                {
                    // 準備簽核參數
                    nextApprover = _srv.GetNextApprover(form.requisitionId);
                    // 測簽核人同意
                    ApproveActionInfo approveInfo = new ApproveActionInfo()
                    {
                        RequisitionId = form.requisitionId,
                        ActionName = "同意",
                        Comment = "API單元測試:同意",
                        ExecutorId = nextApprover.ApproverId,
                        ExecutorName = nextApprover.ApproverName,
                        ExecutorDept = nextApprover.ApproverDept
                    };

                    // 簽核
                    rtnResult = _srv.ApproveAction(approveInfo);
                    // 檢查簽核結果
                    Assert.IsTrue(rtnResult.ReturnCode == 0);
                    curStopInfo = _srv.AskCurrentStopInfo(form.requisitionId, form.identify);

                    if (curStopInfo.Count > 0)
                        stopFlag = curStopInfo[0].processId;
                    else
                        Assert.IsTrue(false); // 正常要有下一站

                    countLoop++; //避免無窮迴圈，簽七層主管沒有就停止
                } while (stopFlag != "DsntList01" && countLoop <= 7);

                // 執行到此，已簽核至會簽站，進行變更會簽人

                // 紀錄變更前會簽人
                List<String> cosigners = new List<String>();
                foreach (ProcessInfo x in curStopInfo)
                    cosigners.Add(x.approverId.ToString());

                if (cosigners.Count < 2)
                    Assert.IsTrue(false); // 至少兩位以上會簽人才能進行有效測試

                // 準備變更會簽人參數
                ShiftCosignerInfo shiftInfo = new ShiftCosignerInfo()
                {
                    RequisitionId = form.requisitionId,
                    ActionName = "變更會簽人",
                    Reason = "API單元測試:變更會簽人",
                    // 設定由其他單位(管理單位)人員進行變更簽核
                    ExecutorId = executorId,
                    ExecutorDept = executorDept,
                    ExecutorName = executorName,
                    CosignerId = cosigners[1], // 指定變更第2位會簽人
                    ShiftId = newSigner,
                    SfiftDept = newSignerDept
                };

                ReturnResult result = _srv.ShiftCosigner(shiftInfo);

                // 檢查結果
                if (result.ReturnCode == 0)
                {
                    curStopInfo = _srv.AskCurrentStopInfo(form.requisitionId, form.identify);
                    // 紀錄變更後會簽人
                    List<String> changeCosigners = new List<String>();
                    foreach (ProcessInfo x in curStopInfo)
                        changeCosigners.Add(x.approverId.ToString());

                    // 會簽人數量相同
                    if (changeCosigners.Count == cosigners.Count)
                        Assert.IsTrue(true);
                    else
                        Assert.IsTrue(false);

                    // 檢查更換會簽人的結果是否正確
                    // 迴圈內的 changeCosigners 與 cosigners 應該完全一樣

                    cosigners.Remove(cosigners[1]);
                    cosigners.Add(newSigner);                    
                    foreach (string newCosigner in changeCosigners) 
                        if (cosigners.Contains(newCosigner))
                            Assert.IsTrue(true);
                        else
                            Assert.IsTrue(false);
                }
                else
                {
                    Assert.IsTrue(false);
                }

                Assert.IsTrue(result.ReturnCode == 0);
                _logSrv.Dispose();
                _logSrv = null;

            }
            else
            {
                // 啟單失敗
                Assert.IsTrue(false);
            }
        }

		/// <summary>
		///		測試變更簽核人
		/// </summary>
		[TestMethod]
		public void TestShiftApprover()
		{
			_srv = new BpmEifService("http://172.17.71.207/BpmHost/api/");
			string id = Guid.NewGuid().ToString();
			FormT0118 form = new FormT0118()
			{
				requisitionId = id,
				applicantDept = "MR510SecB",
				applicantID = "jeffhsu",
				Approver_A = "bokfan",
				identify = "T0118",
				diagramId = "T0118"
			};
			ReturnResult result = _srv.CreateForm(form);
			Assert.IsTrue(result.ReturnCode == 0);
			if (result.ReturnCode == 0)
			{
				List<MxNextApprover> list = _logSrv.GetWaitForApproveByIdNo(id);
				if (list.Count>0) 
				{
					result = AgreeAction(list[0]);
					Assert.IsTrue(result.ReturnCode == 0);
				}

				// 變更簽核人
				list = _logSrv.GetWaitForApproveByIdNo(id);
				if (list.Count > 0)
				{
					MxNextApprover log = list[0];
					ShiftApproverInfo approver = new ShiftApproverInfo() {
						ActionName = "變更簽核人",
						ProcessId = log.ProcessId,
						Identify = log.Identify,
						ExecutorId = log.ApproverId,
						ExecutorDept = log.ApproverDept,
						ExecutorName = log.ApproverName,
						ProcessName = log.FormName,
						RequisitionId = log.IdNo,
						ShiftDept = "MR510SecB",
						ShiftId = "jeffhsu"
					};
					result = _srv.ShiftApprover(approver);
					Assert.IsTrue(result.ReturnCode == 0);
				}

				// 第二次變更簽核人
				list = _logSrv.GetWaitForApproveByIdNo(id);
				if (list.Count > 0)
				{
					MxNextApprover log = list[0];
					ShiftApproverInfo approver = new ShiftApproverInfo()
					{
						ActionName = "變更簽核人",
						ProcessId = log.ProcessId,
						Identify = log.Identify,
						ExecutorId = log.ApproverId,
						ExecutorDept = log.ApproverDept,
						ExecutorName = log.ApproverName,
						ProcessName = log.FormName,
						RequisitionId = log.IdNo,
						ShiftDept = "MR510SecA",
						ShiftId = "tinawen"
					};
					result = _srv.ShiftApprover(approver);
					Assert.IsTrue(result.ReturnCode == 0);
				}
				// 簽核人同意
				//list = _logSrv.GetWaitForApproveByIdNo(id);
				//if (list.Count > 0)
				//{
				//	result = AgreeAction(list[0]);
				//	Assert.IsTrue(result.ReturnCode == 0);
				//}
			}

		}

		private ReturnResult AgreeAction(MxNextApprover log)
		{
			ApproveActionInfo action = new ApproveActionInfo()
			{
				ActionName = "同意",
				ProcessId = log.ProcessId,
				Identify = log.Identify,
				ExecutorId = log.ApproverId,
				ExecutorDept = log.ApproverDept,
				ExecutorName = log.ApproverName,
				ProcessName = log.FormName,
				RequisitionId = log.IdNo
			};
			ReturnResult result = _srv.ApproveAction(action);
			return result;
		}

        #endregion

        #endregion 邏輯測試

        #region 產生參數使用


        /// <summary>
        ///		證照登錄
        /// </summary>
        /// <returns></returns>
        private LicenseRegister MakeLicenseRegister()
        {
            LicenseRegister lic = new LicenseRegister();
            lic.adminEmp = "pollycheng";
            lic.applicantDept = "MR210SecB";
            lic.applicantDeptName = "MR210";
            lic.applicantID = "jenniferyang";
            lic.applicantName = "楊雅喬";
            lic.category = "新登錄";
            lic.controlType = "人員";
            lic.diagramId = "ESH_LIC_ApplyModify_P0";
            lic.docNo = "20121222Test001";
            lic.fillerId = "jenniferyang";
            lic.fillerName = "楊雅喬";
            lic.identify = "ESH_LIC_ApplyModify";
            lic.osdEmp = "plin";
            lic.owner = "tinawen";
            lic.mailSubject = "測試信件主旨";
            lic.showSubject = "測試待簽箱主旨";
            
            lic.requisitionId = Guid.NewGuid().ToString();
            
            return lic;
        }

        /// <summary>
        ///		廠區變更申請單
        /// </summary>
        /// <returns></returns>
        private LicensePlantModify MakeLicensePlantModify()
        {
            LicensePlantModify lic = new LicensePlantModify();
            lic.adminEmp = "tinawen";
            lic.applicantDept = "MR510SecA";
            lic.applicantDeptName = "MR510";
            lic.applicantID = "tinawen";
            lic.applicantName = "溫O婷";
            lic.diagramId = "ESH_LIC_PlantModify_P0";
            lic.docNo = "MR510_01";
            lic.fillerId = "tinawen";
            lic.fillerName = "Tina Wen";
            lic.identify = "ESH_LIC_PlantModify";
            lic.plant = "Fab1";
            lic.sourceDocNo = "SrcDocNo";
            lic.isApplyPlant = "Y";
            lic.requisitionId = Guid.NewGuid().ToString();
			lic.noticeEmp = "tinawen";
            lic.mailSubject = "測試信件主旨";
            lic.showSubject = "測試待簽箱主旨";

            return lic;
        }

        /// <summary>
        ///		管制表變更通知單
        /// </summary>
        /// <returns></returns>
        private LicenseControlRegister MakeLicenseControlRegister()
        {
            LicenseControlRegister form = new LicenseControlRegister();
            form.applicantDept = "MR510";
            form.applicantDeptName = "MR510";
            form.applicantID = "beckcheng";
            form.applicantName = "Beck Cheng";
            form.diagramId = "ESH_LIC_ControlNotice_P0";
            form.docNo = "CM-2015-001";
			form.fillerId = "beckcheng";
            form.fillerName = "Beck Cheng";
            form.identify = "ESH_LIC_ControlNotice";
            form.hqAdminEmp = "tinawen";
            form.fab1AdminEmp = "pollycheng";
            form.fab2AdminEmp = "tinawen";
            form.fab5AdminEmp = "millychuang";
            form.beAdminEmp = "pollycheng";
            form.hqDocNo = "CP-2015-001";
            form.fab1DocNo = "CP-2015-002";
            form.fab2DocNo = "CP-2015-003";
            form.fab5DocNo = "CP-2015-004";
            form.beDocNo = "CP-2015-005";
            form.requisitionId = Guid.NewGuid().ToString();
            form.noticeEmp = "beckcheng";
            form.mailSubject = "測試信件主旨";
            form.showSubject = "測試待簽箱主旨";

            return form;
        }

        /// <summary>
        ///		證照追蹤單
        /// </summary>
        /// <returns></returns>
        private LicenseTracking MakeLicenseTracking()
        {
            LicenseTracking form = new LicenseTracking();
            form.requisitionId = Guid.NewGuid().ToString();
            //form.applicantDept = "MR220SecA";
            //form.applicantDeptName = "MR220";
            //form.applicantID = "perryliu";
            //form.applicantName = "劉O志";
            form.applicantDept = "MR220SecA";
            form.applicantDeptName = "MR220";
            form.applicantID = "jeffcho";
            form.applicantName = "卓O玠";
            form.diagramId = "ESH_LIC_TRACKING_P0";
            form.docNo = "testTracing001";
            form.fillerId = "perryliu";
            form.fillerName = "劉O志";
            form.identify = "ESH_LIC_TRACKING";
            form.osdEmp = "tinawen";
            form.category = "人員";
            form.adminEmp = "tinawen";
            form.formName = "testABC";
            form.mailSubject = "測試信件主旨";
            form.showSubject = "測試待簽箱主旨";

            return form;
        }

        /// <summary>
        ///		申報申請單
        /// </summary>
        /// <returns></returns>
        private DeclareApply MakeDeclareApply()
        {
            DeclareApply form = new DeclareApply();
            form.requisitionId = Guid.NewGuid().ToString();
            form.applicantDept = "MR220SecC";
            form.applicantDeptName = "MR220";
            form.applicantID = "rukachang";
            form.applicantName = "張O容";
            form.diagramId = "ESH_DECLARE_APPLY_P0";
            form.docNo = "CM-2015001";
            form.fillerId = "jamieliu";
            form.fillerName = "jamieliu";
            form.identify = "ESH_DECLARE_APPLY";
            form.cosigner = "pollycheng,tinawen,beckcheng,hcchou";
            form.deptLevel = "-300";
            form.isAuthorize = "Y";
            form.isOutGoing = "Y";
            form.isRemark = "N";
            form.mailSubject = "測試信件主旨";
            form.showSubject = "測試待簽箱主旨";

            return form;
        }

        #endregion

		#region Debug Only

		private string GetApproverInfo(string actionDesc, List<MxNextApprover> approvers)
		{
			string approverInfo = "";
			foreach(MxNextApprover approver in approvers)
				approverInfo += approver.ApproverId + "@" + approver.ApproverDept + ",";
			return actionDesc + " => " + approverInfo;
		}

        // 這裡的程式僅協助 AP team 遇到問題時進行測試

        /// <summary>
        /// 20151216 測試變更簽核站
        /// </summary>
        // [TestMethod]
        public void DebugChangeStop()
        {
            ReturnResult result;
            List<MxNextApprover> forms = _logSrv.GetWaitForApproveByIdNo("2da462fd-8ab9-4137-a771-04cd7f9cfc06");
            if (forms.Count > 0)
            {
                MxNextApprover data = forms[0];
                ApproveActionInfo approveInfo = new ApproveActionInfo();
                ShiftStopInfo shiftInfo = new ShiftStopInfo();

                //退回指定站點
                shiftInfo = new ShiftStopInfo()
                {
                    RequisitionId = data.IdNo,
                    ActionName = "退回指定站",
                    Comment = "退回指定站 - 測試 only",
                    // 設定由其他單位(管理單位)人員進行變更簽核
                    ExecutorId = "shirleychen",
                    ExecutorDept = "KG110",
                    ExecutorName = "陳O玲",
                    targetStop = "RtSpcDpg01"
                };

                result = _srv.ShiftApproveStop(shiftInfo);
                Assert.IsTrue(result.ReturnCode == 0);

                //取得當前簽核資訊
                List<MxNextApprover> check = _logSrv.GetWaitForApproveByIdNo(data.IdNo);
                // 檢查是否成功退回指定站點(第二站)
                Assert.IsTrue(check[0].ApproverId != "");
            }

        }

        

		
		
		#endregion

		



        


	}
}
