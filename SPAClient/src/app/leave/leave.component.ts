import { Component, OnInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {LeaveService} from './leave.service'
import { Leave } from './leave';
import {MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { DataSource } from '@angular/cdk/collections';
import { Observable, of } from 'rxjs';
import {MatDialog, MatDialogConfig} from "@angular/material";
import { ViewEncapsulation } from '@angular/core';
import {FormControl} from "@angular/forms";

import {EmployeeService} from '../employees/employee.service';
import {Employee} from '../employees/employee';
import {LeaveSearch} from './leavesearch';


import { LeaveDialogComponent } from '../leave-dialog/leave-dialog.component';

import * as XLSX from 'xlsx';
import { DatePipe } from '@angular/common';
import { UserService } from 'app/users/userservice';
import { AuthenticationService } from 'app/services/authentication.service';
import { User } from 'app/users/user';


@Component({
    selector: 'app-leave',
    templateUrl: './leave.component.html',
    styleUrls: ['./leave.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers : [LeaveService],
  })
  export class LeaveComponent implements OnInit {

    employeeIDControl = new FormControl();

    employeeNameControl = new FormControl();
    employeeSurnameControl = new FormControl();
    fromDateControl = new FormControl();
    untilDateControl = new FormControl();
    leaveTypeControl = new FormControl();
    
    
    panelOpenState = false;

 

    leaves : Leave[];

    LeaveTypes : string[];

    today : Date;
 
   selectedLeave: Leave;
   imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/';
 
 
 
    dataSource: MatTableDataSource<Leave>;
 
    @ViewChild(MatSort) sort: MatSort;
 
    displayedColumns: string[] = ['employeeID','name','surname','localPlus','workingLocation','employeeCategory'
                                 ,'costCentre','familyStatus','followingPartner','followingChildren','activityStatus'
                                 ,'fromDate','untilDate' ,'countedDays','leaveType','comment','registeredDateTime'
                                 ];
 
 
                                 
 
   constructor(private router: Router,
    private leaveService : LeaveService,
    private employeeService : EmployeeService,
    private dialog: MatDialog
    ,private userService : UserService, private authService:AuthenticationService) { 
     this.today = new Date();
   }
 
   allowed = 0;
   user : User;
   ngOnInit() {
     this.employeeNameControl.disable();
     this.employeeSurnameControl.disable();
     this.leaveService.getLeaveTypes().subscribe(data => {
      this.LeaveTypes = data;
      });
     this.getAllLeave(); 

     this.userService.findUser(this.authService.getUsername()).subscribe(
      (data : User)=>
      {
        this.user = data;
        this.allowed = (this.user.roles.indexOf('leave') > -1) ? 1 : -1;
      }
    );

   }
 
   export(): void {
    /* generate worksheet */

    var datePipe = new DatePipe("en-GB");

    var tt = datePipe.transform(Date(), 'dd-MM-yyyy');

    var fileName = "leave-" + tt +   ".xlsx";;
    
    var data = new Array<any[]>();

    
    data.push(this.displayedColumns);

    for (var i=0;i<this.dataSource.filteredData.length;i++)
    {
      data.push(this.makeArrayFromEmployee(i+1 , this.dataSource.filteredData[i]));
    }

		const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(data);

		/* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
		XLSX.utils.book_append_sheet(wb, ws, 'Leave');

		/* save to file */
    XLSX.writeFile(wb, fileName);
    
    }

    makeArrayFromEmployee(index : number, leave : Leave) : any[]
    {
        var arr = new Array<any>();

        arr.push(leave.employeeID);
        arr.push(leave.name);
        arr.push(leave.surname);
        arr.push(leave.localPlus);
        arr.push(leave.workingLocation);
        arr.push(leave.employeeCategory);
        arr.push(leave.costCentre);
        arr.push(leave.familyStatus);
        arr.push(leave.followingPartner);
        arr.push(leave.followingChildren);
        arr.push(leave.activityStatus);
        arr.push(leave.fromDate);
        arr.push(leave.untilDate);
        arr.push(leave.countedDays);
        arr.push(leave.leaveType);
        arr.push(leave.comment);
        arr.push(leave.registeredDateTime);

        return arr;
    }



   doubleClick(row : Leave)
   {
 
    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  
    dialogConfig.data = row;

    const dialogRef = this.dialog.open(LeaveDialogComponent,dialogConfig);


    dialogRef.afterClosed().subscribe(
        val => {
            if (val)
            {
              this.ngOnInit();
            }
        }
    );

 
   }

   CreateNewLeave()
   {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  
    var newLeave = new Leave();

    newLeave.employeeID = null;
    newLeave.name = null;
    newLeave.surname = null;
    newLeave.localPlus = null;
    newLeave.workingLocation = null;
    newLeave.employeeCategory = null;
    newLeave.costCentre = null;
    newLeave.familyStatus = null;
    newLeave.followingPartner = null;
    newLeave.followingChildren = null;
    newLeave.activityStatus = null;
    
    newLeave.isNew = true;


    dialogConfig.data = newLeave;

    const dialogRef = this.dialog.open(LeaveDialogComponent,dialogConfig);


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
   getAllLeave(): void {
     this.isLoading = true;
     this.leaveService.getAllLeave()
       .subscribe(leave => {this.leaves = leave;
         this.dataSource = new MatTableDataSource(this.leaves);
         this.dataSource.sort = this.sort;
         this.isLoading = false;
       });
   }
 
 
   applyFilter(filterValue: string) {
     this.dataSource.filter = filterValue.trim().toLowerCase();
   }

   EmployeeIDChanged(event)
   {
      if (event.keyCode == 13){

       var val = this.employeeIDControl.value;

       if (val < 100000 )
       {
           val = val + 2442000000;
           this.employeeIDControl.setValue(val);
       }

       this.employeeService.findEmployee(val).subscribe((employee : Employee) =>
           {
               this.employeeNameControl.setValue(employee.name);
               this.employeeSurnameControl.setValue(employee.surname);
           },
           error => {
               this.employeeNameControl.setValue(null);
               this.employeeSurnameControl.setValue(null);
           });
       }
   }

   Search()
   {
      var criteria = new LeaveSearch();
      if (this.employeeNameControl.value)
      {
        criteria.employeeID = this.employeeIDControl.value;
      }


      criteria.fromDate = this.fromDateControl.value;
      criteria.untilDate = this.untilDateControl.value;

      criteria.leaveType = this.leaveTypeControl.value;

      this.isLoading = true;
      this.leaveService.Search(criteria)
        .subscribe(leave => {this.leaves = leave;
          this.dataSource = new MatTableDataSource(this.leaves);
          this.dataSource.sort = this.sort;
          this.isLoading = false;
        });
   }

   ResetSearch()
   {
      this.employeeIDControl.setValue(null);
      this.employeeNameControl.setValue(null);
      this.employeeSurnameControl.setValue(null);
      this.fromDateControl.setValue(null);
      this.untilDateControl.setValue(null);
      this.leaveTypeControl.setValue(null);
      this.Search();
  }

  calcTotalDays() : number
  {
    var total = 0;
    if (this.leaves)
    {
          this.leaves.forEach( item => {
            total = total + item.countedDays;
          } );
    }
    
    return total;
  }

  }