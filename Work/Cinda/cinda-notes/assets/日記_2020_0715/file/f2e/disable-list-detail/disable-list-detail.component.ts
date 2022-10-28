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


@Component({
    selector: 'app-disable-list-detail',
    templateUrl: './disable-list-detail.component.html',
    styleUrls: ['./disable-list-detail.component.css']
})
export class DisableListDetailComponent implements OnInit, DoCheck {

    fixPreDisabledDate: Date;
    dynamicPreDisabledDate: Date; // 用來放置 DisabledDate 選擇資料
    items = {} as DisableDetail;
    StatusEnum = DisableListStatus;

    // signData 流程
    signData = new SignData();
    BackSignId; // SignFormId
    BackAFId; // 子表自己的 ID

    // header 資料
    ApplicationCategory = 'ResignationDisabled';

    constructor(
        private route: ActivatedRoute,
        private PamAFDService: PamAccounFunctionDisabledService,
        private baseHelper: BaseHelper,
        private router: Router
    ) { }

    ngOnInit() {
        this.getDetail();
    }

    ngDoCheck(): void {
        this.chkCalendarData();
    }

    Save(e) {
        // console.log('event/header');
        // console.log(e);
        this.signData = this.setSignData(e, this.setBackendData());
        // console.log('this.signData');
        // console.log(this.signData);
        this.PamAFDService.Update(this.setBackendData()).subscribe(res => {
            // console.log('response');
            // console.log(res);

            if (res == true) {
                this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                // this.router.navigateByUrl('/pages/ApplyFormQuery', { skipLocationChange: true }).then(() =>
                //     this.router.navigate(['/pages/ApplyFormQuery', this.signFromID]));
            } else {
                this.baseHelper.ShowErrorMsgByStatusCode(res.StatusCode);
            }
        });
    }

    send(e) {
        console.log('send');
        console.log(e);
        //     if (this.signData.Sign.ServiceCode.includes('AA01')) {
        //         this.formData.FunctionType = this.typeEnum.ComputerAccount;
        //     }
        //     this.signData.FormData = this.formData;
        //     this.signData.Sign.FormStatus = 'SignOff';
        //     this.signData.Sign.SignFromID = this.signFromID;
        //     this.signData.SignButtonKey = e.SignButtonKey;

        //     this.accountApplyFormService.Create(this.signData).subscribe(res => {
        //         if (res.StatusCode === this.errorCode.Success) {
        //             this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
        //             this.router.navigateByUrl('/pages/ApplyFormQuery', { skipLocationChange: true }).then(() =>
        //                 this.router.navigate(['/pages/ApplyFormQuery', this.signFromID]));
        //         } else {
        //             this.baseHelper.ShowErrorMsgByStatusCode(res.StatusCode);
        //         }
        // });
    }

    edit() { }

    getDetail() {
        // 顯示資料庫資料
        let SignFormId;
        let EF;
        this.route.params.subscribe(params => {
            // console.log(params.EmpNo);
            SignFormId = params.EmpNo;
        });
        this.PamAFDService.getDetail(SignFormId).subscribe(
            ref => (
                // console.log('★★★ ref ★★★'),
                // console.log(ref),
                EF = ref.Entries[0],
                this.items.User = EF.Name,
                this.items.Type = EF.FormType,
                this.items.CloseDate = new Date(EF.CloseDate),
                // 帳號權限預計關閉日 EF.CloseDate 暫時替代需處理資料庫時間資料格式
                this.items.DisabledDate = new Date(EF.DisabledDate), // 停用及設備繳回日期 EF.DisabledDate
                this.items.Data = [] as DisableDetailData[],
                ref.Entries.forEach(item => {
                    let fakedata = {} as DisableDetailData;
                    fakedata.Id = item.Id;
                    fakedata.ServiceItem = item.ServiceName;
                    if (item.Disabled === '0') {
                        item.Disabled = false;
                    } else {
                        item.Disabled = true;
                    }
                    // console.log(item.Disabled);
                    fakedata.Disabled = item.Disabled;
                    fakedata.PreCloseDate = new Date(item.PrecloseDate);
                    // console.log('★★★ item.PrecloseDate ★★★');
                    // console.log(item.PrecloseDate);
                    // 權限預計停用日期 item.PrecloseDate; // 時間格式必須正確 new Date() 先替用
                    fakedata.Status = item.Status;
                    this.items.Data.push(fakedata);
                    // 保持帳號與權限皆不提前停用 註: AD / Note / Novell帳號停用及設備繳回日期
                    this.fixPreDisabledDate = new Date(EF.DisabledDate);
                    this.dynamicPreDisabledDate = new Date(EF.DisabledDate);

                    // 供後端 SignForm 使用之參數
                    this.BackSignId = EF.SignFormId;
                    this.BackAFId = EF.Id;
                },
                    // 空子單報錯 轉址不給編輯
                    this.chkIdNull(EF.Id),
                )
            )
        );
    }

