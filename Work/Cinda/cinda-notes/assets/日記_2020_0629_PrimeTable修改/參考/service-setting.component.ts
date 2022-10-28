import { Component, OnInit } from "@angular/core";
import { ServiceSettingItem } from "../../../models/ServiceSetting";
import { SelectItem } from "primeng/api";
import { StaffOption } from "src/app/models/StaffOption";
import { GLEmployee } from "src/app/models/GLEmployee";
import { TranslateService } from "@ngx-translate/core";
import { Title } from "@angular/platform-browser";
import { forkJoin } from "rxjs";
import { ServiceSetting } from "../../../services/serviceSetting.service";
import { MenuService } from "../../../services/menu.service";
import Menu from 'src/app/models/Menu';
import { SignFlowService } from "../../../services/signFlowSetting.service";
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { EnumHelper } from 'src/app/_helpers/enum-helpers';
import { Enable } from '../../../enum/Enable';

@Component({
    selector: "app-vip-maintain",
    templateUrl: "./service-setting.component.html",
    styleUrls: ["./service-setting.component.css"]
})
export class ServiceSettingComponent implements OnInit {
    displayDialog: boolean;
    ServiceCode: string;
    Staffs: SelectItem[];
    selectedStaff: StaffOption[];

    selectedVipUser: ServiceSettingItem;

    dataLists = [] as ServiceSettingItem[];

    cols: any[];

