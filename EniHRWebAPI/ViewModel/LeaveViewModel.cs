using System;
using EniHRWebAPI.Models;

namespace EniHRWebAPI.ViewModel
{
    public class LeaveViewModel
    {

        public LeaveViewModel()
        {
            
        }

        public LeaveViewModel(Leave leave)
        {
            employeeID = leave.EmployeeID;
            name = leave.employee.Name;
            surname = leave.employee.Surname;
            localPlus = leave.employee.LocalPlus?.Description;
            technicalID = leave.employee.UserId;
            workingLocation = leave.employee.location?.Description;
            employeeCategory = leave.employee.standardEmployeeCategory?.Description;
            costCentre = leave.employee.workingCostCentre?.ID + " | " + leave.employee.workingCostCentre?.Description;
            familyStatus = leave.employee.familyStatus?.Description;
            followingPartner = leave.employee.FollowingPartner;
            followingChildren = leave.employee.FollowingChildren;
            activityStatus = leave.employee.activityStatus?.Description;
            leaveID = leave.LeaveID;
            fromDate = leave.FromDate;
            untilDate = leave.UntilDate;
            countedDays = leave.CountedDays;
            leaveType = leave.LeaveType;
            comment = leave.Comment;
            attachmentUrl = leave.AttachmentUrl;
            registeredDateTime = leave.RegisteredDataTime;

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
        public long? leaveID { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime untilDate { get; set; }
        public int countedDays { get; set; }
        public string leaveType { get; set; }
        public string comment { get; set; }
        public string attachmentUrl { get; set; }
        public DateTime? registeredDateTime { get; set; }

        public Leave GetLeave(MyDBContext context)
        {
            Leave leave = new Leave();
            leave.EmployeeID = employeeID;
            leave.employee = context.Employees.Find(leave.EmployeeID);
            leave.AttachmentUrl = attachmentUrl;
            leave.Comment = comment;
            leave.CountedDays = countedDays;
            leave.FromDate = fromDate;
            leave.UntilDate = untilDate;
            leave.LeaveType = leaveType;
            leave.RegisteredDataTime = DateTime.Now;

            return leave;
        }
    }
}
