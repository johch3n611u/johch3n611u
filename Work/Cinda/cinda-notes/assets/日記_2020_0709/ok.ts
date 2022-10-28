import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import AccountFunctionDisabled from 'src/app/models/AccountFunctionDisabled';
import DisableDetail from 'src/app/models/DisableDetail';
import { DisableListStatus } from 'src/app/enum/DisableListStatus';
import DisableDetailData from 'src/app/models/DisableDetailData';
import { PamAccounFunctionDisabledService } from 'src/app/services/pam-account-function-disabled.service';
import { DoCheck } from '@angular/core';
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

    constructor(
        private route: ActivatedRoute,
        private PamAFDService: PamAccounFunctionDisabledService
    ) { }

    ngOnInit() {
        // 顯示資料庫資料
        let urlEmpNo, EF;
        this.route.params.subscribe(params => {
            // console.log(params.EmpNo);
            urlEmpNo = params.EmpNo;
        });
        this.PamAFDService.getDetail(urlEmpNo).subscribe(
            ref => (
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
                    fakedata.ServiceItem = item.ServiceName;
                    if (item.Disabled === '0') {
                        item.Disabled = false;
                    } else {
                        item.Disabled = true;
                    }
                    // console.log(item.Disabled);
                    fakedata.Disabled = item.Disabled;
                    fakedata.PreCloseDate = new Date(EF.PrecloseDate);
                    // 權限預計停用日期 item.PrecloseDate; // 時間格式必須正確 new Date() 先替用
                    fakedata.Status = item.Status;
                    this.items.Data.push(fakedata);
                    // 保持帳號與權限皆不提前停用 註: AD / Note / Novell帳號停用及設備繳回日期
                    this.fixPreDisabledDate = new Date(EF.DisabledDate);
                    this.dynamicPreDisabledDate = new Date(EF.DisabledDate);
                })
            )
            // AFdisabledId: 0
            // CloseDate: "2020-07-07T03:44:19"
            // Disabled: "0"
            // DisabledDate: null
            // FormType: "0"
            // Id: 0
            // Name: "李先生(部3)"
            // PrecloseDate: "2020-07-07T03:44:19"
            // ServiceName: "權限13"
            // SignFormId: 0
            // Status: "0"
        );
    }

    ngDoCheck(): void {
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

    Save(e) {
        // console.log('Save');
        // console.log(e);

        this.PamAFDService.Create(e, this.items).subscribe(ref => {
            console.log('ref');
            console.log(ref);
        });
        //     if (this.signData.Sign.ServiceCode.includes('AA01')) {
        //         this.formData.FunctionType = this.typeEnum.ComputerAccount;
        //     }
        //     this.signData.FormData = this.formData;
        //     this.signData.Sign.FormStatus = 'Draft';
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
        //     });
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
}
