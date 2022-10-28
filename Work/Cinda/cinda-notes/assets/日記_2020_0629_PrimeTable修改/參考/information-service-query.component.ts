import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { TranslateService } from "@ngx-translate/core";
import { SwapplyFormService } from "../../../services/SwapplyForm.service";
import { PageQueryResult } from "../../../models/PageQueryResult";
import { PageQuery } from "../../../models/PageQuery";
import { ServiceQuery } from "src/app/models/ServiceQuery";
import { Response } from "selenium-webdriver/http";
import { Router } from "@angular/router";
import { MenuService } from 'src/app/services/menu.service';
import Menu from 'src/app/models/Menu';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { SelectItem } from 'primeng/api';
import { BaseHelper } from 'src/app/_helpers/base-helper';

@Component({
    selector: "app-information-service-query",
    templateUrl: "./information-service-query.component.html",
    styleUrls: ["./information-service-query.component.css"]
})
export class InformationServiceQueryComponent implements OnInit {
    pageQueryResult: PageQueryResult<ServiceQuery> = new PageQueryResult();
    items: ServiceQuery[];
    serviceClassList: any[] = [];
    menuList: Menu[];
    IntQueryType = 0;
    cols: any[];
    FormStatusItem: SelectItem[];
    ServiceProjectItem: SelectItem[];
    ServiceClassItem: SelectItem[];
    ServiceSubClassItem: SelectItem[];
    constructor(
        private translate: TranslateService,
        private swapplyFormService: SwapplyFormService,
        private router: Router,
        public baseHelper: BaseHelper,
        private menuService: MenuService
    ) { }

    ngOnInit() {
        setTimeout(() => {

            console.log("");

        }, 0);

        this.menuService.GetAllMenus().subscribe(res => {
            this.menuList = res.filter(x => x.MenuId === 1)[0].Children;
            this.serviceClassList = [];
            this.menuList.forEach(element => {
                this.serviceClassList.push({
                    label: element.Name,
                    value: element.MenuId,
                    children: element.Children
                });
            });
        });
        this.cols = [
            { field: 'ServiceNo' },
            { field: 'ServiceClass' },
            { field: 'ServiceSubClass' },
            { field: 'ServiceProject' },
            { field: 'FormStatus' },
            { field: 'ApplicanterName' },
            { field: 'ApplicanterDept' },
            { field: 'CreateDate' },
            { field: 'SignName' }
        ];
        this.cols.forEach(element => {

            forkJoin([
                this.translate.get(`InfoQuery.${element.field}`)
            ]).subscribe(res => {
                element.header = res;
            });
        });

        // forkJoin([
        //     this.translate.get('Type.Draft'),
        //     this.translate.get('Type.SignOff'),
        //     this.translate.get('Type.Reject'),
        //     this.translate.get('Type.Program'),
        //     this.translate.get('Type.Closed')
        // ]).subscribe(res => {
        //     this.FormStatusItem = [
        //         { label: 'All', value: null },
        //         { label: res[0], value: 'Draft' },
        //         { label: res[1], value: 'SignOff' },
        //         { label: res[2], value: 'Reject' },
        //         { label: res[3], value: 'Program' },
        //         { label: res[4], value: 'Closed' }
        //     ];
        // });

        this.QueryMode();

    }

    classNameMap(classId: number, subClassId: number) {
        const classFilter = this.serviceClassList.filter(x => x.value == classId);
        if (subClassId == 0) {
            return classFilter.length > 0 ? classFilter[0].label : "";
        }
        else {
            if (classFilter.length > 0) {
                const subClassFilter = classFilter[0].children.filter(x => x.MenuId == subClassId);
                return subClassFilter.length > 0 ? subClassFilter[0].Name : "";
            }
        }

        return "";
    }
    QueryMode() {
        const requet = new PageQuery<number>();
        requet.QueryObject = 1;
        this.swapplyFormService.GetServiceList(requet, this.IntQueryType).subscribe(res => {
            if (res) {
                if (res.Entries) {
                    this.pageQueryResult = res;
                    this.items = res.Entries;
                    this.FormStatusItem = [];
                    this.ServiceClassItem = [];
                    this.ServiceSubClassItem = [];
                    this.ServiceProjectItem = [];
                    this.items.map(x => {
                        x.FormStatus = this.baseHelper.GetTranslateValue(`Type.${x.FormStatus}`);
                        if (x.SignName) {
                            x.SignName = (x.Merge === 0 ? x.SignName.replace(/,/g, '/') : x.SignName);
                        }
                        if (x.FormStatus && this.FormStatusItem.filter(y => y.value === x.FormStatus).length === 0) {
                            this.FormStatusItem.push({
                                value: x.FormStatus,
                                label: x.FormStatus
                            } as SelectItem);
                        }
                        if (x.ServiceClass && this.ServiceClassItem.filter(y => y.value === x.ServiceClass).length === 0) {
                            this.ServiceClassItem.push({
                                value: x.ServiceClass,
                                label: x.ServiceClass
                            } as SelectItem);
                        }
                        if (x.ServiceSubClass && this.ServiceSubClassItem.filter(y => y.value === x.ServiceSubClass).length === 0) {
                            this.ServiceSubClassItem.push({
                                value: x.ServiceSubClass,
                                label: x.ServiceSubClass
                            } as SelectItem);
                        }
                        if (x.ServiceProject && this.ServiceProjectItem.filter(y => y.value === x.ServiceProject).length === 0) {
                            this.ServiceProjectItem.push({
                                value: x.ServiceProject,
                                label: x.ServiceProject
                            } as SelectItem);
                        }
                    });


                    setTimeout(() => {


                    }, 0);
                }
            }
        });

    }

    SignDetail(pType: string, pID: number, pStrFormStatus: string) {
        if (pStrFormStatus === 'Draft') {
            if (pType === 'SAMSwapplyForm') {
                this.router.navigate(['/pages/SoftwareApply'], { queryParams: { id: pID } });
            } else if (pType === 'SWChangeForm') {
                this.router.navigate(['/pages/SoftwareChange'], { queryParams: { id: pID } });
            } else if (pType === 'PAMCitrixPermissionForm') {
                this.router.navigate([`/pages/AccountCitrixChange/${pID}`]);
            } else if (pType === 'AccountApplyForm') {
                this.router.navigate([`/pages/AccountApply/${pID}`]);
            }

        } else {
            if (pType === 'SAMSwapplyForm') {
                this.router.navigate(['/pages/ApplyFormSign', pID]);
            } else if (pType === 'SWChangeForm') {
                this.router.navigate(['/pages/ChangeFormSign', pID]);
            } else if (pType === 'PAMCitrixPermissionForm') {
                this.router.navigate([`/pages/AccountCitrixChange/${pID}`]);
            } else if (pType === 'AccountApplyForm') {
                this.router.navigate([`/pages/AccountApply/${pID}`]);
            }
        }
    }

}
