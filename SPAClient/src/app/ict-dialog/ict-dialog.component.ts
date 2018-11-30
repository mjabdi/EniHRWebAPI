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
import { ICT } from 'app/ict/ict';
import { ICTService } from 'app/ict/ict.service';

//import {DifferencePipe} from 'angular2-moment';




@Component({
    selector: 'ict-dialog',
    templateUrl: './ict-dialog.component.html',
    styleUrls: ['./ict-dialog.component.scss']
})
export class ICTDialogComponent implements OnInit {

    today : Date ;

    isSubmit: boolean = false;

    ict : ICT;

    macroAggregationList : string[];

    currencyString = "GBP";

    unfurnished_value = 0;
    
    imageUrl;

    activeDirAccountControl = new FormControl();
    workstaionNumberControl = new FormControl();
    approvalLevel1Control = new FormControl();
    approvalLevel2Control = new FormControl();
    macroAggregationControl = new FormControl();
    deskPhoneNumberControl = new FormControl();
    mobileNumberControl = new FormControl();
    advancedLyncProfileControl = new FormControl();
    sapControl = new FormControl();
    linkToAttachmentControl = new FormControl();
    noteControl = new FormControl();
    startMoveDateControl = new FormControl();
    emailAddressControl = new FormControl();
    
    constructor(
        private cp: CurrencyPipe,
        private dialog: MatDialog,
        private snackBar: MatSnackBar,
        private baseInfoService : BaseInfoService,
        private ictService : ICTService,
        private toastrService : ToastrService,
        private dialogRef: MatDialogRef<ICTDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {

            this.ict = data;
            this.today = new Date();
            this.imageUrl = 'http://my.eeep.intranet:8099/PhotoIDs/' + this.ict.technicalID + '.jpg'

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
        this.macroAggregationList = ['Agency Contractor' , 'Seconded' , 'Service'  , 'N/A'];
    }

    LoadFormValues()
    {


        this.activeDirAccountControl.setValue(this.ict.activeDirAccount);
        this.workstaionNumberControl.setValue(this.ict.workstaionNumber);
        this.approvalLevel1Control.setValue(this.ict.approvalLevel1);
        this.approvalLevel2Control.setValue(this.ict.approvalLevel2);
        this.macroAggregationControl.setValue(this.ict.macroAggregation);
        this.deskPhoneNumberControl.setValue(this.ict.deskPhoneNumber);
        this.mobileNumberControl.setValue(this.ict.mobileNumber);
        this.advancedLyncProfileControl.setValue(this.ict.advancedLyncProfile);
        this.sapControl.setValue(this.ict.sap);
        this.noteControl.setValue(this.ict.note);
        this.startMoveDateControl.setValue(this.ict.startMoveDate);
        this.emailAddressControl.setValue(this.ict.emailAddress);

    }


    save() {

        this.isSubmit = true;

        var newICT = this.buildICTfromForm();
        this.ictService.updateICT(this.ict.employeeID,newICT)
        .subscribe( (data) => {
          this.toastrService.success(this.ict.name + ' ICT information has been updated successfully','ICT Info Updated');
          this.CopyICT(this.ict,newICT);
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




    CopyICT(ict1 : ICT, ict2 : ICT)
    {

        ict1.activeDirAccount = ict2.activeDirAccount;
        ict1.workstaionNumber = ict2.workstaionNumber;
        ict1.approvalLevel1 = ict2.approvalLevel1;
        ict1.approvalLevel2 = ict2.approvalLevel2;
        ict1.macroAggregation = ict2.macroAggregation;
        ict1.deskPhoneNumber = ict2.deskPhoneNumber;
        ict1.mobileNumber = ict2.mobileNumber;
        ict1.advancedLyncProfile = ict2.advancedLyncProfile;
        ict1.sap = ict2.sap;
        ict1.note = ict2.note;
        ict1.startMoveDate = ict2.startMoveDate;
        ict1.emailAddress = ict2.emailAddress;
    }

     buildICTfromForm() : ICT
    {
        var newICT= new ICT();

        newICT.employeeID = this.ict.employeeID;
        newICT.activeDirAccount = this.activeDirAccountControl.value;
        newICT.workstaionNumber = this.workstaionNumberControl.value;
        newICT.approvalLevel1 = this.approvalLevel1Control.value;
        newICT.approvalLevel2 = this.approvalLevel2Control.value;
        newICT.macroAggregation = this.macroAggregationControl.value;
        newICT.deskPhoneNumber = this.deskPhoneNumberControl.value;
        newICT.mobileNumber = this.mobileNumberControl.value;
        newICT.advancedLyncProfile = this.advancedLyncProfileControl.value;
        newICT.sap = this.sapControl.value;
        newICT.note = this.noteControl.value;
        newICT.startMoveDate = this.startMoveDateControl.value;
        newICT.emailAddress = this.emailAddressControl.value;

        return newICT;        
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
}