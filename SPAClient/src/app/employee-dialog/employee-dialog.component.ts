import {Component, Inject, OnInit, ViewEncapsulation} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {MatSnackBar} from '@angular/material';

import {Employee} from "../employees/employee";
import {FormControl} from "@angular/forms";
import * as moment from 'moment';
import {BaseInfoService} from '../services/baseInfo.service';
import {EmployeeService} from '../employees/employee.service';

import { Observable } from 'rxjs'
import {map, startWith} from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import {MatDialog, MatDialogConfig} from "@angular/material";
import { UploadPictureDialogComponent } from 'app/uploadpicture-dialog/upload-picture-dialog.component';

//import {DifferencePipe} from 'angular2-moment';

import { environment } from 'environments/environment';
import { getRandomString } from 'selenium-webdriver/safari';

const baseUrl :string = environment.apiUrl;


@Component({
    selector: 'employee-dialog',
    templateUrl: './employee-dialog.component.html',
    styleUrls: ['./employee-dialog.component.scss']
})
export class EmployeeDialogComponent implements OnInit {

    today : Date ;

    isSubmit: boolean = false;

    employee : Employee;

    employeeIDControl = new FormControl();
    
    imageUrl;



    nameControl = new FormControl();
    surnameControl = new FormControl();
    technicalIDControl = new FormControl();
    companyHiringDateControl = new FormControl();
    genderControl = new FormControl();
    birthDateControl = new FormControl();  
    followingPartnerControl = new FormControl();  
    followingChildrenControl = new FormControl();    
    visaExpiryDateControl = new FormControl();   
    terminationDateControl = new FormControl();   

    emailControl = new FormControl(); 
   

    localPlus : string[];
    filter_localPlus : Observable<string[]>;
    localPlusControl  = new FormControl();

    workinglocation : string[];
    filter_workinglocation : Observable<string[]>;
    workinglocationControl  = new FormControl();

    employeecategory : string[];
    filter_employeecategory: Observable<string[]>;
    employeecategoryControl  = new FormControl();

    organizationunit : string[];
    filter_organizationunit: Observable<string[]>;
    organizationunitControl  = new FormControl();

    costcentre : string[];
    filter_costcentre: Observable<string[]>;
    costcentreControl  = new FormControl();

    position : string[];
    filter_position: Observable<string[]>;
    positionControl  = new FormControl();

    professionalarea : string[];
    filter_professionalarea: Observable<string[]>;
    professionalareaControl  = new FormControl();

    homecompany : string[];
    filter_homecompany: Observable<string[]>;
    homecompanyControl  = new FormControl();

    country : string[];
    filter_countryofbirth: Observable<string[]>;
    countryofbirthControl  = new FormControl();

    filter_nationality: Observable<string[]>;
    nationalityControl  = new FormControl();

    filter_spousenationality: Observable<string[]>;
    spousenationalityControl  = new FormControl();

    familystatus : string[];
    filter_familystatus: Observable<string[]>;
    familystatusControl  = new FormControl();

    typeofvisa : string[];
    filter_typeofvisa: Observable<string[]>;
    typeofvisaControl  = new FormControl();

    activitystatus : string[];
    filter_activitystatus: Observable<string[]>;
    activitystatusControl  = new FormControl();

    gender : string[];

    CreateMode : boolean = false;
  
    random : number;

