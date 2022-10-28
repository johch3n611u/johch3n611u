import { Injectable } from '@angular/core';
import { ServiceBase } from './service-base';
import { Observable } from 'rxjs';
import CrossCenter from '../models/CrossCenter';
import ResignationList from '../models/ResignationList';
import ProcessRecord from '../models/ProcessRecord';

@Injectable({
    providedIn: 'root'
})
export class ResignationDisabledService extends ServiceBase {
    private baseUrl = `${this.apiBaseUrl}/ResignationDisableService`;
    getCrossCenter(): Observable<CrossCenter[]> {
        const url = `${this.baseUrl}/GetCrossCenter`;
        return this.http.
            post<CrossCenter[]>(
                url,
                this.httpOptions);
    }
    getDisableList(): Observable<ResignationList> {
        const url = `${this.baseUrl}/GetDisableList`;
        return this.http.
            post<ResignationList>(
                url,
                this.httpOptions);
    }
    OrgazizationChange(model: CrossCenter): Observable<boolean> {
        const url = `${this.baseUrl}/OrgazizationChange`;
        return this.http.
            post<boolean>(
                url,
                model,
                this.httpOptions);
    }
    CrossCenter(model: CrossCenter): Observable<boolean> {
        const url = `${this.baseUrl}/CrossCenter`;
        return this.http.
            post<boolean>(
                url,
                model,
                this.httpOptions);
    }
    getResignationList(): Observable<ResignationList[]> {
        const url = `${this.baseUrl}/getResignationList`;
        return this.http.
            post<ResignationList[]>(
                url,
                this.httpOptions);
    }
    getProcessRecord(): Observable<ProcessRecord[]> {
        const url = `${this.baseUrl}/getProcessRecord`;
        return this.http.
            post<ProcessRecord[]>(
                url,
                this.httpOptions);
    }
}
