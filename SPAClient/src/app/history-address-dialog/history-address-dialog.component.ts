import {Component, Inject, OnInit, ViewEncapsulation, Host} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import { HomeAddress } from './homeaddress';


@Component({
    selector: 'history-address-dialog',
    templateUrl: './history-address-dialog.component.html',
    styleUrls: ['./history-address-dialog.component.scss']
})
export class HistoryAddressDialogComponent implements OnInit {

    addressList : HomeAddress[];

    constructor(
        private dialogRef: MatDialogRef<HistoryAddressDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {

            this.addressList = data;
    }

    ngOnInit() {

    }


}