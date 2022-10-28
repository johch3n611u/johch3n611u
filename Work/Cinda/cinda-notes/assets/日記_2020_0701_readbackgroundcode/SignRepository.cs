using AutoMapper;
using Mxic.QEC.ICMS.Interface;
using Mxic.ITC.Portal.Model.Entity;
using Mxic.ITC.Portal.Model.HumanResource;
using Mxic.ITC.Portal.Model.Sign;
using Mxic.ITC.Portal.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
//using Mxic.ITC.Portal.Enum;
using Mxic.ITC.Portal.Model.Business;
using Mxic.Soa.Bpm.VO;
using Mxic.ITC.Portal.Model.BPM;
using Newtonsoft.Json;
using Dapper;
using Mxic.ITC.Portal.Enum;
using System.Runtime.InteropServices;
using Mxic.ITC.Portal.Model;
using System.Web.Configuration;

namespace Mxic.ITC.Portal.Repository.UnitOfWork
{

    public class SignRepository<T> : RepositoryBase where T : class
    {
        ISignRepository<T> Repository;
        private readonly IMapper _mapper;
        public SignRepository(ISignRepository<T> Decorator)
        {
            Repository = Decorator;
            Decorator.SetEntities(Entities);
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<FlowRoles, FLOW_ROLES>()
                    .ForMember(s => s.ROLE_ID, opt => opt.MapFrom(s => s.RoleID))
                    .ForMember(s => s.ROLE_NAME, opt => opt.MapFrom(s => s.RoleName)); ;

                    cfg.CreateMap<FLOW_ROLES, FlowRoles>()
                    .ForMember(s => s.RoleID, opt => opt.MapFrom(s => s.ROLE_ID))
                    .ForMember(s => s.RoleName, opt => opt.MapFrom(s => s.ROLE_NAME)); ;

                    cfg.CreateMap<FlowStageSetting, FLOW_STAGE_SETTING>()
                    .ForMember(s => s.FLOW_ID, opt => opt.MapFrom(s => s.FlowID))
                    .ForMember(s => s.ROLE_ID, opt => opt.MapFrom(s => s.RoleID))
                    .ForMember(s => s.STAGE_ID, opt => opt.MapFrom(s => s.StageID))
                    .ForMember(s => s.STAGE_ORDER, opt => opt.MapFrom(s => s.StageOrder));

                    cfg.CreateMap<FLOW_STAGE_SETTING, FlowStageSetting>()
                    .ForMember(s => s.RoleID, opt => opt.MapFrom(s => s.ROLE_ID))
                    .ForMember(s => s.FlowID, opt => opt.MapFrom(s => s.FLOW_ID))
                    .ForMember(s => s.StageID, opt => opt.MapFrom(s => s.STAGE_ID))
                    .ForMember(s => s.StageOrder, opt => opt.MapFrom(s => s.STAGE_ORDER));

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
                    .ForMember(s => s.RequiredDesc, opt => opt.MapFrom(s => s.REQUIRED_DESCRIPTION))
                    .ForMember(s => s.RequiredDate, opt => opt.MapFrom(s => s.REQUIRED_DATE))
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

                    cfg.CreateMap<SignFormRelated, SIGN_FORM_RELATED>()
                    .ForMember(s => s.RELATED_SUBTABLE, opt => opt.MapFrom(s => s.RelatedSubtable))
                    .ForMember(s => s.SIGN_FORM_ID, opt => opt.MapFrom(s => s.SignFormID))
                    .ForMember(s => s.SYSTEM_TYPE, opt => opt.MapFrom(s => s.SystemType))
                    .ForMember(s => s.UPDATE_DATE, opt => opt.MapFrom(s => s.UpdateDate));

                    cfg.CreateMap<SIGN_FORM_RELATED, SignFormRelated>()
                    .ForMember(s => s.RelatedSubtable, opt => opt.MapFrom(s => s.RELATED_SUBTABLE))
                    .ForMember(s => s.SignFormID, opt => opt.MapFrom(s => s.SIGN_FORM_ID))
                    .ForMember(s => s.SystemType, opt => opt.MapFrom(s => s.SYSTEM_TYPE))
                    .ForMember(s => s.UpdateDate, opt => opt.MapFrom(s => s.UPDATE_DATE));

                    cfg.CreateMap<SignLog, SIGN_LOG>()
                    .ForMember(s => s.SIGNATORY_DEPT_NO, opt => opt.MapFrom(s => s.SignatoryDeptNO))
                    .ForMember(s => s.SIGNATORY_EMP_NO, opt => opt.MapFrom(s => s.SignatoryEmpNO))
                    .ForMember(s => s.SIGNATORY_NAME, opt => opt.MapFrom(s => s.SignatoryName))
                    .ForMember(s => s.SIGN_DATE, opt => opt.MapFrom(s => s.SignDate))
                    .ForMember(s => s.SIGN_LOG_NUMBER, opt => opt.MapFrom(s => s.SignLogNumber))
                    .ForMember(s => s.SIGN_STAGE_NUMBER, opt => opt.MapFrom(s => s.SignStageNumber))
                    .ForMember(s => s.SIGN_STATUS, opt => opt.MapFrom(s => s.SignStatus))
                    .ForMember(s => s.SIGN_SUGGESTION, opt => opt.MapFrom(s => s.SignSuggestion));

                    cfg.CreateMap<SIGN_LOG, SignLog>()
                    .ForMember(s => s.SignatoryDeptNO, opt => opt.MapFrom(s => s.SIGNATORY_DEPT_NO))
                    .ForMember(s => s.SignatoryEmpNO, opt => opt.MapFrom(s => s.SIGNATORY_EMP_NO))
                    .ForMember(s => s.SignatoryName, opt => opt.MapFrom(s => s.SIGNATORY_NAME))
                    .ForMember(s => s.SignDate, opt => opt.MapFrom(s => s.SIGN_DATE))
                    .ForMember(s => s.SignLogNumber, opt => opt.MapFrom(s => s.SIGN_LOG_NUMBER))
                    .ForMember(s => s.SignStageNumber, opt => opt.MapFrom(s => s.SIGN_STAGE_NUMBER))
                    .ForMember(s => s.SignStatus, opt => opt.MapFrom(s => s.SIGN_STATUS))
                    .ForMember(s => s.SignSuggestion, opt => opt.MapFrom(s => s.SIGN_SUGGESTION));

                    cfg.CreateMap<SignStage, SIGN_STAGE>()
                    .ForMember(s => s.ROLE_ID, opt => opt.MapFrom(s => s.RoleID))
                    .ForMember(s => s.SIGNATORY_DEPT_NO, opt => opt.MapFrom(s => s.SignatoryDeptNO))
                    .ForMember(s => s.SIGNATORY_EMP_NO, opt => opt.MapFrom(s => s.SignatoryEmpNO))
                    .ForMember(s => s.SIGNATORY_NAME, opt => opt.MapFrom(s => s.SignatoryName))
                    .ForMember(s => s.SIGN_FORM_ID, opt => opt.MapFrom(s => s.SignFormID))
                    .ForMember(s => s.SIGN_STAGE_NUMBER, opt => opt.MapFrom(s => s.SignStageNumber))
                    .ForMember(s => s.STAGE_ORDER, opt => opt.MapFrom(s => s.StageOrder));

                    cfg.CreateMap<SIGN_STAGE, SignStage>()
                    .ForMember(s => s.RoleID, opt => opt.MapFrom(s => s.ROLE_ID))
                    .ForMember(s => s.SignatoryDeptNO, opt => opt.MapFrom(s => s.SIGNATORY_DEPT_NO))
                    .ForMember(s => s.SignatoryEmpNO, opt => opt.MapFrom(s => s.SIGNATORY_EMP_NO))
                    .ForMember(s => s.SignatoryName, opt => opt.MapFrom(s => s.SIGNATORY_NAME))
                    .ForMember(s => s.SignFormID, opt => opt.MapFrom(s => s.SIGN_FORM_ID))
                    .ForMember(s => s.SignStageNumber, opt => opt.MapFrom(s => s.SIGN_STAGE_NUMBER))
                    .ForMember(s => s.StageOrder, opt => opt.MapFrom(s => s.STAGE_ORDER));

                    cfg.CreateMap<FLOW_SETTING_MAIN, FlowSettingMain>()
                    .ForMember(s => s.FlowID, opt => opt.MapFrom(s => s.FLOW_ID))
                    .ForMember(s => s.FlowName, opt => opt.MapFrom(s => s.FLOW_NAME))
                    .ForMember(s => s.NumberForBPM, opt => opt.MapFrom(s => s.NUMBER_FOR_BPM))
                    .ForMember(s => s.RiskLevel, opt => opt.MapFrom(s => s.RISK_LEVEL))
                    .ForMember(s => s.Status, opt => opt.MapFrom(s => s.STATUS))
                    .ForMember(s => s.UpdateDate, opt => opt.MapFrom(s => s.UPDATE_DATE))
                    .ForMember(s => s.UpdateEmpNO, opt => opt.MapFrom(s => s.UPDATER_EMP_NO));

                    cfg.CreateMap<FlowSettingMain, FLOW_SETTING_MAIN>()
                    .ForMember(s => s.FLOW_ID, opt => opt.MapFrom(s => s.FlowID))
                    .ForMember(s => s.FLOW_NAME, opt => opt.MapFrom(s => s.FlowName))
                    .ForMember(s => s.NUMBER_FOR_BPM, opt => opt.MapFrom(s => s.NumberForBPM))
                    .ForMember(s => s.RISK_LEVEL, opt => opt.MapFrom(s => s.RiskLevel))
                    .ForMember(s => s.STATUS, opt => opt.MapFrom(s => s.Status))
                    .ForMember(s => s.UPDATER_EMP_NO, opt => opt.MapFrom(s => s.UpdateEmpNO))
                    .ForMember(s => s.UPDATE_DATE, opt => opt.MapFrom(s => s.UpdateDate));

                });

