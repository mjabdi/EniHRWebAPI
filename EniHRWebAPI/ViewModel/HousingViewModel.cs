using System;
using EniHRWebAPI.Models;

namespace EniHRWebAPI.ViewModel
{
    public class HousingViewModel
    {
        public HousingViewModel(Housing housing)
        {
            employeeID = housing.employee.EmployeeID;
            name = MyStringFormatter.Capitalize(housing.employee.Name);
            surname = MyStringFormatter.Capitalize(housing.employee.Surname);
            localPlus = housing.employee.LocalPlus?.Description;
            technicalID = housing.employee.UserId;
            workingLocation = housing.employee.location?.Description;
            employeeCategory = housing.employee.standardEmployeeCategory?.Description;
            costCentre =  housing.employee.workingCostCentre?.ID + " | " + housing.employee.workingCostCentre?.Description;
            familyStatus = housing.employee.familyStatus?.Description;
            followingPartner = housing.employee.FollowingPartner;
            followingChildren = housing.employee.FollowingChildren;
            activityStatus = housing.employee.activityStatus?.Description;
            homeAddress = housing.HomeAddressUK;
            entitledBedrooms = housing.EntitledNumberofBedrooms;
            actualBedrooms = housing.ActualNumberofBedrooms;
            typeofProperty = housing.TypeOfProperty;
            rentDueDate = housing.RentDueDate;
            tenancyStartDate = housing.TenancyAgreementStartDate;
            tenancyEndDate = housing.TenancyAgreementEndDate;
            monthNoticePeriod = housing.MonthNoticePeriod;
            initialRent = housing.InitialHouseContractRent;
            currentRental = housing.CurrentHouseRental;
            unfurnishedAllowanceWeek = housing.EntitledUnFurnishedAllowanceWeek;

            differenceAllowanceMonthlyCostsPaid = housing.DifferenceAllowanceMonthlyCostsPaid;
            furnitureAllowance = housing.FurnitureAllowance;
            actualFurnitureCosts = housing.ActualFurnitureCosts;
            parkingCharges = housing.ParkingCharges;
            regularPayrollDeduction = housing.RegularPayrollDeduction;
            utilitiesIncluded = housing.UtilitiesIncluded;
            furnishedUnFurnished = housing.FurnishedUnFurnished;
            hrApproval = housing.HRApproval;

            deposit = housing.Deposit;
            currency = housing.Currency;

            housingComments = housing.HousingComments;
        }


        public long employeeID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string localPlus { get; set; }
        public string technicalID { get; set; }
        public string workingLocation { get; set; }
        public string employeeCategory { get; set; }
        public string costCentre { get; set; }
        public string familyStatus { get; set; }
        public bool? followingPartner { get; set; }
        public int? followingChildren { get; set; }
        public string activityStatus { get; set; }
        public string homeAddress { get; set; }
        public int? entitledBedrooms { get; set; }
        public int? actualBedrooms { get; set; }
        public string typeofProperty { get; set; }
        public int? rentDueDate { get; set; }
        public DateTime? tenancyStartDate { get; set; }
        public DateTime? tenancyEndDate { get; set; }
        public string monthNoticePeriod { get; set; }
        public decimal? initialRent { get; set; }
        public decimal? currentRental { get; set; }
        public decimal? unfurnishedAllowanceWeek { get; set; }

        public decimal? differenceAllowanceMonthlyCostsPaid { get; set; }
        public decimal? furnitureAllowance { get; set; }
        public decimal? actualFurnitureCosts { get; set; }
        public decimal? parkingCharges { get; set; }
        public decimal? regularPayrollDeduction { get; set; }
        public decimal? deposit { get; set; }
        public decimal? hrApproval { get; set; }


        public string utilitiesIncluded { get; set; }
        public string furnishedUnFurnished { get; set; }

        public string currency { get; set; }

        public string housingComments { get; set; }
    }
}
