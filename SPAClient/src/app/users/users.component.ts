import { Component, OnInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { DataSource } from '@angular/cdk/collections';
import { Observable, of } from 'rxjs';
import {MatDialog, MatDialogConfig} from "@angular/material";
import { ViewEncapsulation } from '@angular/core';
import {CountryService} from '../services/country.service';
import * as XLSX from 'xlsx';
import { DatePipe } from '@angular/common';
import { UserService } from './userservice';
import { User } from './user';
import { UserDialogComponent } from 'app/user-dialog/user-dialog.component';

import { environment } from 'environments/environment';

const baseUrl :string = environment.apiUrl;

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss'],
  providers : [UserService],
})
export class UsersComponent implements OnInit {

  users : User[];

   today : Date;

   

  selectedUser: User;

  baseImageUrl = baseUrl + "/api/employee/images/";


   dataSource: MatTableDataSource<User>;

   @ViewChild(MatSort) sort: MatSort;

   displayedColumns: string[] = ['ID','username','name','surname'
                                ,'email','lastlogon','status'
                                ];

  constructor(private router: Router,
    private userService : UserService
    ,private dialog: MatDialog
     ) { 
    this.today = new Date();
  }

  random : number;
  ngOnInit() {
      this.getAllUsers(); 
      this.random = this.getRandom();
  }

  
  export(): void {
    /* generate worksheet */

    var datePipe = new DatePipe("en-GB");

    var tt = datePipe.transform(Date(), 'dd-MM-yyyy');

    var fileName = "users-" + tt +   ".xlsx";;
    
    var data = new Array<any[]>();

    
    data.push(this.displayedColumns);

    for (var i=0;i<this.dataSource.filteredData.length;i++)
    {
      data.push(this.makeArrayFromUser(i+1 , this.dataSource.filteredData[i]));
    }

		const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(data);

		/* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
		XLSX.utils.book_append_sheet(wb, ws, 'Users');

		/* save to file */
    XLSX.writeFile(wb, fileName);
    
    }

    makeArrayFromUser(index : number, user : User) : any[]
    {
        var arr = new Array<any>();

        arr.push(index);
        arr.push(user.surname);
        arr.push(user.name);
        arr.push(user.surname);
        arr.push(user.email);
        arr.push(user.lastlogon);
        arr.push(user.active ? 'Active' : 'Disabled');
        return arr;
    }



  doubleClick(row : User)
  {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

    row.isNew = false;
    if (!row.roles)
      row.roles = '';

    dialogConfig.data = row;

    const dialogRef = this.dialog.open(UserDialogComponent,dialogConfig);


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
  getAllUsers(): void {
    this.isLoading = true;
    //this.dataSource = null;
    this.userService.getAllUsers(this.showOnlyActive)
      .subscribe(users => {this.users = users;
        this.dataSource = new MatTableDataSource(this.users);
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      });
  }

  CreateNewUser()
  {
    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  
    var newUser= new User();
    newUser.isNew = true;
    newUser.active = true;
    newUser.roles = 'employees$housing$ict$leave$settings';

    dialogConfig.data = newUser;

    const dialogRef = this.dialog.open(UserDialogComponent,dialogConfig);


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
  filterActiveUsers(checked)
  {
    this.showOnlyActive = checked;
    this.ngOnInit();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getRandom()
  {
    return Math.floor(Math.random() * (999999 - 100000)) + 100000;
  }

}