            _mapper = config.CreateMapper();
        }
        public PageQueryResult<string> Create(SignData<T> Data, string filler, IHrMasterService hrMaster, IBPMService bpm, [Optional] Signer signer)
        // 反傳 PageQueryResult 類別 string 型別
        // 需傳入
        // Sign 簽核資料
        // filler > HrMasterService 撈員工資料的
        // hrMaster > UserInfo.Account
        // bpm > BPMService
        // [Optional] 可選的 Signer 承辦人
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            using (var tran = Entities.Database.BeginTransaction())
            // RepositoryBase 本身就提供操作 Entities 類別 = new Entities(ConnectionString.DefaultConnectionName);
            // <https://dotblogs.com.tw/mickey/2017/01/07/225349>
            // SqlConnection：資料庫的連線字串，告訴程式你要針對哪一個資料庫做動作
            // SqlCommand：資料庫指令，Select、Insert、Delete、Update
            // SqlDataAdapter：資料連結器，負責把資料的結果集傳回
            // DataTable：用記憶體當作一張資料表，存放查詢的結果
            // DataSet：用記憶體當作一個資料集，包含多個查詢結果
            // SqlDataReader：資料讀取器，把查詢的結果讀
            // System.Data.SqlClient 在 using 範圍內啟動資料庫交易
            {
                try // 資料操作都比較容易出後續問題所以最好包 try
                {

                    #region 表單基本資訊
                    // C# 前置處理器指示詞 以下到 endregion 都為表單基本資訊
                    var BPM_DIAGRAM_ID = "";
                    var BPM_IDENTIFY = "";

                    //主表單
                    SIGN_FORM_MAIN SIGN = new SIGN_FORM_MAIN();
                    var AutoIncrement = Data.Sign.SignFromID;
                    bool IsNew = false;
                    //先看有沒有這張單  ***之後要去寫退件 假如他重送要跑新流程
                    if (AutoIncrement == 0)
                    // 確定無這張單
                    {
                        //取流水號
                        AutoIncrement = Entities.SIGN_FORM_MAIN.Max(s => (decimal?)s.SIGN_FORM_ID).GetValueOrDefault() + 1;
                        // DBSet SIGN_FORM_MAIN
                        // Max(s => (decimal?)s.SIGN_FORM_ID) 比對 dbset 內每一個項目的 id 找出最大值
                        // GetValueOrDefault() 擷取目前 Nullable<T> 物件的值，或底層型別的預設值
                        // <https://www.cnblogs.com/tangge/p/4528102.html>
                        SIGN = _mapper.Map<SignFormMain, SIGN_FORM_MAIN>(Data.Sign);
                        // 涉及到 AutoMap 套件等流程了解清楚候補
                        SIGN.SIGN_FORM_ID = AutoIncrement;
                        //填表人
                        var Filler = hrMaster.GetEmployee(filler); // 取員工資料的
                        SIGN.FILLER_EMP_NO = Filler.empNo;
                        SIGN.FILLER_NAME = Filler.empName;
                        SIGN.FILLER_DEPT_NO = Filler.deptNo;
                        //申請人
                        if (!string.IsNullOrEmpty(Data.Sign.ApplicanterEmpNO))
                        // <https://blog.yowko.com/string-isnullorempty-isnullorwhitespace/>
                        {
                            var Applicanter = hrMaster.GetEmployee(Data.Sign.ApplicanterEmpNO);
                            SIGN.APPLICANTER_EMP_NO = Applicanter.empNo;
                            SIGN.APPLICANTER_NAME = Applicanter.empName;
                            SIGN.APPLICANTER_DEPT_NO = Applicanter.deptNo;
                        }
                        IsNew = true;
                    }
                    else
                    {
                        SIGN = Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == AutoIncrement);
                        if (SIGN == null)
                        {
                            return response;
                        }

                        //申請人
                        if (!string.IsNullOrEmpty(Data.Sign.ApplicanterEmpNO))
                        {
                            var Applicanter = hrMaster.GetEmployee(Data.Sign.ApplicanterEmpNO);
                            SIGN.APPLICANTER_EMP_NO = Applicanter.empNo;
                            SIGN.APPLICANTER_NAME = Applicanter.empName;
                            SIGN.APPLICANTER_DEPT_NO = Applicanter.deptNo;
                        }

                        //如果有更改申請人(草稿->送件時可能會發生)
                        if (Data.Sign.ApplicanterEmpNO != SIGN.APPLICANTER_EMP_NO)
                        {
                            if (!string.IsNullOrEmpty(Data.Sign.ApplicanterEmpNO))
                            {
                                var Applicanter = hrMaster.GetEmployee(Data.Sign.ApplicanterEmpNO);
                                SIGN.FILLER_EMP_NO = Applicanter.empNo;
                                SIGN.FILLER_NAME = Applicanter.empName;
                                SIGN.FILLER_DEPT_NO = Applicanter.deptNo;
                            }
                        }
                        SIGN.SERVICE_CODE = Data.Sign.ServiceCode;
                    }
                    #endregion

                    // 以上就是一堆商業邏輯的資料操作

                    List<SIGN_STAGE> stages = new List<SIGN_STAGE>();
                    SIGN.FORM_TYPE = Data.Sign.BpmFormType.ToString();
                    var isSignState = false;
                    //流程&關卡
                    if (Data.Sign.FormStatus == System.Enum.GetName(typeof(EnumFormStatus), EnumFormStatus.Draft))
                    {//草稿
                        SIGN.FLOW_ID = 1;//先塞1
                        SIGN.FORM_STATUS = EnumFormStatus.Draft.ToString();
                    }
                    else
                    {//送件
                     //取SIGN_FORM_NO
                        if (string.IsNullOrEmpty(SIGN.SIGN_FORM_NO))
                        {
                            SIGN.SIGN_FORM_NO = GetSignNo(Data.Sign.FormType);
                        }

                        #region 判斷哪一種流程VIP OR 海外
                        SIGN.NOW_STAGE_ORDER = 0;
                        //依照SERVICE_CODE找相關流程
                        var sys = Entities.PORTAL_SYSTEM_SERVICES.FirstOrDefault(x => x.SERVICE_CODE == Data.Sign.ServiceCode);

                        string FlowCode = "";
                        //檢查是否為VIP
                        int IntCheckVIP = Entities.PORTAL_SYSTEM_VIP_APPLICANT.Where(x => x.EMPNO == SIGN.APPLICANTER_EMP_NO).Count();
                        int IntCheckVIPM = Entities.PORTAL_SYSTEM_VIP_MAINTAINER.Where(x => x.EMPNO == SIGN.FILLER_EMP_NO).Count();
                        if (IntCheckVIP > 0 && IntCheckVIPM > 0)
                        {
                            //申請人是VIP、填表人是VIP申請單處理人員(含backup)
                            //則簽核流程為VIP(ITC代提)
                            FlowCode = WebConfigurationManager.AppSettings["VIPFlowCode"];
                        }
                        else if (IntCheckVIP > 0 && IntCheckVIPM == 0)
                        {
                            //申請人是VIP、填表人不是VIP申請單處理人員(含backup)
                            //則簽核流程為VIP(秘書代填)
                            FlowCode = WebConfigurationManager.AppSettings["VIPAgentFlowCode"];
                        }
                        else
                        {
                            //判斷海外或HQ 海外: 申請人工號為6碼且第一碼是英文
                            Regex reg = new Regex(@"^[a-zA-Z].{5}$");

                            if (reg.IsMatch(SIGN.APPLICANTER_EMP_NO.Trim()))
                                FlowCode = sys.OVERSEAS_FLOW_CODE;
                            else
                                FlowCode = sys.HQ_FLOW_CODE;
                        }

                        //取相對應流程設定
                        var sysFlow = Entities.FLOW_SETTING_MAIN.FirstOrDefault(x => x.FLOW_NO == FlowCode);//sys.HQ_FLOW_CODE
                        BPM_DIAGRAM_ID = sysFlow.NUMBER_FOR_BPM; //BPM_DIAGRAM_ID
                        BPM_IDENTIFY = sysFlow.NAME_FOR_BPM; //BPM_IDENTIFY
                        var FlowMain = Entities.FLOW_SETTING_MAIN.FirstOrDefault(x => x.FLOW_NO == sysFlow.FLOW_NO);
                        //設定主表單對應的FLOW_ID
                        SIGN.FLOW_ID = FlowMain.FLOW_ID;
                        //取流程關卡設定
                        var FlowSetting = Entities.FLOW_STAGE_SETTING.Where(x => x.FLOW_ID == FlowMain.FLOW_ID).OrderBy(x => x.STAGE_ORDER).ToList();

                        #endregion

                        #region 建立關卡
                        SIGN_STAGE stage;
                        var StageAutoIncrement = Entities.SIGN_STAGE.Max(s => (decimal?)s.SIGN_STAGE_NUMBER).GetValueOrDefault() + 1;

                        isSignState = Entities.SIGN_STAGE.Any(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID);
                        //建立關卡
                        foreach (var Flow in FlowSetting)
                        {
                            //判斷關卡是否建立過
                            if (isSignState)
                                continue;

                            stage = new SIGN_STAGE();
                            stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                            stage.STAGE_ORDER = Flow.STAGE_ORDER;
                            stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                            var Role = Entities.FLOW_ROLES.FirstOrDefault(x => x.ROLE_ID == Flow.ROLE_ID);

                            stage.ROLE_ID = Role.ROLE_ID;
                            stage.MERGE = Flow.MERGE;
                            //依照角色找相對應人員
                            switch (Role.ROLE_ID)
                            {
                                case (int)EnumFlowRoles.Writer:

                                    stage.SIGNATORY_DEPT_NO = SIGN.FILLER_DEPT_NO;
                                    stage.SIGNATORY_EMP_NO = SIGN.FILLER_EMP_NO;
                                    stage.SIGNATORY_NAME = SIGN.FILLER_NAME;
                                    stages.Add(stage);
                                    break;
                                case (int)EnumFlowRoles.Applicant:
                                    if (SIGN.FILLER_EMP_NO != SIGN.APPLICANTER_EMP_NO)
                                    {//申請人與填表人不同
                                        stage.SIGNATORY_DEPT_NO = SIGN.APPLICANTER_DEPT_NO;
                                        stage.SIGNATORY_EMP_NO = SIGN.APPLICANTER_EMP_NO;
                                        stage.SIGNATORY_NAME = SIGN.APPLICANTER_NAME;
                                        stages.Add(stage);
                                    }
                                    break;
                                case (int)EnumFlowRoles.VIPCaseOfficerCosign:
                                case (int)EnumFlowRoles.OrganizerUnit:
                                //承辦人承辦 軟體SAM
                                case (int)EnumFlowRoles.Organizer:
                                    //承辦人 軟體SAM
                                    foreach (var sign in signer.CaseOfficerCosign)
                                    {
                                        var signEmp = hrMaster.GetEmployee(sign);
                                        if (signEmp == null)
                                        {
                                            _logger.Error($"起單HRMaster找不到人 工號:{sign},SIGN__FORM_MAIN_NO:{SIGN.SIGN_FORM_NO}");
                                            continue;
                                        }
                                        stage = new SIGN_STAGE();
                                        stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                        stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                        stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                        stage.ROLE_ID = Role.ROLE_ID;
                                        stage.MERGE = Flow.MERGE;
                                        stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                        stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                        stage.SIGNATORY_NAME = signEmp.empName;
                                        stage.SEQ = 0;
                                        stages.Add(stage);
                                    }
                                    foreach (var sign in signer.CaseOfficerCosignBack)
                                    {
                                        var signEmp = hrMaster.GetEmployee(sign);
                                        if (signEmp == null)
                                        {
                                            _logger.Error($"起單HRMaster找不到人 工號:{sign},SIGN__FORM_MAIN_NO:{SIGN.SIGN_FORM_NO}");
                                            continue;
                                        }
                                        if (stages.Where(x => x.ROLE_ID == Role.ROLE_ID && x.SIGNATORY_EMP_NO == signEmp.empNo).Count() == 0)
                                        {//承辦BACK如果為不同人才加入
                                            stage = new SIGN_STAGE();
                                            stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                            stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                            stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                            stage.ROLE_ID = Role.ROLE_ID;
                                            stage.MERGE = Flow.MERGE;
                                            stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                            stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                            stage.SIGNATORY_NAME = signEmp.empName;
                                            stage.SEQ = 1;
                                            stages.Add(stage);
                                        }
                                    }
                                    break;
                                case (int)EnumFlowRoles.Countersign:
                                    //TODO 會簽人待確認需求

                                    break;
                                case (int)EnumFlowRoles.OrganizerManager:
                                    //承辦人直屬主管 軟體SAM 不需要放BACK會出錯
                                    foreach (var sign in signer.CaseOfficerCosign)
                                    {
                                        GLEmployee signEmp = hrMaster.GetEmployee(sign);
                                        GLEmployee signManager = hrMaster.GetEmployee(signEmp.managerEmpNo);
                                        if (signEmp == null)
                                        {
                                            _logger.Error($"起單HRMaster找不到人 工號:{sign},SIGN__FORM_MAIN_NO:{SIGN.SIGN_FORM_NO}");
                                            continue;
                                        }
                                        if (signManager == null)
                                        {
                                            _logger.Error($"起單HRMaster找不到人 工號:{signEmp.managerEmpNo},SIGN__FORM_MAIN_NO:{SIGN.SIGN_FORM_NO}");
                                            continue;
                                        }
                                        stage = new SIGN_STAGE();
                                        stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                        stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                        stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                        stage.ROLE_ID = Role.ROLE_ID;
                                        stage.MERGE = Flow.MERGE;
                                        stage.SIGNATORY_DEPT_NO = signManager.deptNo;
                                        stage.SIGNATORY_EMP_NO = signManager.empNo;
                                        stage.SIGNATORY_NAME = signManager.empName;
                                        stages.Add(stage);
                                    }

                                    break;
                                case (int)EnumFlowRoles.ApplicantManager:
                                    //申請人直屬主管
                                    GLEmployee emp = hrMaster.GetEmployee(SIGN.APPLICANTER_EMP_NO);
                                    GLEmployee manager = hrMaster.GetEmployee(emp.managerEmpNo);
                                    if (emp == null)
                                    {
                                        _logger.Error($"起單HRMaster找不到人 工號:{SIGN.APPLICANTER_EMP_NO},SIGN__FORM_MAIN_NO:{SIGN.SIGN_FORM_NO}");
                                        continue;
                                    }
                                    if (manager == null)
                                    {
                                        _logger.Error($"起單HRMaster找不到人 工號:{emp.managerEmpNo},SIGN__FORM_MAIN_NO:{SIGN.SIGN_FORM_NO}");
                                        continue;
                                    }
                                    if (manager != null)
                                    {
                                        stage.SIGNATORY_DEPT_NO = manager.deptNo;
                                        stage.SIGNATORY_EMP_NO = manager.empNo;
                                        stage.SIGNATORY_NAME = manager.empName;
                                        stages.Add(stage);
                                    }
                                    break;
                                case (int)EnumFlowRoles.ApplicantMgrCosign:
                                    //申請人主管
                                    if (int.TryParse(sys.LEVEL_CODE, out int LevelCode))
                                    {
                                        //直接取所有階層  再由LEVELCODE去判斷要抓到那些人
                                        List<MxEmployee> ManagerList = hrMaster.GetAllManagerByEmpNoIncludeSP(SIGN.APPLICANTER_EMP_NO, 0);
                                        if (ManagerList != null && ManagerList.Count() >= 0)
                                        {
                                            StageAutoIncrement--;
                                        }
                                        var seq = 0;
                                        if (int.Parse(ManagerList.Max(x => x.orgLevelCode)) < LevelCode)
                                        {
                                            LevelCode = int.Parse(ManagerList.Max(x => x.orgLevelCode));
                                        }
                                        for (int i = int.Parse(ManagerList.Max(x => x.orgLevelCode)); i >= LevelCode; i -= 100)
                                        {
                                            MxEmployee Manager = ManagerList.Where(x => x.orgLevelCode == i.ToString("000")).FirstOrDefault();
                                            if (Manager == null && LevelCode > 0)
                                            {//抓不到人就在往上抓
                                                LevelCode = LevelCode - 100;
                                                continue;
                                            }
                                            else if (i >= 0 && Manager == null)
                                            {
                                                continue;
                                            }
                                            else if (LevelCode < 0 || (LevelCode == 0 && Manager == null))
                                            {
                                                break;
                                            }
                                            stage = new SIGN_STAGE
                                            {
                                                SIGN_FORM_ID = SIGN.SIGN_FORM_ID,
                                                STAGE_ORDER = Flow.STAGE_ORDER,
                                                ROLE_ID = Role.ROLE_ID,
                                                MERGE = Flow.MERGE,
                                                SIGNATORY_DEPT_NO = Manager.deptNo,
                                                SIGNATORY_EMP_NO = Manager.empNo,
                                                SIGNATORY_NAME = Manager.empName,
                                                SEQ = seq,
                                                SIGN_STAGE_NUMBER = StageAutoIncrement++
                                            };
                                            stages.Add(stage);
                                            seq++;
                                        }
                                    }
                                    break;
                                case (int)EnumFlowRoles.BusinessManager:
                                    //總部業務主管
                                    var SalesMgrList = Entities.PORTAL_SYSTEM_SALES_MAINTAINER.ToList();
                                    foreach (var sign in SalesMgrList.Select(x => x.EMPNO).ToList())
                                    {
                                        GLEmployee signEmp = hrMaster.GetEmployee(sign);
                                        stage = new SIGN_STAGE();
                                        stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                        stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                        stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                        stage.ROLE_ID = Role.ROLE_ID;
                                        stage.MERGE = Flow.MERGE;
                                        stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                        stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                        stage.SIGNATORY_NAME = signEmp.empName;
                                        stages.Add(stage);
                                    }
                                    break;
                                case (int)EnumFlowRoles.VipProcessor:
                                    //VIP處理人員
                                    var vipList = Entities.PORTAL_SYSTEM_VIP_MAINTAINER.ToList();
                                    foreach (var sign in vipList.Select(x => x.EMPNO).ToList())
                                    {
                                        GLEmployee signEmp = hrMaster.GetEmployee(sign);
                                        if (signEmp != null)
                                        {
                                            stage = new SIGN_STAGE();
                                            stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                            stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                            stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                            stage.ROLE_ID = Role.ROLE_ID;
                                            stage.MERGE = Flow.MERGE;
                                            stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                            stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                            stage.SIGNATORY_NAME = signEmp.empName;
                                            stages.Add(stage);
                                        }

                                    }
                                    break;
                                case (int)EnumFlowRoles.OverseasChairman:
                                    //海外董事長 取得方法:
                                    //1.請用文字拆解取得部門代碼 & Notes Account
                                    //2.利用部門代碼Call HrMaster API getEmployeesByDeptNo 取得部門所有人員
                                    //3.利用部門代碼 + Notes Account 找到員工資料
                                    emp = hrMaster.GetEmployee(SIGN.APPLICANTER_EMP_NO);
                                    var Chairman = Entities.PORTAL_IF_COMPANY_CHAIRMAN.Where(x => x.COMPANY == emp.company).FirstOrDefault();
                                    if (Chairman != null)
                                    {
                                        var dept = Chairman.CHAIRMAN_ACCOUNT.Split(':')[0];
                                        var deptEmpList = hrMaster.GetEmployeesByDeptNo(dept);
                                        foreach (var item in deptEmpList)
                                        {
                                            if (Chairman.CHAIRMAN_ACCOUNT.ToLower().Contains(item.accountName.ToLower()))
                                            {
                                                GLEmployee signEmp = hrMaster.GetEmployee(item.empNo);
                                                if (signEmp != null)
                                                {
                                                    stage = new SIGN_STAGE();
                                                    stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                                    stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                                    stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                                    stage.ROLE_ID = Role.ROLE_ID;
                                                    stage.MERGE = Flow.MERGE;
                                                    stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                                    stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                                    stage.SIGNATORY_NAME = signEmp.empName;
                                                    stages.Add(stage);
                                                }
                                                break;
                                            }
                                        }
                                    }

                                    break;
                                case (int)EnumFlowRoles.GeneralManager:
                                    //總部總經理
                                    var GM = Entities.PORTAL_SYSTEM_VIP_APPLICANT.Where(x => x.STATUS == 1 && x.ROLE_TYPE == (int)EnumVipMaintainerRoleType.Gmanger).ToList();
                                    //ROLE_TYPE == 1:總經理 2:董事長
                                    foreach (var sign in GM.Select(x => x.EMPNO).ToList())
                                    {
                                        GLEmployee signEmp = hrMaster.GetEmployee(sign);
                                        if (signEmp != null)
                                        {
                                            stage = new SIGN_STAGE();
                                            stage.SIGN_FORM_ID = SIGN.SIGN_FORM_ID;
                                            stage.STAGE_ORDER = Flow.STAGE_ORDER;
                                            stage.SIGN_STAGE_NUMBER = StageAutoIncrement++;
                                            stage.ROLE_ID = Role.ROLE_ID;
                                            stage.MERGE = Flow.MERGE;
                                            stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                            stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                            stage.SIGNATORY_NAME = signEmp.empName;
                                            stages.Add(stage);
                                        }
                                    }
                                    break;
                                case (int)EnumFlowRoles.NewUser:
                                    //異動的新保管人人

                                    if (Data.FormData.GetType() == typeof(Model.Sign.SWChangeForm))
                                    {

                                        var changeForm = Data.FormData as Model.Sign.SWChangeForm;
                                        var changeData = changeForm.SWChange[0];
                                        var addPerson = false;
                                        var applicanter = hrMaster.GetEmployee(SIGN.APPLICANTER_EMP_NO);
                                        GLEmployee signEmp = hrMaster.GetEmployee(changeData.AssetsManagerEmpno);
                                        if (changeForm.ApplyItemId == "CgType1")
                                        {//保管人不同人，不同部門：新使用人、新使用人主管 需塞值
                                            if (SIGN.APPLICANTER_EMP_NO != changeData.AssetsManagerEmpno)
                                                addPerson = true;
                                        }
                                        if (changeForm.ApplyItemId == "CgType4")
                                        {
                                            addPerson = true;
                                        }
                                        if (changeForm.ApplyItemId == "CgType8")
                                        {//掛帳人不同部門：新使用人、新使用人主管 需塞值。
                                            addPerson = true;
                                        }
                                        if (addPerson)
                                        {
                                            stage.SIGNATORY_DEPT_NO = signEmp.deptNo;
                                            stage.SIGNATORY_EMP_NO = signEmp.empNo;
                                            stage.SIGNATORY_NAME = signEmp.empName;
                                            stages.Add(stage);
                                        }

                                    }
                                    break;
                                case (int)EnumFlowRoles.NewUserManager:
                                    //異動的新保管人主管
                                    if (Data.FormData.GetType() == typeof(Model.Sign.SWChangeForm))
                                    {
                                        var changeForm = Data.FormData as Model.Sign.SWChangeForm;
                                        var changeData = changeForm.SWChange[0];
                                        var addPerson = false;
                                        var applicanter = hrMaster.GetEmployee(SIGN.APPLICANTER_EMP_NO);
                                        GLEmployee signEmp = hrMaster.GetEmployee(changeData.AssetsManagerEmpno);
                                        if (changeForm.ApplyItemId == "CgType1")
                                        {//保管人不同人，不同部門：新使用人、新使用人主管 需塞值
                                            if (SIGN.APPLICANTER_EMP_NO != changeData.AssetsManagerEmpno && applicanter.deptNo != signEmp.deptNo)
                                                addPerson = true;
                                        }
                                        if (changeForm.ApplyItemId == "CgType3")
                                        {//原使用人與新保留部門為跨部門：新使用人不塞值；新使用人主管需塞值
                                            if (applicanter.deptNo != changeData.NewReservedDepartment)
                                            {
                                                var detpMgr = hrMaster.GetDeptMgrInfo(changeData.NewReservedDepartment);
                                                stage = new SIGN_STAGE
                                                {
                                                    SIGN_FORM_ID = SIGN.SIGN_FORM_ID,
                                                    STAGE_ORDER = Flow.STAGE_ORDER,
                                                    ROLE_ID = Role.ROLE_ID,
                                                    MERGE = Flow.MERGE,
                                                    SIGNATORY_DEPT_NO = detpMgr.deptNo,
                                                    SIGNATORY_EMP_NO = detpMgr.empNo,
                                                    SIGNATORY_NAME = detpMgr.empName,
                                                    SEQ = 0,
                                                    SIGN_STAGE_NUMBER = StageAutoIncrement++
                                                };
                                                stages.Add(stage);
                                            }
                                        }
                                        if (changeForm.ApplyItemId == "CgType4")
                                        {//保留部門與新使用人為跨部門：新使用人主管 需塞值。
                                            if (changeData.ReservedDepartment != signEmp.deptNo)
                                                addPerson = true;
                                        }
                                        if (changeForm.ApplyItemId == "CgType5")
                                        {//只有部門異動 需要塞入部門主管
                                            if (changeData.ReservedDepartment != changeData.NewReservedDepartment)
                                            {
                                                var detpMgr = hrMaster.GetDeptMgrInfo(changeData.NewReservedDepartment);
                                                stage = new SIGN_STAGE
                                                {
                                                    SIGN_FORM_ID = SIGN.SIGN_FORM_ID,
                                                    STAGE_ORDER = Flow.STAGE_ORDER,
                                                    ROLE_ID = Role.ROLE_ID,
                                                    MERGE = Flow.MERGE,
                                                    SIGNATORY_DEPT_NO = detpMgr.deptNo,
                                                    SIGNATORY_EMP_NO = detpMgr.empNo,
                                                    SIGNATORY_NAME = detpMgr.empName,
                                                    SEQ = 0,
                                                    SIGN_STAGE_NUMBER = StageAutoIncrement++
                                                };
                                                stages.Add(stage);
                                            }
                                        }
                                        if (changeForm.ApplyItemId == "CgType8")
                                        {//掛帳人不同部門：新使用人、新使用人主管 需塞值
                                            if (applicanter.deptNo != signEmp.deptNo)
                                                addPerson = true;
                                        }
                                        if (addPerson)
                                        {
                                            LevelCode = 400;//到部級主管
                                                            //直接取所有階層  再由LEVELCODE去判斷要抓到那些人
                                            List<MxEmployee> ManagerList = hrMaster.GetAllManagerByEmpNoIncludeSP(changeData.AssetsManagerEmpno, 0);
                                            if (ManagerList != null && ManagerList.Count() > 0)
                                            {
                                                StageAutoIncrement--;
                                            }
                                            var seq = 0;
                                            if (int.Parse(ManagerList.Max(x => x.orgLevelCode)) < LevelCode)
                                            {//如果
                                                LevelCode = int.Parse(ManagerList.Max(x => x.orgLevelCode));
                                            }
                                            for (int i = int.Parse(ManagerList.Max(x => x.orgLevelCode)); i >= LevelCode; i -= 100)
                                            {
                                                MxEmployee Manager = ManagerList.Where(x => x.orgLevelCode == i.ToString("000")).FirstOrDefault();
                                                if (Manager == null && LevelCode > 0)
                                                {//抓不到人就在往上抓
                                                    LevelCode = LevelCode - 100;
                                                    continue;
                                                }
                                                else if (i >= 0 && Manager == null)
                                                {
                                                    continue;
                                                }
                                                else if (LevelCode < 0 || (LevelCode == 0 && Manager == null))
                                                {
                                                    break;
                                                }
                                                stage = new SIGN_STAGE
                                                {
                                                    SIGN_FORM_ID = SIGN.SIGN_FORM_ID,
                                                    STAGE_ORDER = Flow.STAGE_ORDER,
                                                    ROLE_ID = Role.ROLE_ID,
                                                    MERGE = Flow.MERGE,
                                                    SIGNATORY_DEPT_NO = Manager.deptNo,
                                                    SIGNATORY_EMP_NO = Manager.empNo,
                                                    SIGNATORY_NAME = Manager.empName,
                                                    SEQ = seq,
                                                    SIGN_STAGE_NUMBER = StageAutoIncrement++
                                                };
                                                stages.Add(stage);
                                                seq++;

                                            }
                                        }

                                    }
                                    break;

                                case (int)EnumFlowRoles.ApplicantAcceptance:
                                    break;
                                case (int)EnumFlowRoles.Close:
                                    break;
                                default:
                                    break;
                            }

                        }

                        //串簽連續關卡同一個人 只保留後面的
                        var prevStage = new SIGN_STAGE();
                        foreach (var checkItem in stages.Where(x => x.MERGE == false).OrderBy(x => x.STAGE_ORDER).ThenBy(x => x.SEQ))
                        {
                            if(checkItem.STAGE_ORDER == prevStage.STAGE_ORDER && checkItem.SIGNATORY_EMP_NO == prevStage.SIGNATORY_EMP_NO)
                            {
                                stages.Remove(prevStage);
                            }else
                            {
                                prevStage = checkItem;
                            }
                        }

                        #endregion


                    }


                    if (IsNew)
                    {
                        SIGN.CREATE_DATE = DateTime.Now;
                        Entities.SIGN_FORM_MAIN.Add(SIGN);
                    }

                    if (stages.Count > 0)
                    {
                        Entities.SIGN_STAGE.AddRange(stages);
                    }

                    Entities.SaveChanges();
                    Repository.SetEntities(Entities);
                    var nextStage = Entities.SIGN_STAGE.Where(x => x.STAGE_ORDER > 1 && x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).FirstOrDefault();
                    //if (SIGN.APPLICANTER_EMP_NO == SIGN.FILLER_EMP_NO && nextStage != null && nextStage.ROLE_ID == (int)EnumFlowRoles.Applicant)
                    //{//填表人 申請人是不是同一個是的話下一關是第三關
                    //    nextStage = stages.Where(x => x.STAGE_ORDER == 3).FirstOrDefault();
                    //}
                    Repository.Create(Data.FormData, _mapper.Map<SIGN_FORM_MAIN, SignFormMain>(SIGN), SIGN.SIGN_FORM_ID, IsNew, nextStage);
                    Entities.SaveChanges();

                    #region 第一關or第二關簽核
                    //新增自動簽合LOG
                    var StageID = Entities.SIGN_STAGE.FirstOrDefault(x => x.SIGN_FORM_ID == AutoIncrement && x.STAGE_ORDER == 1);
                    decimal AutoIncrementLog = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault();
                    if (StageID != null)
                    {
                        List<SIGN_LOG> logs = new List<SIGN_LOG>();
                        SIGN_LOG log = new SIGN_LOG();
                        log.SIGNATORY_EMP_NO = SIGN.FILLER_EMP_NO;
                        log.SIGNATORY_DEPT_NO = SIGN.FILLER_DEPT_NO;
                        log.SIGNATORY_NAME = SIGN.FILLER_NAME;
                        log.SIGN_DATE = DateTime.Now;
                        log.SIGN_STATUS = "Approved";
                        log.SIGN_STAGE_NUMBER = StageID.SIGN_STAGE_NUMBER;
                        log.SIGN_LOG_NUMBER = AutoIncrementLog + 1;
                        log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                        Entities.SIGN_LOG.Add(log);
                        SIGN.NOW_STAGE_ORDER = nextStage.STAGE_ORDER;


                        ////填表人 申請人是不是同一個是的話自動簽過
                        //if (SIGN.APPLICANTER_EMP_NO == SIGN.FILLER_EMP_NO && nextStage.STAGE_ORDER == 3)
                        //{
                        //    #region BPM
                        //    var bpmApproveResponse = bpm.ApproveAction(new ActionQuery
                        //    {
                        //        IdNo = SIGN.GUID_FOR_BPM,
                        //        Comment = "",
                        //        ActionName = "Type.AutoApprov",
                        //        ExecutorEmpNo = SIGN.APPLICANTER_EMP_NO,
                        //        ApproverId = SIGN.APPLICANTER_EMP_NO,
                        //    });
                        //    if (bpmApproveResponse.rtnCode != "0")
                        //    {
                        //        return response;
                        //    }
                        //    if (bpmApproveResponse.changeApprover.Count > 0)
                        //    {//BPM回傳更換簽核人
                        //        foreach (var item in bpmApproveResponse.changeApprover)
                        //        {
                        //            var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                        //            var approveStages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                        //            stages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmApproveResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                        //            {
                        //                c.SIGNATORY_EMP_NO = item.NewApprover;
                        //                c.SIGNATORY_NAME = changeEmp.empName;
                        //                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                        //                return c;
                        //            }).FirstOrDefault();

                        //        }
                        //    }
                        //    #endregion

                        //    StageID = Entities.SIGN_STAGE.FirstOrDefault(x => x.SIGN_FORM_ID == AutoIncrement && x.STAGE_ORDER == 2);
                        //    log = new SIGN_LOG();
                        //    log.SIGNATORY_EMP_NO = SIGN.APPLICANTER_EMP_NO;
                        //    log.SIGNATORY_DEPT_NO = SIGN.APPLICANTER_DEPT_NO;
                        //    log.SIGNATORY_NAME = SIGN.APPLICANTER_NAME;
                        //    log.SIGN_DATE = DateTime.Now;
                        //    log.SIGN_STATUS = "Approved";
                        //    log.SIGN_STAGE_NUMBER = StageID.SIGN_STAGE_NUMBER;
                        //    log.SIGN_LOG_NUMBER = AutoIncrementLog + 2;
                        //    Entities.SIGN_LOG.Add(log);
                        //    log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                        //    SIGN.NOW_STAGE_ORDER = 3;
                        //}

                        Entities.SaveChanges();
                    }
                    var signState = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID
                                && x.STAGE_ORDER == SIGN.NOW_STAGE_ORDER).FirstOrDefault();
                    if (signState != null)
                    {
                        SIGN.FORM_STATUS = signState.FLOW_ROLES.FLOW_STATUS;
                        Entities.SaveChanges();
                    }
                    #endregion

                    if (Data.Sign.FormStatus != System.Enum.GetName(typeof(EnumFormStatus), EnumFormStatus.Draft))
                    {
                        #region BPM
                        if (isSignState)
                        {
                            //重新跑關卡
                            var bpmApproveResponse = bpm.ApproveAction(new ActionQuery
                            {
                                IdNo = SIGN.GUID_FOR_BPM,
                                ActionName = Data.SignButtonKey,
                                Comment = Data.Comment,
                                ExecutorEmpNo = SIGN.APPLICANTER_EMP_NO,
                                ApproverId = SIGN.APPLICANTER_EMP_NO,
                            });
                            if (bpmApproveResponse.rtnCode != "0")
                            {
                                tran.Rollback();
                                return response;
                            }
                            if (bpmApproveResponse.changeApprover.Count > 0)
                            {//BPM回傳更換簽核人
                                foreach (var item in bpmApproveResponse.changeApprover)
                                {
                                    var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                                    var approveStages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                                    approveStages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmApproveResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                                    {
                                        c.SIGNATORY_EMP_NO = item.NewApprover;
                                        c.SIGNATORY_NAME = changeEmp.empName;
                                        c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                        return c;
                                    }).FirstOrDefault();

                                }
                            }
                        }
                        else
                        {
                            //呼叫BPM
                            SIGN.GUID_FOR_BPM = Guid.NewGuid().ToString();
                            var _BPMrequest = new CreateFormQuery
                            {
                                requistionId = SIGN.GUID_FOR_BPM,
                                applicantEmpNo = SIGN.APPLICANTER_EMP_NO,
                                docNo = SIGN.SIGN_FORM_NO,
                                formType = Data.FormData.GetType() == typeof(SwapplyForm) ? 1 : 2,
                                bpmDiagramId = BPM_DIAGRAM_ID,
                                bpmIdentity = BPM_IDENTIFY,
                            };
                            var flowRole = Entities.FLOW_ROLES.ToList();
                            var bpmFlow = new List<BPMFlowArgs>();
                            bpmFlow = (from t1 in stages
                                       join t2 in flowRole on t1.ROLE_ID equals t2.ROLE_ID
                                       select new BPMFlowArgs
                                       {
                                           BPMCode = t2.CODE_FOR_BPM,
                                           EmpNO = t1.SIGNATORY_EMP_NO,
                                       }).ToList();
                            var bpmResponse = bpm.CreateForm(_BPMrequest, bpmFlow);

                            if (bpmResponse.rtnCode != "0")
                            {
                                tran.Rollback();
                                return response;
                            }

                            if (bpmResponse.changeApprover.Count > 0)
                            {//BPM回傳更換簽核人
                                foreach (var item in bpmResponse.changeApprover)
                                {
                                    var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                                    var approveStages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                                    if (bpmResponse.ProcessId == "ApplicantCosign")
                                    {//申請人代理
                                        SIGN.APPLICANTER_EMP_NO = item.NewApprover;
                                    }
                                    var emp = hrMaster.GetEmployee(item.NewApprover);
                                    approveStages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                                    {
                                        c.SIGNATORY_EMP_NO = item.NewApprover;
                                        c.SIGNATORY_NAME = emp.empName;
                                        c.SIGNATORY_DEPT_NO = emp.deptNo;
                                        return c;
                                    }).FirstOrDefault();

                                }
                            }
                        }
                        #endregion BPM
                        Entities.SaveChanges();
                    }


                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.Message = ex.ToString();
                    _logger.Error(ex.ToString());
                    return response;
                }
            }
            //return "";
            response.StatusCode = (int)EnumStatusCode.Success;
            return response;


        }

        #region 取得表單編號
        /// <summary>
        /// 取得表單編號
        /// </summary>
        /// <param name="pFormType"></param>
        /// <returns></returns>
        private string GetSignNo(string pFormType)
        {
            //取SIGN_FORM_NO
            string StrNo = "";
            string StrSignFormType = "MA";
            //Data.FormData;
            switch (pFormType)
            {
                case "software":
                case "SAMSwapplyForm":
                case "SWChangeForm":
                    StrSignFormType = "SA";
                    break;
                case "Hardward":
                    StrSignFormType = "HA";
                    break;
                case "Account":
                case "AccountApplyForm":
                case "PAMCitrixPermissionForm":
                    StrSignFormType = "PA";
                    break;
            }

            var builder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Entities"].ConnectionString);
            string _connectString = builder["provider connection string"].ToString();
            using (var cn = new Oracle.ManagedDataAccess.Client.OracleConnection(_connectString))
            {
                cn.Open();
                var vListNo = cn.Query("SELECT * FROM(SELECT SIGN_FORM_NO FROM SIGN_FORM_MAIN sfm WHERE SIGN_FORM_NO LIKE '" + StrSignFormType + "_" + DateTime.Now.ToString("yyMM") + "_" + "%' ORDER BY  SIGN_FORM_NO DESC) WHERE rownum = 1").ToList();
                if (vListNo.Count > 0)
                {
                    string StrTopNo = vListNo[0].SIGN_FORM_NO;
                    string StrIndex = StrTopNo.Substring(StrTopNo.Length - 5, 5);
                    StrNo = StrSignFormType + "_" + DateTime.Now.ToString("yyMM") + "_" + (int.Parse(StrIndex) + 1).ToString("00000");
                }
                else
                {
                    //SA_20200108_0033
                    StrNo = StrSignFormType + "_" + DateTime.Now.ToString("yyMM") + "_00001";
                }
            }

            return StrNo;
        }
        #endregion

        /// <summary>
        /// 簽核
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public PageQueryResult<string> Approve(SignData<T> Data, string empno, IHrMasterService hrMaster, IBPMService bpm)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            using (var tran = Entities.Database.BeginTransaction())
            {
                try
                {


                    var signVerification = SignVerification(Data, empno);
                    if (signVerification.StatusCode == (int)EnumStatusCode.Fail)
                    {
                        return response;
                    }
                    var stageList = signVerification.SignStageList;
                    var SIGN = signVerification.Sign;
                    var stage = signVerification.SignStage;

                    //寫入LOG SIGN_STATUS = 'Approved' 才會觸發更新當前關卡 可以參照上面Create最後寫LOG的時候 記得要SaveChanges();
                    SIGN_LOG Log = new SIGN_LOG();
                    var AutoIncrement = Data.Sign.SignFromID;

                    //新增自動簽合LOG
                    decimal AutoIncrementLog = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault() + 1;


                    var goNext = false;
                    if (stage.MERGE == true)
                    {//並簽
                        goNext = true;
                    }
                    else
                    {
                        if (stageList.Where(x => x.STAGE_ORDER == SIGN.NOW_STAGE_ORDER).ToList().Count == 1)
                        {//單人
                            goNext = true;
                        }
                        else
                        {
                            var approved = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Approved).ToString();
                            var otherSignLog = Entities.SIGN_LOG.Where(x => x.SIGN_STAGE.SIGN_FORM_ID == SIGN.SIGN_FORM_ID && x.MARK != true && x.SIGN_STATUS == approved && x.SIGN_STAGE.STAGE_ORDER == SIGN.NOW_STAGE_ORDER).ToList();
                            if (otherSignLog.Count + 1 >= stageList.Where(x => x.STAGE_ORDER == SIGN.NOW_STAGE_ORDER).Count())
                            {//都簽完了
                                goNext = true;
                            }
                        }
                    }

                    List<SIGN_LOG> logs = new List<SIGN_LOG>();
                    SIGN_LOG log = new SIGN_LOG();
                    var emp = hrMaster.GetEmployee(empno);
                    log.SIGNATORY_EMP_NO = empno;
                    log.SIGNATORY_DEPT_NO = emp.deptNo;
                    log.SIGNATORY_NAME = emp.empName;
                    log.SIGN_DATE = DateTime.Now;
                    log.SIGN_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Approved);
                    log.SIGN_STAGE_NUMBER = stage.SIGN_STAGE_NUMBER;
                    log.SIGN_LOG_NUMBER = AutoIncrementLog;
                    log.SIGN_SUGGESTION = Data.Comment;
                    log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                    SIGN.FINAL_SIGN_DATE = DateTime.Now;
                    Entities.SIGN_LOG.Add(log);

                    if (goNext)
                    {//進下一關
                        var nextOrder = stageList.Where(x => x.STAGE_ORDER > SIGN.NOW_STAGE_ORDER && x.STAGE_ORDER < (int)EnumFlowRoles.Deploy).OrderBy(x => x.STAGE_ORDER).ToList();
                        //if (nextOrder.Count == 1)
                        //{//進入核定關卡
                        //    SIGN.FORM_STATUS = nextOrder[0].FLOW_ROLES.FLOW_STATUS;
                        //    SIGN.NOW_STAGE_ORDER = nextOrder[0].STAGE_ORDER;

                        //}
                        //else
                        if (nextOrder.Count == 0)//沒有下一關了 應走核定
                        {
                            response = Approved(Data, hrMaster);
                            if (response.StatusCode != (int)EnumStatusCode.Success)
                            {
                                return response;
                            }
                        }
                        else
                        {
                            SIGN.FORM_STATUS = nextOrder[0].FLOW_ROLES.FLOW_STATUS;
                            SIGN.NOW_STAGE_ORDER = nextOrder[0].STAGE_ORDER;

                            Repository.SetEntities(Entities);
                            Repository.Approve(Data.FormData, _mapper.Map<SIGN_FORM_MAIN, SignFormMain>(SIGN), nextOrder[0]);
                        }
                    }
                    Entities.SaveChanges();
                    #region BPM
                    var bpmResponse = bpm.ApproveAction(new ActionQuery
                    {
                        IdNo = SIGN.GUID_FOR_BPM,
                        ActionName = Data.SignButtonKey,
                        Comment = Data.Comment,
                        ExecutorEmpNo = stage.SIGNATORY_EMP_NO,
                        ApproverId = stage.SIGNATORY_EMP_NO,
                        ApproverDept = stage.SIGNATORY_DEPT_NO,
                        ApproverName = stage.SIGNATORY_NAME
                    });
                    if (bpmResponse.rtnCode != "0")
                    {
                        tran.Rollback();
                        return response;
                    }

                    if (bpmResponse.changeApprover.Count > 0)
                    {//BPM回傳更換簽核人
                        foreach (var item in bpmResponse.changeApprover)
                        {
                            var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                            var stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                            stages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                            {
                                c.SIGNATORY_EMP_NO = item.NewApprover;
                                c.SIGNATORY_NAME = changeEmp.empName;
                                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                return c;
                            }).FirstOrDefault();

                        }
                    }
                    #endregion
                    Entities.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.Message = ex.ToString();
                    _logger.Error(ex.ToString());
                    return response;
                }

                response.StatusCode = (int)EnumStatusCode.Success;
                return response;
            }
        }
        /// <summary>
        /// 核定
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public PageQueryResult<string> Approved(SignData<T> Data, IHrMasterService hrMaster)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;

            try
            {
                SIGN_FORM_MAIN Sign = Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == Data.Sign.SignFromID);
                Sign.FORM_STATUS = System.Enum.GetName(typeof(EnumFormStatus), EnumFormStatus.Closed);
                Sign.NOW_STAGE_ORDER = (int)EnumFlowRoles.Close;
                Sign.FINAL_SIGN_DATE = DateTime.Now;


                Repository.SetEntities(Entities);
                Repository.Approved(Data.FormData, _mapper.Map<SIGN_FORM_MAIN, SignFormMain>(Sign));
                Entities.SaveChanges();

                //Entities.SaveChanges();
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                _logger.Error(ex.StackTrace);
                return response;
            }

            response.StatusCode = (int)EnumStatusCode.Success;
            return response;
        }

        /// <summary>
        /// 拒絕且回到第一關
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public PageQueryResult<string> Rejected(SignData<T> Data, string empno, IHrMasterService hrMaster, IBPMService bpm)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            using (var tran = Entities.Database.BeginTransaction())
            {
                try
                {
                    var signVerification = SignVerification(Data, empno);
                    if (signVerification.StatusCode == (int)EnumStatusCode.Fail)
                    {
                        return response;
                    }
                    var SIGN = signVerification.Sign;
                    var stage = signVerification.SignStage;

                    SIGN.FORM_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Rejected);
                    SIGN.NOW_STAGE_ORDER = 1;

                    decimal AutoIncrementLog = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault() + 1;
                    List<SIGN_LOG> logs = new List<SIGN_LOG>();
                    SIGN_LOG log = new SIGN_LOG();
                    var emp = hrMaster.GetEmployee(empno);
                    log.SIGNATORY_EMP_NO = empno;
                    log.SIGNATORY_DEPT_NO = emp.deptNo;
                    log.SIGNATORY_NAME = emp.empName;
                    log.SIGN_DATE = DateTime.Now;
                    log.SIGN_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Rejected);
                    log.SIGN_STAGE_NUMBER = stage.SIGN_STAGE_NUMBER;
                    log.SIGN_LOG_NUMBER = AutoIncrementLog;
                    log.SIGN_SUGGESTION = Data.Comment;
                    log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                    log.MARK = true;
                    SIGN.FINAL_SIGN_DATE = DateTime.Now;
                    Entities.SIGN_LOG.Add(log);

                    Entities.SIGN_STAGE
                        .Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID)
                        .SelectMany(x => x.SIGN_LOG)
                        .ToList()
                        .ForEach(u =>
                        {
                            u.MARK = true;
                        });
                    Entities.SaveChanges();
                    #region BPM
                    var bpmResponse = bpm.Reject(new ActionQuery
                    {
                        IdNo = SIGN.GUID_FOR_BPM,
                        Comment = Data.Comment,
                        ActionName = Data.SignButtonKey,
                        ExecutorEmpNo = stage.SIGNATORY_EMP_NO,
                        ApproverId = stage.SIGNATORY_EMP_NO,
                        ApproverDept = stage.SIGNATORY_DEPT_NO,
                        ApproverName = stage.SIGNATORY_NAME,
                        TargetStop = "FillerID" //填表人
                    });
                    if (bpmResponse.rtnCode != "0")
                    {
                        tran.Rollback();
                        return response;
                    }

                    if (bpmResponse.changeApprover.Count > 0)
                    {//BPM回傳更換簽核人
                        foreach (var item in bpmResponse.changeApprover)
                        {
                            var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                            var stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                            stages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                            {
                                c.SIGNATORY_EMP_NO = item.NewApprover;
                                c.SIGNATORY_NAME = changeEmp.empName;
                                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                return c;
                            }).FirstOrDefault();

                        }
                    }
                    #endregion
                    Entities.SaveChanges();


                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.Message = ex.ToString();
                    _logger.Error(ex.ToString());
                    return response;
                }
            }
            response.StatusCode = (int)EnumStatusCode.Success;
            return response;

        }


        /// <summary>
        /// 結案
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public PageQueryResult<string> Close(SignData<T> Data, string empno, IHrMasterService hrMaster, IBPMService bpm)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            using (var tran = Entities.Database.BeginTransaction())
            {
                try
                {
                    SIGN_FORM_MAIN SIGN = Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == Data.Sign.SignFromID);
                    var emp = hrMaster.GetEmployee(empno);
                    SIGN.FORM_STATUS = System.Enum.GetName(typeof(EnumFormStatus), EnumFormStatus.Closed);
                    SIGN.NOW_STAGE_ORDER = (int)EnumFlowRoles.Close;
                    SIGN.FINAL_SIGN_DATE = DateTime.Now;
                    var stage = new SIGN_STAGE
                    {
                        SIGN_FORM_ID = SIGN.SIGN_FORM_ID,
                        ROLE_ID = (int)EnumFlowRoles.Close,
                        SIGN_STAGE_NUMBER = Entities.SIGN_STAGE.Max(s => (decimal?)s.SIGN_STAGE_NUMBER).GetValueOrDefault() + 1,
                        STAGE_ORDER = (int)EnumFlowRoles.Close,
                        SIGNATORY_DEPT_NO = emp.deptNo,
                        SIGNATORY_EMP_NO = empno,
                        SIGNATORY_NAME = emp.empName,
                    };
                    Entities.SIGN_STAGE.Add(stage);

                    SIGN_LOG log = new SIGN_LOG();

                    log.SIGNATORY_EMP_NO = empno;
                    log.SIGNATORY_DEPT_NO = emp.deptNo;
                    log.SIGNATORY_NAME = emp.empName;
                    log.SIGN_DATE = DateTime.Now;
                    log.SIGN_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Approved);
                    log.SIGN_STAGE_NUMBER = stage.SIGN_STAGE_NUMBER;
                    log.SIGN_LOG_NUMBER = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault() + 1;
                    log.SIGN_SUGGESTION = Data.Comment;
                    log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                    SIGN.FINAL_SIGN_DATE = DateTime.Now;
                    Entities.SIGN_LOG.Add(log);


                    Repository.Close(Data.FormData, _mapper.Map<SIGN_FORM_MAIN, SignFormMain>(SIGN));

                    Entities.SaveChanges();
                    #region BPM
                    var bpmResponse = bpm.ApproveAction(new ActionQuery
                    {
                        IdNo = SIGN.GUID_FOR_BPM,
                        Comment = Data.Comment,
                        ExecutorEmpNo = log.SIGNATORY_EMP_NO,
                        ApproverId = log.SIGNATORY_EMP_NO,
                        ApproverDept = log.SIGNATORY_DEPT_NO,
                        ApproverName = log.SIGNATORY_NAME
                    });
                    if (bpmResponse.rtnCode != "0")
                    {
                        tran.Rollback();
                        return response;
                    }

                    if (bpmResponse.changeApprover.Count > 0)
                    {//BPM回傳更換簽核人
                        foreach (var item in bpmResponse.changeApprover)
                        {
                            var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                            var stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                            stages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                            {
                                c.SIGNATORY_EMP_NO = item.NewApprover;
                                c.SIGNATORY_NAME = changeEmp.empName;
                                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                return c;
                            }).FirstOrDefault();

                        }
                    }
                    #endregion                    Repository.SetEntities(Entities);
                    Entities.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.Message = ex.ToString();
                    _logger.Error(ex.ToString());
                    return response;
                }
            }

            response.StatusCode = (int)EnumStatusCode.Success;
            return response;

        }


        /// <summary>
        /// 變更簽核人
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public PageQueryResult<string> ChangeApprover(SignData<T> Data, string empno, IHrMasterService hrMaster, IBPMService bpm)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            var Approver = hrMaster.GetEmployee(empno);
            using (var tran = Entities.Database.BeginTransaction())
            {
                SIGN_FORM_MAIN SIGN = Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == Data.Sign.SignFromID);

                decimal AutoIncrementLog = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault() + 1;
                SIGN_LOG log = new SIGN_LOG();
                var emp = hrMaster.GetEmployee(empno);

                var vStage = SIGN.SIGN_STAGE.Where(x => x.STAGE_ORDER == SIGN.NOW_STAGE_ORDER);
                foreach (var item in vStage)
                {
                    log = new SIGN_LOG();
                    log.SIGN_DATE = DateTime.Now;
                    log.SIGN_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.ChangeApprover);
                    log.SIGN_STAGE_NUMBER = item.SIGN_STAGE_NUMBER;
                    log.SIGN_LOG_NUMBER = AutoIncrementLog++;
                    log.SIGN_STATUS_TRANSLATE = $"({item.SIGNATORY_NAME}->{emp.empName}){Data.Comment}";
                    log.SIGN_SUGGESTION = Data.Comment;
                    log.SIGNATORY_EMP_NO = empno;
                    log.SIGNATORY_DEPT_NO = emp.deptNo;
                    log.SIGNATORY_NAME = emp.empName;
                    log.MARK = true;
                    Entities.SIGN_LOG.Add(log);

                    int IntIndex = Data.ExecutorsID.FindIndex(x => x == item.SIGNATORY_EMP_NO);
                    item.SIGNATORY_EMP_NO = Data.ShiftsID[IntIndex];
                    item.SIGNATORY_NAME = Data.ShiftsName[IntIndex];
                    item.SIGNATORY_DEPT_NO = Data.ShiftsDept[IntIndex];
                    Entities.SaveChanges();
                    #region BPM
                    var bpmResponse = bpm.ShiftApprover(new ActionQuery
                    {
                        IdNo = SIGN.GUID_FOR_BPM,
                        Comment = Data.Comment,
                        ExecutorEmpNo = Approver.empNo,
                        ApproverId = Approver.empNo,
                        ApproverDept = Approver.deptNo,
                        ApproverName = Approver.empName,
                        NewApproverDept = item.SIGNATORY_DEPT_NO,
                        NewApproverId = item.SIGNATORY_EMP_NO,

                    });
                    if (bpmResponse.rtnCode != "0")
                    {
                        tran.Rollback();
                        return response;
                    }

                    if (bpmResponse.changeApprover.Count > 0)
                    {//BPM回傳更換簽核人
                        foreach (var citem in bpmResponse.changeApprover)
                        {
                            var changeEmp = hrMaster.GetEmployee(citem.NewApprover);
                            var stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                            stages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmResponse.ProcessId && x.SIGNATORY_EMP_NO == citem.OriginalApprover).Select(c =>
                            {
                                c.SIGNATORY_EMP_NO = citem.NewApprover;
                                c.SIGNATORY_NAME = changeEmp.empName;
                                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                return c;
                            }).FirstOrDefault();

                        }
                    }
                    #endregion
                }

                Repository.SetEntities(Entities);

                Entities.SaveChanges();
                tran.Commit();
            }

            response.StatusCode = (int)EnumStatusCode.Success;
            return response;

        }


        /// <summary>
        /// 作廢
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="empno"></param>
        /// <param name="hrMaster"></param>
        /// <param name="bpm"></param>
        /// <returns></returns>
        public PageQueryResult<string> Invalid(SignData<T> Data, string empno, IHrMasterService hrMaster, IBPMService bpm)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            using (var tran = Entities.Database.BeginTransaction())
            {
                try
                {
                    var signVerification = SignVerification(Data, empno);
                    if (signVerification.StatusCode == (int)EnumStatusCode.Fail)
                    {
                        return response;
                    }
                    var SIGN = signVerification.Sign;
                    var stage = signVerification.SignStage;

                    SIGN.FORM_STATUS = EnumFormStatus.Invalid.ToString();
                    SIGN.NOW_STAGE_ORDER = 0;

                    decimal AutoIncrementLog = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault() + 1;
                    List<SIGN_LOG> logs = new List<SIGN_LOG>();
                    SIGN_LOG log = new SIGN_LOG();
                    var emp = hrMaster.GetEmployee(empno);
                    log.SIGNATORY_EMP_NO = empno;
                    log.SIGNATORY_DEPT_NO = emp.deptNo;
                    log.SIGNATORY_NAME = emp.empName;
                    log.SIGN_DATE = DateTime.Now;
                    log.SIGN_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Invalid);
                    log.SIGN_STAGE_NUMBER = stage.SIGN_STAGE_NUMBER;
                    log.SIGN_LOG_NUMBER = AutoIncrementLog;
                    log.SIGN_SUGGESTION = Data.Comment;
                    log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                    SIGN.FINAL_SIGN_DATE = DateTime.Now;
                    Entities.SIGN_LOG.Add(log);

                    Entities.SaveChanges();
                    #region BPM
                    var bpmResponse = bpm.Invalid(new ActionQuery
                    {
                        IdNo = SIGN.GUID_FOR_BPM,
                        Comment = Data.Comment,
                        ActionName = Data.SignButtonKey,
                        ExecutorEmpNo = stage.SIGNATORY_EMP_NO,
                        ApproverId = stage.SIGNATORY_EMP_NO,
                        ApproverDept = stage.SIGNATORY_DEPT_NO,
                        ApproverName = stage.SIGNATORY_NAME,
                        TargetStop = "FillerID" //填表人
                    });
                    if (bpmResponse.rtnCode != "0")
                    {
                        tran.Rollback();
                        return response;
                    }

                    if (bpmResponse.changeApprover.Count > 0)
                    {//BPM回傳更換簽核人
                        foreach (var item in bpmResponse.changeApprover)
                        {
                            var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                            var stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                            stages.Where(x => x.FLOW_ROLES.CODE_FOR_BPM == bpmResponse.ProcessId).Select(c =>
                            {
                                c.SIGNATORY_EMP_NO = item.NewApprover;
                                c.SIGNATORY_NAME = changeEmp.empName;
                                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                return c;
                            }).FirstOrDefault();

                        }
                    }
                    #endregion
                    Entities.SaveChanges();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.Message = ex.ToString();
                    _logger.Error(ex.ToString());
                    return response;
                }
            }
            response.StatusCode = (int)EnumStatusCode.Success;
            return response;

        }


        /// <summary>
        /// 退回上一關
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public PageQueryResult<string> Reject(SignData<T> Data, string empno, IHrMasterService hrMaster, IBPMService bpm)
        {
            var response = new PageQueryResult<string>();
            response.StatusCode = (int)EnumStatusCode.Fail;
            using (var tran = Entities.Database.BeginTransaction())
            {
                try
                {
                    var signVerification = SignVerification(Data, empno);
                    if (signVerification.StatusCode == (int)EnumStatusCode.Fail)
                    {
                        return response;
                    }
                    var SIGN = signVerification.Sign;
                    var stage = signVerification.SignStage;

                    var lastStage = signVerification.SignStageList.Where(x => x.STAGE_ORDER < stage.STAGE_ORDER).OrderByDescending(x => x.STAGE_ORDER).FirstOrDefault();

                    if (stage.MERGE == false)
                    {//會簽要判斷當下關卡是否有人簽過了 回前一個人簽核

                        var prevStage = signVerification.SignStageList.Where(x => x.STAGE_ORDER == stage.STAGE_ORDER && x.SEQ < stage.SEQ).OrderByDescending(x => x.SEQ).FirstOrDefault();
                        if (prevStage != null)
                        {
                            lastStage = prevStage;
                            var lastLogs = Entities.SIGN_LOG.Where(x => x.SIGN_STAGE_NUMBER == lastStage.SIGN_STAGE_NUMBER && x.SIGN_STAGE.SEQ >= prevStage.SEQ).ToList();

                            foreach (var item in lastLogs)
                            {
                                item.MARK = true;
                            }
                        }
                        else
                        {
                            var lastLogs = Entities.SIGN_LOG.Where(x => x.SIGN_STAGE_NUMBER == lastStage.SIGN_STAGE_NUMBER).ToList();

                            foreach (var item in lastLogs)
                            {
                                item.MARK = true;
                            }
                        }

                    }
                    else
                    {
                        var lastLogs = Entities.SIGN_LOG.Where(x => x.SIGN_STAGE_NUMBER == lastStage.SIGN_STAGE_NUMBER).ToList();

                        foreach (var item in lastLogs)
                        {
                            item.MARK = true;
                        }
                    }

                    SIGN.FORM_STATUS = lastStage.FLOW_ROLES.FLOW_STATUS;
                    SIGN.NOW_STAGE_ORDER = lastStage.STAGE_ORDER;//回上一關


                    decimal AutoIncrementLog = Entities.SIGN_LOG.Max(s => (decimal?)s.SIGN_LOG_NUMBER).GetValueOrDefault() + 1;
                    List<SIGN_LOG> logs = new List<SIGN_LOG>();
                    SIGN_LOG log = new SIGN_LOG();
                    var emp = hrMaster.GetEmployee(empno);
                    log.SIGNATORY_EMP_NO = empno;
                    log.SIGNATORY_DEPT_NO = emp.deptNo;
                    log.SIGNATORY_NAME = emp.empName;
                    log.SIGN_DATE = DateTime.Now;
                    log.SIGN_STATUS = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Return);
                    log.SIGN_STAGE_NUMBER = stage.SIGN_STAGE_NUMBER;
                    log.SIGN_LOG_NUMBER = AutoIncrementLog;
                    log.SIGN_SUGGESTION = Data.Comment;
                    log.SIGN_STATUS_TRANSLATE = Data.SignButtonKey;
                    log.MARK = true;
                    SIGN.FINAL_SIGN_DATE = DateTime.Now;
                    Entities.SIGN_LOG.Add(log);
                    Entities.SaveChanges();
                    #region BPM
                    var bpmResponse = bpm.RejectToPrevious(new ActionQuery
                    {
                        IdNo = SIGN.GUID_FOR_BPM,
                        Comment = Data.Comment,
                        ActionName = Data.SignButtonKey,
                        ExecutorEmpNo = stage.SIGNATORY_EMP_NO,
                        ApproverId = stage.SIGNATORY_EMP_NO,
                        ApproverDept = stage.SIGNATORY_DEPT_NO,
                        ApproverName = stage.SIGNATORY_NAME
                    });
                    if (bpmResponse.rtnCode != "0")
                    {
                        tran.Rollback();
                        return response;
                    }

                    if (bpmResponse.changeApprover.Count > 0)
                    {//BPM回傳更換簽核人
                        foreach (var item in bpmResponse.changeApprover)
                        {
                            var changeEmp = hrMaster.GetEmployee(item.NewApprover);
                            var stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
                            stages.Where(x => x.FLOW_ROLES.FLOW_STATUS == bpmResponse.ProcessId && x.SIGNATORY_EMP_NO == item.OriginalApprover).Select(c =>
                            {
                                c.SIGNATORY_EMP_NO = item.NewApprover;
                                c.SIGNATORY_NAME = changeEmp.empName;
                                c.SIGNATORY_DEPT_NO = changeEmp.deptNo;
                                return c;
                            }).FirstOrDefault();

                        }
                    }
                    #endregion
                    Entities.SaveChanges();
                    tran.Commit();

                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.Message = ex.ToString();
                    _logger.Error(ex.ToString());
                    return response;
                }
            }
            response.StatusCode = (int)EnumStatusCode.Success;
            return response;

        }

        public List<FlowDiagram> GetFlowDiagram(SignData<T> Data)
        {
            List<FlowDiagram> flowDiagrams = new List<FlowDiagram>();
            FlowDiagram flowDiagram = new FlowDiagram();
            var flowsettingmain = Entities.FLOW_SETTING_MAIN.Where(x => Data.Sign.FlowID == x.FLOW_ID);
            var flowstagesetting = Entities.FLOW_STAGE_SETTING;
            foreach (var row in flowsettingmain)
            {
                var settingdata = flowstagesetting.FirstOrDefault(x => row.FLOW_ID == x.FLOW_ID);
                flowDiagram.FlowStageSetting = _mapper.Map<FLOW_STAGE_SETTING, FlowStageSetting>(settingdata);
                flowDiagram.FlowSettingMain = _mapper.Map<FLOW_SETTING_MAIN, FlowSettingMain>(row);
                flowDiagrams.Add(flowDiagram);
            }
            return flowDiagrams;
        }
        /// <summary>
        /// 簽核驗證
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="empno"></param>
        /// <returns></returns>
        public SignVerificationResponse SignVerification(SignData<T> Data, string empno)
        {
            var response = new SignVerificationResponse();
            response.StatusCode = (int)EnumStatusCode.Fail;

            //找該張單子的主檔
            SIGN_FORM_MAIN SIGN = Entities.SIGN_FORM_MAIN.FirstOrDefault(x => x.SIGN_FORM_ID == Data.Sign.SignFromID);
            if (SIGN == null)
            {
                return response;
            }
            //確認該關卡是由他簽核
            var stageList = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID == SIGN.SIGN_FORM_ID).ToList();
            var stage = stageList.Where(x => x.SIGNATORY_EMP_NO == empno && x.STAGE_ORDER == SIGN.NOW_STAGE_ORDER).FirstOrDefault();
            if (stage == null)
            {
                return response;
            }
            //確認該流程尚未結案
            if (SIGN.NOW_STAGE_ORDER >= (decimal)EnumFlowRoles.Deploy)
            {
                return response;
            }

            response.Sign = SIGN;
            response.SignStage = stage;
            response.SignStageList = stageList;

            response.StatusCode = (int)EnumStatusCode.Success;

            return response;
        }

        public class SignVerificationResponse
        {
            public SIGN_STAGE SignStage { get; set; }
            public SIGN_FORM_MAIN Sign { get; set; }
            public List<SIGN_STAGE> SignStageList { get; set; }
            public int StatusCode { get; set; }
        }

        /// <summary>
        /// 取得當前關卡 0 = 草稿、999 = 簽核完成、(Other) = 關卡序號
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        public int GetCurrent(string No)
        {
            List<SIGN_STAGE> Stages = Entities.SIGN_STAGE.Where(x => x.SIGN_FORM_ID.ToString() == No).ToList();
            if (!Stages.Any())
                return 0;
            List<SIGN_LOG> Logs = Entities.SIGN_LOG.Where(x => Stages.Any(s => s.SIGN_STAGE_NUMBER.ToString().Contains(x.SIGN_STAGE_NUMBER.ToString()))).OrderByDescending(x => x.SIGN_LOG_NUMBER).ToList();
            foreach (SIGN_LOG Log in Logs)
            {
                switch (Log.SIGN_STATUS)
                {
                    case "Reject":
                        return -1;
                    default://送件 Approve
                        int temp = Stages.FindIndex(x => x.SIGN_STAGE_NUMBER == Log.SIGN_STAGE_NUMBER);
                        if (temp > -1)
                            Stages.RemoveAt(temp);
                        break;
                }
            }
            if (!Stages.Any())
                return 999;
            else
                return (int)Stages.Max(x => x.SIGN_STAGE_NUMBER);
        }
    }
}
