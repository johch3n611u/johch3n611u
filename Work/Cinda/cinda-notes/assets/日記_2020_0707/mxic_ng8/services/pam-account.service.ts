import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { PageQuery } from '../models/PageQuery';
import { Observable } from 'rxjs';
import { PageQueryResult } from '../models/PageQueryResult';
import AccountRequest from '../models/Request/AccountRequest';
import PAMAccount from '../models/PAMAccount';

@Injectable({
  providedIn: 'root'
})
export class PamAccountService extends ServiceBase {

    private baseUrl = `${this.apiBaseUrl}/AccountService`;


    getAccounts(query: AccountRequest): Observable<PageQueryResult<PAMAccount>> {
        console.log(query);
        const url = `${this.baseUrl}/GetAccounts`;
        return this.http.
            post<PageQueryResult<PAMAccount>>(
                url,
                query,
                this.httpOptions);
    }
}
