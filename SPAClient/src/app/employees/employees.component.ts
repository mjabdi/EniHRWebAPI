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

import {
  Overlay,
  OverlayConfig,
  OverlayContainer,
  OverlayRef,
  ScrollStrategy,
  ScrollStrategyOptions,
} from '@angular/cdk/overlay';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers : [EmployeeService,CountryService],
})
export class EmployeesComponent implements OnInit {

   employees : Employee[];

   today : Date;

  selectedEmployee: Employee;

   flagUrl = 'http://flagpedia.net/data/flags/h160/';
   imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/';


   dataSource: MatTableDataSource<Employee>;

   @ViewChild(MatSort) sort: MatSort;

   displayedColumns: string[] = ['ID','employeeID','name','surname','technicalID','localPlus','workingLocation'
                                ,'employeeCategory','organizationUnit','costCentre','position','professionalArea'
                                ,'companyHiringDate','homeCompany','yearsInEni','gender','birthDate','age'
                                ,'countryOfBirth','nationality','familyStatus','followingPartner','followingChildren'
                                ,'spouseNationality','typeOfVisa','visaExpiryDate','emailAddress','activityStatus'
                                ];


                                

  constructor(private router: Router,private employeeService : EmployeeService
    ,private dialog: MatDialog
    ,private countryService : CountryService ) { 
    this.today = new Date();
  }

  ngOnInit() {
    this.getAllEmployees(); 
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
}