    constructor(
        // private vipUserService: VipUserService
        private translate: TranslateService,
        private serviceSetting: ServiceSetting,
        private menuService: MenuService,
        private signFlowService: SignFlowService,
        public baseHelper: BaseHelper,

    ) {
        this.Staffs = [
            { label: "sigma", value: { id: 1, name: "sigma" } },
            { label: "eddie", value: { id: 2, name: "eddie" } },
            { label: "barney", value: { id: 3, name: "barney" } },
            { label: "vincent", value: { id: 4, name: "vincent" } },
            { label: "roy", value: { id: 5, name: "roy" } }
        ];
    }
    OrganizerItem: SelectItem[];
    BackupOrganizerItem: SelectItem[];
    StatusItem: SelectItem[];
    LeavingControlsItem: SelectItem[];
    ComplexApplicationItem: SelectItem[];
    IsUseStandardItem: SelectItem[];
    ServiceClassItem: SelectItem[];
    dataSet: any[];
    menuList: Menu[];
    serviceClassList: any[] = [];
    serviceSubClassList: any[] = [];
    signDataList: any[] = [];
    levelCodeList: any[] = [];
    ngOnInit() {
        this.StatusItem = EnumHelper.getSelectItems(Enable);
        this.LeavingControlsItem = EnumHelper.getSelectItems(Enable);
        this.ComplexApplicationItem = EnumHelper.getSelectItems(Enable);
        this.IsUseStandardItem = EnumHelper.getSelectItems(Enable);
        // this.vipUserService.getCarsSmall().then(vipUsers => this.vipUsers = vipUsers);
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


        this.signFlowService.getPortalSystemFlowList().subscribe(res => {
            this.signDataList = [];
            res.Entries.filter(x => x.Status === false).forEach(element => {
                this.signDataList.push({
                    label: element.FlowNo,
                    value: element.FlowNo
                });
            });
        });

        this.serviceSetting.getPortalSystemServiceList().subscribe(res => {
            this.dataLists = res.Entries;
            setTimeout(() => {

            }, 0);
        });
        this.serviceSetting.getLevelCodeList().subscribe(res => {
            this.levelCodeList = [];
            this.levelCodeList.push({
                label: '',
                value: ''
            });

            res.forEach(element => {
                this.levelCodeList.push({
                    label: element.Value,
                    value: element.Key
                });
            });
        });

        this.cols = [
            { field: "Edit" },
            { field: "ServiceCode" },
            { field: "ServiceProject" },
            { field: "ServiceProjectEn" },
            { field: "ServiceSubProject" },
            { field: "ServiceSubProjectEn" },
            { field: "ServiceClass" },
            { field: "ServiceSubClass" },
            { field: "HqFlowCode" },
            { field: "OverseasFlowCode" },
            { field: "RiskLevel" },
            { field: "LevelCode" },
            { field: "IsUseStandardForm" },
            { field: "ComplexApplicationOptions" },
            { field: "LeavingControls" },
            { field: "Status" },
            { field: "Organizer" },
            { field: "BackupOrganizer" },
            // { field: "InfoSecurityDescription" },
            // { field: "InfoSecurityDescriptionEn" },
            // { field: "ApplicationDescription" },
            // { field: "ApplicationDescriptionEn" },
            { field: "ModifyEmp" },
            { field: "ModifyDate" }
        ];
        this.cols.forEach(element => {
            forkJoin([
                this.translate.get(`ServiceSetting.${element.field}`)
            ]).subscribe(res => {
                element.header = res;
            });
        });

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

    onServiceClassChange(event) {
        const classFilter = this.serviceClassList.filter(x => x.value == event.value);

        if (classFilter.length > 0) {
            const subClassFilter = classFilter[0].children;
            this.serviceSubClassList = [];
            subClassFilter.forEach(element => {
                this.serviceSubClassList.push({
                    label: element.Name,
                    value: element.MenuId
                });
            });
            this.input.ServiceSubClass = this.serviceSubClassList[0].value;
        }
    }

    headerMode = "header.Computercustodian";
    emp_display = false;
    ClickId: number;
    organizerType: number;
    CanMultiple: boolean;
    emp_showDialog(Id, Type) {
        if (Id == null) {
            this.headerMode = "header.MemberList";
        } else {
            this.headerMode = "header.Computercustodian";
        }

        this.emp_display = true;
        this.CanMultiple = false;
        this.organizerType = Type;
        console.log(this.emp_display);

        this.ClickId = Id;
    }
    emp_ColseDialog(event) {
        this.emp_display = false;
    }
    emp_Add(data: GLEmployee) {
        console.log(data, 'data');
        if (this.organizerType == 1) {
            this.input.OrganizerEmpno = data.empNo;
            this.input.Organizer = data.empName;
        } else {
            this.input.BackupOrganizerEmpno = data.empNo;
            this.input.BackupOrganizer = data.empName;
        }
        this.emp_display = false;
    }

    showDialogToAdd(editService: ServiceSettingItem = new ServiceSettingItem) {
        this.displayDialog = true;
        console.log(editService, 'editService');
        if (editService.ServiceClass != -1) {
            this.onServiceClassChange({ value: editService.ServiceClass });
        }
        if (editService.LevelCode == null) {
            editService.LevelCode = this.levelCodeList[0].value;
        }
        if (editService.HqFlowCode == "") {
            editService.HqFlowCode = this.signDataList[0].value;
        }
        if (editService.OverseasFlowCode == "") {
            editService.OverseasFlowCode = this.signDataList[0].value;
        }


        this.input = editService;
        this.selectedStaff = [];
        console.log(this.selectedStaff);
    }

    input: ServiceSettingItem = new ServiceSettingItem();
    organizerName: string;
    colseDialog() {
        this.displayDialog = false;
    }

    Submit_OK() {
        console.log(this.input);
        let msg = "";
        if (this.input.ServiceClass == -1) {
            forkJoin([
                this.translate.get("ServiceSetting.SelectServiceClass")
            ]).subscribe(res => {
                msg = res[0] + "\n" + msg;
            });

        }
        if (this.input.ServiceSubClass == -1) {
            forkJoin([
                this.translate.get("ServiceSetting.SelectServiceSubClass")
            ]).subscribe(res => {
                msg = res[0] + "\n" + msg;
            });

        }
        if (this.input.ServiceProject == "") {
            forkJoin([
                this.translate.get("ServiceSetting.InputServiceProject")
            ]).subscribe(res => {
                msg = res[0] + "\n" + msg;
            });

        }
        if (this.input.ServiceProjectEn == "") {
            forkJoin([
                this.translate.get("ServiceSetting.InputServiceProjectEn")
            ]).subscribe(res => {
                msg = res[0] + "\n" + msg;
            });

        }
        if (this.input.OrganizerEmpno == "" || this.input.OrganizerEmpno == null) {
            forkJoin([
                this.translate.get("ServiceSetting.SelectOrganizer")
            ]).subscribe(res => {
                msg = res[0] + "\n" + msg;
            });

        }
        if (this.input.BackupOrganizerEmpno == "" || this.input.BackupOrganizerEmpno == null) {
            forkJoin([
                this.translate.get("ServiceSetting.SelectBackOrganizer")
            ]).subscribe(res => {
                msg = res[0] + "\n" + msg;
            });

        }
        if (msg != "") {
            this.baseHelper.ShowErrorMsg(msg);
            return;
        }


        this.serviceSetting.insertOrUpdate(this.input).subscribe(res => {
            this.ngOnInit();
            console.log(res);
        });

        this.displayDialog = false;
    }
}
