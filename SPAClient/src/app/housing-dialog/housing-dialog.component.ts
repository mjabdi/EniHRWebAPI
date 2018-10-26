import {Component, Inject, OnInit, ViewEncapsulation, Host} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {MatSnackBar} from '@angular/material';

import {Housing} from "../housing/housing";
import {FormControl} from "@angular/forms";
import * as moment from 'moment';
import {BaseInfoService} from '../services/baseInfo.service';
import {HousingService} from '../housing/housing.service';


import { Observable } from 'rxjs'
import {map, startWith} from 'rxjs/operators';

import { ToastrService } from 'ngx-toastr';

import {MatDialog, MatDialogConfig} from "@angular/material";
import { DecimalPipe, formatNumber, formatCurrency } from '@angular/common';

import {CurrencyPipe} from '@angular/common'
import { isNumber } from 'util';

//import {DifferencePipe} from 'angular2-moment';




@Component({
    selector: 'housing-dialog',
    templateUrl: './housing-dialog.component.html',
    styleUrls: ['./housing-dialog.component.scss']
})
export class HousingDialogComponent implements OnInit {

    today : Date ;

    isSubmit: boolean = false;

    housing : Housing;

    furnishedList : string[];
    currencyList : string[];

    currencyString = "GBP";

    unfurnished_value = 0;
    
    imageUrl;

    homeAddressControl = new FormControl();
    entitledBedroomsControl = new FormControl();
    actualBedroomsControl = new FormControl();
    typeofPropertyControl = new FormControl();
    rentDueDateControl = new FormControl();
    tenancyStartDateControl = new FormControl();
    tenancyEndDateControl = new FormControl();
    monthNoticePeriodControl = new FormControl();
    initialRentControl = new FormControl();
    currentRentalControl = new FormControl();
    unfurnishedAllowanceWeekControl = new FormControl();
    depositControl = new FormControl();
    currencyControl = new FormControl();


    differenceAllowanceMonthlyCostsPaidControl = new FormControl();
    furnitureAllowanceControl = new FormControl();
    actualFurnitureCostsControl = new FormControl();
    parkingChargesControl = new FormControl();
    regularPayrollDeductionControl = new FormControl();
    utilitiesIncludedControl = new FormControl();
    furnishedUnFurnishedControl = new FormControl();

    hrApprovalControl = new FormControl();

    housingCommentsControl = new FormControl();

