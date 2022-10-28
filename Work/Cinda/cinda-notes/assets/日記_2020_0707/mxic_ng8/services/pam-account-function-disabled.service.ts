import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { Observable } from 'rxjs';
import { PageQueryResult } from '../models/PageQueryResult';
import AccountRequest from '../models/Request/AccountRequest';
import PAMAccount from '../models/PAMAccount';
import AccountFunctionDisabled from '../models/AccountFunctionDisabled';
@Injectable({
    providedIn: 'root'
})
export class PamAccounFunctionDisabledService extends ServiceBase {
    private baseUrl = `${this.apiBaseUrl}/AccountFunctionDisabledService`;
    getAll(): Observable<PageQueryResult<AccountFunctionDisabled>> {
        const url = `${this.baseUrl}/GetAll`;
        return this.http.
            post<any>(
                url,
                this.httpOptions);
    }
}
