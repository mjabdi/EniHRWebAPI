import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Http, Response, Headers, RequestOptions, RequestMethod } from '@angular/http';
import { HttpHeaders } from '@angular/common/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { Observable} from 'rxjs';
import { catchError , map } from 'rxjs/operators';
import { User } from './user'

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
export class UserService {

  userUrl = baseUrl + '/api/user';
  handleError: HandleError;

    constructor(private http: HttpClient,httpErrorHandler: HttpErrorHandler) { 
        this.handleError = httpErrorHandler.createHandleError('UserService');
    }

    getAllUsers(onlyActive = false) : Observable<User[]> {

    let url = onlyActive? this.userUrl + "/active" : this.userUrl;

    return this.http.get<User[]>(url)
    .pipe(
    catchError(this.handleError('getAllUsers', []))
    );
    }


    findUser(username) : Observable<User | any[]> {

    return this.http.get<User>(this.userUrl + "/" + username)
    .pipe(
    catchError(this.handleError('findUser', []))
    );
    }


    updateUser(username , user)
    {
    var body = JSON.stringify(user);

    return this.http.put<User>(this.userUrl + '/' + username,
        body,
        httpOptions);
    }

    createNewUser(user)
    {
    var body = JSON.stringify(user);

    return this.http.post(this.userUrl,
        body,
        httpOptions);
    }

    deleteUser(username)
    {
    return this.http.delete(this.userUrl + "/" + username,
        httpOptions);
    }

}