    constructor(
        private cp: CurrencyPipe,
        private dialog: MatDialog,
        private snackBar: MatSnackBar,
        private baseInfoService : BaseInfoService,
        private housingService : HousingService,
        private toastrService : ToastrService,
        private dialogRef: MatDialogRef<HousingDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {

            this.housing = data;
            this.today = new Date();
            this.imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/' + this.housing.technicalID + '.jpg'

    }

    isloaded() : boolean {
       
        return this.isLoaded;
    }



    isLoaded : boolean = false;
    ngOnInit() {

         this.setupFilters();
         this.LoadFormValues();
         this.isLoaded = true;

    }

    setupFilters()
    {
        this.furnishedList = ['Furnished' , 'Partially Furnished' , 'Unfurnished'  , 'N/A'];
        this.currencyList = ['GBP' , 'EUR' , 'JPY' , 'KRW' , 'MZN' , 'NOK' , 'SGD' , 'USD' ];

    }

    LoadFormValues()
    {

        var curr = "GBP";
        if (this.housing.currency)
        {
            curr = this.housing.currency;
        }

        this.currencyControl.setValue(curr);

        this.homeAddressControl.setValue(this.housing.homeAddress);
        this.entitledBedroomsControl.setValue(this.housing.entitledBedrooms);
        this.actualBedroomsControl.setValue(this.housing.actualBedrooms);
        this.typeofPropertyControl.setValue(this.housing.typeofProperty);
        this.rentDueDateControl.setValue(this.housing.rentDueDate);
        this.tenancyStartDateControl.setValue(this.housing.tenancyStartDate);
        this.tenancyEndDateControl.setValue(this.housing.tenancyEndDate);
        this.monthNoticePeriodControl.setValue(this.housing.monthNoticePeriod);

        // this.initialRentControl.setValue(formatCurrency(this.housing.initialRent, 'en-GB', curr ,'1.0-2'));
        this.initialRentControl.setValue(this.housing.initialRent);
        this.currentRentalControl.setValue(this.housing.currentRental);
        this.unfurnishedAllowanceWeekControl.setValue(this.housing.unfurnishedAllowanceWeek);
        this.housingCommentsControl.setValue(this.housing.housingComments);

       
        this.differenceAllowanceMonthlyCostsPaidControl.setValue(this.housing.differenceAllowanceMonthlyCostsPaid);

        this.furnitureAllowanceControl.setValue(this.housing.furnitureAllowance);
        this.actualFurnitureCostsControl.setValue(this.housing.actualFurnitureCosts);
        this.parkingChargesControl.setValue(this.housing.parkingCharges);
        this.regularPayrollDeductionControl.setValue(this.housing.regularPayrollDeduction);
        this.utilitiesIncludedControl.setValue(this.housing.utilitiesIncluded);
        this.furnishedUnFurnishedControl.setValue(this.housing.furnishedUnFurnished);
        this.depositControl.setValue(this.housing.deposit);

        this.unfurnished_value = this.housing.unfurnishedAllowanceWeek;

        this.hrApprovalControl.setValue(this.housing.hrApproval);
    }


    save() {

        this.isSubmit = true;

        var newHousing = this.buildHousingfromForm();
        this.housingService.updateHousing(this.housing.employeeID,newHousing)
        .subscribe(data => {
          this.toastrService.success(this.housing.name + ' Housing information has been updated successfully','Housing Info Updated');
          this.CopyHousing(this.housing,newHousing);
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




    CopyHousing(ho1 : Housing, ho2 : Housing)
    {


        ho1.currency = ho2.currency;


        ho1.homeAddress = ho2.homeAddress;
        ho1.entitledBedrooms = ho2.entitledBedrooms;
        ho1.actualBedrooms = ho2.actualBedrooms;
        ho1.typeofProperty = ho2.typeofProperty;
        ho1.rentDueDate = ho2.rentDueDate;
        ho1.tenancyStartDate = ho2.tenancyStartDate;
        ho1.tenancyEndDate = ho2.tenancyEndDate;
        ho1.monthNoticePeriod = ho2.monthNoticePeriod;
        ho1.initialRent = ho2.initialRent;
        ho1.currentRental = ho2.currentRental;
        ho1.unfurnishedAllowanceWeek = ho2.unfurnishedAllowanceWeek;
        ho1.housingComments = ho2.housingComments;

        ho1.differenceAllowanceMonthlyCostsPaid = ho2.differenceAllowanceMonthlyCostsPaid;
        ho1.furnitureAllowance = ho2.furnitureAllowance;
        ho1.actualFurnitureCosts = ho2.actualFurnitureCosts;
        ho1.parkingCharges = ho2.parkingCharges;
        ho1.regularPayrollDeduction = ho2.regularPayrollDeduction;
        ho1.utilitiesIncluded = ho2.utilitiesIncluded;
        ho1.furnishedUnFurnished = ho2.furnishedUnFurnished;

        ho1.deposit = ho2.deposit;
        ho1.hrApproval = ho2.hrApproval;

    }

     buildHousingfromForm() : Housing
    {
        var newHousing= new Housing();

        newHousing.employeeID = this.housing.employeeID;
        newHousing.name = this.housing.name;
        newHousing.surname = this.housing.surname;
        newHousing.localPlus = this.housing.localPlus;
        newHousing.technicalID = this.housing.technicalID;
        newHousing.workingLocation = this.housing.workingLocation;
        newHousing.employeeCategory = this.housing.employeeCategory;
        newHousing.costCentre = this.housing.costCentre;
        newHousing.familyStatus = this.housing.familyStatus;
        newHousing.followingPartner = this.housing.followingPartner;
        newHousing.followingChildren = this.housing.followingChildren;
        newHousing.activityStatus = this.housing.activityStatus;

        newHousing.homeAddress = this.homeAddressControl.value;
        newHousing.entitledBedrooms = this.entitledBedroomsControl.value;
        newHousing.actualBedrooms = this.actualBedroomsControl.value;
        newHousing.typeofProperty = this.typeofPropertyControl.value;
        newHousing.rentDueDate = this.rentDueDateControl.value;
        newHousing.tenancyStartDate = this.tenancyStartDateControl.value;
        newHousing.tenancyEndDate = this.tenancyEndDateControl.value;
        newHousing.monthNoticePeriod = this.monthNoticePeriodControl.value;
        newHousing.initialRent = this.initialRentControl.value;
        newHousing.currentRental = this.currentRentalControl.value;
        newHousing.unfurnishedAllowanceWeek = this.unfurnishedAllowanceWeekControl.value;
        newHousing.housingComments = this.housingCommentsControl.value;      
        
        newHousing.differenceAllowanceMonthlyCostsPaid = this.differenceAllowanceMonthlyCostsPaidControl.value;      
        newHousing.furnitureAllowance = this.furnitureAllowanceControl.value;      
        newHousing.actualFurnitureCosts = this.actualFurnitureCostsControl.value;      
        newHousing.parkingCharges = this.parkingChargesControl.value;      
        newHousing.regularPayrollDeduction = this.regularPayrollDeductionControl.value;      
        newHousing.utilitiesIncluded = this.utilitiesIncludedControl.value;      
        newHousing.furnishedUnFurnished = this.furnishedUnFurnishedControl.value;    
        
        newHousing.deposit = this.depositControl.value;
        newHousing.currency = this.currencyString;

        newHousing.hrApproval = this.hrApprovalControl.value;

        return newHousing;        
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