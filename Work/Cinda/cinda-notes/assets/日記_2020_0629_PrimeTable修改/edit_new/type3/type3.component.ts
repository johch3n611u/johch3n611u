import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import AsseetList from 'src/app/models/AsseetList';
import { GLEmployee } from 'src/app/models/GLEmployee';
import { PageQuery } from 'src/app/models/PageQuery';
import { SWChangeFormService } from 'src/app/services/swchange-form.service';
import { GLDept } from 'src/app/models/GLDept';
import { BaseComponent } from 'src/app/pages/base/BaseComponent';

import { forkJoin } from "rxjs";
import { TranslateService } from "@ngx-translate/core";

@Component({
    selector: 'app-type3',
    templateUrl: './type3.component.html',
    styleUrls: ['./type3.component.css']
})
export class Type3Component extends BaseComponent implements OnInit {

    cols: any[];
    cols2: any[];

    @Input() IsEanble: boolean = true;
    @Input() StrEmpNo: string;
    items = [] as any[];
    @Input() items_Selected = [] as any[];

    selectedValues_Delete: string[] = [];
    selectedValues_All: any;
    selectedValues_RemoveAll: any;

    asseet_display = false;
    assetEmpNo: string = '10124';
    ClickId: number;

    emp_display = false;
    dept_display = false;
    CanMultiple: boolean;
    custodian: string;
    custodianID: string;

    department: string;

    @Output() MoveEvent = new EventEmitter();

    constructor(
        private sWChangeFormService: SWChangeFormService,
        private translate: TranslateService
        ) {
        super();
    }

    ngOnInit() {

        this.cols = [
            { field: "softwarename" },
            { field: "assetnumber" },
            { field: "computername" },
            { field: "custodian" },
            { field: "bookkeeper" }
        ];
        this.cols.forEach(element => {
            forkJoin([
                this.translate.get(`Change.${element.field}`)
            ]).subscribe(res => {
                element.header = res;
            });
        });
        this.cols2 = [
            { field: "FormatId" },
            { field: "softwarename" },
            { field: "Oriassetnumber" },
            { field: "department" },
            { field: "bookkeeper" }
        ];
        this.cols2.forEach(element => {
            forkJoin([
                this.translate.get(`Change.${element.field}`)
            ]).subscribe(res => {
                element.header = res;
            });
        });




        if (this.IsEanble) {
            this.items = [];
            this.items_Selected = [];
        }
    }

    ngOnChanges(changes) {
        for (let propertyName in changes) {
            if (propertyName === 'StrEmpNo') {
                let change = changes[propertyName];
                let cur = JSON.stringify(change.currentValue);
                let prev = JSON.stringify(change.previousValue);
                if (cur != prev) {
                    if (prev != null) {
                        this.items_Selected = [] as any[];
                    }
                    this.GetData();
                }
            }
        }

    }

    GetData() {
        const requet = new PageQuery<number>();
        requet.QueryObject = 1;
        this.sWChangeFormService.GetType3List(requet, this.StrEmpNo).subscribe(res => {
            this.items = res.Entries;
            if (this.items.length > 0) {
                this.department = this.items[0].NewReservedDepartment;
            }
            else {
                this.department = "";
            }
            // this.items_Selected.forEach(element => {
            //     const index = this.items.findIndex(x => x.HsamMainaccessNo == element.HsamMainaccessNo &&
            //         x.HsamSubaccessNo == element.HsamSubaccessNo);
            //     if (index > -1) {
            //         this.items.splice(index, 1);
            //     }

            // });

            if (!this.IsEanble) {
                this.items = [];
            }
            this.items_Selected.forEach(element => {
                this.selectedValues_Delete.push(element.Id.toString());
                if (!this.IsEanble) {// 帶出原資料
                    this.department = element.NewReservedDepartment;
                    this.items = [...this.items, {
                        SwName: element.SwName,
                        authorizationName: element.authorizationName,
                        HsamMainaccessNo: element.OriginalHsamMainaccessNo,
                        HsamComputerName: element.OriginalHsamComputerName,
                        custodian: element.OriginalAssetsManager,
                        AuthBookkeeper: element.AuthBookkeeper
                    }]
                }
            });
        });

    }

    addItems() {
        this.department = '';

        this.selectedValues_Delete.forEach(element => {
            var tempItem = this.items.find(x => x.Id == element);
            tempItem.AuthMethod = tempItem.authorization;
            tempItem.NewReservedDepartment = '';
            this.items_Selected.push(tempItem);

            const index = this.items.findIndex(x => x.Id == element);
            if (index > -1) {
                this.items.splice(index, 1);
            }

        });
        this.selectedValues_Delete = [];
        this.selectedValues_All = '';
        this.MoveEvent.emit(this.items_Selected);
    }



    All(pType: number) {
        let itemsD = this.items;
        let All = this.selectedValues_All;
        if (pType == 2) {
            itemsD = this.items_Selected;
            All = this.selectedValues_RemoveAll;
        }
        this.selectedValues_Delete = [];
        if (All.length < 1) {
            return;
        }

        if (All[0] == "all") {
            itemsD.forEach(element => {
                this.selectedValues_Delete.push(element.Id.toString());
            });
        }
    }

    reomveItems() {
        this.selectedValues_Delete.forEach(element => {
            var tempItem = this.items_Selected.find(x => x.Id == element);
            if (tempItem != null) {
                this.items.push(tempItem);

                const index = this.items_Selected.findIndex(x => x.Id == element);
                if (index > -1) {
                    this.items_Selected.splice(index, 1);
                }
            }

        });
        this.selectedValues_Delete = [];
        this.selectedValues_RemoveAll = '';
        this.MoveEvent.emit(this.items_Selected);
    }

    // 申請人
    emp_showDialog(Id: number) {
        this.emp_display = true;
        this.CanMultiple = false;
        this.ClickId = Id;

    }
    emp_Add(data: GLEmployee) {
        console.log('emp_Add', data);
        const item = this.items_Selected.find(x => x.Id === this.ClickId);
        this.custodian = data.empName;
        this.custodianID = data.empNo;
        item.NewAuthBookkeeper = data.empName;
        item.NewAuthBookkeeperEmpno = data.empNo;
        /*
        this.items_Selected.forEach(element => {
            element.NewAuthBookkeeper = data.empName;
            element.NewAuthBookkeeperEmpno = data.empNo;
        });
        */
        this.MoveEvent.emit(this.items_Selected);
        this.emp_display = false;
    }

    emp_ColseDialog(event) {
        this.emp_display = false;
    }



    dept_showDialog() {
        this.dept_display = true;
        this.CanMultiple = false;

    }
    dept_Add(data: GLDept) {
        this.department = data.deptNo;
        this.dept_display = false;

        this.items_Selected.forEach(element => {
            element.NewReservedDepartment = this.department;
        });

        this.MoveEvent.emit(this.items_Selected);
    }

    dept_ColseDialog(event) {
        this.dept_display = false;
    }
}
