import { Component, OnInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {HousingService} from './housing.service'
import { Housing } from './housing';
import {MatPaginator, MatSort, MatTableDataSource} from '@angular/material';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { DataSource } from '@angular/cdk/collections';
import { Observable, of } from 'rxjs';
import {MatDialog, MatDialogConfig} from "@angular/material";
import {EmployeeDialogComponent} from '../employee-dialog/employee-dialog.component';
import { ViewEncapsulation } from '@angular/core';
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
import { HousingDialogComponent } from '../housing-dialog/housing-dialog.component';
import { User } from 'app/users/user';
import { UserService } from 'app/users/userservice';
import { AuthenticationService } from 'app/services/authentication.service';

import { environment } from 'environments/environment';

const baseUrl :string = environment.apiUrl;

@Component({
  selector: 'app-housing',
  templateUrl: './housing.component.html',
  styleUrls: ['./housing.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers : [HousingService],
})
export class HousingComponent implements OnInit {

   housings : Housing[];

   today : Date;

   user : User;

  selectedHousing: Housing;
  baseImageUrl = baseUrl + "/api/employee/images/";



   dataSource: MatTableDataSource<Housing>;

   @ViewChild(MatSort) sort: MatSort;

   displayedColumns: string[] = ['ID','employeeID','name','surname','localPlus','workingLocation'
                                ,'employeeCategory','costCentre','familyStatus','followingPartner','followingChildren'
                                ,'activityStatus','homeAddress','entitledBedrooms','actualBedrooms','typeofProperty'
                                ,'rentDueDate','tenancyStartDate','tenancyEndDate','monthRemaining','monthNoticePeriod'
                                ,'initialRent','currentRental','unfurnishedAllowanceWeek','unfurnishedAllowanceMonth'
                                ,'hrApproval',
                                'differenceAllowanceMonthlyCostsPaid','furnitureAllowance','actualFurnitureCosts',
                                'parkingCharges','regularPayrollDeduction','utilitiesIncluded','furnishedUnFurnished',
                                'housingComments'
                                ];


                                

  constructor(private router: Router,private housingService : HousingService,private dialog: MatDialog,private userService : UserService,private authService:AuthenticationService) { 
    this.today = new Date();
  }

  allowed = 0;
  random : number;
  ngOnInit() {
    this.getAllHousing(); 

    this.userService.findUser(this.authService.getUsername()).subscribe(
      (data : User)=>
      {
        this.user = data;
        this.allowed = (this.user.roles.indexOf('housing') > -1) ? 1 : -1;
      }
    );

    this.random = this.getRandom();
  }

  export(): void {
    /* generate worksheet */
    var datePipe = new DatePipe("en-GB");

    var tt = datePipe.transform(Date(), 'dd-MM-yyyy');

    var fileName = "housing-" + tt +   ".xlsx";;
    
    var data = new Array<any[]>();

    
    data.push(this.displayedColumns);

    for (var i=0;i<this.dataSource.filteredData.length;i++)
    {
      data.push(this.makeArrayFromEmployee(i+1 , this.dataSource.filteredData[i]));
    }

		const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(data);

		/* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
		XLSX.utils.book_append_sheet(wb, ws, 'Housing');

		/* save to file */
    XLSX.writeFile(wb, fileName);
    
    }

    makeArrayFromEmployee(index : number, housing : Housing) : any[]
    {
        var arr = new Array<any>();

        arr.push(index);
        arr.push(housing.employeeID);
        arr.push(housing.name);
        arr.push(housing.surname);
        arr.push(housing.localPlus);
        arr.push(housing.workingLocation);
        arr.push(housing.employeeCategory);
        arr.push(housing.costCentre);
        arr.push(housing.familyStatus);
        arr.push(housing.followingPartner);
        arr.push(housing.followingChildren);
        arr.push(housing.activityStatus);
        arr.push(housing.homeAddress);
        arr.push(housing.entitledBedrooms);
        arr.push(housing.actualBedrooms);
        arr.push(housing.typeofProperty);
        arr.push(housing.rentDueDate);
        arr.push(housing.tenancyStartDate);
        arr.push(housing.tenancyEndDate);
        //arr.push('month remaining');
        arr.push('');

        arr.push(housing.monthNoticePeriod);
        arr.push(housing.initialRent);
        arr.push(housing.currentRental);
        arr.push(housing.unfurnishedAllowanceWeek);
        arr.push(housing.unfurnishedAllowanceWeek * 4.333);
        arr.push(housing.hrApproval)
        arr.push(housing.differenceAllowanceMonthlyCostsPaid);
        arr.push(housing.furnitureAllowance);
        arr.push(housing.actualFurnitureCosts);
        arr.push(housing.parkingCharges);
        arr.push(housing.regularPayrollDeduction);
        arr.push(housing.utilitiesIncluded);
        arr.push(housing.furnishedUnFurnished);
        arr.push(housing.housingComments);

        return arr;
    }







  doubleClick(row : Housing)
  {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  

    dialogConfig.data = row;

    const dialogRef = this.dialog.open(HousingDialogComponent,dialogConfig);


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
  getAllHousing(): void {
    this.isLoading = true;
    //this.dataSource = null;
    this.housingService.getAllHousing(this.showOnlyActive)
      .subscribe(housings => {this.housings = housings;
        this.dataSource = new MatTableDataSource(this.housings);
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      });
  }


  private showOnlyActive : boolean = true;
  filterActiveHousings(checked)
  {
    this.showOnlyActive = checked;
    this.ngOnInit();
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  getCurrency(val) : string
  {
      if (!val)
          return 'GBP';
      else
          return val;    
  }

  getRandom()
  {
    return Math.floor(Math.random() * (999999 - 100000)) + 100000;
  }
}

