import {
    Component,
    OnInit,
    ViewEncapsulation,
    Input,
    EventEmitter,
    Output,
    OnChanges
} from '@angular/core';
import { Location, DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { SignHeader } from '../../../models/SignHeader';
import { TranslateService } from '@ngx-translate/core';
import { EditStateService } from 'src/app/services/edit-state.service';
import { TimeService } from '../../../services/time.service';
import SignData from 'src/app/models/SignData';
import { MenuStateService } from 'src/app/services/menu-state.service';
import { VIPMaintainService } from '../../../services/vipmaintain.service';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { SignBaseService } from 'src/app/services/sign-base.service';
import { BaseComponent } from '../../base/BaseComponent';
import { GLEmployee } from 'src/app/models/GLEmployee';

@Component({
    selector: 'app-sign-header',
    templateUrl: './sign-header.component.html',
    styleUrls: ['./sign-header.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class SignHeaderComponent extends BaseComponent implements OnInit, OnChanges {
    @Input() signFormID: number; // 從待審清單來
    @Input() signFormName: string; // 從外部系統來
    @Input() isEdit = true;
    @Input() isClose = false;
    @Input() forceStatus: string;//強制設定狀態

    @Input() ApplicationCategory: string;
    // @Output() outputLeave = new EventEmitter<any>();
    @Output() outputEdit = new EventEmitter();
    @Output() outputSave = new EventEmitter<any>();
    @Output() outputSend = new EventEmitter<any>();
    @Output() outputapprove = new EventEmitter<any>();
    @Output() outputreject = new EventEmitter<any>();
    @Output() outputrejected = new EventEmitter<any>();
    @Output() outputClose = new EventEmitter<any>();
    @Output() outputChangeApprover = new EventEmitter<any>();
    @Output() outputInvalid = new EventEmitter<any>();

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private location: Location,
        private translate: TranslateService,
        private editstate: EditStateService,
        private signService: SignBaseService,
        private timeservice: TimeService,
        public menuState: MenuStateService,
        private VIPMaintain: VIPMaintainService,
        public baseHelper: BaseHelper,
    ) {
        super();
        this.bIsAdmin = this.isSWAdm();

        editstate.OKEvent.subscribe(x => {
            this.location.back();
        });
        editstate.CancelEvent.subscribe(x => {

        });
    }
    data = {} as SignHeader;
    signdata = {} as SignData;
    redata = {} as SignData;
    apptype: string;
    IsSignatory = false;
    displayDialog = false;
    actionType = '';
    bIsAdmin = false;

    displayChangeDialog = false;
    empdisplay = false;
    empIndex = 0;

    // Sign Button On/Off Config
    isSaveButton = false;   // 儲存
    isSendButton = false;   // 送件
    isApproveButton = false;    // 核准
    isApprovedButton = false;   // 核定
    isRejectButton = false;     // 回上一關
    isRejectedButton = false;   // 拒絕
    isClosedButton = false;     // 結案
    isInvalidButton = false;    // 作廢

    saveButtonKey = '';
    sendButtonKey = '';
    approveButtonKey = '';
    approvedButtonKey = '';
    rejectButtonKey = '';
    rejectedButtonKey = '';
    closedButtonKey = '';
    invalidButtonKey = '';

    ngOnInit() {
        this.data.Filler = localStorage.getItem('sam_UserName'); // 填寫人
        this.data.FillTime = new Date(); // 填寫時間
        this.data.FormType = 'Draft';
        this.data.SignFormNO = '0';
        this.data.ShowMode = 1;
        this.translate
            .get(`Type.${this.ApplicationCategory}`)
            .subscribe((res: string) => {
                this.apptype = res;
            }); // 傳入的softward轉乘語系對照
        this.editstate.IsEdit = false;
        this.editstate.IsSave = false;
        this.SignButtonSetting(this.data.FormType);
        console.log(this.data, 'data');

    }


    ngOnChanges(changes) {
        console.log(changes, 'changes');

        for (const propertyName in changes) {
            if (propertyName === 'signFormID') {
                const change = changes[propertyName];
                const cur = JSON.stringify(change.currentValue);
                const prev = JSON.stringify(change.previousValue);

                if (cur !== prev) {
                    this.signService.GetSignform(this.signFormID).subscribe(res => {
                        if (res.SignFormNO) {
                            this.data.FillTime = res.FillTime;
                            this.data.SignFormNO = res.SignFormNO;
                            this.data.FormType = res.FormType;
                            this.data.ServiceType = res.ServiceType;
                            this.data.Signatory = res.Signatory;
                            this.data.ShowMode = res.ShowMode;
                            this.data.RoleId = res.RoleId;
                            this.data.FlowStatus = res.FlowStatus;
                            this.data.Filler = res.Filler;
                            this.data.ExecutorsID = res.ExecutorsID;
                            this.data.ExecutorsName = res.ExecutorsName;
                            this.data.ExecutorsDept = res.ExecutorsDept;
                            this.data.ShiftsName = [];
                            this.data.ShiftsID = [];
                            this.data.ShiftsDept = [];

                            this.SignButtonSetting(this.data.FormType);
                            if (this.data.FormType === 'Closed') {
                                this.isEdit = false;
                            }
                            this.outputEdit.emit(this.isEdit);
                        }
                    });
                }
            }
        }

    }

    SignButtonSetting(formStatus: string) {
        const buttons: string[] = ['Save', 'Send', 'Approve', 'Reject', 'Rejected', 'Closed', 'Invalid'];
        buttons.forEach(btn => {
            const key = 'SignButton.' + formStatus + '.' + btn;
            const text = this.baseHelper.GetTranslateValue(key);
            if (text === key) {
                this.signButtonDisableSetting(btn, key);
            } else {
                this.signButtonKeySetting(btn, key);
            }
        });
    }

    signButtonDisableSetting(btn: string, key: string) {
        switch (btn) {
            case 'Save':
                this.isSaveButton = false;
                break;
            case 'Send':
                this.isSendButton = false;
                break;
            case 'Approve':
                this.isApproveButton = false;
                this.approveButtonKey = key;
                if (this.data.FormType === 'CaseOfficerMgrCosign') {
                    if (this.data.Signatory === this.data.ExecutorsName.pop()) {
                        key = 'SignButton.CaseOfficerMgrCosign.Approved';
                        this.approveButtonKey = key;
                    }
                }
                break;
            case 'Reject':
                this.isRejectButton = false;
                break;
            case 'Rejected':
                this.isRejectButton = false;
                break;
            case 'Closed':
                this.isClosedButton = false;
                break;
            case 'Invalid':
                this.isInvalidButton = false;
                break;
        }
    }

    signButtonKeySetting(btn: string, key: string) {
        switch (btn) {
            case 'Save':
                this.isEdit = this.data.ShowMode === 1;
                this.isSaveButton = this.data.ShowMode === 1;
                this.saveButtonKey = key;
                break;
            case 'Send':
                this.isEdit = this.data.ShowMode === 1;
                this.isSendButton = this.data.ShowMode === 1;
                this.sendButtonKey = key;
                break;
            case 'Approve':
                this.isEdit = false;
                this.isApproveButton = this.data.ShowMode === 2;
                this.approveButtonKey = key;
                break;
            case 'Reject':
                this.isEdit = false;
                this.isRejectButton = this.data.ShowMode === 2;
                this.rejectButtonKey = key;
                break;
            case 'Rejected':
                this.isEdit = false;
                this.isRejectedButton = this.data.ShowMode === 2;
                this.rejectedButtonKey = key;
                break;
            case 'Closed':
                this.isEdit = false;
                this.isClosedButton = this.data.ShowMode === 2;
                this.closedButtonKey = key;
                break;
            case 'Invalid':
                this.isEdit = false;
                this.isInvalidButton = this.data.ShowMode === 2;
                this.invalidButtonKey = key;
                break;
        }
    }


    Leave() {
        this.router.navigate(['/']);
        // this.editstate.CheckOut();
    }

    Edit() {
        this.outputEdit.emit(this.isEdit);
        this.editstate.IsSave = false;
        this.editstate.IsEdit = true;
    }

    Save() {
        this.data.SignButtonKey = this.saveButtonKey;
        this.outputSave.emit(this.data);
        this.editstate.IsEdit = false;
        this.editstate.IsSave = true;
    }

    Send() {
        this.data.SignButtonKey = this.sendButtonKey;
        this.data.FormType = 'SignOff';
        this.outputSend.emit(this.data);
        this.editstate.IsEdit = false;
        this.editstate.IsSave = true;
    }

    Close() {
        this.data.SignButtonKey = this.closedButtonKey;
        this.data.FormType = 'Close';
        this.outputClose.emit(this.data);
    }

    CheckButton(val: string) {
        return true;
    }

    showCommentDialog(actionType: string) {
        this.displayDialog = true;
        this.actionType = actionType;
    }

    GetCommentDescription() {
        switch (this.actionType) {
            case 'Approve':
                return this.baseHelper.GetTranslateValue('Sign.SignComment')
                    .replace('{}', `<span class="approve">${this.baseHelper.GetTranslateValue(this.approveButtonKey)}</span>`);
            case 'Rejected':
                return this.baseHelper.GetTranslateValue('Sign.SignComment')
                    .replace('{}', `<span class="reject">${this.baseHelper.GetTranslateValue(this.rejectedButtonKey)}</span>`);
            case 'Reject':
                return this.baseHelper.GetTranslateValue('Sign.SignComment')
                    .replace('{}', `<span class="reject">${this.baseHelper.GetTranslateValue(this.rejectButtonKey)}</span>`);
            case 'Approved':
                return this.baseHelper.GetTranslateValue('Sign.SignComment')
                    .replace('{}', `<span class="approve">${this.baseHelper.GetTranslateValue(this.approvedButtonKey)}</span>`);
            case 'Closed':
                return this.baseHelper.GetTranslateValue('Sign.SignComment')
                    .replace('{}', `<span class="close">${this.baseHelper.GetTranslateValue(this.closedButtonKey)}</span>`);
            case 'ChangeApprover':
                return '*';
            case 'Invalid':
                return this.baseHelper.GetTranslateValue('Sign.SignComment')
                    .replace('{}', `<span class="invalid">${this.baseHelper.GetTranslateValue(this.invalidButtonKey)}</span>`);
        }
    }

    ColseDialog() {
        this.displayDialog = false;
    }
    confirm_OK() {// 意見送出
        switch (this.actionType) {
            case 'Approve':
                this.approve();
                break;
            case 'Rejected':
                this.rejected();
                break;
            case 'Reject':
                this.reject();
                break;
            case 'Approved':
                this.approve();
                break;
            case 'Closed':
                this.Close();
                break;
            case 'ChangeApprover':
                let vCount = 0;
                this.data.ShiftsID.forEach(element => {
                    vCount++;
                });

                if (this.data.ExecutorsID.length !== vCount) {
                    alert(this.baseHelper.GetTranslateValue('Sign.Signatory') + this.baseHelper.GetTranslateValue('Sign.required'));
                    return;
                }

                if (this.data.Comment == null || this.data.Comment === '') {
                    alert(this.baseHelper.GetTranslateValue('Sign.ChangeExplain') + this.baseHelper.GetTranslateValue('Sign.required'));
                    return;
                }
                this.ChangeApprover();
                break;
            case 'Invalid':
                this.invalid();
                break;
        }
    }
    approve() {
        this.data.SignButtonKey = this.approveButtonKey;
        this.outputapprove.emit(this.data);
    }
    rejected() {
        this.data.SignButtonKey = this.rejectedButtonKey;
        this.outputrejected.emit(this.data);
    }
    reject() {
        this.data.SignButtonKey = this.rejectButtonKey;
        this.outputreject.emit(this.data);
    }
    invalid() {
        this.data.SignButtonKey = this.invalidButtonKey;
        this.outputInvalid.emit(this.data);
    }

    ChangeApprover() {
        this.outputChangeApprover.emit(this.data);
    }

    ColseChangeDialog() {
        this.displayChangeDialog = false;
    }

    showChangeDialog(actionType: string) {
        this.displayChangeDialog = true;
        this.actionType = actionType;
    }

    emp_showDialog(pempIndex: number) {
        this.empIndex = pempIndex;
        this.empdisplay = true;
    }
    emp_Add(data: GLEmployee) {
        this.data.ShiftsName[this.empIndex] = data.empName;
        this.data.ShiftsID[this.empIndex] = data.empNo;
        this.data.ShiftsDept[this.empIndex] = data.deptNo;

        this.empdisplay = false;
    }

    emp_ColseDialog(event) {
        this.empdisplay = false;
    }
}
