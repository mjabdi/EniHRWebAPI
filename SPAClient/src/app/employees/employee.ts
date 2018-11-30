import { stream } from "xlsx/types";

export class Employee {

employeeID : number;
name : string;
surname : string;
technicalID : string;
localPlus : string;
workingLocation : string;
employeeCategory : string;
organizationUnit : string;
costCentre : string;
position : string;
professionalArea : string;
companyHiringDate : Date;
homeCompany : string;
yearsInEni : number;
gender : string;
birthDate : Date;
age : number;
countryOfBirth : string;
nationality : string;
familyStatus : string;
followingPartner : boolean;
followingChildren : number;
spouseNationality : string;
typeOfVisa : string;
visaExpiryDate : Date;
emailAddress : string;
activityStatus : string;

isNew: boolean;


public toArray(length) : string[]
{
    var strarray : string[];
    for(var i = 0; i< length; i++)
    {
        strarray.push('test');
    }
    return strarray;
}

}
