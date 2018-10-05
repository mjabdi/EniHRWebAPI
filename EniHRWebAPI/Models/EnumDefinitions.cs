using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EniHRWebAPI.Models
{
   
    public class LocalPlus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Local.Plus")]
        public string Description { get; set; }
    }

    public class StaffTypology
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Staff.Typology")]
		public string Description { get; set; }
    }

    public class StandardEmployeeCategory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Employee.Cat")]
		public string Description { get; set; }
    }

    public class BusinessUnit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Business.Unit")]
        public string Description { get; set; }
    }


    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Location")]
		public string Description { get; set; }
    }

    public class OrganisationUnit
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Organisation.Unit")]
		public string Description { get; set; }
    }

    public class WorkingCostCentre
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
		[Display(Name = "Cost.Centre")]
		public string Description { get; set; }
    }

    public class Position
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string ID { get; set; }
		[Display(Name = "Position")]
        public string Description { get; set; }
    }

    public class ParentPosition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Parent.Position")]
        public string Description { get; set; }
    }


    public class ReportsTo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Reports.To")]
        public string Description { get; set; }
    }

    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Project")]
        public string Description { get; set; }
    }

    public class COS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "COS")]
        public string Description { get; set; }
    }

    public class FamilyStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Family.Status")]
        public string Description { get; set; }
    }

    public class ActivityStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Activity.Status")]
        public string Description { get; set; }
    }



    public class ProfessionalArea
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Professional.Area")]
		public string Description { get; set; }
    }

    public class HomeCompany
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Home.Company")]
        public string Description { get; set; }
    }

    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Country")]
        public string CountryName { get; set; }
    }

    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string CityName { get; set; }
        public Country CityCountry { get; set; }
    }

    public class AssignmentStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Assignment.Status")]
        public string Description { get; set; }
    }

    public class TypeOfVISA
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
		[Display(Name = "Type.of.VISA")]
        public string Description { get; set; }
    }


    public class TypeOfProperty
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Display(Name = "Type.of.Property")]
        public string Description { get; set; }
    }


    public class Child
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Display(Name = "Child.Name")]
        public string ChildName { get; set; }

		[DisplayFormat(NullDisplayText = "N/A", ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
		[Display(Name = "Birth.Date")]
		public DateTime? DateofBirth { get; set; }

        [DisplayFormat(NullDisplayText = "N/A")]
        public double? Age
        {
            get
            {
                if (DateofBirth == null)
                    return null;
                else
                {
                    int days = ((TimeSpan)(DateofBirth - DateTime.Today)).Days;
                    return ((double)days) / 365.0;
                }
            }
        }

    }
}
