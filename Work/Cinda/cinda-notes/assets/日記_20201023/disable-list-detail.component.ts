import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, ParamMap } from "@angular/router";
import AccountFunctionDisabled from 'src/app/models/AccountFunctionDisabled';
import DisableDetail from 'src/app/models/DisableDetail';
import { DisableListStatus } from 'src/app/enum/DisableListStatus';
import DisableDetailData from 'src/app/models/DisableDetailData';
import { PamAccounFunctionDisabledService } from 'src/app/services/pam-account-function-disabled.service';
import { DoCheck } from '@angular/core';
import SignData from 'src/app/models/SignData';
import BackendDisableDetail from '../../../models/BackendDisableDetail';
import SignFormMain from 'src/app/models/SignFormMain';
import { BaseHelper } from '../../../_helpers/base-helper';
import { PamDeviceReturnService } from '../../../services/pam-device-return.service';
import { ResignationDisabledService } from 'src/app/services/resignation-disabled.service';
import { FormType } from 'src/app/enum/FormType';
import PermissionDisableDetail from 'src/app/models/PermissionDisableDetail';
import { SelectItem } from 'primeng/api';
import { DatePipe } from '@angular/common'

@Component({
    selector: 'app-disable-list-detail',
    templateUrl: './disable-list-detail.component.html',
    styleUrls: ['./disable-list-detail.component.css']
})
export class DisableListDetailComponent implements OnInit, DoCheck {

    forceStatus = 'ThreeOfOne';
    e
    displayDialog;
    SIGN_SUGGESTION; // for stageLoge

    DetailCloseDate: SelectItem[];
    NewDetailCloseDate: SelectItem;

    fixPreDisabledDate: Date;
    dynamicPreDisabledDate: Date; // 用來放置 DisabledDate 選擇資料
    items = {} as DisableDetail;
    StatusEnum = DisableListStatus;
    typeenum = FormType;
    // signData 流程
    signData = new SignData();
    BackSignId; // SignFormId
    BackAFId; // 子表自己的 ID

    CheckDetail;// 如果此值為負則無子單

    // header 資料
    ApplicationCategory = 'ResignationDisabled';

    // 供 DeviceReturn 啟單之 ID
    DeviceReturnId;

    constructor(
        private route: ActivatedRoute,
        private PamAFDService: PamAccounFunctionDisabledService,
        private baseHelper: BaseHelper,
        private router: Router,
        private PamDRService: PamDeviceReturnService,
        private RDService: ResignationDisabledService,
        public datepipe: DatePipe
    ) { }

    ngOnInit() {
        const RoleArr = localStorage.getItem('pam_RoleNames').split(',');
        RoleArr.forEach(role => {
            console.log('★★★ role ★★★', role);
            if (role === 'PAM_Adm') {
                this.forceStatus = 'ThreeOfOneAdmin';
            }
        });

        this.getDetail();
    }

    ngDoCheck(): void {
        this.chkCalendarData();
    }

    Save(e) {
        this.PamAFDService.Update(this.setBackendData('儲存')).subscribe(res => {
            if (res.StatusCode === ErrorCode.Success) {
                this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                this.router.navigate(['/pages/DisabledList']);
            } else {
                this.baseHelper.ShowErrorMsg(this.baseHelper.GetTranslateValue('Form.Fail'));
            }
        }
        )
    }

    Send(e) {
        this.displayDialog = true;
        this.e = e;
    }

    edit() { }

