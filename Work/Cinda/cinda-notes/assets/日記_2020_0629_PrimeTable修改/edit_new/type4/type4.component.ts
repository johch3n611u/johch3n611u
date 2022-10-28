import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import AsseetList from 'src/app/models/AsseetList';
import { GLEmployee } from 'src/app/models/GLEmployee';
import { PageQuery } from 'src/app/models/PageQuery';
import { SWChangeFormService } from 'src/app/services/swchange-form.service';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { GLDept } from 'src/app/models/GLDept';
import { BaseComponent } from 'src/app/pages/base/BaseComponent';

import { forkJoin } from "rxjs";
import { TranslateService } from "@ngx-translate/core";
@Component({
    selector: 'app-type4',
    templateUrl: './type4.component.html',
    styleUrls: ['./type4.component.css']
})
export class Type4Component extends BaseComponent implements OnInit {

    cols: any[];
    cols2: any[];

    @Input() IsEanble: boolean = true;
    @Input() StrEmpNo: string;
    items = [] as any[];
    @Input() items_Selected = [] as any[];

    selectedValues_Delete: string[] = [];
    selectedValues_All: string;
    selectedValues_RemoveAll: string;

    asseet_display = false;
    assetEmpNo: string = '10124';
    ClickId: string;

    emp_display = false;
    dept_display = false;
    CanMultiple: boolean;
    custodian: string;
    custodianID: string;
    department: string = "";
    departmentS: string = "";


    @Output() MoveEvent = new EventEmitter();

    constructor(
        private sWChangeFormService: SWChangeFormService,
        public baseHelper: BaseHelper,
        private translate: TranslateService
        ) {
        super();
    }

