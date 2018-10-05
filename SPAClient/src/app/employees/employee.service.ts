import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { Observable} from 'rxjs';
import { catchError , map } from 'rxjs/operators';
import { Employee } from './employee'

import { environment } from 'environments/environment';

import { HttpErrorHandler, HandleError } from '../http-error-handler.service';


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
export class EmployeeService {

  employeerUrl = baseUrl + '/api/employee';
  handleError: HandleError;


  constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
    this.handleError = httpErrorHandler.createHandleError('EmployeeService');
}



getAllEmployees(onlyActive = false) : Observable<Employee[]> {

  let url = onlyActive? this.employeerUrl + "/active" : this.employeerUrl;

  return this.http.get<Employee[]>(url)
.pipe(
  catchError(this.handleError('getAllEmployees', []))
);
}


findEmployee(id) : Observable<Employee | any[]> {

  return this.http.get<Employee>(this.employeerUrl + "/" + id)
.pipe(
  catchError(this.handleError('findEmployee', []))
);
}


getMaxEmployeeNumber() : Observable<number | any[]>
{
  return this.http.get<number>(this.employeerUrl + "/max").pipe(
    catchError(this.handleError('getMaxEmployee', []))
  ); 
}


updateEmployee(id , emp)
{
  var body = JSON.stringify(emp);

  return this.http.put<Employee>(this.employeerUrl + '/' + id,
    body,
    httpOptions);
}

createNewEmployee(emp)
{
  var body = JSON.stringify(emp);

  return this.http.post(this.employeerUrl,
    body,
    httpOptions);
}

deleteEmployee(id)
{
  return this.http.delete(this.employeerUrl + "/" + id,
    httpOptions);
}

getCompleteness() : Observable<number | any[]>
{
  return this.http.get<number>(this.employeerUrl + "/completeness").pipe(
    catchError(this.handleError('getCompleteness', []))
  ); 
}

getActiveCount() : Observable<number | any[]>
{
  return this.http.get<number>(this.employeerUrl + "/activecount").pipe(
    catchError(this.handleError('getActiveCount', []))
  ); 
}


}
