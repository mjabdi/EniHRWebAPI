import { Component , Input , Output , EventEmitter} from '@angular/core';
import { AotSummaryResolver, ReturnStatement } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';
import {MatSnackBar} from '@angular/material';
import { ViewEncapsulation,ViewChildren } from '@angular/core';

@Component({
    selector: 'app-dynamictable',
    templateUrl: './dynamictable.component.html',
    styleUrls: ['./dynamictable.component.scss'],
    encapsulation: ViewEncapsulation.None,
  })
  export class DynamicTableComponent {
    @Input('displayname') displayName: string;
    @Input('data') mydata: Array<any>;
    @Output('saveitem') saveEvent: EventEmitter<any> = new EventEmitter<any>();


    goSave(item)
    {
        if (this.newDesc != null)
        {
          item.oldDescription = item.description;
          item.description = this.newDesc;
          this.saveEvent.emit(item);
        }
        this.editItem = null;
        this.newDesc = null;
    }


    editItem = null;
    newDesc = null;
    goEdit(item)
    {
        this.editItem = item;
    }

    goCancel(item)
    {
      this.editItem = null;
      this.newDesc = null;
    }

  }

