import { Component, OnInit, Input ,EventEmitter, Output} from '@angular/core';
import AsseetList from 'src/app/models/AsseetList';
import { GLEmployee } from 'src/app/models/GLEmployee';
import { SWChangeFormService } from 'src/app/services/swchange-form.service';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { PageQuery } from 'src/app/models/PageQuery';
import { GLDept } from 'src/app/models/GLDept';
import { BaseComponent } from 'src/app/pages/base/BaseComponent';

@Component({
    selector: 'app-type5',
    templateUrl: './type5.component.html',
    styleUrls: ['./type5.component.css']
})
export class Type5Component extends BaseComponent implements OnInit {
    @Input() IsEanble: boolean = true;
    @Input() StrEmpNo: string;
    items = [] as any[];
    @Input() items_Selected = [] as any[];

    selectedValues_Delete: string[] = [];
    selectedValues_All: string;
    selectedValues_RemoveAll: string;

    ClickId: number;

    emp_display = false;
    dept_display = false;
    dept_display2 = false;
    CanMultiple: boolean;
    department: string = "";
    departmentS: string = "";
    departmentS2: string = "";
    @Output() MoveEvent = new EventEmitter();
    constructor(private sWChangeFormService: SWChangeFormService,
        public baseHelper: BaseHelper) {
        super();
    }

    ngOnInit() {
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
                    if(prev != null){
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
        this.sWChangeFormService.GetType5List(requet, this.StrEmpNo, this.departmentS).subscribe(res => {
            this.items = res.Entries;

            if(this.items_Selected.length > 0){
                this.departmentS2 = this.items_Selected[0].NewReservedDepartment;
            }
            else{
                this.departmentS2 = "";
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
                    this.departmentS2 = element.NewReservedDepartment;
                    this.items = [...this.items, {
                        SwName: element.SwName,
                        authorizationName: element.authorizationName,
                        HsamMainaccessNo: element.OriginalHsamMainaccessNo,
                        HsamComputerName: element.OriginalHsamComputerName,
                        custodian: element.OriginalAssetsManager,
                        AuthBookkeeper: element.AuthBookkeeper,
                        ReservedDepartment: element.ReservedDepartment,
                        ReservedQuantity: element.ReservedQuantity,
                    }];
                }
            });

        });
    }


    addItems()
    {
        this.selectedValues_Delete.forEach(element => {
            var tempItem =  this.items.find(x=> x.Id == element);
            tempItem.AuthMethod = tempItem.authorization;
            tempItem.NewReservedDepartment = '';
            this.items_Selected.push(tempItem);

            const index = this.items.findIndex(x => x.Id == element);
            if (index > -1) {
            this.items.splice(index, 1);
            }

        });
        this.selectedValues_Delete = [];
        this.selectedValues_All ='';
        this.MoveEvent.emit(this.items_Selected);
    }



    All(pType: number) {
        let itemsD = this.items;
        let All = this.selectedValues_All;
        if(pType == 2){
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

    reomveItems()
    {
        this.selectedValues_Delete.forEach(element => {
            var tempItem =  this.items_Selected.find(x=> x.Id == element);
            if (tempItem != null) {
                this.items.push(tempItem);

                const index = this.items_Selected.findIndex(x => x.Id == element);
                if (index > -1) {
                    this.items_Selected.splice(index, 1);
                }
            }

        });
        this.selectedValues_Delete = [];
        this.selectedValues_RemoveAll ='';
        this.MoveEvent.emit(this.items_Selected);
    }


     // 申請人
     emp_showDialog(Id: number) {
        this.emp_display = true;
        this.CanMultiple = false;
        this.ClickId = Id;

        const emp = this.items_Selected.find(x => x.Id == this.ClickId);
        this.department = emp.NewReservedDepartment;

    }
    emp_Add(data: GLEmployee) {
        const emp = this.items_Selected.find(x => x.Id == this.ClickId);
        emp.NewAuthBookkeeper = data.empName;
        emp.NewAuthBookkeeperEmpno = data.empNo;
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



   dept_showDialog2() {
        this.dept_display2 = true;
        this.CanMultiple = false;

    }
    dept_Add2(data: GLDept) {
        this.departmentS2 = data.deptNo;
        this.dept_display2 = false;
        this.items_Selected.forEach(element => {
            element.NewReservedDepartment = this.departmentS2;
        });
        this.MoveEvent.emit(this.items_Selected);
    }

    dept_ColseDialog2(event) {
        this.dept_display2 = false;
    }
}
