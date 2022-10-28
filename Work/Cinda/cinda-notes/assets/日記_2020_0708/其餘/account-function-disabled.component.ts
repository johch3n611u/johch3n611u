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


    constructor(
        private baseHelper: BaseHelper,
        private router: Router,
        private PamAFDService: PamAccounFunctionDisabledService
    ) { }

    ngOnInit() {

        this.getDate();

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

    getDate() {
        this.PamAFDService.getAll().subscribe(
            ref => (
                console.log(ref),
                ref.Entries.forEach(item => {
                    let fake = {} as AccountFunctionDisabled;
                    fake.Department = item.Department;
                    fake.UserEmpNo = item.EmpNo;
                    fake.User = item.Name;
                    fake.CloseDate = item.CloseDate;
                    fake.Status = item.FormStatus;
                    fake.Item = this.chkType(item.FormType);
                    // console.log(item.FormType);
                    // console.log(this.chkType(item.FormType));
                    // console.log(fake.Item);
                    this.items.push(fake);
                })
            )
        );
    }

    // 離職帳號或權限停用單狀態
    chkType(key) {
        switch (key) {
            case '0':
                key = '';
                break;
            case '1':
                key = this.baseHelper.GetTranslateValue('ResignationDisabled.FunctionDisable');
                break;
            case '2':
                key = this.baseHelper.GetTranslateValue('ResignationDisabled.Before');
                break;
            case '3':
                key = this.baseHelper.GetTranslateValue('ResignationDisabled.NotBefore');
                break;
            default:
                key = '此參數無預設值';
                break;
        }
        return key;
    }

    // URL 塞值
    URL(EmpNo: string) {
        this.router.navigate(['/pages/DisabledList/' + EmpNo]);
    }


}
