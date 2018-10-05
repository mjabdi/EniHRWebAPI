import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { Observable} from 'rxjs';
import { catchError , map } from 'rxjs/operators';
import { Leave } from './leave'

import { environment } from 'environments/environment';

import { HttpErrorHandler, HandleError } from '../http-error-handler.service';
import { LeaveSearch } from './leavesearch';


const baseUrl :string = environment.apiUrl;

const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json',
      'Accept':  'application/json'
    })
  };


@Injectable({
  providedIn: 'root'
})
export class LeaveService {

  leaveUrl = baseUrl + '/api/leave';
  handleError: HandleError;


  constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
    this.handleError = httpErrorHandler.createHandleError('LeaveService');
}


getAllLeave() : Observable<Leave[]> {

  return this.http.get<Leave[]>(this.leaveUrl)
.pipe(
  catchError(this.handleError('getAllLeave', []))
);
}

Search(criteria) : Observable<Leave[]> {

  var body = JSON.stringify(criteria);

  return this.http.post<Leave[]>(this.leaveUrl + '/search' ,
    body,
    httpOptions);
}


getLeaveTypes() :  Observable<string[]> {

    return this.http.get<string[]>(this.leaveUrl + "/types")
  .pipe(
    catchError(this.handleError('getLeaveTypes', []))
  );
 
}


updateLeave(id , leave)
{
  var body = JSON.stringify(leave);

  return this.http.put<Leave>(this.leaveUrl + '/' + id,
    body,
    httpOptions);
}

createLeave(leave)
{
  var body = JSON.stringify(leave);

  return this.http.post<Leave>(this.leaveUrl ,
    body,
    httpOptions);
}




}


