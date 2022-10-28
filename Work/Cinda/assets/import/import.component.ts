import { Component, OnInit } from '@angular/core';
import { BaseHelper } from 'src/app/_helpers/base-helper';
import { HighPermissionFormService } from 'src/app/services/high-permission-form.service';
import { BaseComponent } from '../../base/BaseComponent';
@Component({
    selector: 'app-import',
    templateUrl: './import.component.html',
    styleUrls: ['./import.component.css']
})
export class ImportComponent extends BaseComponent implements OnInit {
    UploadFile;
    fileName;
    downloadFile;
    constructor(
        private baseHelper: BaseHelper,
        private highPermissionFormService: HighPermissionFormService
    ) {
        super();
    }

    ngOnInit(): void {
        this.highPermissionFormService.GetFileDownloadPath().subscribe(res => {
            this.downloadFile = res + 'HighPermissionAccount.xlsx';
        })
    }

    uploadHandler(event: string[]) {
        if (event.length = 1) {
            //this.fileName = event[0];
            this.fileName = event[0].split('/').pop();
            if (event.length > 0) {
                this.UploadFile = event.join(',');
            } else {
                this.UploadFile = '';
            }
        }
    }
    ImportXLSX() {
        console.log('★★★ this.fileName ★★★', this.downloadFile);
        if (this.fileName) {
            if (confirm('確定要將資料表資料刪除，並將整份xlsx檔案匯入嗎?')) {
                this.highPermissionFormService.ImportXLSX(this.fileName).subscribe(res => {
                    if (res.StatusCode === this.errorCode.Success) {
                        this.baseHelper.ShowSuccessMsg(this.baseHelper.GetTranslateValue('Form.Success'));
                    } else {
                        this.baseHelper.ShowErrorMsgByStatusCode(res.StatusCode);
                    }
                });
            }
        } else {
            let EroMsg = this.baseHelper.GetTranslateValue('Button.confirm') + this.baseHelper.GetTranslateValue('file-upload.file-upload');
            this.baseHelper.ShowErrorMsg(EroMsg);
        }
    }
}
