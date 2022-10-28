import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import AccountFunctionDisabled from 'src/app/models/AccountFunctionDisabled';
import { DatePipe } from '@angular/common';
import { Router } from '@angular/router';
import { PamAccounFunctionDisabledService } from 'src/app/services/pam-account-function-disabled.service';

@Component({
    selector: 'app-account-function-disabled',
    templateUrl: './account-function-disabled.component.html',
    styleUrls: ['./account-function-disabled.component.css']
})
export class AccountFunctionDisabledComponent implements OnInit {
    selectStatus: SelectItem[];
    status: number;
    cols: any[];
    items = [] as AccountFunctionDisabled[];
    fake = {} as AccountFunctionDisabled;

    constructor(
        private baseHelper: BaseHelper,
        private router: Router,
        private PamAFDService: PamAccounFunctionDisabledService
    ) { }

    ngOnInit() {
        this.PamAFDService.getAll().subscribe(
            res => (console.log(res.Entries[0]),
                this.fake = res.Entries[0],
                console.log(this.fake),
                this.items.push(this.fake)
            ));
        // this.fake.EmpNo = 9999;
        // this.fake.User = "ABCD";
        // this.fake.FormStatus = '0';
        // this.fake.CloseDate = new Date;
        // this.fake.FormType = '1234';
        // this.fake.Department = "MM000";
        console.log('aaa' + this.fake);

        this.selectStatus = [
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.All'), value: null },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.Doing'), value: 0 },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.Wait'), value: 1 },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.Stop'), value: 2 },
            { label: this.baseHelper.GetTranslateValue('ResignationDisabled.Finish'), value: 3 },
        ];
        this.cols = [
            { field: 'Department' },
            { field: 'UserEmpNo' },
            { field: 'User' },
            { field: 'CloseDate' },
            { field: 'Item' }
        ];
        this.cols.forEach(x =>
            x.header = this.baseHelper.GetTranslateValue(`ResignationDisabled.${x.field}`));
    }
    URL(EmpNo: string) {
        this.router.navigate(['/pages/DisabledList/' + EmpNo]);
    }
}
