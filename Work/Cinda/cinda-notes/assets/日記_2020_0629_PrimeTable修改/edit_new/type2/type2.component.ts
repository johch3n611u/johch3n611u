import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PageQuery } from 'src/app/models/PageQuery';
import { SWChangeFormService } from 'src/app/services/swchange-form.service';

import { forkJoin } from "rxjs";
import { TranslateService } from "@ngx-translate/core";
@Component({
    selector: 'app-type2',
    templateUrl: './type2.component.html',
    styleUrls: ['./type2.component.css']
})
export class Type2Component implements OnInit {

    cols: any[];

    @Input() IsEanble: boolean = true;
    items = [] as any[];
    selectedValues_Delete: string[] = [];
    selectedValues_All: string;

    @Input() StrEmpNo: string;
    @Input() items_Selected = [] as any[];
    @Output() CheckEvent = new EventEmitter();

    constructor(
        private sWChangeFormService: SWChangeFormService,

        private translate: TranslateService) { }

    ngOnInit() {

        this.cols = [
            { field: "FormatId" },
            { field: "softwarename" },
            { field: "authorization" },
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
        this.sWChangeFormService.GetType2List(requet, this.StrEmpNo).subscribe(res => {
            this.items = res.Entries;
            if (!this.IsEanble) {
                this.items = [];
            }
            this.items_Selected.forEach(element => {
                this.selectedValues_Delete.push(element.Id.toString());
                if (!this.IsEanble) {// 帶出原資料
                    this.items = [...this.items, {
                        SwName: element.SwName,
                        authorizationName: element.authorizationName,
                        HsamMainaccessNo: element.HsamMainaccessNo,
                        HsamComputerName: element.HsamComputerName,
                        custodian: element.AssetsManager,
                        AuthBookkeeper: element.AuthBookkeeper
                    }]
                }
            });
        });
    }

    All() {
        let itemsD = this.items;
        let All = this.selectedValues_All;

        this.selectedValues_Delete = [];
        if (All.length < 1) {
            this.items_Selected = [] as any[];
            return;
        }

        if (All[0] == "all") {
            itemsD.forEach(element => {
                this.selectedValues_Delete.push(element.Id.toString());
            });

            this.items_Selected = this.items;

        }

        this.CheckEvent.emit(this.items_Selected);
    }

    SelectRowItem(pItem: any)
    {
        console.log('zzz');
        this.items_Selected = [] as any[];
        this.selectedValues_Delete.forEach(element => {
            var tempItem = this.items.find(x => x.Id == element);
            this.items_Selected.push(tempItem);
        });

        this.CheckEvent.emit(this.items_Selected);
    }

}
