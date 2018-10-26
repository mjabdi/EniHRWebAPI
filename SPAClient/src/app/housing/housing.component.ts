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

import {
  Overlay,
  OverlayConfig,
  OverlayContainer,
  OverlayRef,
  ScrollStrategy,
  ScrollStrategyOptions,
} from '@angular/cdk/overlay';
import { HousingDialogComponent } from '../housing-dialog/housing-dialog.component';

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

  selectedHousing: Housing;
  imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/';



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


                                

  constructor(private router: Router,private housingService : HousingService,private dialog: MatDialog) { 
    this.today = new Date();
  }

  ngOnInit() {
    this.getAllHousing(); 
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
}

