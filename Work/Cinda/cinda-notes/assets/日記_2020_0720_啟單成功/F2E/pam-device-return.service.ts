import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { Observable } from 'rxjs';
import { PageQueryResult } from '../models/PageQueryResult';

import SignData from '../models/SignData';

@Injectable({
    providedIn: 'root'
})

export class PamDeviceReturnService extends ServiceBase {

    private baseUrl = `${this.apiBaseUrl}/DeviceReturnService`;

    getAll(): Observable<PageQueryResult<any>> {
        const url = `${this.baseUrl}/GetAll`;
        return this.http.
            post<any>(
                url,
                this.httpOptions);
    }

    getDetail(SignFormId: string): Observable<PageQueryResult<any>> {
        const url = `${this.baseUrl}/GetDetail`;
        // console.log('SignFormId' + SignFormId);
        return this.http.
            post<any>(
                url,
                SignFormId,
                this.httpOptions);
    }

    Create(signData: SignData): Observable<PageQueryResult<any>> {
        const url = `${this.baseUrl}/Create`;
        console.log('★★★ signData ★★★', signData);
        return this.http.
            post<any>(
                url,
                signData,
                this.httpOptions);
    }

    Update(formDate: any): Observable<any> {
        const url = `${this.baseUrl}/Update`;
        console.log('★★★ formDate ★★★', formDate);
        return this.http.
            post<any>(
                url,
                formDate,
                this.httpOptions);
    }
}
