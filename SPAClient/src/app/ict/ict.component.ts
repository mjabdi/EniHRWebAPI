import { Component, OnInit,ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import {ICTService} from './ict.service'
import { ICT } from './ict';
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

import { ICTDialogComponent } from '../ict-dialog/ict-dialog.component';

@Component({
  selector: 'app-ict',
  templateUrl: './ict.component.html',
  styleUrls: ['./ict.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers : [ICTService],
})
export class ICTComponent implements OnInit {

   ICTs : ICT[];

   today : Date;

  selectedICT: ICT;
  imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/';



   dataSource: MatTableDataSource<ICT>;

   @ViewChild(MatSort) sort: MatSort;

   displayedColumns: string[] = ['ID','employeeID','name','surname','activeDirAccount','workstaionNumber'
                                ,'localPlus','workingLocation'
                                ,'employeeCategory','costCentre','activityStatus'
                                ,'approvalLevel1','approvalLevel2','macroAggregation'
                                ,'deskPhoneNumber','mobileNumber','advancedLyncProfile','sap'
                                ,'linkToAttachment','emailAddress' ,'note','startMoveDate'
                                ];
                                

  constructor(private router: Router,private ICTService : ICTService,private dialog: MatDialog) { 
    this.today = new Date();
  }

  ngOnInit() {
    this.getAllICT(); 
  }

  export(): void {
    /* generate worksheet */
    var datePipe = new DatePipe("en-GB");

    var tt = datePipe.transform(Date(), 'dd-MM-yyyy');

    var fileName = "ICT-" + tt +   ".xlsx";;
    
    var data = new Array<any[]>();

    
    data.push(this.displayedColumns);

    for (var i=0;i<this.dataSource.filteredData.length;i++)
    {
      data.push(this.makeArrayFromEmployee(i+1 , this.dataSource.filteredData[i]));
    }

		const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(data);

		/* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
		XLSX.utils.book_append_sheet(wb, ws, 'ICT');

		/* save to file */
    XLSX.writeFile(wb, fileName);
    
    }

    makeArrayFromEmployee(index : number, ICT : ICT) : any[]
    {
        var arr = new Array<any>();

        arr.push(index);
        arr.push(ICT.employeeID);
        arr.push(ICT.name);
        arr.push(ICT.surname);
        arr.push(ICT.localPlus);
        arr.push(ICT.workingLocation);
        arr.push(ICT.employeeCategory);
        arr.push(ICT.costCentre);
        arr.push(ICT.activityStatus);
        arr.push(ICT.activeDirAccount);
        arr.push(ICT.workingLocation);
        arr.push(ICT.approvalLevel1);
        arr.push(ICT.approvalLevel2);
        arr.push(ICT.macroAggregation);
        arr.push(ICT.deskPhoneNumber);
        arr.push(ICT.mobileNumber);
        arr.push(ICT.advancedLyncProfile);
        arr.push(ICT.sap);
        arr.push(ICT.linkToAttachment);
        arr.push(ICT.note);
        arr.push(ICT.startMoveDate);

        return arr;
    }



  doubleClick(row : ICT)
  {

    const dialogConfig = new MatDialogConfig();

    dialogConfig.hasBackdrop = true;
    dialogConfig.disableClose = true;
    dialogConfig.autoFocus = false;

    dialogConfig.panelClass = "custom-modalbox";

  

    dialogConfig.data = row;

    const dialogRef = this.dialog.open(ICTDialogComponent,dialogConfig);


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
  getAllICT(): void {
    this.isLoading = true;
    //this.dataSource = null;
    this.ICTService.getAllICT()
      .subscribe(ICTs => {this.ICTs = ICTs;
        this.dataSource = new MatTableDataSource(this.ICTs);
        this.dataSource.sort = this.sort;
        this.isLoading = false;
      });
  }


  private showOnlyActive : boolean = true;
  filterActiveICTs(checked)
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

