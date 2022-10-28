import { Component, OnInit } from '@angular/core';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import PermissionDisable from 'src/app/models/PermissionDisable';
import { Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
import { ResignationDisabledService } from 'src/app/services/resignation-disabled.service';
import { PermissionDisableListStatus } from 'src/app/enum/PermissionDisableListStatus';

@Component({
    selector: 'app-permission-disable-list',
    templateUrl: './permission-disable-list.component.html',
    styleUrls: ['./permission-disable-list.component.css']
})
export class PermissionDisableListComponent implements OnInit {
    selectStatus: SelectItem[];
    cols: any[];
    status: number;
    items = [] as PermissionDisable[];
    statusEnum = PermissionDisableListStatus;
    constructor(
        private service: ResignationDisabledService,
        private baseHelper: BaseHelper,
        private router: Router,
    ) { }

    ngOnInit() {
        this.status = this.statusEnum.All;
        this.selectStatus = [
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.All'), value: this.statusEnum.All },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.AlreadyDo'), value: this.statusEnum.AlreadyDo },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.Finish'), value: this.statusEnum.Finish },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.Cancel'), value: this.statusEnum.Cancel },
        ];
        this.cols = [
            { field: 'Date' },
            { field: 'Id' },
            { field: 'Item' },
            { field: 'EmpNo' },
            { field: 'Department' },
            { field: 'EmpName' },
            { field: 'Signer' },
            { field: 'DisableDesc' }
        ];
        this.cols.forEach(x => {
            x.header = this.baseHelper.GetTranslateValue(`DeviceReturn.${x.field}`);
        });
        this.getData();
    }
    URL(Id: string) {
        console.log(Id, 'Id');
        this.router.navigate(['/pages/PermissionDisable', Id]);
    }
    getData() {
        this.service.getPermissionDisableList().subscribe(res => this.items = res);
    }
}
