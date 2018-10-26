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

    getLocalPlusTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'localplustable');
    }

    UpdateLocalPlusTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'localplustable',
          body,
          httpOptions);
    }



    getWorkingLocation() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'workinglocation');
    }

    getWorkingLocationTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'workinglocationtable');
    }

    UpdateWorkingLocationTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'workinglocationtable',
          body,
          httpOptions);
    }

    getEmployeeCategory() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'employeecategory');
    }

    getEmployeeCategoryTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'employeecategorytable');
    }

    UpdateEmployeeCategoryTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'employeecategorytable',
          body,
          httpOptions);
    }

    getOrganizationUnit() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'organizationunit');
    }

    getOrganizationUnitTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'organizationunittable');
    }

    UpdateOrganizationUnitTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'organizationunittable',
          body,
          httpOptions);
    }

    getCostCentre() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'costcentre');
    }

    getCostCentreTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'costcentretable');
    }

    UpdateCostCentreTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'costcentretable',
          body,
          httpOptions);
    }

    getPosition() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'position');
    }

    getPositionTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'positiontable');
    }

    UpdatePositionTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'positiontable',
          body,
          httpOptions);
    }

    getProfessionalArea() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'professionalarea');
    }

    getProfessionalAreaTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'professionalareatable');
    }

    UpdateProfessionalAreaTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'professionalareatable',
          body,
          httpOptions);
    }

    getHomeCompany() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'homecompany');
    }

    getHomeCompanyTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'homecompanytable');
    }

    UpdateHomeCompanyTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'homecompanytable',
          body,
          httpOptions);
    }

    getCountry() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'country');
    }

    getCountryTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'countrytable');
    }

    UpdateCountryTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'countrytable',
          body,
          httpOptions);
    }

    getFamilyStatus() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'familystatus');
    }

    getFamilyStatusTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'familystatustable');
    }

    UpdateFamilyStatusTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'familystatustable',
          body,
          httpOptions);
    }

    getTypeofVisa() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'typeofvisa');
    }

    getTypeofVisaTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'typeofvisatable');
    }

    UpdateTypeofVisaTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'typeofvisatable',
          body,
          httpOptions);
    }

    getActivityStatus() : Observable<string[]> {
        return this.http.get<string[]>(this.baseUrl + 'activitystatus');
    }

    getActivityStatusTable() : Observable<any[]> {
        return this.http.get<any[]>(this.baseUrl + 'activitystatustable');
    }

    UpdateActivityStatusTable(record)  {
        var body = JSON.stringify(record);

        return this.http.put<any>(this.baseUrl + 'activitystatustable',
          body,
          httpOptions);
    }

}