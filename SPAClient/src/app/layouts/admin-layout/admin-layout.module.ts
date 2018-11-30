import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AdminLayoutRoutes } from './admin-layout.routing';
import { UsersComponent } from '../../users/users.component';
import { HousingComponent } from '../../housing/housing.component';
import { HomeComponent } from '../../home/home.component';

import {EmployeesComponent} from '../../employees/employees.component';
import {MaterialModule} from '../../material/material.module';

import {AuthenticationService} from '../../services/authentication.service';

import {EmployeeDialogComponent} from '../../employee-dialog/employee-dialog.component'
import {HousingDialogComponent} from '../../housing-dialog/housing-dialog.component'


import { MomentModule } from 'angular2-moment';

import {LeaveComponent} from '../../leave/leave.component';
import {LeaveDialogComponent} from '../../leave-dialog/leave-dialog.component'

import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatMomentDateModule,MAT_MOMENT_DATE_ADAPTER_OPTIONS} from '@angular/material-moment-adapter'
import {ImportDataComponent} from '../../importdata/importdata.component';
import { SettingsComponent } from 'app/settings/settings.component';
import { DynamicTableComponent } from 'app/dynamictable/dynamictable.component';
import { ChangepasswordComponent } from 'app/changepassword/changepassword.component';
import { ICTComponent } from 'app/ict/ict.component';
import { ImportDataICTComponent } from 'app/importdata-ict/importdata-ict.component';
import { ICTDialogComponent } from 'app/ict-dialog/ict-dialog.component';



@NgModule({
  imports: [
    CommonModule,
    RouterModule.forChild(AdminLayoutRoutes),
    FormsModule,
    ReactiveFormsModule,
    MomentModule,
    MaterialModule,
    MatDatepickerModule,
    MatMomentDateModule
  ],
  declarations: [
    EmployeesComponent,
    HousingComponent,
    UsersComponent,
    LeaveComponent,
    HomeComponent,
    EmployeeDialogComponent,
    HousingDialogComponent,
    LeaveDialogComponent,
    ImportDataComponent,
    ImportDataICTComponent,
    SettingsComponent,
    DynamicTableComponent,
    ChangepasswordComponent,
    ICTComponent,
    ICTDialogComponent
  ],
  providers: [AuthenticationService,{ provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }],
  entryComponents: [EmployeeDialogComponent,HousingDialogComponent,LeaveDialogComponent,ICTDialogComponent]
})

export class AdminLayoutModule {}
