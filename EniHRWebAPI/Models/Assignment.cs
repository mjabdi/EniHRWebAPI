using System;
using System.ComponentModel.DataAnnotations;



namespace EniHRWebAPI.Models
{
    public class Assignment
    {

        [Display(Name = "Employee#")]
        [Key]
        public long EmployeeID { get; set; }

        public virtual Employee Employee {get;set;}

        [Display(Name = "Contract.Start.Date")]
		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Contract.End.Date")]
		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true, NullDisplayText = "N/A")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Remaining.Days")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public int? RemainingDays{
            get {
                if (EndDate == null)
                    return null;
                else
                {
                    return ((TimeSpan)(EndDate - DateTime.Today)).Days;
                }
            }
        }
        
    
    }
}
