import { Injectable } from '@angular/core';
import { ServiceBase } from '../../services/service-base';
import { Observable } from 'rxjs';
import { PageQueryResult } from '../../models/PageQueryResult';
import { SELF_ACCOUNT_CHECK } from './self-account-check.component';

@Injectable({
    providedIn: 'root'
})
export class SelfAccountCheckService extends ServiceBase {

    private baseUrl = `${this.apiBaseUrl}/SelfAccountCheckService`;

    CallPAM604(): Observable<PageQueryResult<string>> {
        const url = `${this.baseUrl}/CallPAM604`;
        const data = {};
        let body = JSON.stringify(data);
        return this.http.
            post<PageQueryResult<string>>(
                url,
                body,
                this.httpOptions);
    }

    GetSelfAccountCheck(): Observable<PageQueryResult<SELF_ACCOUNT_CHECK>> {
        const url = `${this.baseUrl}/GetSelfAccountCheck`;
        const data = {};
        let body = JSON.stringify(data);
        return this.http.
            post<PageQueryResult<SELF_ACCOUNT_CHECK>>(
                url,
                body,
                this.httpOptions);
    }

    SendMail(data: string[]): Observable<PageQueryResult<string[]>> {
        const url = `${this.baseUrl}/PAM604SendMail`;
        let body = JSON.stringify(data);
        return this.http.
            post<PageQueryResult<string[]>>(
                url,
                body,
                this.httpOptions);
    }

    // GetAssetList(query: PageQuery<number>, pStrEmpNo: string, pStrType: string): Observable<PageQueryResult<AsseetList>> {
    //     const url = `${this.baseUrl}/PAM604`;
    //     const data = {
    //         'query': query,
    //         'pStrEMPNo': pStrEmpNo,
    //         "pStrType": pStrType
    //     };
    //     let body = JSON.stringify(data);
    //     return this.http.
    //         post<PageQueryResult<AsseetList>>(
    //             url,
    //             body,
    //             this.httpOptions);
    // }

}
