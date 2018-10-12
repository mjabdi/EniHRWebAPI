import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { Observable} from 'rxjs';
import { catchError , map } from 'rxjs/operators';

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
export class ImportDataService {

  validataDatarUrl = baseUrl + '/api/importdata/validatedata';
  applyChangesDatarUrl = baseUrl + '/api/importdata/applychanges';

  handleError: HandleError;


  constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
    this.handleError = httpErrorHandler.createHandleError('importdataError');
}



validateData(data)
{
  var body = JSON.stringify(data);

  return this.http.post<any>(this.validataDatarUrl,
    body,
    httpOptions);
}


applyChanges(data)
{
  var body = JSON.stringify(data);

  return this.http.post<any>(this.applyChangesDatarUrl,
    body,
    httpOptions);
}

}
