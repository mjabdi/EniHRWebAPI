using System;
using System.ComponentModel.DataAnnotations;
using EniHRWebAPI.Models;

namespace EniHRWebAPI.ViewModel
{
    public class EmployeeViewModel
    {
        public EmployeeViewModel()
        {
            
        }


        public EmployeeViewModel(Employee employee)
        {
            employeeID = employee.EmployeeID;
            name = MyStringFormatter.Capitalize(employee.Name);
            surname =  MyStringFormatter.Capitalize(employee.Surname);
            technicalID = employee.UserId;
            localPlus = employee.LocalPlus?.Description;
            workingLocation = employee.location?.Description;
            employeeCategory = employee.standardEmployeeCategory?.Description;
            organizationUnit = employee.organisationUnit?.Description;
            costCentre = employee.workingCostCentre?.ID + " | " +  employee.workingCostCentre?.Description;
            position = employee.position?.Description;
            professionalArea = employee.professionalArea?.Description;
            companyHiringDate = employee.CompanyHiringDate;
            homeCompany = employee.homeCompany?.Description;
            yearsInEni = employee.NumberofYearsInEni;
            gender = employee.GenderString;
            birthDate = employee.BirthDate;
            age = employee.Age;
            countryOfBirth = employee.CountryofBirth?.CountryName;
            nationality = employee.Nationality?.CountryName;
            familyStatus = employee.familyStatus?.Description;
            followingPartner = employee.FollowingPartner;
            followingChildren = employee.FollowingChildren;
            spouseNationality = employee.SpouseNationality?.CountryName;
            typeOfVisa = employee.typeOfVISA?.Description;
            visaExpiryDate = employee.VISAExpirayDate;
            emailAddress = employee.EmailAddress;
            activityStatus = employee.activityStatus?.Description;
                              
        }

        public long employeeID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string technicalID { get; set; }
        public string localPlus { get; set; }
        public string workingLocation { get; set; }
        public string employeeCategory { get; set; }
        public string organizationUnit { get; set; }
        public string costCentre { get; set; }
        public string position { get; set; }
        public string professionalArea { get; set; }
        public DateTime? companyHiringDate { get; set; }
        public string homeCompany { get; set; }
        public double? yearsInEni { get; set; }
        public string gender { get; set; }
        public DateTime? birthDate { get; set; }
        public int? age { get; set; }
        public string countryOfBirth { get; set; }
        public string nationality { get; set; }
        public string familyStatus { get; set; }
        public bool? followingPartner { get; set; }
        public int? followingChildren { get; set; }
        public string spouseNationality { get; set; }
        public string typeOfVisa { get; set; }
        public DateTime? visaExpiryDate { get; set; }
        public string emailAddress { get; set; }
        public string activityStatus { get; set; }


        public double CalcCompleteness()
        {
            double emptyCounter = 0.0;
            double total = 24.0;

            if (isEmpty(name))  emptyCounter++;
            if (isEmpty(surname)) emptyCounter++;
            if (isEmpty(technicalID)) emptyCounter++;
            if (isEmpty(localPlus)) emptyCounter++;
            if (isEmpty(workingLocation)) emptyCounter++;
            if (isEmpty(employeeCategory)) emptyCounter++;
            if (isEmpty(organizationUnit)) emptyCounter++;
            if (isEmpty(costCentre)) emptyCounter++;
            if (isEmpty(position)) emptyCounter++;
            if (isEmpty(professionalArea)) emptyCounter++;
            if (isEmpty(companyHiringDate)) emptyCounter++;
            if (isEmpty(homeCompany)) emptyCounter++;
            if (isEmpty(gender)) emptyCounter++;
            if (isEmpty(birthDate)) emptyCounter++;
            if (isEmpty(countryOfBirth)) emptyCounter++;
            if (isEmpty(nationality)) emptyCounter++;
            if (isEmpty(familyStatus)) emptyCounter++;
            if (isEmpty(followingPartner)) emptyCounter++;
            if (isEmpty(followingChildren)) emptyCounter++;
            if (isEmpty(spouseNationality)) emptyCounter++;
            if (isEmpty(typeOfVisa)) emptyCounter++;
            if (isEmpty(visaExpiryDate)) emptyCounter++;
            if (isEmpty(emailAddress)) emptyCounter++;
            if (isEmpty(activityStatus)) emptyCounter++;

            return (1.0 - (emptyCounter/total));
        }

        private bool isEmpty(object obj)
        {
            if (obj is string)
            {
                return String.IsNullOrWhiteSpace((string)obj)
                             || ((string)obj).Trim().ToLower() == "n/a"
                             || ((string)obj).Trim().ToLower() == "-"
                             || ((string)obj).Trim().ToLower() == "|";
            }
            else if (obj is DateTime?)
            {
                return !((DateTime?)obj).HasValue;
            }
            else if (obj is int?){
                    return !((int?)obj).HasValue;
            }
            else if (obj is bool?)
            {
                return !((bool?)obj).HasValue;
            }
            return false;    
        }
    }
}
