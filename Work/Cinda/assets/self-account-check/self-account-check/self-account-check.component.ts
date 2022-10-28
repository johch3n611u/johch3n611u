import { SelfAccountCheckService } from './self-account-check.service';
import { Component, OnInit, } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { MenuService } from 'src/app/services/menu.service';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { SelectItem } from 'primeng/api';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { Table } from 'primeng/table';
import { Router, ActivatedRoute } from '@angular/router';

export class SELF_ACCOUNT_CHECK {

    ID: string;
    DEPT_CENTER: string;
    DEPT_NO: string;
    EMP_NO: string;
    EMP_NAME: string;
    STATE: string;
    WAIT_NOTIFICATION_ID: string;
    SANDOUT_NOTIFICATION_ID: string;

}

@Component({
    selector: 'app-self-account-check',
    templateUrl: './self-account-check.component.html',
    styleUrls: ['./self-account-check.component.css']
})

export class SelfAccountCheckComponent implements OnInit {

    Mails: SELF_ACCOUNT_CHECK[] = [] as SELF_ACCOUNT_CHECK[];
    Mail: SELF_ACCOUNT_CHECK = {} as SELF_ACCOUNT_CHECK;
    SelectedMailsId: string[] = [] as string[];
    FilterTable: string[] = [] as string[];
    cols: any[];
    DeptCenterSelect: SelectItem[] = [] as SelectItem[];
    DeptNoSelect: SelectItem[] = [] as SelectItem[];
    firstPage = 0;

    constructor(
        private _TranslateService: TranslateService,
        private _Router: Router,
        public _BaseHelper: BaseHelper,
        private _ActivatedRoute: ActivatedRoute,
        public _SelfAccountCheckService: SelfAccountCheckService,
    ) {
        // this.TestDataInit(); // 測試資料
        this.Mails = []; // 初始化避免跑版
    }

    ngOnInit(): void {
        this.GetSelfAccountCheck();
    }

    CheckAll() {
        if (this.FilterTable.length > 0) {
            this.SelectedMailsId = [];
            this.FilterTable.forEach(element => {
                this.SelectedMailsId.push(element.toString());
            });
        }
        else if (this.FilterTable.length == 0 && this.SelectedMailsId.length == 0) {
            this.SelectedMailsId = [];
            this.Mails.forEach(element => {
                this.SelectedMailsId.push(element.ID.toString());
            })
        } else if (this.SelectedMailsId.length > 0) {
            this.SelectedMailsId = [];
        }
    }

    onFilter($event: any, table: Table) {
        $event.filteredValue.forEach(element => {
            this.FilterTable.push(element.ID.toString());
        });
    }

    PtableInit() {

        // FilterItemInit

        this.cols = [
            { field: 'DEPT_CENTER' },
            { field: 'DEPT_NO' },
            { field: 'EMP_NO' },
            { field: 'EMP_NAME' },
            { field: 'STATE' },
        ];

        this.cols.forEach(element => {
            forkJoin([
                this._TranslateService.get(`${element.field}`)
            ]).subscribe(res => {
                element.header = res;
            });
        });

        // SelectItemInit

        let DeptCenter_WaitFilter = [];
        let DeptNo_WaitFilter = [];

        this.Mails.forEach(element => {
            DeptCenter_WaitFilter.push(element.DEPT_CENTER);
            DeptNo_WaitFilter.push(element.DEPT_NO);
        });

        DeptCenter_WaitFilter = DeptCenter_WaitFilter.filter((item, index) => DeptCenter_WaitFilter.indexOf(item) === index);
        DeptNo_WaitFilter = DeptNo_WaitFilter.filter((item, index) => DeptNo_WaitFilter.indexOf(item) === index);

        this.DeptCenterSelect = [];

        DeptCenter_WaitFilter.forEach(element => {
            this.DeptCenterSelect.push({ label: element, value: element })
        });

        this.DeptNoSelect = [];

        DeptNo_WaitFilter.forEach(element => {
            this.DeptNoSelect.push({ label: element, value: element })
        });
    }

    TestDataInit() {
        for (var i = 0; i < 100; i++) {
            let Random = Math.random() * 50;
            let State = "已通知";
            if (Random % 3 > 1) {
                State = "未通知";
            }
            this.Mail = new SELF_ACCOUNT_CHECK;
            this.Mail.ID = i.toString();
            this.Mail.DEPT_CENTER = "Center_" + Random;
            this.Mail.DEPT_NO = "Dept_" + Random;
            this.Mail.EMP_NO = Random.toString();
            this.Mail.EMP_NAME = "TEST" + Random;
            this.Mail.STATE = State;
            this.Mails.push(this.Mail);
        }
        this.PtableInit();
    }

    CallPAM604() {
        this._SelfAccountCheckService.CallPAM604().subscribe(res => {

            if (res.StatusCode === ErrorCode.Success) {
                // console.log('CallPAM604 Success');
                this._BaseHelper.ShowSuccessMsg(this._BaseHelper.GetTranslateValue('Form.Success'));
                this.GetSelfAccountCheck();
            } else {
                // console.log('CallPAM604 Error');
                this._BaseHelper.ShowErrorMsg(this._BaseHelper.GetTranslateValue('Form.Fail'));
            }
        });
        this._BaseHelper.ShowSuccessMsg('進行非同步全刪全建，建置完成後將以 Email 通知');
        this._Router.navigate(['/']);
    }

    GetSelfAccountCheck() {
        // console.log('CallGetSelfAccountCheck');
        this._SelfAccountCheckService.GetSelfAccountCheck().subscribe(res => {
            if (res.StatusCode === ErrorCode.Success) {
                console.log('CallGetSelfAccountCheck Success', res);
                this.Mails = res.Entries;
                this.PtableInit(); // 初始化 ptable
            } else {
                // console.log('CallGetSelfAccountCheck Error');
                this._BaseHelper.ShowErrorMsg(this._BaseHelper.GetTranslateValue('Form.Fail'));
            }
        })
    }

    SendMail() {
        // alert(this.SelectedMailsId);
        this._SelfAccountCheckService.SendMail(this.SelectedMailsId).subscribe(res => {
            if (res.StatusCode === ErrorCode.Success) {
                console.log('SendMail Success', res);
                this._BaseHelper.ShowSuccessMsg(this._BaseHelper.GetTranslateValue('Form.Success'));
                this.GetSelfAccountCheck();
            } else {
                console.log('SendMail Error');
                this._BaseHelper.ShowErrorMsg(this._BaseHelper.GetTranslateValue('Form.Fail'));
            }
        })
    }

    TestMrgLink() {
        let type = "mrg";
        this._Router.navigate(['pages/AccountPermissionReport', type]);
    }
}
