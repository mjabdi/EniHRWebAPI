import { Routes} from '@angular/router';

import { EmailValidator } from '../../../../node_modules/@angular/forms';
import { EmployeesComponent } from '../../employees/employees.component';
import {UsersComponent} from '../../users/users.component';
import {HousingComponent } from '../../housing/housing.component'
import { HomeComponent } from '../../home/home.component';
import { LeaveComponent } from '../../leave/leave.component';

export const AdminLayoutRoutes: Routes = [

    { path: 'employees',        component: EmployeesComponent },
    { path: 'users',        component: UsersComponent },
    { path: 'housing',        component: HousingComponent },
    { path: 'leave',        component: LeaveComponent },
    { path: 'home',        component: HomeComponent }


];
