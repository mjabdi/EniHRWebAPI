import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { Observable} from 'rxjs';
import { catchError , map } from 'rxjs/operators';
import { Housing } from './housing'

import { environment } from 'environments/environment';

import { HttpErrorHandler, HandleError } from '../http-error-handler.service';
import { HomeAddress } from 'app/history-address-dialog/homeaddress';


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
export class HousingService {

  housingrUrl = baseUrl + '/api/housing';
  handleError: HandleError;


  constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
    this.handleError = httpErrorHandler.createHandleError('HousingService');
}


getAllHousing(onlyActive = false) : Observable<Housing[]> {

  let url = onlyActive? this.housingrUrl + "/active" : this.housingrUrl;

  return this.http.get<Housing[]>(url)
.pipe(
  catchError(this.handleError('getAllHousing', []))
);
}


updateHousing(id , housing)
{
  var body = JSON.stringify(housing);

  return this.http.put<Housing>(this.housingrUrl + '/' + id,
    body,
    httpOptions);
}

getAddressHistory(id : number)
{
  return this.http.get<HomeAddress[]>(this.housingrUrl + '/addresshistory/' + id);
}


}