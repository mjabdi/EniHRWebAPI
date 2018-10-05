import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs';
import { catchError, map  } from 'rxjs/operators';
import { User } from '../models/index';

import { environment } from 'environments/environment';

import { HttpErrorHandler, HandleError } from '../http-error-handler.service';


const baseUrl :string = environment.apiUrl;

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Accept':  'application/json'
    })
  };


@Injectable()
export class BaseInfoService {

    baseUrl = baseUrl + '/api/baseinfo/';
    handleError: HandleError;
    
    constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
        this.handleError = httpErrorHandler.createHandleError('BaseInfoService');
    }

    getLocalPlus() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'localplus');
    }

    getWorkingLocation() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'workinglocation');
    }

    getEmployeeCategory() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'employeecategory');
    }

    getOrganizationUnit() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'organizationunit');
    }

    getCostCentre() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'costcentre');
    }

    getPosition() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'position');
    }

    getProfessionalArea() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'professionalarea');
    }

    getHomeCompany() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'homecompany');
    }

    getCountry() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'country');
    }

    getFamilyStatus() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'familystatus');
    }

    getTypeofVisa() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'typeofvisa');
    }

    getActivityStatus() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'activitystatus');
    }

}