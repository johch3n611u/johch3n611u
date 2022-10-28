import { Component, OnInit, Output } from '@angular/core';
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
export class DeviceReturnDetailComponent implements OnInit {

    ApplicationCategory = 'DeviceReturn';
    @Output() signFormID: string;
    item = {} as DeviceReturnList;

    constructor(
        private route: ActivatedRoute,
        private PamDRService: PamDeviceReturnService,
        private baseHelper: BaseHelper,
    ) { }

    ngOnInit() {

        this.route.params.subscribe(params => {
            this.signFormID = params.Id;
        });

        this.getDetail();

    }

    getDetail() {
        // console.log('↡↡↡ signFormID ↡↡↡', this.signFormID);
        this.PamDRService.getDetail(this.signFormID).subscribe(
            res => {
                // console.log('↡↡↡ res ↡↡↡', res);

                this.item = res.Entries[0];

                this.item.DeviceReturn = this.chkBoolean(res.Entries[0].DeviceReturn);
                this.item.XfortReturn = this.chkBoolean(res.Entries[0].XfortReturn);
                this.item.NbBringout = this.chkBoolean(res.Entries[0].NbBringout);

                // console.log('↡↡↡ res.Entries[0].NbBringout ↡↡↡', res.Entries[0].NbBringout);
                // console.log('↡↡↡ this.item.NbBringout ↡↡↡', this.item.NbBringout);
            }
        );
    }

    chkBoolean(key: any) {
        if (key == '1') { return true; } else { return false; }
    }

    send() { }
    Save() {

        this.PamDRService.Update(this.item).subscribe(
            res => {
                // console.log('↡↡↡ res ↡↡↡', res);
                if (res == true) {
                    this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                    // this.router.navigateByUrl('/pages/ApplyFormQuery', { skipLocationChange: true }).then(() =>
                    //     this.router.navigate(['/pages/ApplyFormQuery', this.signFromID]));
                } else {
                    this.baseHelper.ShowErrorMsgByStatusCode(res.StatusCode);
                }
            }
        );

    }
}
