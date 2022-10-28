import { Component, OnInit } from '@angular/core';
import PermissionDisableDetail, { DetailData } from 'src/app/models/PermissionDisableDetail';
import { Data, ActivatedRoute } from '@angular/router';
import { UseType } from 'src/app/enum/UseType';
import { OpenWebFunction } from 'src/app/enum/OpenWebFunction';
import { ResignationDisabledService } from 'src/app/services/resignation-disabled.service';
import { HumanResourceService } from 'src/app/services/human-resource.service';
import SignData from 'src/app/models/SignData';
import { AccountFunctionTypeEnum } from 'src/app/enum/AccountFunctionType';
import { BaseHelper } from 'src/app/_helpers/base-helper';

@Component({
    selector: 'app-permission-disable-detail',
    templateUrl: './permission-disable-detail.component.html',
    styleUrls: ['./permission-disable-detail.component.css']
})
export class PermissionDisableDetailComponent implements OnInit {
    signFromID = 0;
    UsetypeEnum = UseType;
    WebFunctionEnum = OpenWebFunction;
    item = {} as PermissionDisableDetail;
    signData = new SignData();
    AccountFunctionType = AccountFunctionTypeEnum;
    now = new Date();
    close = new Date();
    constructor(
        private service: ResignationDisabledService,
        private route: ActivatedRoute,
        private hr: HumanResourceService,
        private baseHelper: BaseHelper
    ) { }
    fakeData = {} as DetailData;
    ngOnInit() {
        this.getData();
    }

    getData() {
        console.log(this.now, 'now');

        this.route.params.subscribe(params => {
            this.signFromID = params.Id;
            this.service.getPermissionDisable(params.Id).subscribe(res => {
                this.item = res;
                console.log(this.item.CloseDate,'this.item.CloseDate');

                this.close = new Date(this.item.CloseDate);
                console.log(this.close,'this.close');

                this.item.Data.forEach(x => {
                    switch ((Number)(x.Item)) {
                        case AccountFunctionTypeEnum.HighPermission:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.HighPermission');

                            break;

                        case AccountFunctionTypeEnum.ComputerAccount:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.ComputerAccount');
                            break;
                        case AccountFunctionTypeEnum.PushMail:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.PushMail');
                            break;
                        case AccountFunctionTypeEnum.SSL_VPN:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.SSL_VPN');
                            break;
                        case AccountFunctionTypeEnum.Citrix:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.Citrix');
                            break;
                        case AccountFunctionTypeEnum.OpenWebsite:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.OpenWebsite');
                            break;
                        case AccountFunctionTypeEnum.LoaclAdmin:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.LoaclAdmin');
                            break;
                        case AccountFunctionTypeEnum.NetWork_Printing:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.NetWork_Printing');
                            break;
                        case AccountFunctionTypeEnum.Mail_Out:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.Mail_Out');
                            break;
                        case AccountFunctionTypeEnum.NB_CarryOut:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.NB_CarryOut');
                            break;
                        case AccountFunctionTypeEnum.FTP_Account:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.FTP_Account');
                            break;
                        case AccountFunctionTypeEnum.ComputerOther:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.ComputerOther');
                            break;
                        case AccountFunctionTypeEnum.Device_WIFI:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.Device_WIFI');
                            break;
                        case AccountFunctionTypeEnum.People_WIFI:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.People_WIFI');
                            break;
                        case AccountFunctionTypeEnum.eFAX:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.eFAX');
                            break;
                        case AccountFunctionTypeEnum.MailOutDomain:
                            x.Item = this.baseHelper.GetTranslateValue('AccountType.MailOutDomain');
                            break;

                    }
                });
                this.hr.GetEmployee(this.item.Applicant).subscribe(v => {
                    this.item.Applicant = v.empName;
                });

            });
        });
    }
    Save(e) {
        this.signData.Sign.ServiceCode = 'D01';
        this.signData.Sign.ApplicanterEmpNO = localStorage.getItem('sam_Account')
        this.signData.FormData = this.item;
        this.signData.Sign.FormStatus = 'Draft';
        this.signData.Sign.SignFromID = this.signFromID;
        this.signData.SignButtonKey = e.SignButtonKey;
        console.log(this.signData, 'signData');

        this.service.Create(this.signData).subscribe(res => {
            // if (res.StatusCode === this.errorCode.Success) {
            //     this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
            //     this.router.navigateByUrl('/pages/ApplyFormQuery', { skipLocationChange: true }).then(() =>
            //         this.router.navigate(['/pages/ApplyFormQuery', this.signFromID]));
            // } else {
            //     this.baseHelper.ShowErrorMsgByStatusCode(res.StatusCode);
            // }
        });
    }
}

