import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { HashLocationStrategy, LocationStrategy } from '@angular/common';

import { HttpClientModule } from '@angular/common/http'; 


import { AppRoutingModule} from './app.routing';
import {AuthGuard} from './services/AuthGuard';
import { ComponentsModule } from './components/components.module';

import { AppComponent } from './app.component';


import { AlertService} from './services/alert.service';
import  {UserService } from './services/user.service';

import {EmployeesComponent} from './employees/employees.component';

import { HttpErrorHandler }   from './http-error-handler.service';

import { MessageService } from './message.service';

import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import {LoginComponent} from './login/login.component';

import {MaterialModule} from './material/material.module'

import {AuthenticationService} from './services/authentication.service';
import {BaseInfoService} from './services/baseInfo.service';

import { MomentModule } from 'angular2-moment';


import { ToastrModule } from 'ngx-toastr';

import { HTTP_INTERCEPTORS } from '@angular/common/http';

import {AuthInterceptor} from './http-interceptors/auth-interceptor'

@NgModule({
   imports: [
      BrowserAnimationsModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      HttpModule,
      ComponentsModule,
      RouterModule,
      AppRoutingModule,
      MaterialModule,
      MomentModule,
      ToastrModule.forRoot()
      ],
   declarations: [
      AppComponent,
      AdminLayoutComponent,
      LoginComponent
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    {provide: LocationStrategy, useClass: HashLocationStrategy},
    AuthGuard,
    UserService,
    HttpErrorHandler,
    MessageService,
    AuthenticationService,
    BaseInfoService],
  bootstrap: [AppComponent]
})
export class AppModule { }