    chkCalendarData() {
        // 當狀態選項 2 更改日期後，又改回其餘選項，停用及設備繳回日期不會依照狀態選項而變動值錯誤
        // console.log('this.items.Type');
        // console.log(this.items.Type);
        // console.log('this.fixPreDisabledDate');
        // console.log(this.fixPreDisabledDate);
        if (this.items.Type == 1 || this.items.Type == 3) {
            this.dynamicPreDisabledDate = this.items.DisabledDate;
            // 放置置換選項時，選項 2 內的日期
            this.items.DisabledDate = this.fixPreDisabledDate;
        } else if (this.items.Type == 2 && (this.dynamicPreDisabledDate != undefined)) {
            this.items.DisabledDate = this.dynamicPreDisabledDate;
            // 選項 2 內的日期再塞回返傳值
        }
        // console.log('this.dynamicPreDisabledDate');
        // console.log(this.dynamicPreDisabledDate);
        // console.log(this.items);
        // console.log(this.items.DisabledDate);
    }

    setBackendData() {
        let form = this.items;
        // console.log('form');
        // console.log(form);
        // 因為 interface 實作限制類別內容，要把 form 從前端類別改回後端類別
        let Bitems = [];
        // items.Entries = [] as BackendDisableDetail[];
        const index: number = form.Data.length;
        // console.log(index);
        for (let i = 0; i < index; i++) {
            let Bitem = new BackendDisableDetail();
            Bitem.Id = form.Data[i].Id;
            Bitem.CloseDate = form.CloseDate;
            Bitem.DisabledDate = form.DisabledDate;
            Bitem.FormType = form.Type;
            Bitem.Name = form.User;
            Bitem.ServiceName = form.Data[i].ServiceItem;
            // 存值為 SSL-VPN、Citrix、電腦帳號 需在後端做一次判斷轉為 ID
            Bitem.Disabled = form.Data[i].Disabled;
            Bitem.PrecloseDate = form.Data[i].PreCloseDate;
            Bitem.Status = form.Data[i].Status;
            Bitem.SignFormId = this.BackSignId;
            Bitems.push(Bitem);
        }

        // let FakeSignDate: SignData = new SignData();
        // FakeSignDate.FormData = Bitems;

        // console.log('Bitems');
        // console.log(Bitems);

        return Bitems;
    }

    setSignData(e, Bitems) {
        // console.log('Bitems');
        // console.log(Bitems);

        let signData: SignData = new SignData();
        signData.FormType = e.FormType;
        signData.FormData = Bitems; // 是個流程主表的模組，依照初始化宣告他的類型

        signData.Sign = new SignFormMain(); // 這邊是簽核用的
        signData.Sign.RequiredDate = new Date(
            new Date().getUTCFullYear(),
            new Date().getUTCMonth(),
            new Date().getUTCDate()
        );
        signData.Sign.FormStatus = 'Draft'; // 表單狀態
        signData.Sign.SignFromID = this.BackSignId;
        signData.Sign.FormType = 'SAMAuth'; // e.FormType; // 表單類型
        signData.Sign.ApplicanterEmpNO = localStorage.getItem('sam_Account');
        signData.Sign.ApplicanterName = localStorage.getItem('sam_UserName');
        signData.Sign.FillerEmpNO = localStorage.getItem('sam_Account');
        signData.Sign.ServiceCode = 'AH0001';
        // signData.Comment =; // 簽核意見長度不可超過500字元
        // signData.ExecutorsID =; // 目前簽核人ID
        // signData.ExecutorsName =; // 目前簽核人
        // signData.ExecutorsDept =; //目前簽核人部門
        // signData.ShiftsID =; // 變更簽核人ID
        // signData.ShiftsName =; // 變更簽核人
        // signData.ShiftsDept =; // 變更簽核人部門
        signData.SignButtonKey = e.SignButtonKey; // 放前端按鈕的名稱

        // console.log('signData');
        // console.log(signData);
        return signData;
    }

    chkIdNull(Key: any) {
        if (Key === null) {
            this.baseHelper.ShowErrorMsgByStatusCode(103);
            this.router.navigate(['/pages/DisabledList']);
        }
    }
}


