using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EniHRWebAPI.Models
{
    public class HomeAddressHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public long EmployeeID { get; set;}

        public string address { get; set; }
        public DateTime changeddate { get; set; }
        public string changedby { get; set; }
    }
}
