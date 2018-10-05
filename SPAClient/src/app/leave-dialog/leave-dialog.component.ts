import {Component, Inject, OnInit, ViewEncapsulation, Host} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {MatSnackBar} from '@angular/material';

import {Leave} from "../leave/leave";
import {FormControl} from "@angular/forms";
import * as moment from 'moment';
import {BaseInfoService} from '../services/baseInfo.service';
import {LeaveService} from '../leave/leave.service';
import {Employee} from '../employees/employee';

import { Observable } from 'rxjs'
import {map, startWith} from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import {MatDialog, MatDialogConfig} from "@angular/material";

import {EmployeeService} from '../employees/employee.service';


//import {DifferencePipe} from 'angular2-moment';




@Component({
    selector: 'leave-dialog',
    templateUrl: './leave-dialog.component.html',
    styleUrls: ['./leave-dialog.component.scss']
})
export class LeaveDialogComponent implements OnInit {

    today : Date ;

    isSubmit: boolean = false;

    leave : Leave;

    LeaveTypes : string[];

    
    imageUrl;

    employeeIDControl = new FormControl();
    employeeNameControl = new FormControl();
    employeeSurnameControl = new FormControl();
    localPlusControl = new FormControl();
    workingLocationControl = new FormControl();
    categoryControl = new FormControl();
    costCenterControl = new FormControl();
    familyStatusControl = new FormControl();
    followingPartnerControl = new FormControl();
    followingChildrenControl = new FormControl();
    activityStatusControl = new FormControl();

    daysControl = new FormControl();





    fromDateControl = new FormControl();
    untilDateControl = new FormControl();
    leaveTypeControl = new FormControl();
    countedDaysControl = new FormControl();
    commentControl = new FormControl();

    CreateMode : boolean = false;

