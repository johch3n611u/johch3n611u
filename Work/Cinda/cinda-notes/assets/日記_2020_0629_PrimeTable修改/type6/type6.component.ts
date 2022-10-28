import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import AsseetList from 'src/app/models/AsseetList';
import { GLEmployee } from 'src/app/models/GLEmployee';
import { SWChangeFormService } from 'src/app/services/swchange-form.service';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { PageQuery } from 'src/app/models/PageQuery';

@Component({
    selector: 'app-type6',
    templateUrl: './type6.component.html',
    styleUrls: ['./type6.component.css']
})
export class Type6Component implements OnInit {
    @Input() IsEanble: boolean = true;
    @Input() StrEmpNo: string;
    items = [] as any[];
    @Input() items_Selected = [] as any[];

    selectedValues_Delete: string[] = [];
    selectedValues_All: string;
    selectedValues_RemoveAll: string;

    ClickId: number;

    emp_display = false;
    CanMultiple: boolean;

    @Output() MoveEvent = new EventEmitter();

    constructor(private sWChangeFormService: SWChangeFormService,
        public baseHelper: BaseHelper) { }

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
        this.sWChangeFormService.GetType6List(requet, this.StrEmpNo).subscribe(res => {
            this.items = res.Entries;

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
                    this.items = [...this.items, {
                        SwName: element.SwName,
                        authorizationName: element.authorizationName,
                        HsamMainaccessNo: element.OriginalHsamMainaccessNo,
                        HsamComputerName: element.OriginalHsamComputerName,
                        custodian: element.OriginalAssetsManager,
                        AuthBookkeeper: element.AuthBookkeeper,
                        OriginalSoftwareVersion: element.OriginalSoftwareVersion
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
}
