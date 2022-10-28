import { Component, OnInit } from '@angular/core';
import DeviceReturnList from 'src/app/models/DeviceReturnList';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { Router } from '@angular/router';
import { getAllDayHtml } from '@fullcalendar/core';
import { PamDeviceReturnService } from '../../../services/pam-device-return.service';

@Component({
    selector: 'app-device-return',
    templateUrl: './device-return.component.html',
    styleUrls: ['./device-return.component.css']
})

export class DeviceReturnComponent implements OnInit {
    cols: any[];
    items = [] as DeviceReturnList[];
    fakeData = {} as DeviceReturnList;
    constructor(
        private baseHelper: BaseHelper,
        private router: Router,
        private PamDRService: PamDeviceReturnService,
    ) { }

    ngOnInit() {

        this.getAll();

        this.cols = [
            { field: 'SignFormNo' },
            { field: 'ReturnDate' },
            { field: 'ResignDepartment', },
            { field: 'ResignEmpNo' },
            { field: 'ResignName' },
            { field: 'DeviceReturn' },
            { field: 'XfortReturn' },
            { field: 'NbBringout' }
        ];

        this.cols.forEach(element => {
            element.header = this.baseHelper.GetTranslateValue('DeviceReturn.' + element.field);
        });
    }

    URL(SignFormId: string) {
        // console.log(SignFormId, 'SignFormId');
        this.router.navigate(['/pages/DeviceReturnList', SignFormId]);
    }

    getAll() {
        this.PamDRService.getAll().subscribe(
            res => {
                console.log('★★★ res ★★★', res);
                res.Entries.forEach(item => {
                    item.DeviceReturn = this.ChkBoolean(item.DeviceReturn);
                    item.XfortReturn = this.ChkBoolean(item.XfortReturn);
                    item.NbBringout = this.ChkBoolean(item.NbBringout);
                    this.items.push(item);
                });
            }
        );
    }

    ChkBoolean(key: any) {
        if (key === '1') { key = 'Y'; } else { key = 'null'; }
        return key;
    }
}
