import { Component } from '@angular/core';
import * as XLSX from 'xlsx';
import {ImportDataService} from './importdata.service'
import { AotSummaryResolver, ReturnStatement } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';
import {MatSnackBar} from '@angular/material';
import { ValidationResponse } from './validationresponse';
import { ViewEncapsulation } from '@angular/core';


type AOA = any[][];

@Component({
    selector: 'app-importdata',
    templateUrl: './importdata.component.html',
    styleUrls: ['./importdata.component.scss'],
    encapsulation: ViewEncapsulation.None,
    providers : [ImportDataService]
  })
  export class ImportDataComponent {

    validate = false;
    
    rowChecked : Array<boolean>;

    data: AOA ;
    selectedData : Array<Array<any>>;
	wopts: XLSX.WritingOptions = { bookType: 'xlsx', type: 'array' };
    fileName: string;
    isLoading = false;

    constructor(
        private dataService : ImportDataService,
        private snackbar : MatSnackBar,
        private toastrService: ToastrService)
    {

    }

    ngOnInit()
    {

    }

    onFileChange(evt: any) {
        /* wire up file reader */
        this.isLoading = true;
		const target: DataTransfer = <DataTransfer>(evt.target);
		if (target.files.length !== 1) throw new Error('Cannot use multiple files');
		const reader: FileReader = new FileReader();
		reader.onload = (e: any) => {
            this.data = null;

			/* read workbook */
			const bstr: string = e.target.result;
			const wb: XLSX.WorkBook = XLSX.read(bstr, {type: 'binary'});

			/* grab first sheet */
			const wsname: string = wb.SheetNames[0];
			const ws: XLSX.WorkSheet = wb.Sheets[wsname];

			/* save data */
            this.data = <AOA>(XLSX.utils.sheet_to_json(ws, {header: 1}));
            for (let i =0;i<this.data.length;i++)
            {
                if (!this.data[i][0])
                    {
                        this.data.splice(i,1);
                    }
            }
            this.rowChecked = new Array<boolean>(this.data.length-1);
            this.validate = false;
		};
		reader.readAsBinaryString(target.files[0]);
    }
    
    export(): void {
		/* generate worksheet */
		const ws: XLSX.WorkSheet = XLSX.utils.aoa_to_sheet(this.data);

		/* generate workbook and add the worksheet */
		const wb: XLSX.WorkBook = XLSX.utils.book_new();
		XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

		/* save to file */
		XLSX.writeFile(wb, this.fileName);
    }

    validating = false;
    valiationResponse : ValidationResponse[];
    changedRecords : number;
    newRecords : number;
    changedFields : number;
    validateData()    
    {
        this.validating = true;        

        this.dataService.validateData(this.data).subscribe(
            (data : ValidationResponse[]) =>
            {
                // this.toastrService.success("You can move to the next step.","Data Format is Valid");
                this.valiationResponse = data;

                this.changedRecords = 0;
                this.newRecords = 0;
                this.changedFields = 0;
                for (let i =0 ; i<this.valiationResponse.length;i++)
                {
                    if (this.valiationResponse[i].isNew)
                        this.newRecords ++;
                    if (this.valiationResponse[i].isChanged) 
                    {
                        this.changedRecords ++;   
                        this.changedFields += this.valiationResponse[i].changedColumns.length;
                    }
                }

                // console.log(JSON.stringify(data));   
                this.validate = true;
                this.validating = false;                   
            }
            ,
            error =>
            {
                this.toastrService.error("Invalid Data Format","Error");
                this.validate = false;
                this.validating = false;    
            }
        );
    }

    SelectedCounts() : number
    {
        let selectedCounts = 0;

        for (let i=0; i<this.rowChecked.length; i++)
        {
            if (this.rowChecked[i])
                selectedCounts++;
        }
        return selectedCounts;
    }

    changing = false;

    ApplyChanges()
    {
        let selectedCounts = 0;

        for (let i=0; i<this.rowChecked.length; i++)
        {
            if (this.rowChecked[i])
                selectedCounts++;
        }

        if (selectedCounts == 0)
        {
            alert('No Rows Selected!');
            return;
        }

        this.changing = true;

        this.selectedData = new Array<Array<any>>();

        var newRecords = new Array<number>();
        var changedRecords = new Array<number>();

        for (let i=0 ;i < this.rowChecked.length;i++)
        {
            if (this.rowChecked[i])
            {
                if (this.valiationResponse[i].isNew)
                    newRecords.push(i);
                else if (this.valiationResponse[i].isChanged)
                    changedRecords.push(i);

                this.selectedData.push(this.data[i+1]);
            }
        }

        

        this.dataService.applyChanges(this.selectedData).subscribe(
            data =>
            {
                if (newRecords.length > 0)
                {
                    this.toastrService.success(newRecords.length +  " New records added to database!","New Records");
                    for (let i=0 ; i < newRecords.length; i++)
                    {
                        this.valiationResponse[newRecords[i]].isNew = false;    
                        this.rowChecked[newRecords[i]] = false;
                    }
                }

                if (changedRecords.length > 0)
                {
                    this.toastrService.success(changedRecords.length +  " records updated successfully!","Database Updatad");
                    for (let i=0 ; i < changedRecords.length; i++)
                    {
                        this.valiationResponse[changedRecords[i]].isChanged = false;    
                        this.rowChecked[changedRecords[i]] = false;
                    }
                }

                this.changedRecords = 0;
                this.newRecords = 0;
                this.changedFields = 0;
                for (let i =0 ; i<this.valiationResponse.length;i++)
                {
                    if (this.valiationResponse[i].isNew)
                        this.newRecords ++;
                    if (this.valiationResponse[i].isChanged) 
                    {
                        this.changedRecords ++;   
                        this.changedFields += this.valiationResponse[i].changedColumns.length;
                    }
                }
                this.changing = false;
            }
            ,
            error =>
            {
                this.toastrService.error("An error occured while trying to apply the changes! Please try again!","Error");
                this.changing = false;
            }
        );

    }


    chkallchanged = false;
    CheckAll = false;
    checkallchanged(val : boolean)
    {
        this.chkallchanged = true;
        this.CheckAll = val;   
        for (let i=0;i<this.rowChecked.length;i++)
        {
            if (this.valiationResponse[i].isChanged || this.valiationResponse[i].isNew)
                this.rowChecked[i] = val;
        }
    }

    isChangedCell(row : number,col : number) : boolean
    {
        if (!this.valiationResponse)
            return false;

        for (let i=0 ; i<this.valiationResponse[row].changedColumns.length; i++)
        {
            if (this.valiationResponse[row].changedColumns[i].colIndex == col)
                return true;
        }
        return false;
    }

    IsNewRow(row : number) : boolean
    {
        if (!this.valiationResponse)
            return false;
            
        return this.valiationResponse[row].isNew;
    }
    
    IsChangedRow(row : number) : boolean
    {
        if (!this.valiationResponse)
            return false;

       return this.valiationResponse[row].isChanged;  
    }

    getCellBackGround(row : number, col : number) : string
    {
        if (!this.validate)
            return 'white';

        if (this.valiationResponse[row].isNew)
            return 'rgb(189, 252, 189)';

        if (this.valiationResponse[row].isChanged)
        {
            for (let i=0 ; i < this.valiationResponse[row].changedColumns.length;i++)
            {
                if (this.valiationResponse[row].changedColumns[i].colIndex == col)
                    return 'rgb(168, 4, 4)';
            }
            return 'rgb(255, 199, 199)'
        }   
        return 'white';
    }

    getCellColor(row,col) : string
    {
        if (!this.validate)
            return 'black';

        if (this.valiationResponse[row].isNew)
            return 'black';

        if (this.valiationResponse[row].isChanged)
        {
            for (let i=0 ; i < this.valiationResponse[row].changedColumns.length;i++)
            {
                if (this.valiationResponse[row].changedColumns[i].colIndex == col)
                    return 'white';
            }
            return 'black'
        }   
        return 'black';
    }

    getCellClass(row,col) : string
    {
        if (!this.validate)
            return '';

        if (this.valiationResponse[row].isNew)
            return 'bg-new-record';

        if (this.valiationResponse[row].isChanged)
        {
            for (let i=0 ; i < this.valiationResponse[row].changedColumns.length;i++)
            {
                if (this.valiationResponse[row].changedColumns[i].colIndex == col)
                    return 'bg-changed-cell tip';
            }
            return 'bg-changed-record'
        }   
        return '';
    }

    HasNewOrChanged() : boolean
    {
        if (!this.valiationResponse)
            return false;

        for (let i = 0;i<this.valiationResponse.length;i++)
        {
            if (this.valiationResponse[i].isChanged || this.valiationResponse[i].isNew)
                return true;
        }
        return false;
    }

    IsNewOrChanged(row : number) : boolean
    {
        if (!this.valiationResponse)
            return false;

         if (!this.valiationResponse[row])
            return false;

         return (this.valiationResponse[row].isChanged || this.valiationResponse[row].isNew)
    }



    getCurrentCellValue(row,col) : string
    {
        if (!this.validate)
        return "";

        for (let i=0 ; i<this.valiationResponse[row].changedColumns.length; i++)
        {
            if (this.valiationResponse[row].changedColumns[i].colIndex == col)
                return this.valiationResponse[row].changedColumns[i].currentValue;
        }
        return ""; 
    }
    
    fromCharCode(val : number) : string
    {
        if (65 + val <= 90)
            return String.fromCharCode(65 + val);
        else if (65 + val <= 90 + 26)
            return "A" + String.fromCharCode(65 + val - 26);    
        else if (65 + val <= 90 + 26 + 26)
            return "B" + String.fromCharCode(65 + val - 26 - 26);           
    }

  }