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
        public decimal? EntitledUnFurnishedAllowanceMonth
        {
            get
            {
                if (EntitledUnFurnishedAllowanceWeek == null)
                    return null;
                else
                    return (EntitledUnFurnishedAllowanceWeek * 52 / 12);
            }
        }



        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? EntitledFurnishedAllowanceWeek {
            get
            {
                if (EntitledUnFurnishedAllowanceWeek == null)
                    return null;
                else
                    return (EntitledUnFurnishedAllowanceWeek * new decimal(1.1));
            }
        }

        public void UpdateFromHousingViewModel(MyDBContext context, HousingViewModel housing)
        {
            this.HomeAddressUK = housing.homeAddress;
            this.EntitledNumberofBedrooms = housing.entitledBedrooms;
            this.ActualNumberofBedrooms = housing.actualBedrooms;
            this.TypeOfProperty = housing.typeofProperty;
            this.RentDueDate = housing.rentDueDate;
            this.TenancyAgreementStartDate = housing.tenancyStartDate;
            this.TenancyAgreementEndDate = housing.tenancyEndDate;
            this.MonthNoticePeriod = housing.monthNoticePeriod;
            this.InitialHouseContractRent = housing.initialRent;
            this.CurrentHouseRental = housing.currentRental;
            this.EntitledUnFurnishedAllowanceWeek = housing.unfurnishedAllowanceWeek;
            this.HousingComments = housing.housingComments;
        }

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

            this.InitialHouseContractRent = (decimal?)housing["initialRent"];

            this.CurrentHouseRental = (decimal?)housing["currentRental"];

            this.EntitledUnFurnishedAllowanceWeek = (decimal?)housing["unfurnishedAllowanceWeek"];

            this.HousingComments = (string)housing["housingComments"];
        }


        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? EntitledFurnishedAllowanceMonth
        {
            get
            {
                return (EntitledFurnishedAllowanceWeek * 52 / 12);
            }
        }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? DifferenceBetweenAllowanceMonthlyRate
        {
            get
            {
                return (EntitledFurnishedAllowanceMonth - CurrentHouseRental);
            }
        }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? TotalAllowance { get; set; }


        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? Furniture { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(NullDisplayText = "N/A", DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        public decimal? ActualHousingCosts { get; set; }

        public string HousingComments { get; set; }


    }
}
