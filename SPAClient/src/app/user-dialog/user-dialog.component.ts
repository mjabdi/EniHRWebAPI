import {Component, Inject, OnInit, ViewEncapsulation} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {MatSnackBar} from '@angular/material';

import {Employee} from "../employees/employee";
import {FormControl} from "@angular/forms";
import * as moment from 'moment';
import {BaseInfoService} from '../services/baseInfo.service';
import {UserService} from '../users/userservice';

import { Observable } from 'rxjs'
import {map, startWith} from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import {MatDialog, MatDialogConfig} from "@angular/material";
import { User } from '../users/user';


import { environment } from 'environments/environment';

const baseUrl :string = environment.apiUrl;


@Component({
    selector: 'user-dialog',
    templateUrl: './user-dialog.component.html',
    styleUrls: ['./user-dialog.component.scss']
})
export class UserDialogComponent implements OnInit {

    today : Date ;

    isSubmit: boolean = false;

    user : User;
    
    imageUrl;

    panelOpenState = false;

    usernameControl = new FormControl();
    nameControl = new FormControl();
    surnameControl = new FormControl();
    emailControl = new FormControl();
    technicalidControl = new FormControl();
    statusControl = new FormControl();  

    CreateMode : boolean = false;

    constructor(
        public dialog: MatDialog,
        public snackBar: MatSnackBar,
        private baseInfoService : BaseInfoService,
        private userService : UserService,
        private toastrService : ToastrService,
        private dialogRef: MatDialogRef<UserDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {

            this.user = data;
            this.CreateMode = this.user.isNew;
            this.today = new Date();

            this.imageUrl = baseUrl + "/api/employee/images/" + this.user.employeeid +  "/" + Math.floor(Math.random() * (999999 - 100000)) + 100000 ;

    }

    isloaded() : boolean {
        return true;
    }

    ngOnInit() {
         this.setupFilters();
         this.LoadFormValues();
         this.updateRoleStr();
    }

    setupFilters()
    {
    }

    LoadFormValues()
    {
        this.usernameControl.setValue(this.user.username);
        this.nameControl.setValue(this.user.name);
        this.surnameControl.setValue(this.user.surname);
        this.technicalidControl.setValue(this.user.technicalid);
        this.emailControl.setValue(this.user.email);
        this.statusControl.setValue(this.user.active);

        this.role_employees = this.user.roles.indexOf('employees') > -1;
        this.role_housing = this.user.roles.indexOf('housing') > -1;
        this.role_ict = this.user.roles.indexOf('ict') > -1;
        this.role_leave = this.user.roles.indexOf('leave') > -1;
        this.role_settings = this.user.roles.indexOf('settings') > -1;

    }

    createNew()
    {
    

      var newUser = this.buildUserfromForm();

      if (!newUser.username || newUser.username.length < 6)
      {
        this.toastrService.warning( 'Please enter a valid username','Invalid Data');
        return;        
      }

      this.isSubmit = true;

      this.userService.createNewUser(newUser)
      .subscribe(data => {
        this.toastrService.success( newUser.username + ' has been added successfully','New User Created');
        this.CopyUser(this.user,newUser);
        this.dialogRef.close(true);
        this.isSubmit = false;  
        return true;  
      },
      error =>
      {
        if  (error.error ==  'DuplicateUsername')
          this.toastrService.error( newUser.username + ' already exists! Please choose another username.','Invalid Data');
        else
          this.toastrService.error('Sorry, something went wrong. Please try again','Server Error');

          this.isSubmit = false;
      }
    );
    }


    save() {

        this.isSubmit = true;

        var newUser = this.buildUserfromForm();
        this.userService.updateUser(newUser.username,newUser)
        .subscribe(data => {
          this.toastrService.success(newUser.username + ' information has been updated successfully','User Updated');
          this.CopyUser(this.user,newUser);
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
    }


    confirmDelete()
    {
      let snackBarRef = this.snackBar.open('Are you sure you want to delete ' + this.usernameControl.value+ '?','Delete',{
        duration: 5000,
        panelClass : 'my-snackbar-style',
  
      });
  
      snackBarRef.onAction().subscribe(() => {
        this.deleteUser();
      });
    }

    deleteUser()
    {
      this.isSubmit = true;

      var newUser = this.buildUserfromForm();
      this.userService.deleteUser(newUser.username)
      .subscribe(data => {
        this.toastrService.success(newUser.username + ' was successfully deleted','User Deleted');
        this.isSubmit = false;  
        this.dialogRef.close(true); 
      },
      error =>
      {
        this.toastrService.error('Sorry, something went wrong. Please try again','Server Error');
        this.isSubmit = false;
      }
    );
    }

    CopyUser(usr1 : User, usr2 : User)
    {
      usr1.username = usr2.username;
      usr1.name = usr2.name;
      usr1.surname = usr2.surname;
      usr1.email = usr2.email;
      usr1.active = usr2.active;
      usr1.roles = usr2.roles;
      usr1.technicalid = usr2.technicalid;
    }

     buildUserfromForm() : User
    {
        var newUser = new User();
        
        newUser.username = this.usernameControl.value;        
        newUser.name = this.nameControl.value;
        newUser.surname = this.surnameControl.value;
        newUser.technicalid = this.technicalidControl.value;
        newUser.email = this.emailControl.value;
        newUser.active = this.statusControl.value;
        newUser.roles = this.roleStr;
       
        return newUser;        
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


      role_employees = true;
      role_housing = true;
      role_ict = true;
      role_leave = true;
      role_settings = true;

      roleChange(role :string , checked : boolean )
      {
        if (role == 'employees')
          this.role_employees = checked;
        else if (role == 'housing')
          this.role_housing = checked;
        else if (role == 'ict')
          this.role_ict = checked;
        else if (role == 'leave')
          this.role_leave = checked;
         else if (role == 'settings')
          this.role_settings = checked;

          this.updateRoleStr();

      }

     
      roleStr = '';
      updateRoleStr()
      {
        this.roleStr = '';

        if (this.role_employees)
          this.roleStr += 'employees$';
        if (this.role_housing)
          this.roleStr += 'housing$';
        if (this.role_ict)
          this.roleStr += 'ict$';
        if (this.role_leave)
          this.roleStr += 'leave$';
        if (this.role_settings)
          this.roleStr += 'settings$';
      }
}