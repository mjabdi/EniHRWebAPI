import {Component, Inject, OnInit, ViewEncapsulation, Host} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import { environment } from 'environments/environment';
import { FileHolder } from 'angular2-image-upload';
import { EmployeeService } from 'app/employees/employee.service';
import { ToastrService } from 'ngx-toastr';

const baseUrl :string = environment.apiUrl;

@Component({
    selector: 'upload-picture-dialog',
    templateUrl: './upload-picture-dialog.component.html',
    styleUrls: ['./upload-picture-dialog.component.scss']
})
export class UploadPictureDialogComponent implements OnInit {

    employeeID : number;

    // imageUrl : string;

    constructor(
        private employeeService : EmployeeService,
        private toastrService : ToastrService,
        private dialogRef: MatDialogRef<UploadPictureDialogComponent>,
        @Inject(MAT_DIALOG_DATA) data ) {
            this.employeeID = data;
            // this.imageUrl = baseUrl + '/api/employee/images/' + this.employeeID + '/' + Math.floor(Math.random() * (999999 - 100000)) + 100000 ;
    }

    myUrl : string;

    ngOnInit() {
        this.myUrl = baseUrl + '/api/employee/uploadphoto/' + this.employeeID ;
    }



    customStyle = {
        selectButton: {
        //   "background-color": "yellow",
        //   "border-radius": "25px",
        //   "color": "#000"
            "font-size" : "18px",
            "background-color": "#f44336",
            "margin-right" : "10px",

        },
        clearButton: {
              "background-color": "#eee",
              "color" : "#333",
              "font-size" : "18px"
        //   "border-radius": "25px",
        //   "color": "#000",
        //   "margin-left": "10px"
        },
        layout: {
        //   "background-color": "purple",
        //   "border-radius": "25px",
        //   "color": "#FFF",
        //   "font-size": "15px",
        //   "margin": "10px",
        //   "padding-top": "5px",
        //   "width": "500px"
        },
        previewPanel: {
        //   "background-color": "#894489",
        //   "border-radius": "0 0 25px 25px",
            "width" : "100%",
            "height" : "100px"
        }
      }


      onRemoved(event)
      {
        this.uploadSuccess = false;
      }

      uploadSuccess = false;
      
      onUploadFinished(file: FileHolder)
      {
          if (file.serverResponse.status == 200)
          {
            this.uploadSuccess = true;
          }
          else
          {
            this.toastrService.error("An error occured! Please try again.","Error");
          }
      }


      close()
      {
        this.dialogRef.close(false);
      }

      save()
      {
          this.isSubmit = true;

          this.employeeService.uploadSave(this.employeeID).subscribe(

            data => {
                this.toastrService.success("Profile picture changed successfully.","Picture Changed");
                this.isSubmit = false;
                this.dialogRef.close(true);
            },
            error =>
            {
                this.toastrService.error("An error occured! Please try again.","Error");
                this.isSubmit = false;
            }

          );
      }

      isSubmit = false;
}