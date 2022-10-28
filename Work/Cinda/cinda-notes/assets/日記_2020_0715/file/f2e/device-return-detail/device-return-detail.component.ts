import { Component, OnInit, Output, DoCheck } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import DeviceReturnList from 'src/app/models/DeviceReturnList';
import { PamDeviceReturnService } from '../../../services/pam-device-return.service';
import DeviceChangeList from 'src/app/models/DeviceChangeList';
import { BaseHelper } from 'src/app/_helpers/base-helper';

@Component({
    selector: 'app-device-return-detail',
    templateUrl: './device-return-detail.component.html',
    styleUrls: ['./device-return-detail.component.css']
})
export class DeviceReturnDetailComponent implements OnInit, DoCheck {

    ApplicationCategory = 'DeviceReturn';
    @Output() signFormID: string;
    item = {} as DeviceReturnList;
    AuthorityToken; // 判斷權限
    TopBtn = false;
    DeviceChk = false;
    XfortChk = false;
    NBChk = false;

    constructor(
        private route: ActivatedRoute,
        private PamDRService: PamDeviceReturnService,
        private baseHelper: BaseHelper,
    ) { }

    ngOnInit() {
        this.AuthorityToken = prompt('設備處理人員 1 , XFort(電腦週邊權限) 2 , NB攜出承辦人 3 , PAM管理員 4');
        // alert(this.AuthorityToken);
        console.log('★★★ this.AuthorityToken ★★★', this.AuthorityToken);
        if (this.AuthorityToken != null && this.AuthorityToken !== '') {
            this.route.params.subscribe(params => {
                this.signFormID = params.Id;
            });
            this.getDetail();
        } else {
            this.baseHelper.ShowErrorMsgByStatusCode(107);
        }

    }

    ngDoCheck() {

    }

    getDetail() {
        // console.log('★★★ signFormID ★★★', this.signFormID);
        this.PamDRService.getDetail(this.signFormID).subscribe(
            res => {
                // console.log('★★★ res ★★★', res);

                this.item = res.Entries[0];

                this.item.DeviceReturn = this.chkBoolean(res.Entries[0].DeviceReturn);
                this.item.XfortReturn = this.chkBoolean(res.Entries[0].XfortReturn);
                this.item.NbBringout = this.chkBoolean(res.Entries[0].NbBringout);

                // console.log('★★★ res.Entries[0].NbBringout ★★★', res.Entries[0].NbBringout);
                // console.log('★★★ this.item ★★★', this.item);

                this.chkAuthority(this.AuthorityToken);
            }
        );
    }

    chkBoolean(key: any) {
        if (key === '1') { return true; } else { return false; }
    }

    send() { }
    Save() {

        this.PamDRService.Update(this.item).subscribe(
            res => {
                // console.log('★★★ res ★★★', res);
                if (res === true) {
                    this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                    // this.router.navigateByUrl('/pages/ApplyFormQuery', { skipLocationChange: true }).then(() =>
                    //     this.router.navigate(['/pages/ApplyFormQuery', this.signFromID]));
                } else {
                    this.baseHelper.ShowErrorMsgByStatusCode(res.StatusCode);
                }
            }
        );
    }

    chkAuthority(AuthorityToken) {
        // TopBtn: boolean;
        // DeviceChk: boolean;
        // XfortChk: boolean;
        // NBChk: boolean;
        let AuthorityName;
        // console.log('★★★ this.item.XfortControl == "Y" ★★★', this.item.XfortControl == 'Y');
        // console.log('★★★ this.item.NbCustody == "Y" ★★★', this.item.NbCustody == 'Y');
        switch (AuthorityToken) {
            case '1':
                if (this.item.XfortControl === 'Y' || this.item.NbCustody === 'Y') {
                    AuthorityName = '設備處理人員';
                    this.TopBtn = true;
                    this.DeviceChk = true;
                }
                break;
            case '2':
                if (this.item.DeviceReturn === true) {
                    AuthorityName = 'XFort(電腦週邊權限)';
                    this.XfortChk = true;
                    this.NBChk = true;
                }
                break;
            case '3':
                if (this.item.DeviceReturn === true) {
                    AuthorityName = 'NB攜出承辦人';
                    this.XfortChk = true;
                    this.NBChk = true;
                }
                break;
            case '4':
                AuthorityName = 'PAM管理員';
                break;
            default:
                return false;
        }
        console.log('★★★ AuthorityName ★★★', AuthorityName);
    }
}
