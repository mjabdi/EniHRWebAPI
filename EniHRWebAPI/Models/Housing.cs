using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EniHRWebAPI.ViewModel;
using Newtonsoft.Json.Linq;

namespace EniHRWebAPI.Models
{
    public class Housing
    {
        [Key]
        public long HousingID { get; set; }

        public virtual Employee employee { get; set; }

        public string HomeAddressUK { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public int? EntitledNumberofBedrooms { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public int? ActualNumberofBedrooms { get; set; }

        public string TypeOfProperty { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public int? RentDueDate { get; set; }

        [Display(Name = "Tenancy.Agreement.Start.Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? TenancyAgreementStartDate { get; set; }

        [Display(Name = "Tenancy.Agreement.End.Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? TenancyAgreementEndDate { get; set; }

        [Display(Name = "Remaining.Months")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public int? MonthRemainingInHouseContract
        {
            get
            {
                if (TenancyAgreementEndDate == null)
                    return null;
                else
                {
                    return (((TimeSpan)(TenancyAgreementEndDate - DateTime.Today)).Days / 30);
                }
            }
        }

        public string MonthNoticePeriod { get; set; }

        public string Currency { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText ="N/A",DataFormatString = "{0:0.00}",ApplyFormatInEditMode =true)]
        public decimal? InitialHouseContractRent { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? CurrentHouseRental { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? EntitledUnFurnishedAllowanceWeek { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? TotalAllowance { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? ActualHousingCosts { get; set; }

        // new changes *************************

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? Deposit { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? DifferenceAllowanceMonthlyCostsPaid { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? FurnitureAllowance { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? ActualFurnitureCosts { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? ParkingCharges { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? RegularPayrollDeduction { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? HRApproval { get; set; }


        public string  UtilitiesIncluded { get; set; }

        public string FurnishedUnFurnished { get; set; }

        // end of new changed *********************************

        public string HousingComments { get; set; }


        public void UpdateFromHousingViewModel(MyDBContext context, JToken housing)
        {
            this.HomeAddressUK = (string)housing["homeAddress"];

            this.EntitledNumberofBedrooms = (int?)housing["entitledBedrooms"];

            this.ActualNumberofBedrooms = (int?)housing["actualBedrooms"];

            this.TypeOfProperty = (string)housing["typeofProperty"];

            this.RentDueDate = (int?)housing["rentDueDate"];

            this.TenancyAgreementStartDate = (DateTime?)housing["tenancyStartDate"];

            this.TenancyAgreementEndDate = (DateTime?)housing["tenancyEndDate"];

            this.MonthNoticePeriod = (string)housing["monthNoticePeriod"];

            try
            {
                this.InitialHouseContractRent = (decimal?)housing["initialRent"];
            }catch (Exception){}

            try
            {
                this.CurrentHouseRental = (decimal?)housing["currentRental"];
            }catch(Exception){}


            try
            {
                this.EntitledUnFurnishedAllowanceWeek = (decimal?)housing["unfurnishedAllowanceWeek"];
            }catch(Exception){}


            try
            {
                this.DifferenceAllowanceMonthlyCostsPaid = (decimal?)housing["differenceAllowanceMonthlyCostsPaid"];
            }catch(Exception){}

            try
            {
                this.FurnitureAllowance = (decimal?)housing["furnitureAllowance"];
            }catch(Exception){}

            try
            {
                this.ActualFurnitureCosts = (decimal?)housing["actualFurnitureCosts"];
            }catch(Exception){}

            try
            {
                this.ParkingCharges = (decimal?)housing["parkingCharges"];
            }catch(Exception){}

            try
            {
                this.RegularPayrollDeduction = (decimal?)housing["regularPayrollDeduction"];
            }catch(Exception){}

            this.UtilitiesIncluded = (string)housing["utilitiesIncluded"];

            this.FurnishedUnFurnished = (string)housing["furnishedUnFurnished"];

            try
            {
                this.Deposit = (decimal?)housing["deposit"];
            }catch(Exception){}

            this.Currency = (string)housing["currency"];

            this.HousingComments = (string)housing["housingComments"];

            try
            {
                this.HRApproval = (decimal?)housing["hrApproval"];
            }catch(Exception){}
        }

        //public void UpdateFromHousingViewModel(MyDBContext context, HousingViewModel housing)
        //{
        //    this.HomeAddressUK = housing.homeAddress;
        //    this.EntitledNumberofBedrooms = housing.entitledBedrooms;
        //    this.ActualNumberofBedrooms = housing.actualBedrooms;
        //    this.TypeOfProperty = housing.typeofProperty;
        //    this.RentDueDate = housing.rentDueDate;
        //    this.TenancyAgreementStartDate = housing.tenancyStartDate;
        //    this.TenancyAgreementEndDate = housing.tenancyEndDate;
        //    this.MonthNoticePeriod = housing.monthNoticePeriod;
        //    this.InitialHouseContractRent = housing.initialRent;
        //    this.CurrentHouseRental = housing.currentRental;
        //    this.EntitledUnFurnishedAllowanceWeek = housing.unfurnishedAllowanceWeek;
        //    this.HousingComments = housing.housingComments;
        //}




    }
}
