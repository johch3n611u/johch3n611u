import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { Observable } from 'rxjs';
import { PageQueryResult } from '../models/PageQueryResult';
import AccountRequest from '../models/Request/AccountRequest';
import PAMAccount from '../models/PAMAccount';
import AccountFunctionDisabled from '../models/AccountFunctionDisabled';
import DisableDetail from '../models/DisableDetail';

@Injectable({
    providedIn: 'root'
})

export class PamAccounFunctionDisabledService extends ServiceBase {
    private baseUrl = `${this.apiBaseUrl}/AccountFunctionDisabledService`;
    getAll(): Observable<PageQueryResult<any>> {
        const url = `${this.baseUrl}/GetAll`;
        return this.http.
            post<any>(
                url,
                this.httpOptions);
    }
    getDetail(urlEmpNo: string): Observable<PageQueryResult<any>> {
        const url = `${this.baseUrl}/GetDetail`;
        console.log('urlEmpNo' + urlEmpNo);
        return this.http.
            post<any>(
                url,
                urlEmpNo,
                this.httpOptions);
    }
    Create(signform: any, form: any): Observable<PageQueryResult<any>> {
        const url = `${this.baseUrl}/Create`;
        console.log('Create');
        console.log(form);
        return this.http.
            post<any>(
                url,
                this.httpOptions);
    }
}
