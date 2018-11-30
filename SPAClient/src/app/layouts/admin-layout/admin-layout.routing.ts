import { Routes} from '@angular/router';

import { EmailValidator } from '../../../../node_modules/@angular/forms';
import { EmployeesComponent } from '../../employees/employees.component';
import {UsersComponent} from '../../users/users.component';
import {HousingComponent } from '../../housing/housing.component'
import { HomeComponent } from '../../home/home.component';
import { LeaveComponent } from '../../leave/leave.component';
import {ImportDataComponent} from '../../importdata/importdata.component';
import { SettingsComponent } from 'app/settings/settings.component';
import { ChangepasswordComponent } from 'app/changepassword/changepassword.component';
import { ICTComponent } from 'app/ict/ict.component';


export const AdminLayoutRoutes: Routes = [

    { path: 'employees',        component: EmployeesComponent },
    { path: 'users',        component: UsersComponent },
    { path: 'housing',        component: HousingComponent },
    { path: 'leave',        component: LeaveComponent },
    { path: 'ict',        component: ICTComponent },
    { path: 'home',        component: HomeComponent },
    { path: 'importdata',        component: ImportDataComponent },
    { path: 'settings',        component: SettingsComponent },
    {path : 'changepassword' , component: ChangepasswordComponent}
];
