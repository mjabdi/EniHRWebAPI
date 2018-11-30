import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { Observable} from 'rxjs';
import { catchError , map } from 'rxjs/operators';
import { ICT } from './ict'

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
export class ICTService {

  ICTrUrl = baseUrl + '/api/ict';
  handleError: HandleError;


  constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
    this.handleError = httpErrorHandler.createHandleError('ICTService');
}


getAllICT() : Observable<ICT[]> {

  let url = this.ICTrUrl ;

  return this.http.get<ICT[]>(url)
.pipe(
  catchError(this.handleError('getAllICT', []))
);
}


updateICT(id , ICT)
{
  var body = JSON.stringify(ICT);
  return this.http.put<ICT>(this.ICTrUrl + '/' + id,
    body,
    httpOptions);
}


}
