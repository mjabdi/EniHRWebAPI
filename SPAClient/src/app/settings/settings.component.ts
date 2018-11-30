import { Component } from '@angular/core';
import { AotSummaryResolver, ReturnStatement } from '@angular/compiler';
import { ToastrService } from 'ngx-toastr';
import {MatSnackBar} from '@angular/material';
import { ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { BaseInfoService } from 'app/services/baseInfo.service';



@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.scss'],
    encapsulation: ViewEncapsulation.None,
  })
  export class SettingsComponent {

    constructor(
        private router : Router,
        private baseinfoService : BaseInfoService,
        private snackBar : MatSnackBar
    ){}


stafftypology = null;
workinglocation = null;
employeeCategory = null;
organizationUnit = null;
costCentre = null;
position = null;
professionalArea = null;
homeCompany = null;
country = null;
familyStatus = null;
typeofVisa = null;
activityStatus = null;

ngOnInit()
{
    this.loadStaffTypology();
    this.loadWorkingLocation();
    this.loadEmployeeCategory();
    this.loadOrganizationUnit();
    this.loadCostCentre();
    this.loadPosition();
    this.loadProfessionalArea();
    this.loadHomeCompany();
    this.loadCountry();
    this.loadFamilyStatus();
    this.loadTypeofVisa();
    this.loadActivityStatus();
}

loadStaffTypology()
{
    this.baseinfoService.getLocalPlusTable().subscribe(
        (data : any[]) =>
        {
            this.stafftypology = data;
        }
    );
}

loadWorkingLocation()
{
    this.baseinfoService.getWorkingLocationTable().subscribe(
        (data : any[]) =>
        {
            this.workinglocation = data;
        }
    );
}


loadEmployeeCategory()
{
    this.baseinfoService.getEmployeeCategoryTable().subscribe(
        (data : any[]) =>
        {
            this.employeeCategory = data;
        }
    );
}

loadOrganizationUnit()
{
    this.baseinfoService.getOrganizationUnitTable().subscribe(
        (data : any[]) =>
        {
            this.organizationUnit = data;
        }
    );
}

loadCostCentre()
{
    this.baseinfoService.getCostCentreTable().subscribe(
        (data : any[]) =>
        {
            this.costCentre = data;
        }
    );
}

loadPosition()
{
    this.baseinfoService.getPositionTable().subscribe(
        (data : any[]) =>
        {
            this.position = data;
        }
    );
}

loadProfessionalArea()
{
    this.baseinfoService.getProfessionalAreaTable().subscribe(
        (data : any[]) =>
        {
            this.professionalArea = data;
        }
    );
}

loadHomeCompany()
{
    this.baseinfoService.getHomeCompanyTable().subscribe(
        (data : any[]) =>
        {
            this.homeCompany = data;
        }
    );
}

loadCountry()
{
    this.baseinfoService.getCountryTable().subscribe(
        (data : any[]) =>
        {
            this.country = data;
        }
    );
}

loadFamilyStatus()
{
    this.baseinfoService.getFamilyStatusTable().subscribe(
        (data : any[]) =>
        {
            this.familyStatus = data;
        }
    );
}

loadTypeofVisa()
{
    this.baseinfoService.getTypeofVisaTable().subscribe(
        (data : any[]) =>
        {
            this.typeofVisa = data;
        }
    );
}

loadActivityStatus()
{
    this.baseinfoService.getActivityStatusTable().subscribe(
        (data : any[]) =>
        {
            this.activityStatus = data;
        }
    );
}





savestafftypology(event)
{
    this.baseinfoService.UpdateLocalPlusTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadStaffTypology();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadStaffTypology();
        }
    );

}

saveworkinglocation(event)
{
    this.baseinfoService.UpdateWorkingLocationTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadWorkingLocation();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadWorkingLocation();
        }
    );
}

saveEmployeeCategory(event)
{
    this.baseinfoService.UpdateEmployeeCategoryTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadEmployeeCategory();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadEmployeeCategory();
        }
    );
}

saveOrganizationUnit(event)
{
    this.baseinfoService.UpdateOrganizationUnitTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadOrganizationUnit();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadOrganizationUnit();
        }
    );
}

saveCostCentre(event)
{
    this.baseinfoService.UpdateCostCentreTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadCostCentre();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadCostCentre();
        }
    );
}

savePosition(event)
{
    this.baseinfoService.UpdatePositionTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadPosition();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadPosition();
        }
    );
}

saveProfessionalArea(event)
{
    this.baseinfoService.UpdateProfessionalAreaTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadProfessionalArea();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadProfessionalArea();
        }
    );
}

saveHomeCompany(event)
{
    this.baseinfoService.UpdateHomeCompanyTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadHomeCompany();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadHomeCompany();
        }
    );
}

saveCountry(event)
{
    this.baseinfoService.UpdateCountryTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadCountry();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadCountry();
        }
    );
}

saveFamilyStatus(event)
{
    this.baseinfoService.UpdateFamilyStatusTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadFamilyStatus();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadFamilyStatus();
        }
    );
}

saveTypeofVisa(event)
{
    this.baseinfoService.UpdateTypeofVisaTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadTypeofVisa();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadTypeofVisa();
        }
    );
}

saveActivityStatus(event)
{
    this.baseinfoService.UpdateActivityStatusTable(event).subscribe(
        data =>
        {
            this.snackBar.open('Record Updated','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });
              this.loadActivityStatus();
        },
        error =>
        {
            this.snackBar.open('An Error Occured!','OK',{
                duration: 5000,
                panelClass : 'my-snackbar-style'
              });  
              this.loadActivityStatus();
        }
    );
}


selectedIndex:number=0;
one()
{
   this.selectedIndex = 1;
}

two()
{
    this.selectedIndex = 2;
}

three()
{
    this.selectedIndex = 3;
}

  }