    constructor(
        public dialog: MatDialog,
        public snackBar: MatSnackBar,
        private baseInfoService : BaseInfoService,
        private leaveService : LeaveService,
        private employeeService : EmployeeService,
        private toastrService : ToastrService,
        private dialogRef: MatDialogRef<LeaveDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {


            this.employeeNameControl.disable();
            this.employeeSurnameControl.disable();
            this.localPlusControl.disable();
            this.workingLocationControl.disable();
            this.categoryControl.disable();
            this.costCenterControl.disable();
            this.familyStatusControl.disable();
            this.followingPartnerControl.disable();
            this.followingChildrenControl.disable();
            this.activityStatusControl.disable();

            this.daysControl.disable();



            this.leaveService.getLeaveTypes().subscribe(data => {
            this.LeaveTypes = data;
            });
            this.leave = data;
            
            this.CreateMode = this.leave.isNew;
            this.today = new Date();
           // this.imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/' + this.leave.technicalID + '.jpg'

    }

    isloaded() : boolean {
       
        return this.isLoaded;
    }



    isLoaded : boolean = false;

    ngOnInit() {

         this.LoadFormValues();
         this.updateWorkingDays();
         this.isLoaded = true;
    }



    LoadFormValues()
    {
        this.employeeIDControl.setValue(this.leave.employeeID);
        this.employeeNameControl.setValue(this.leave.name);


        this.fromDateControl.setValue(this.leave.fromDate);
        this.untilDateControl.setValue(this.leave.untilDate);
        this.leaveTypeControl.setValue(this.leave.leaveType);
        this.countedDaysControl.setValue(this.leave.countedDays);
        this.commentControl.setValue(this.leave.comment);
    }


    save() {

        this.isSubmit = true;

        var newLeave = this.buildLeavefromForm();
        this.leaveService.updateLeave(this.leave.leaveID,newLeave)
        .subscribe(data => {
          this.toastrService.success(this.leave.name + ' Leave information has been updated successfully','Leave Info Updated');
          this.CopyLeave(this.leave,newLeave);
          this.dialogRef.close(false);
          this.isSubmit = false;  
          return true;  
        },
        error =>
        {
          this.toastrService.error('Sorry, something went wrong. Please try again','Server Error');
          this.isSubmit = false;
        }
      );

       // alert(JSON.stringify(newEmployee)); 
    }

    DateChenged(event)
    {
       var fromdate = this.fromDateControl.value;
       var untildate = this.untilDateControl.value;

       if (!fromdate || !untildate)
       {
            this.daysControl.setValue(null);
       }    
       else
       {
            this.daysControl.setValue(this.getBusinessDatesCount(fromdate,untildate)); 
            this.countedDaysControl.setValue(this.getBusinessDatesCount(fromdate,untildate)); 
       }
    }

    updateWorkingDays()
    {
        var fromdate = this.leave.fromDate;
        var untildate = this.leave.untilDate;

        if (!fromdate || !untildate)
        {
             this.daysControl.setValue(null);
        } 
        else{
            this.daysControl.setValue(this.getBusinessDatesCount(fromdate,untildate)); 
        }
    }


    
     getBusinessDatesCount(startDate :Date, endDate : Date ) : number {
        if (endDate < startDate)
        {
            alert('until date must be greater than or equal to start date');
            return 0;
        } 
        var count = 0;
        var curDate = new Date(startDate);
        var endDate2 = new Date(endDate);

        while (curDate <= endDate2) {
            var dayOfWeek = curDate.getDay();
            if(!((dayOfWeek == 6) || (dayOfWeek == 0)))
               count++;
            curDate.setDate(curDate.getDate() + 1);
        }
        return count;
    }



    createNew()
    {
        this.isSubmit = true;

        var newLeave = this.buildLeavefromForm();
        this.leaveService.createLeave(newLeave)
        .subscribe(data => {
          this.toastrService.success(this.employeeNameControl.value + ' Leave has been registerd successfully','Leave Registered');
          this.CopyLeave(this.leave,newLeave);
          this.dialogRef.close(true);
          this.isSubmit = false;  
          return true;  
        },
        error =>
        {
          this.toastrService.error('Sorry, something went wrong. Please try again','Server Error');
          this.isSubmit = false;
        }
      );

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
                this.localPlusControl.setValue(employee.localPlus);
                this.workingLocationControl.setValue(employee.workingLocation);
                this.categoryControl.setValue(employee.employeeCategory);
                this.costCenterControl.setValue(employee.costCentre);
                this.familyStatusControl.setValue(employee.familyStatus);
                this.followingPartnerControl.setValue(employee.followingPartner);
                this.followingChildrenControl.setValue(employee.followingChildren);
                this.activityStatusControl.setValue(employee.activityStatus);
                this.imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/' + employee.technicalID + '.jpg'


            },
            error => {
                this.employeeNameControl.setValue(null);
                this.employeeSurnameControl.setValue(null);
                this.localPlusControl.setValue(null);
                this.workingLocationControl.setValue(null);
                this.categoryControl.setValue(null);
                this.costCenterControl.setValue(null);
                this.familyStatusControl.setValue(null);
                this.followingPartnerControl.setValue(null);
                this.followingChildrenControl.setValue(null);
                this.activityStatusControl.setValue(null);
                this.imageUrl = '';

            });
        }
    }


    CopyLeave(ho1 : Leave, ho2 : Leave)
    {
        ho1.fromDate = ho2.fromDate;
        ho1.untilDate = ho2.untilDate;
        ho1.leaveType = ho2.leaveType;
        ho1.comment = ho2.comment;
        ho1.countedDays = ho2.countedDays;
    }

     buildLeavefromForm() : Leave
    {
        var newLeave = new Leave();

        newLeave.employeeID = this.employeeIDControl.value;
        newLeave.name = this.employeeNameControl.value;
        newLeave.surname = this.employeeSurnameControl.value;
        newLeave.localPlus = this.localPlusControl.value;
        newLeave.technicalID = this.leave.technicalID;
        newLeave.workingLocation = this.workingLocationControl.value;
        newLeave.employeeCategory = this.categoryControl.value;
        newLeave.costCentre = this.costCenterControl.value;
        newLeave.familyStatus = this.familyStatusControl.value;
        newLeave.followingPartner = this.followingPartnerControl.value;
        newLeave.followingChildren = this.followingChildrenControl.value;
        newLeave.activityStatus = this.activityStatusControl.value;

        newLeave.registeredDateTime = this.today;

        newLeave.fromDate = this.fromDateControl.value;
        newLeave.untilDate = this.untilDateControl.value;
        newLeave.leaveType = this.leaveTypeControl.value;
        newLeave.countedDays = this.countedDaysControl.value;
        newLeave.comment = this.commentControl.value;

        return newLeave;        
    }

    ValidateData() : boolean
    {
        if (!this.employeeNameControl.value)
            return false;
        if (!this.fromDateControl.value)
            return false;

        if (!this.untilDateControl.value)
            return false;

       if (!this.leaveTypeControl.value)
            return false;     
            
        if (!this.countedDaysControl.value)
            return false;

        if (!this.daysControl.value)
            return false;  
    
        if (this.daysControl.value < 1)
            return false;    

        return true;

    }

    close() {
        this.dialogRef.close(false);
    }

    private dofilter(originalList : string[], value: string): string[] {

        const filterValue = value.toLowerCase();
    
        return originalList.filter(option => option.toLowerCase().includes(filterValue));
      }

    //  private calcYearsinEni(date : Date) : number
    //  {
    //     return (Date.now - date). ;
    //  } 

}