    ngOnInit() {

        this.cols = [
            { field: "softwarename" },
            { field: "department" },
            { field: "Qty" },
            { field: "UseQty" },
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
            { field: "assetnumber" },
            { field: "computername" },
            { field: "location" },
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
        this.custodian = localStorage.getItem('portal_UserName');
        this.custodianID = localStorage.getItem('portal_Account');
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
        this.sWChangeFormService.GetType4List(requet, this.StrEmpNo, this.departmentS).subscribe(res => {
            this.items = res.Entries;
            if (!this.IsEanble) {
                this.items = [];
            }
            this.items_Selected.forEach(element => {
                this.selectedValues_Delete.push(element.Id.toString());
                if (!this.IsEanble) {// 帶出原資料
                    this.custodian = element.AssetsManager;
                    this.custodianID = element.AssetsManagerEmpno;
                    if (this.items.filter(x => x.SoftwareAuthorizationId === element.SoftwareAuthorizationId).length === 0) {
                        this.items = [...this.items, {
                            SwName: element.SwName,
                            authorizationName: element.authorizationName,
                            HsamMainaccessNo: element.HsamMainaccessNo,
                            HsamComputerName: element.HsamComputerName,
                            custodian: element.AssetsManager,
                            AuthBookkeeper: element.AuthBookkeeper,
                            ReservedDepartment: element.ReservedDepartment,
                            ReservedQuantity: element.ReservedQuantity,
                            SoftwareAuthorizationId: element.SoftwareAuthorizationId,
                            UseQty: this.items_Selected.filter(x => x.SoftwareAuthorizationId === element.SoftwareAuthorizationId).length
                        }];
                    }

                }
            });
        });
    }

    GGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0,
                v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    addItems() {
        let errorMessage = '';
        this.custodian = '';
        this.custodianID = '';

        console.log('this.selectedValues_Delete', this.selectedValues_Delete);

        let breakFlag = false;
        this.selectedValues_Delete.forEach(element => {

            if (breakFlag) {
                return false;
            }
            this.items_Selected = this.items_Selected.filter(x => x.SoftwareAuthorizationId != element);


            var tempItem = this.items.find(x => x.Id == element);
            console.log('tempItem', tempItem);

            tempItem.AuthMethod = tempItem.authorization;
            tempItem.AssetsManager = this.custodian;
            tempItem.AssetsManagerEmpno = this.custodianID;
            if (tempItem.UseQty == null) {
                breakFlag = true;
                errorMessage = this.baseHelper.GetTranslateValue('Change.CheckUsingCountNotInput');
                return false;
            }
            if (tempItem.UseQty > tempItem.ReservedQuantity) {
                breakFlag = true;
                errorMessage = this.baseHelper.GetTranslateValue('Change.CheckUsingCount');
                return false;
            }
            for (var i = 0; i < tempItem.UseQty; i++) {
                let CopyItem = JSON.parse(JSON.stringify(tempItem));
                CopyItem.GId = this.GGuid();
                this.items_Selected.push(CopyItem);
            }

        });

        if (breakFlag) {
            this.baseHelper.ShowErrorMsg(errorMessage);
            return;
        }

        let IntIndex = 1;
        this.items_Selected.forEach(element => {
            element.Id = IntIndex;
            IntIndex++;
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
        else {

        }
        this.selectedValues_Delete = [];
        if (All.length < 1) {
            return;
        }

        if (All[0] == "all") {
            itemsD.forEach(element => {
                if (pType == 2) {
                    this.selectedValues_Delete.push(element.GId.toString());
                }
                else {
                    this.selectedValues_Delete.push(element.Id.toString());
                }
            });
        }
    }

    reomveItems() {
        this.selectedValues_Delete.forEach(element => {
            const index = this.items_Selected.findIndex(x => x.GId == element);
            if (index > -1) {
                this.items_Selected.splice(index, 1);
            }

        });
        this.selectedValues_Delete = [];
        this.selectedValues_RemoveAll = '';
        this.MoveEvent.emit(this.items_Selected);
    }


    Asseet_showDialog(Id, pEmpNo) {
        if (this.custodian.length === 0) {
            this.baseHelper.ShowErrorMsg(this.baseHelper.GetTranslateValue('Change.CheckNewCustodian'));
            return;
        }
        this.asseet_display = true;
        this.ClickId = Id;
        this.assetEmpNo = pEmpNo;
    }

    Asseet_Add(data: AsseetList) {
        const asset = this.items_Selected.find(x => x.GId == this.ClickId);
        asset.HsamMainaccessNo = data.AsseetId;
        asset.HsamSubaccessNo = data.AsseetChrildId;
        asset.HsamComputerName = data.PCName;
        asset.HsamPleace = data.floor;
        asset.HsamCompanyCode = data.Code;
        this.asseet_display = false;
        this.MoveEvent.emit(this.items_Selected);
    }

    Asseet_ColseDialog(event) {
        this.asseet_display = false;
    }



    // 申請人
    emp_showDialog(Id: string) {
        this.emp_display = true;
        this.CanMultiple = false;
        this.ClickId = Id;
        if (this.ClickId == "0") {
            this.department = "";
        }
        else {
            const emp = this.items_Selected.find(x => x.GId == this.ClickId);
            this.department = emp.ReservedDepartment;
        }
    }
    emp_Add(data: GLEmployee) {
        if (this.ClickId == "0") {
            this.custodian = data.empName;
            this.custodianID = data.empNo;

            this.items_Selected.forEach(element => {
                element.AssetsManager = data.empName;
                element.AssetsManagerEmpno = data.empNo;
            });
        } else {
            const emp = this.items_Selected.find(x => x.GId == this.ClickId);
            emp.NewAuthBookkeeper = data.empName;
            emp.NewAuthBookkeeperEmpno = data.empNo;
        }

        this.items_Selected.forEach(item => {
            item.HsamMainaccessNo = '';
        });

        this.emp_display = false;
        this.MoveEvent.emit(this.items_Selected);
    }

    emp_ColseDialog(event) {
        this.emp_display = false;
    }



    dept_showDialog() {
        this.dept_display = true;
        this.CanMultiple = false;

    }
    dept_Add(data: GLDept) {
        this.departmentS = data.deptNo;
        this.dept_display = false;
        this.GetData();
        this.MoveEvent.emit(this.items_Selected);
    }

    dept_ColseDialog(event) {
        this.dept_display = false;
    }

}