    getDetail() {
        // 顯示資料庫資料
        let SignFormId;
        let EF;
        this.route.params.subscribe(params => {
            SignFormId = params.Id;
        });
        this.PamAFDService.getDetail(SignFormId).subscribe(
            ref => (

                EF = ref.Entries[0],
                this.CheckDetail = EF.Id,
                console.log('★★★ this.CheckDetail ★★★', this.CheckDetail),
                this.items.FormStatus = EF.FormStatus,
                this.items.ApplicanterEmpNO = EF.ApplicanterEmpNO,
                this.items.User = EF.Name,
                this.items.Type = EF.FormType,
                this.items.CloseDate = new Date(EF.CloseDate),
                this.GetCloseDateSelect(),
                // 帳號權限預計關閉日 EF.CloseDate 暫時替代需處理資料庫時間資料格式
                this.items.DisabledDate = new Date(EF.DisabledDate), // 停用及設備繳回日期 EF.DisabledDate
                this.items.Status = EF.FormStatus,// 帳號停用及設備繳回狀態
                this.items.Data = [] as DisableDetailData[],
                ref.Entries.forEach(item => {
                    let fakedata = {} as DisableDetailData;
                    fakedata.Id = item.Id;
                    fakedata.FunctionType = item.FunctionType;
                    if (item.Disabled === '0') {
                        item.Disabled = false;
                    } else {
                        item.Disabled = true;
                    }



                    // console.log(item.Disabled);
                    fakedata.Disabled = item.Disabled;
                    // console.log('item.PrecloseDateitem.PrecloseDate', item.PrecloseDate);
                    // fakedata.PreCloseDate = new Date(item.PrecloseDate);
                    fakedata.PreCloseDate = new Date(this.datepipe.transform(item.PrecloseDate, 'yyyy/MM/dd'));
                    console.log('★★★  fakedata.PreCloseDate ★★★', fakedata.PreCloseDate);
                    // console.log(item.PrecloseDate);
                    // 權限預計停用日期 item.PrecloseDate; // 時間格式必須正確 new Date() 先替用
                    fakedata.Status = item.Status;
                    this.items.Data.push(fakedata);
                    // 保持帳號與權限皆不提前停用 註: AD / Note / Novell帳號停用及設備繳回日期
                    this.fixPreDisabledDate = new Date(EF.DisabledDate);
                    this.dynamicPreDisabledDate = new Date(EF.DisabledDate);

                    // 供後端 SignForm 使用之參數
                    this.BackSignId = EF.SignFormId;
                    console.log('★★★ BackSignId ★★★', this.BackSignId);
                    this.BackAFId = EF.Id;

                    // 供子單判斷啟單 ID DeviceReturnId
                    this.DeviceReturnId = EF.DeviceReturnId;

                }
                    // 空子單報錯 轉址不給編輯
                    //this.chkIdNull(EF.Id),
                    //console.log('★★★ this.BackAFId ★★★', this.BackAFId)

                )
            )
        );


    }

    chkCalendarData() {
        // 當狀態選項 2 更改日期後，又改回其餘選項，停用及設備繳回日期不會依照狀態選項而變動值錯誤
        if (this.items.Type == 1 || this.items.Type == 3) {
            this.dynamicPreDisabledDate = this.items.DisabledDate;
            // 放置置換選項時，選項 2 內的日期
            this.items.DisabledDate = this.fixPreDisabledDate;
        } else if (this.items.Type == 2 && (this.dynamicPreDisabledDate != undefined)) {
            this.items.DisabledDate = this.dynamicPreDisabledDate;
            // 選項 2 內的日期再塞回返傳值
        }
    }

    setBackendData(ThisFormType) {
        let form = this.items;
        console.log('★★★ this.items★★★', this.items);

        // 因為 interface 實作限制類別內容，要把 form 從前端類別改回後端類別
        let Bitems = [];
        // items.Entries = [] as BackendDisableDetail[];
        const index: number = form.Data.length;
        for (let i = 0; i < index; i++) {
            let Bitem = new BackendDisableDetail();
            Bitem.ThisFormType = ThisFormType;
            Bitem.Id = form.Data[i].Id;
            Bitem.CloseDate = form.CloseDate;
            Bitem.DisabledDate = form.DisabledDate;
            Bitem.FormType = form.Type;
            Bitem.Name = form.User;
            Bitem.ServiceName = form.Data[i].FunctionType;
            // 存值為 SSL-VPN、Citrix、電腦帳號 需在後端做一次判斷轉為 ID
            Bitem.Disabled = form.Data[i].Disabled;
            Bitem.PrecloseDate = form.Data[i].PreCloseDate;
            console.log('★★★ form.Data[i].PreCloseDate  ★★★', Bitem.PrecloseDate);
            Bitem.Status = form.Data[i].Status;
            Bitem.SignFormId = this.BackSignId;
            // 供子單判斷啟單
            Bitem.DeviceReturnId = this.DeviceReturnId;

            // for stageloge
            Bitem.pam_DeptNo = localStorage.getItem('pam_DeptNo');
            Bitem.pam_Account = localStorage.getItem('pam_Account');
            Bitem.pam_UserID = localStorage.getItem('pam_UserID');
            Bitem.SIGN_SUGGESTION = this.SIGN_SUGGESTION;
            Bitems.push(Bitem);
        }
        return Bitems;
    }