    constructor(
        public dialog: MatDialog,
        public snackBar: MatSnackBar,
        private baseInfoService : BaseInfoService,
        private employeeService : EmployeeService,
        private toastrService : ToastrService,
        private dialogRef: MatDialogRef<EmployeeDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {

            this.employee = data;
            this.CreateMode = this.employee.isNew;
            this.today = new Date();
            this.random = this.getRandom();
            this.imageUrl = baseUrl + "/api/employee/images/" + this.employee.employeeID +  "/" + this.random ;
    }

    isloaded() : boolean {
        return (this.localPlus != null) &&
        (this.workinglocation != null) &&
        (this.employeecategory != null) && 
        (this.organizationunit != null) &&
        (this.costcentre != null) &&
        (this.position != null) &&
        (this.professionalarea != null) &&
        (this.homecompany != null) &&
        (this.country != null) &&
        (this.familystatus != null) &&
        (this.typeofvisa != null) &&
        (this.activitystatus != null)
    }




    ngOnInit() {

         this.setupFilters();
         this.LoadFormValues();

    }

    setupFilters()
    {
      this.baseInfoService.getLocalPlus().subscribe(data => {
        this.localPlus = data;

        this.filter_localPlus = this.localPlusControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.localPlus,value))
        );

        });

       this.baseInfoService.getWorkingLocation().subscribe(data => {
           this.workinglocation = data;
           this.filter_workinglocation = this.workinglocationControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.workinglocation,value))
           );
       });

       this.baseInfoService.getEmployeeCategory().subscribe(data => {
           this.employeecategory = data;
           this.filter_employeecategory = this.employeecategoryControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.employeecategory,value))
           );
           });
           
       this.baseInfoService.getOrganizationUnit().subscribe(data => {
        this.organizationunit = data;
        this.filter_organizationunit = this.organizationunitControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.organizationunit,value))
        );
        });
        
       this.baseInfoService.getCostCentre().subscribe(data => {
           this.costcentre = data;
           this.filter_costcentre= this.costcentreControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.costcentre,value))
           );
           });
           
       this.baseInfoService.getPosition().subscribe(data => {
        this.position = data;
        this.filter_position= this.positionControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.position,value))
        );
        });
        
        this.baseInfoService.getProfessionalArea().subscribe(data => {
           this.professionalarea = data;
           this.filter_professionalarea= this.professionalareaControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.professionalarea,value))
           );
       });

       this.baseInfoService.getHomeCompany().subscribe(data => {
           this.homecompany = data;
           this.filter_homecompany= this.homecompanyControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.homecompany,value))
           );
           });
           
       this.baseInfoService.getCountry().subscribe(data => {
        this.country = data;
        this.filter_countryofbirth= this.countryofbirthControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.country,value))
        );
        this.filter_nationality= this.nationalityControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.country,value))
        );
        this.filter_spousenationality= this.spousenationalityControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.country,value))
        );

        });
        
        this.baseInfoService.getFamilyStatus().subscribe(data => {
           this.familystatus = data;
           this.filter_familystatus= this.familystatusControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.familystatus,value))
           );
       });

       this.baseInfoService.getTypeofVisa().subscribe(data => {
           this.typeofvisa = data;
           this.filter_typeofvisa= this.typeofvisaControl.valueChanges
           .pipe(
             startWith(''),
             map(value => this.dofilter(this.typeofvisa,value))
           );
           });
           
       this.baseInfoService.getActivityStatus().subscribe(data => {
        this.activitystatus = data;
        this.filter_activitystatus= this.activitystatusControl.valueChanges
        .pipe(
          startWith(''),
          map(value => this.dofilter(this.activitystatus,value))
        );
        });

        this.gender = ['F' , 'M' , 'N/A'];
    }

    LoadFormValues()
    {
        this.employeeIDControl.setValue(this.employee.employeeID);
        this.nameControl.setValue(this.employee.name);
        this.surnameControl.setValue(this.employee.surname);
        this.technicalIDControl.setValue(this.employee.technicalID);
        this.companyHiringDateControl.setValue(this.employee.companyHiringDate);
        this.genderControl.setValue(this.employee.gender);
        this.birthDateControl.setValue(this.employee.birthDate);
        this.followingPartnerControl.setValue(this.employee.followingPartner);
        this.followingChildrenControl.setValue(this.employee.followingChildren);
        this.visaExpiryDateControl.setValue(this.employee.visaExpiryDate);
        this.emailControl.setValue(this.employee.emailAddress);
        this.localPlusControl.setValue(this.employee.localPlus);
        this.workinglocationControl.setValue(this.employee.workingLocation);
        this.employeecategoryControl.setValue(this.employee.employeeCategory);
        this.organizationunitControl.setValue(this.employee.organizationUnit);
        this.costcentreControl.setValue(this.employee.costCentre);
        this.positionControl.setValue(this.employee.position);
        this.professionalareaControl.setValue(this.employee.professionalArea);
        this.homecompanyControl.setValue(this.employee.homeCompany);
        this.countryofbirthControl.setValue(this.employee.countryOfBirth);
        this.nationalityControl.setValue(this.employee.nationality);
        this.familystatusControl.setValue(this.employee.familyStatus);
        this.spousenationalityControl.setValue(this.employee.spouseNationality);
        this.typeofvisaControl.setValue(this.employee.typeOfVisa);
        this.activitystatusControl.setValue(this.employee.activityStatus);
        this.terminationDateControl.setValue(this.employee.terminationDate);


    }

    createNew()
    {
      this.isSubmit = true;

      var newEmployee = this.buildEmployeefromForm();
      this.employeeService.createNewEmployee(newEmployee)
      .subscribe(data => {
        this.toastrService.success( newEmployee.employeeID + ' has been added successfully','New Employee Created');
        this.CopyEmployee(this.employee,newEmployee);
        
        this.dialogRef.close(true);
        this.isSubmit = false;  
        return true;  
      },
      error =>
      {
        if  (error.error ==  'DuplicateEmployeeID')
          this.toastrService.error('Duplicate Employee #','Invalid Data');
        else
          this.toastrService.error('Sorry, something went wrong. Please try again','Server Error');

          this.isSubmit = false;
      }
    );
    }


    save() {

        this.isSubmit = true;

        var newEmployee = this.buildEmployeefromForm();
        this.employeeService.updateEmployee(newEmployee.employeeID,newEmployee)
        .subscribe(data => {
          this.toastrService.success(newEmployee.name + ' information has been updated successfully','Employee Updated');
          this.CopyEmployee(this.employee,newEmployee);
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


    confirmDelete()
    {

      let snackBarRef = this.snackBar.open('Are you sure you want to delete this record? employee# : ' + this.employeeIDControl.value+ '?','Delete',{
        duration: 5000,
        panelClass : 'my-snackbar-style',
  
      });
  
      snackBarRef.onAction().subscribe(() => {
        this.deleteEmployee();
      });
    }

    deleteEmployee()
    {
      this.isSubmit = true;

      var newEmployee = this.buildEmployeefromForm();
      this.employeeService.deleteEmployee(newEmployee.employeeID)
      .subscribe(data => {
        this.toastrService.success( newEmployee.employeeID + ' was successfully deleted','Employee Deleted');
        this.isSubmit = false;  
        this.dialogRef.close(true);
        return true;  
      },
      error =>
      {
        this.toastrService.error('Sorry, something went wrong. Please try again','Server Error');
        this.isSubmit = false;
      }
    );
    }

    CopyEmployee(em1 : Employee, em2 : Employee)
    {
      em1.activityStatus = em2.activityStatus;
      em1.age = em2.age;
      em1.birthDate = em2.birthDate;
      em1.companyHiringDate = em2.companyHiringDate;
      em1.costCentre = em2.costCentre;
      em1.countryOfBirth = em2.countryOfBirth;
      em1.emailAddress = em2.emailAddress;
      em1.employeeCategory = em2.employeeCategory;
      em1.employeeID = em2.employeeID;
      em1.familyStatus = em2.familyStatus;
      em1.followingChildren = em2.followingChildren;
      em1.followingPartner = em2.followingPartner;
      em1.gender = em2.gender;
      em1.homeCompany = em2.homeCompany;
      em1.localPlus = em2.localPlus;
      em1.name = em2.name;
      em1.nationality = em2.nationality;
      em1.organizationUnit = em2.organizationUnit;
      em1.position = em2.position;
      em1.professionalArea = em2.professionalArea;
      em1.spouseNationality = em2.spouseNationality;
      em1.surname = em2.surname;
      em1.technicalID = em2.technicalID;
      em1.typeOfVisa = em2.typeOfVisa;
      em1.visaExpiryDate = em2.visaExpiryDate;
      em1.workingLocation = em2.workingLocation;
      em1.yearsInEni = em2.yearsInEni;
      em1.terminationDate = em2.terminationDate;
    }

     buildEmployeefromForm() : Employee
    {
        var newEmployee = new Employee();
        
        newEmployee.employeeID = this.employeeIDControl.value;
        newEmployee.name = this.nameControl.value;
        newEmployee.surname = this.surnameControl.value;
        newEmployee.technicalID = this.technicalIDControl.value;
        newEmployee.localPlus = this.localPlusControl.value;
        newEmployee.workingLocation = this.workinglocationControl.value;
        newEmployee.employeeCategory = this.employeecategoryControl.value;
        newEmployee.organizationUnit = this.organizationunitControl.value;
        newEmployee.costCentre = this.costcentreControl.value;
        newEmployee.position = this.positionControl.value;
        newEmployee.professionalArea = this.professionalareaControl.value;
        newEmployee.companyHiringDate = this.companyHiringDateControl.value;
        newEmployee.homeCompany = this.homecompanyControl.value;
        newEmployee.gender = this.genderControl.value;
        newEmployee.birthDate = this.birthDateControl.value;
        newEmployee.countryOfBirth = this.countryofbirthControl.value;
        newEmployee.nationality = this.nationalityControl.value;
        newEmployee.familyStatus = this.familystatusControl.value;
        newEmployee.followingPartner = this.followingPartnerControl.value;
        newEmployee.followingChildren = this.followingChildrenControl.value;
        newEmployee.spouseNationality = this.spousenationalityControl.value;
        newEmployee.typeOfVisa = this.typeofvisaControl.value;
        newEmployee.visaExpiryDate = this.visaExpiryDateControl.value;
        newEmployee.emailAddress = this.emailControl.value;
        newEmployee.activityStatus = this.activitystatusControl.value;
        newEmployee.terminationDate = this.terminationDateControl.value;

        return newEmployee;        
    }

    CorrectDateFormat(val : string)
    {
        if (!val)
          return '';

        var split =  val.split('/');

        if (split.length != 3)
          return val;

       return split[1] + "/" + split[0] + "/" + split[2];   
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


    showChangePicture()
    {

        if (!this.employeeIDControl.value)
        {
          this.toastrService.warning("Please enter an Employee# first","Employee# Required!");
          return;
        }

        const dialogConfig = new MatDialogConfig();

        dialogConfig.hasBackdrop = true;
        dialogConfig.disableClose = true;
        dialogConfig.autoFocus = false;
        dialogConfig.width = "500px";
        dialogConfig.height = "350px";

    
        dialogConfig.panelClass = "custom-modalbox";

        dialogConfig.data = this.employeeIDControl.value;
    
    
        const dialogRef = this.dialog.open(UploadPictureDialogComponent,dialogConfig);
    
        dialogRef.afterClosed().subscribe(
          val => {
              if (val)
              {
                  this.random = this.getRandom();
                  this.imageUrl = baseUrl + "/api/employee/images/" + this.employee.employeeID + "/" + this.random;
              }
          }
      );
    
    }

    getRandom()
    {
      return Math.floor(Math.random() * (999999 - 100000)) + 100000;
    }

}