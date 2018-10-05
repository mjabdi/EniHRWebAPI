using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EniHRWebAPI.ViewModel;

namespace EniHRWebAPI.Models
{
    public class Leave
    {

        [Key]
        public long LeaveID { get; set; }

        public long EmployeeID { get; set; }

        public virtual Employee employee { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime UntilDate { get; set; }

        public int CountedDays { get; set; }

        public string LeaveType { get; set; }

        public string Comment { get; set; }

        public string AttachmentUrl { get; set; }

        public DateTime RegisteredDataTime { get; set; }

        public void updateLeaveFromViewModel(MyDBContext context, LeaveViewModel leaveVM)
        {
            EmployeeID = leaveVM.employeeID;
            employee = context.Employees.Find(EmployeeID);
            FromDate = leaveVM.fromDate;
            UntilDate = leaveVM.untilDate;
            CountedDays = leaveVM.countedDays;
            LeaveType = leaveVM.leaveType;
            Comment = leaveVM.comment;
            AttachmentUrl = leaveVM.attachmentUrl;
        }
    }
}
