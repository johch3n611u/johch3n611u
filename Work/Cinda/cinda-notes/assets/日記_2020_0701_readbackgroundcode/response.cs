/// <summary>
        /// UCP102、104 申請單簽核通知
        /// </summary>
        /// <returns></returns>
        [ExposeWebAPI(true)]
        [EnabledAnonymous(true)]
        public PageQueryResult<string> FlowNotification()
        {
            var response = new PageQueryResult<string>();
            try
            {
                using (var repository = new BatchRepository())
                {
                    var HR = new HumanResourceService();
                    var notificationTasks = new List<NOTIFICATION_TASK>();
                    var mailTo = new List<string>();
                    string StrMailTitle = "";
                    string StrMailContent = "";

                    var signFormMains = repository.GetFlow().Entries.ToList();
                    int IntFormType = 1;

                    foreach (var vItem in signFormMains)
                    {
                        var signLog = vItem.SIGN_STAGE.SelectMany(x => x.SIGN_LOG);
                        string StrApplyNo = "";
                        if (vItem.PORTAL_SERVICE_SWAPPLYFORM.FirstOrDefault() != null)
                        {
                            StrApplyNo = vItem.PORTAL_SERVICE_SWAPPLYFORM.FirstOrDefault().SIGN_FORM_ID.ToString();
                            IntFormType = 1;
                        }
                        else if (vItem.PORTAL_SERVICE_SWCHANGEFORM.FirstOrDefault() != null)
                        {
                            StrApplyNo = vItem.PORTAL_SERVICE_SWCHANGEFORM.FirstOrDefault().SIGN_FORM_ID.ToString();
                            IntFormType = 2;
                        }
                        else
                        {
                            continue;
                        }

                        var vVIP = repository.GetVIP().Entries.Where(x => x.EMPNO == vItem.APPLICANTER_EMP_NO).FirstOrDefault();
                        //所有關卡
                        foreach (var stageLog in signLog.Where(x => x.SIGN_STATUS != "ChangeApprover").ToList())
                        {
                            if (vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER == stageLog.SIGN_STAGE.STAGE_ORDER + 1 && x.ROLE_ID == (int)EnumFlowRoles.VipProcessor).ToList().Count > 0)
                            {//如果下一關是VIP結案則不通知
                                continue;
                            }
                            if (stageLog.SIGN_STAGE.ROLE_ID == (int)EnumFlowRoles.Close)
                            {//結案通知
                                var emp = HR.GetEmployee(vItem.FILLER_EMP_NO);
                                var vEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                                var IsApplicanter = signFormMains.Where(x => x.SIGN_FORM_NO == vItem.SIGN_FORM_NO && x.SIGN_STAGE.Where(y => y.ROLE_ID == (int)EnumFlowRoles.ApplicantAcceptance && y.STAGE_ORDER == x.NOW_STAGE_ORDER).FirstOrDefault() != null).FirstOrDefault() == null ? false : true;
                                if (IsApplicanter)//且流程於承辦站後為申請人確認站，則將主單狀態改為申請人確認並發送通知
                                {
                                    StrMailTitle = GetMailTitle(7);
                                    StrMailContent = GetFlowMailContent(7, IntFormType).Replace("@vFormApplyNo", StrApplyNo);
                                    var SendTo = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                                    notificationTasks.Add(new NOTIFICATION_TASK
                                    {
                                        TITLE = StrMailTitle.Replace("@emp", emp.empName).Replace("@vEmp", vEmp.empName),//主旨
                                        CONTENT = StrMailContent.Replace("@emp", emp.empName).Replace("@vEmp", vEmp.empName),//內容
                                        SEND_EMP_NO = SendTo.email,
                                        TYPE = "UCP107_結案通知",
                                        SEND_STATUS = 0,
                                        REMARK = "備註",
                                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                                        CC_EMP_NO = "",
                                        REF_ID = vItem.SIGN_FORM_NO
                                    });
                                }
                                else//發送通知
                                {
                                    StrMailTitle = GetMailTitle(6);
                                    StrMailContent = GetFlowMailContent(6, IntFormType).Replace("@vFormApplyNo", StrApplyNo);
                                    var SendTo = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                                    notificationTasks.Add(new NOTIFICATION_TASK
                                    {
                                        TITLE = StrMailTitle.Replace("@emp", emp.empName).Replace("@vEmp", vEmp.empName),//主旨
                                        CONTENT = StrMailContent.Replace("@emp", emp.empName).Replace("@vEmp", vEmp.empName),//內容
                                        SEND_EMP_NO = SendTo.email,
                                        TYPE = "UCP107_結案通知",
                                        SEND_STATUS = 0,
                                        REMARK = "備註",
                                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                                        CC_EMP_NO = "",
                                        REF_ID = vItem.SIGN_FORM_NO
                                    });
                                }
                            }
                            else if (stageLog.SIGN_STAGE.STAGE_ORDER == 1)
                            {
                                var nextStage = vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER > 1).OrderBy(x => x.STAGE_ORDER).FirstOrDefault();
                                if (nextStage.ROLE_ID == (int)Enum.EnumFlowRoles.OrganizerUnit || nextStage.ROLE_ID == (int)Enum.EnumFlowRoles.Organizer || nextStage.ROLE_ID == (int)Enum.EnumFlowRoles.VIPCaseOfficerCosign)
                                {//下一關是承辦人承辦 就發給承辦
                                    var vEmps = HR.GetEmployees(vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER == nextStage.STAGE_ORDER).Select(x => x.SIGNATORY_EMP_NO).ToList());
                                    var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);

                                    StrMailTitle = GetFlowMailTitle(2).Replace("@vEmp", vApplicaterEmp.empName);
                                    StrMailContent = GetFlowMailContent(2, IntFormType).Replace("@vEmp", vApplicaterEmp.empName).Replace("@vFormApplyNo", StrApplyNo);

                                    notificationTasks.Add(new NOTIFICATION_TASK
                                    {
                                        TITLE = StrMailTitle,//主旨
                                        CONTENT = StrMailContent,//內容
                                        SEND_EMP_NO = String.Join(";", vEmps.Select(x => x.email)),
                                        TYPE = "UCP102",
                                        SEND_STATUS = 0,
                                        REMARK = string.Empty,
                                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                                        CC_EMP_NO = string.Empty,
                                        REF_ID = vItem.SIGN_FORM_ID + "_" + stageLog.SIGN_LOG_NUMBER.ToString()
                                    });
                                }
                                else
                                {//填表人與申請人不同發信給申請人
                                    var vFillerEmp = HR.GetEmployee(vItem.FILLER_EMP_NO);
                                    var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);

                                    StrMailTitle = GetFlowMailTitle(1).Replace("@vEmp2", vApplicaterEmp.empName).Replace("@vEmp", vFillerEmp.empName);
                                    StrMailContent = GetFlowMailContent(1, IntFormType).Replace("@vEmp2", vApplicaterEmp.empName).Replace("@vEmp", vFillerEmp.empName).Replace("@vFormApplyNo", StrApplyNo);

                                    notificationTasks.Add(new NOTIFICATION_TASK
                                    {
                                        TITLE = StrMailTitle,//主旨
                                        CONTENT = StrMailContent,//內容
                                        SEND_EMP_NO = vApplicaterEmp.email,
                                        TYPE = "UCP102",
                                        SEND_STATUS = 0,
                                        REMARK = string.Empty,
                                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                                        CC_EMP_NO = string.Empty,
                                        REF_ID = vItem.SIGN_FORM_ID + "_" + stageLog.SIGN_LOG_NUMBER.ToString()//vStage[0].SIGN_LOG.FirstOrDefault().SIGN_LOG_NUMBER.ToString()
                                    });
                                }
                            }
                            else
                            {
                                string StrApproved = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Approved).ToString();
                                string StrChangeApprover = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.ChangeApprover).ToString();
                                if (stageLog.SIGN_STATUS != StrApproved && stageLog.SIGN_STATUS != StrChangeApprover)
                                {
                                    //退件
                                    string StrMailTo = "";
                                    string StrCCMail = string.Empty;
                                    var vFillerEmp = HR.GetEmployee(vItem.FILLER_EMP_NO);
                                    var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                                    //mailTo.Add(vFillerEmp.email);
                                    StrMailTo = vFillerEmp.email;
                                    StrCCMail = vApplicaterEmp.email;
                                    string StrLastEmpName = HR.GetEmployee(stageLog.SIGNATORY_EMP_NO).empName; //退件人
                                    StrMailTitle = GetFlowMailTitle(5).Replace("@vEmp", StrLastEmpName);
                                    StrMailContent = GetFlowMailContent(5, IntFormType).Replace("@vEmp", StrLastEmpName).Replace("@vFormApplyNo", StrApplyNo).Replace("@vComment", stageLog.SIGN_SUGGESTION);

                                    notificationTasks.Add(new NOTIFICATION_TASK
                                    {
                                        TITLE = StrMailTitle,//主旨
                                        CONTENT = StrMailContent,//內容
                                        SEND_EMP_NO = StrMailTo,
                                        TYPE = "UCP102",
                                        SEND_STATUS = 0,
                                        REMARK = string.Empty,
                                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                                        CC_EMP_NO = StrCCMail,
                                        REF_ID = vItem.SIGN_FORM_ID + "_" + stageLog.SIGN_LOG_NUMBER.ToString()
                                    });
                                }
                                else
                                {//核准

                                    var vEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                                    var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                                    //mailTo.Add(vEmp.email);
                                    string StrMailTo = "";
                                    string StrCCMail = "";
                                    var StageItems = vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER == stageLog.SIGN_STAGE.STAGE_ORDER + 1).ToList();
                                    var StageItem = vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER == stageLog.SIGN_STAGE.STAGE_ORDER + 1).FirstOrDefault();
                                    if (StageItem != null && (StageItem.ROLE_ID == (int)EnumFlowRoles.Organizer || StageItem.ROLE_ID == (int)EnumFlowRoles.OrganizerUnit || StageItem.ROLE_ID == (int)EnumFlowRoles.VIPCaseOfficerCosign))
                                    {//申請人主管核定後送至承辦人
                                        StrMailTitle = GetFlowMailTitle(4).Replace("@vEmp", vApplicaterEmp.empName);
                                        StrMailContent = GetFlowMailContent(4, IntFormType).Replace("@vEmp", vApplicaterEmp.empName).Replace("@vFormApplyNo", vItem.SIGN_FORM_RELATED.Select(x => x.REF_SIGN_FORM_ID).FirstOrDefault().ToString());
                                        StrMailTo = "";
                                        foreach (var sendEmp in StageItems)
                                        {//承辦人&BACK
                                            vEmp = HR.GetEmployee(sendEmp.SIGNATORY_EMP_NO);
                                            StrMailTo += vEmp.email + ";";
                                        }
                                        if (vVIP != null)
                                        {//VIP CC給中心級主管
                                            List<MxEmployee> ManagerList = HR.GetAllManagerByEmpNo(StageItem.SIGNATORY_EMP_NO, 0);
                                            var Manager = HR.GetEmployee(ManagerList.Where(x => x.orgLevelCode == "200").Select(x => x.empNo).FirstOrDefault());
                                            if (Manager == null)
                                            {
                                                _logger.Error($"中心級主管找不到 {ManagerList.Where(x => x.orgLevelCode == "200").Select(x => x.empNo).FirstOrDefault()}");
                                                continue;
                                            }
                                            StrCCMail = HR.GetEmployee(ManagerList.Where(x => x.orgLevelCode == "200").Select(x => x.empNo).FirstOrDefault()).email;
                                        }
                                    }
                                    else
                                    {//往下一關簽核

                                        var nextSigner = stageLog.SIGN_STAGE.MERGE == true ? vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER > stageLog.SIGN_STAGE.STAGE_ORDER).Select(x => x.SIGNATORY_EMP_NO).FirstOrDefault() : vItem.SIGN_STAGE.Where(x => x.SIGN_STAGE_NUMBER > stageLog.SIGN_STAGE_NUMBER).Select(x => x.SIGNATORY_EMP_NO).FirstOrDefault();
                                        if (nextSigner != null)
                                        {

                                            vEmp = HR.GetEmployee(nextSigner);
                                            StrMailTo = vEmp.email;
                                            StrMailTitle = GetFlowMailTitle(3).Replace("@vEmp", vApplicaterEmp.empName);
                                            StrMailContent = GetFlowMailContent(3, IntFormType).Replace("@vEmp", vApplicaterEmp.empName).Replace("@vFormApplyNo", StrApplyNo);
                                        }

                                    }
                                    if (StrMailTo != "")
                                    {
                                        notificationTasks.Add(new NOTIFICATION_TASK
                                        {
                                            TITLE = StrMailTitle,//主旨
                                            CONTENT = StrMailContent,//內容
                                            SEND_EMP_NO = StrMailTo,
                                            TYPE = "UCP102",
                                            SEND_STATUS = 0,
                                            REMARK = string.Empty,
                                            ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                                            CC_EMP_NO = StrCCMail,
                                            REF_ID = vItem.SIGN_FORM_ID + "_" + stageLog.SIGN_LOG_NUMBER.ToString()
                                        });
                                    }

                                }
                            }
                        }


                        //foreach (var vItem in signFormMains)
                        //{
                        //    mailTo = new List<string>();
                        //    decimal? dNowStage = vItem.NOW_STAGE_ORDER;
                        //    if (dNowStage != null)
                        //    {
                        //        //取得目前關卡
                        //        var vStage = vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER == dNowStage).ToList();

                        //        //判斷邏輯發信
                        //        if (vStage != null && vStage.Count > 0 && (vItem.PORTAL_SERVICE_SWAPPLYFORM.FirstOrDefault() != null || vItem.PORTAL_SERVICE_SWCHANGEFORM.FirstOrDefault() != null))
                        //        {
                        //            string StrApplyNo = "";
                        //            if (vItem.PORTAL_SERVICE_SWAPPLYFORM.FirstOrDefault() != null)
                        //            {
                        //                StrApplyNo = vItem.PORTAL_SERVICE_SWAPPLYFORM.FirstOrDefault().APPLY_NO.ToString();
                        //                IntFormType = 1;
                        //            }
                        //            else
                        //            {
                        //                StrApplyNo = vItem.PORTAL_SERVICE_SWCHANGEFORM.FirstOrDefault().APPLY_NO.ToString();
                        //                IntFormType = 2;
                        //            }


                        //            if (vCheckTask.Where(x => x.REF_ID.Contains(vStage[0].SIGN_STAGE_NUMBER.ToString())).Count() == 0
                        //                && vStage[0].STAGE_ORDER == 2)//簽核第一關
                        //            {
                        //                //填表人與申請人不同發信給申請人
                        //                if (vItem.FILLER_EMP_NO != vItem.APPLICANTER_EMP_NO)
                        //                {
                        //                    var vFillerEmp = HR.GetEmployee(vItem.FILLER_EMP_NO);
                        //                    var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                        //                    mailTo.Add(vApplicaterEmp.email);

                        //                    StrMailTitle = GetFlowMailTitle(1).Replace("@vEmp", vFillerEmp.empName).Replace("@vEmp2", vApplicaterEmp.empName);
                        //                    StrMailContent = GetFlowMailContent(1, IntFormType).Replace("@vEmp", vFillerEmp.empName).Replace("@vEmp2", vApplicaterEmp.empName).Replace("@vFormApplyNo", StrApplyNo);

                        //                    notificationTasks.Add(new NOTIFICATION_TASK
                        //                    {
                        //                        TITLE = StrMailTitle,//主旨
                        //                        CONTENT = StrMailContent,//內容
                        //                        SEND_EMP_NO = string.Join(";", mailTo),
                        //                        TYPE = "UCP102",
                        //                        SEND_STATUS = 0,
                        //                        REMARK = string.Empty,
                        //                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                        //                        CC_EMP_NO = string.Empty,
                        //                        REF_ID = vStage[0].SIGN_STAGE_NUMBER.ToString()//vStage[0].SIGN_LOG.FirstOrDefault().SIGN_LOG_NUMBER.ToString()
                        //                    });
                        //                }
                        //                else //填表人與申請人相同發信給承辦人
                        //                {
                        //                    var vEmp = HR.GetEmployee(vStage[0].SIGNATORY_EMP_NO);
                        //                    var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                        //                    mailTo.Add(vEmp.email);

                        //                    StrMailTitle = GetFlowMailTitle(2).Replace("@vEmp", vApplicaterEmp.empName);
                        //                    StrMailContent = GetFlowMailContent(2, IntFormType).Replace("@vEmp", vApplicaterEmp.empName).Replace("@vFormApplyNo", StrApplyNo);

                        //                    notificationTasks.Add(new NOTIFICATION_TASK
                        //                    {
                        //                        TITLE = StrMailTitle,//主旨
                        //                        CONTENT = StrMailContent,//內容
                        //                        SEND_EMP_NO = string.Join(";", mailTo),
                        //                        TYPE = "UCP102",
                        //                        SEND_STATUS = 0,
                        //                        REMARK = string.Empty,
                        //                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                        //                        CC_EMP_NO = string.Empty,
                        //                        REF_ID = vStage[0].SIGN_STAGE_NUMBER.ToString()
                        //                    });
                        //                }
                        //            }
                        //            else
                        //            {
                        //                foreach (var StageItem in vStage)
                        //                {

                        //                    string StrMailTo = "";
                        //                    string StrCCMail = string.Empty;
                        //                    string StrRefID = "";
                        //                    if (StageItem.SIGN_LOG.FirstOrDefault() != null && StageItem.SIGN_LOG.FirstOrDefault().MARK == true)
                        //                    {
                        //                        //退件
                        //                        var vFillerEmp = HR.GetEmployee(vItem.FILLER_EMP_NO);
                        //                        var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                        //                        //mailTo.Add(vFillerEmp.email);
                        //                        StrMailTo = vFillerEmp.email;
                        //                        StrCCMail = vApplicaterEmp.email;

                        //                        //往前抓一關
                        //                        string StrLastEmpName = "";
                        //                        try
                        //                        {
                        //                            string StrReturn = System.Enum.GetName(typeof(EnumSignStatus), EnumSignStatus.Return).ToString();
                        //                            var vLastStage = vItem.SIGN_STAGE.Where(x => x.STAGE_ORDER == dNowStage + 1 && x.SIGN_LOG.FirstOrDefault().SIGN_STATUS == StrReturn).FirstOrDefault();
                        //                            var vLastEmp = HR.GetEmployee(vLastStage.SIGNATORY_EMP_NO);
                        //                            if (vLastEmp != null)
                        //                            {
                        //                                StrLastEmpName = vLastEmp.empName;
                        //                            }
                        //                            StrRefID = vLastStage.SIGN_LOG.Where(x => x.SIGN_STATUS == StrReturn).FirstOrDefault().SIGN_LOG_NUMBER.ToString();
                        //                        }
                        //                        catch (Exception ex)
                        //                        {
                        //                            string StrError = ex.Message;
                        //                        }


                        //                        StrMailTitle = GetFlowMailTitle(5).Replace("@vEmp", StrLastEmpName);
                        //                        StrMailContent = GetFlowMailContent(5, IntFormType).Replace("@vEmp", StrLastEmpName).Replace("@vFormApplyNo", StrApplyNo);
                        //                    }
                        //                    else
                        //                    {
                        //                        if (vCheckTask.Where(x => x.REF_ID.Contains(StageItem.SIGN_STAGE_NUMBER.ToString())).Count() != 0)
                        //                        {
                        //                            continue;
                        //                        }
                        //                        var vEmp = HR.GetEmployee(StageItem.SIGNATORY_EMP_NO);
                        //                        var vApplicaterEmp = HR.GetEmployee(vItem.APPLICANTER_EMP_NO);
                        //                        //mailTo.Add(vEmp.email);
                        //                        StrMailTo = vEmp.email;

                        //                        if (StageItem.ROLE_ID == (int)EnumFlowRoles.OrganizerUnit)//申請人主管核定後送至承辦人
                        //                        {
                        //                            StrMailTitle = GetFlowMailTitle(4).Replace("@vEmp", vApplicaterEmp.empName);
                        //                            StrMailContent = GetFlowMailContent(4, IntFormType).Replace("@vEmp", vApplicaterEmp.empName).Replace("@vFormApplyNo", StrApplyNo);
                        //                        }
                        //                        else
                        //                        {
                        //                            StrMailTitle = GetFlowMailTitle(3).Replace("@vEmp", vApplicaterEmp.empName);
                        //                            StrMailContent = GetFlowMailContent(3, IntFormType).Replace("@vEmp", vApplicaterEmp.empName).Replace("@vFormApplyNo", StrApplyNo);
                        //                        }

                        //                        StrRefID = StageItem.SIGN_STAGE_NUMBER.ToString();
                        //                    }

                        //                    notificationTasks.Add(new NOTIFICATION_TASK
                        //                    {
                        //                        TITLE = StrMailTitle,//主旨
                        //                        CONTENT = StrMailContent,//內容
                        //                        SEND_EMP_NO = StrMailTo,//string.Join(";", mailTo),
                        //                        TYPE = "UCP102",
                        //                        SEND_STATUS = 0,
                        //                        REMARK = string.Empty,
                        //                        ESTIMATE_DATE = DateTime.Now.AddMinutes(1),
                        //                        CC_EMP_NO = StrCCMail,
                        //                        REF_ID = StrRefID//vStage[0].SIGN_LOG.FirstOrDefault().SIGN_LOG_NUMBER.ToString()
                        //                    });

                        //                }

                        //            }
                        //        }

                        //    }
                        //}

                        if (notificationTasks.Count > 0)
                        {
                            repository.InsertTask(notificationTasks);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = (long)EnumStatusCode.Exception;
                _logger.Error("FlowNotification:" + ex.ToString());
                response.Message = ex.Message;
            }
            return response;
        }