    setSignData(e, Bitems) {
        console.log('★★★ e ★★★', e);
        let signData: SignData = new SignData();
        signData.FormType = e.FormType;
        signData.FormData = Bitems; // 是個流程主表的模組，依照初始化宣告他的類型

        signData.Sign = new SignFormMain(); // 這邊是簽核用的
        signData.Sign.RequiredDate = new Date(
            new Date().getUTCFullYear(),
            new Date().getUTCMonth(),
            new Date().getUTCDate()
        );
        switch ((Number)(this.items.Type)) {
            case (Number)(this.typeenum.DeviceReturn):
                signData.Sign.FormStatus = 'Draft'; // 表單狀態
                signData.Sign.FormType = 'DeviceReturn'; // e.FormType; // 表單類型
                break;

            case (Number)(this.typeenum.PermissionDisable):
                signData.Sign.FormStatus = 'PermissionDisable'; // 表單狀態
                signData.Sign.FormType = 'PAMPD'; // e.FormType; // 表單類型
                signData.Sign.ServiceCode = 'PD001';
                break;
        }
        signData.Sign.SignFromID = this.BackSignId;
        signData.Sign.ApplicanterEmpNO = localStorage.getItem('pam_Account');

        signData.Sign.ApplicanterName = localStorage.getItem('pam_UserName');

        signData.Sign.FillerEmpNO = localStorage.getItem('pam_Account');

        // signData.Comment =; // 簽核意見長度不可超過500字元
        // signData.ExecutorsID =; // 目前簽核人ID
        // signData.ExecutorsName =; // 目前簽核人
        // signData.ExecutorsDept =; //目前簽核人部門
        // signData.ShiftsID =; // 變更簽核人ID
        // signData.ShiftsName =; // 變更簽核人
        // signData.ShiftsDept =; // 變更簽核人部門
        signData.SignButtonKey = e.SignButtonKey; // 放前端按鈕的名稱
        signData.Sign.ApplicanterEmpNO = this.items.ApplicanterEmpNO;
        return signData;
    }

    chkIdNull(Key: any) {
        if (Key === null) {
            this.baseHelper.ShowErrorMsgByStatusCode(103);
            this.router.navigate(['/pages/DisabledList']);
        }
    }

    GetCloseDateSelect() {
        this.PamAFDService.GetCloseDateSelect(this.items.CloseDate).subscribe(res => {
            this.DetailCloseDate = res.Entries;
        });
    }

    SetCloseDateSelect(PreCloseDate) {
        this.NewDetailCloseDate.value = PreCloseDate;
        this.NewDetailCloseDate.label = PreCloseDate;
        PreCloseDate = this.NewDetailCloseDate.label;
    }

    confirm_OK(e) {
        if (this.items.Type != null && this.items.Type != 0) {

            // 20201023 PAM 402 改單一接口 trigger

            this.PamAFDService.Update(this.setBackendData('送件')).subscribe(res => { // AccountFunctionDisabledService
                // console.log('★★★ res ★★★', res);
                // if (res.Entries[0] === 'true') {
                //     this.baseHelper.ShowSuccessMsg(
                //         this.baseHelper.GetTranslateValue('Form.Success')
                //     );
                //     // 儲存成功後啟單，三種按鈕兩種子單都必須起
                //     // p.45 A B C

                //     // 啟 帳號關閉及設備繳回單
                //     const emptyDisableDetail = new BackendDisableDetail();
                //     const data = new PermissionDisableDetail();
                //     data.Id = this.BackSignId;
                //     emptyDisableDetail.SignFormId = this.BackSignId;
                //     emptyDisableDetail.DeviceReturnId = this.DeviceReturnId;
                //     let list = [] as any[];
                //     list.push(emptyDisableDetail);
                //     this.signData = this.setSignData(e, list);

                //     this.PamDRService.Create(this.signData).subscribe(res2 => { // DeviceReturnService

                //         if (res2.Entries[0] === 'true') { // 避免異步 SignFormMain 啟單
                //             this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                //             // 啟 權限停用單
                //             list = [] as any[];
                //             this.items.Data.forEach(x => {
                //                 list.push(data);
                //             });
                //             this.signData = this.setSignData(e, list);
                //             this.items.Data.forEach(x => x.CloseDate = this.fixPreDisabledDate);
                //             this.signData.FormData = this.items.Data;

                //             this.RDService.Create(this.signData).subscribe(res3 => { // ResignationDisableService

                //                 this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                //                 // p.45.A.3 確定前兩單有啟單後更改三選一單狀態為[已派工]
                //                 // 因為前兩單已做起單處理如走到這關直接改狀態即可
                if (res.StatusCode === ErrorCode.Success) {
                    this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                    this.router.navigate(['/pages/DisabledList']);
                } else {
                    this.baseHelper.ShowErrorMsg(this.baseHelper.GetTranslateValue('Form.Fail'));
                }

                //                 });
                //             }
                //         });
                //     } else {
                //         this.baseHelper.ShowErrorMsgByStatusCode(114);
                //     }
                //     this.getDetail();
            });
        } else {
            this.baseHelper.ShowErrorMsg(this.baseHelper.GetTranslateValue('ResignationDisabled.PleaseSelect'));
        }
    }

    ColseDialog() {
        this.displayDialog = false;
    }






}


