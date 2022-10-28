import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import AsseetList from 'src/app/models/AsseetList';
import { GLEmployee } from 'src/app/models/GLEmployee';
import { PageQuery } from 'src/app/models/PageQuery';
import { SWChangeFormService } from 'src/app/services/swchange-form.service';
import { BaseHelper } from 'src/app/_helpers/base-helper';

@Component({
    selector: 'app-type1',
    templateUrl: './type1.component.html',
    styleUrls: ['./type1.component.css']
})
export class Type1Component implements OnInit {

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
    CanMultiple: boolean;
    custodian: string;
    custodianID: string;

    @Output() MoveEvent = new EventEmitter();

    constructor(
        private sWChangeFormService: SWChangeFormService,
        public baseHelper: BaseHelper,
        private translate: TranslateService,
        ) { }

    ngOnInit() {

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
        this.sWChangeFormService.GetType1List(requet, this.StrEmpNo).subscribe(res => {
            if (this.IsEanble) {
                this.items = res.Entries;
            }
            this.items_Selected.forEach(element => {
                this.custodian = element.AssetsManager;
                this.custodianID = element.AssetsManagerEmpno;

                const index = this.items.findIndex(x => x.HsamMainaccessNo == element.HsamMainaccessNo &&
                    x.HsamSubaccessNo == element.HsamSubaccessNo);
                if (index > -1) {
                    this.items.splice(index, 1);
                }
                if (!this.IsEanble) {// 帶出原資料
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
        this.custodian = '';
        this.custodianID = '';

        this.selectedValues_Delete.forEach(element => {
            var tempItem = this.items.find(x => x.Id == element);
            // tempItem.AssetsManager = this.custodian;
            // tempItem.AssetsManagerEmpno = this.custodianID;
            tempItem.HsamMainaccessNo = '';
            tempItem.HsamComputerName = '';
            this.items_Selected.push(tempItem);

            const index = this.items.findIndex(x => x.Id == element);
            if (index > -1) {
                this.items.splice(index, 1);
            }

        });
        this.selectedValues_Delete = [];
        this.selectedValues_All = '';
        this.MoveEvent.emit(this.items_Selected);
        console.log('items_Selected', this.items_Selected);
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
        const asset = this.items_Selected.find(x => x.Id == this.ClickId);
        asset.HsamMainaccessNo = data.AsseetId;
        asset.HsamSubaccessNo = data.AsseetChrildId;
        asset.HsamComputerName = data.PCName;
        asset.HsamPleace = data.floor;
        asset.HsamCompanyCode = data.Code;
        asset.HsamIsVM = data.ISVMType;
        this.asseet_display = false;
        console.log('items_Selected', this.items_Selected);
    }

    Asseet_ColseDialog(event) {
        this.asseet_display = false;
    }



    // 申請人
    emp_showDialog(Id: number = 0) {
        this.emp_display = true;
        this.CanMultiple = false;
        this.ClickId = Id;

    }
    emp_Add(data: GLEmployee) {
        this.custodian = data.empName;
        this.custodianID = data.empNo;
        this.emp_display = false;

        this.items_Selected.forEach(element => {
            element.AssetsManager = data.empName;
            element.AssetsManagerEmpno = data.empNo;
        });
    }

    emp_ColseDialog(event) {
        this.emp_display = false;
    }

}
