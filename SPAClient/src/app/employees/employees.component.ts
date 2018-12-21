import { Component, OnInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {EmployeeService} from './employee.service'
import { Employee } from './employee';
import {MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { DataSource } from '@angular/cdk/collections';
import { Observable, of } from 'rxjs';
import {MatDialog, MatDialogConfig} from "@angular/material";
import {EmployeeDialogComponent} from '../employee-dialog/employee-dialog.component';
import { ViewEncapsulation } from '@angular/core';
import {CountryService} from '../services/country.service';
import * as XLSX from 'xlsx';
import { DatePipe } from '@angular/common';

import {
  Overlay,
  OverlayConfig,
  OverlayContainer,
  OverlayRef,
  ScrollStrategy,
  ScrollStrategyOptions,
} from '@angular/cdk/overlay';
import { UserService } from 'app/users/userservice';
import { AuthenticationService } from 'app/services/authentication.service';
import { User } from 'app/users/user';

import { environment } from 'environments/environment';

const baseUrl :string = environment.apiUrl;

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers : [EmployeeService,CountryService],
})
export class EmployeesComponent implements OnInit {

   employees : Employee[];

   today : Date;

   

  selectedEmployee: Employee;

   flagUrl = 'http://flagpedia.net/data/flags/h160/';
   baseImageUrl = baseUrl + "/api/employee/images/";


   dataSource: MatTableDataSource<Employee>;

   @ViewChild(MatSort) sort: MatSort;

   displayedColumns: string[] = ['ID','employeeID','name','surname','technicalID','localPlus','workingLocation'
                                ,'employeeCategory','organizationUnit','costCentre','position','professionalArea'
                                ,'companyHiringDate','homeCompany','yearsInEni','gender','birthDate','age'
                                ,'countryOfBirth','nationality','familyStatus','followingPartner','followingChildren'
                                ,'spouseNationality','typeOfVisa','visaExpiryDate','emailAddress','activityStatus','terminationDate'
                                ];


                                

  constructor(private router: Router,private employeeService : EmployeeService
    ,private dialog: MatDialog
    ,private countryService : CountryService 
    ,private userService : UserService,private authService:AuthenticationService) { 
    this.today = new Date();
  }

  allowed = 0;
  user : User;
  random : number;
  ngOnInit() {
      this.getAllEmployees(); 

      this.userService.findUser(this.authService.getUsername()).subscribe(
        (data : User)=>
        {
          this.user = data;
          this.allowed = (this.user.roles.indexOf('employees') > -1) ? 1 : -1;
        }
      );

      this.random = this.getRandom();
  }

  
  export(): void {
    /* generate worksheet */

    var datePipe = new DatePipe("en-GB");

    var tt = datePipe.transform(Date(), 'dd-MM-yyyy');

    var fileName = "employees-" + tt +   ".xlsx";;
    
    var data = new Array<any[]>();

    
    data.push(this.displayedColumns);

    for (var i=0;i<this.dataSource.filteredData.length;i++)
    {
      data.push(this.makeArrayFromEmployee(i+1 , this.dataSource.filteredData[i]));
    }

		const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(data);

		/* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
		XLSX.utils.book_append_sheet(wb, ws, 'Employees');

		/* save to file */
    XLSX.writeFile(wb, fileName);
    
    }

    makeArrayFromEmployee(index : number, emp : Employee) : any[]
    {
        var arr = new Array<any>();

        arr.push(index);
        arr.push(emp.employeeID);
        arr.push(emp.name);
        arr.push(emp.surname);
        arr.push(emp.technicalID);
        arr.push(emp.localPlus);
        arr.push(emp.workingLocation);
        arr.push(emp.employeeCategory);
        arr.push(emp.organizationUnit);
        arr.push(emp.costCentre);
        arr.push(emp.position);
        arr.push(emp.professionalArea);
        arr.push(emp.companyHiringDate);
        arr.push(emp.homeCompany);
        arr.push(emp.yearsInEni);
        arr.push(emp.gender);
        arr.push(emp.birthDate);
        arr.push(emp.age);
        arr.push(emp.countryOfBirth);
        arr.push(emp.nationality);
        arr.push(emp.familyStatus);
        arr.push(emp.followingPartner);
        arr.push(emp.followingChildren);
        arr.push(emp.spouseNationality);
        arr.push(emp.typeOfVisa);
        arr.push(emp.visaExpiryDate);
        arr.push(emp.emailAddress);
        arr.push(emp.activityStatus);
        return arr;
    }



  doubleClick(row : Employee)
  {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  

    dialogConfig.data = row;

    const dialogRef = this.dialog.open(EmployeeDialogComponent,dialogConfig);


    dialogRef.afterClosed().subscribe(
      val => {
          if (val)
          {
            this.ngOnInit();
          }
          this.random = this.getRandom();
      }
  );

  }

  isLoading = true;
  getAllEmployees(): void {
    this.isLoading = true;
    //this.dataSource = null;
    this.employeeService.getAllEmployees(this.showOnlyActive)
      .subscribe(employees => {this.employees = employees;
        this.dataSource = new MatTableDataSource(this.employees);
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      });
  }

  CreateNewEmployee()
  {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  
    var newEmployee = new Employee();
    newEmployee.isNew = true;

    this.employeeService.getMaxEmployeeNumber().subscribe( (max : number) =>
      newEmployee.employeeID = max + 1
    );

    dialogConfig.data = newEmployee;

    const dialogRef = this.dialog.open(EmployeeDialogComponent,dialogConfig);


    dialogRef.afterClosed().subscribe(
        val => {
            if (val)
            {
              this.ngOnInit();
            }
        }
    );
  }

  private showOnlyActive : boolean = true;
  filterActiveEmployees(checked)
  {
    this.showOnlyActive = checked;
    this.ngOnInit();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getCountryCode(country) : string
  {
    return this.countryService.getCountryCode(country);
  }

  getRandom()
  {
    return Math.floor(Math.random() * (999999 - 100000)) + 100000;
  }
}

