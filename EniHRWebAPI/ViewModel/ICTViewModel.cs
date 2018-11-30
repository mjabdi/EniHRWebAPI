using System;
using EniHRWebAPI.Models;

namespace EniHRWebAPI.ViewModel
{
    public class ICTViewModel
    {


        public ICTViewModel(ICT ict)
        {
            employeeID = (ict.employee !=null ) ? ict.employee.EmployeeID : 0;
            name = ict.employee?.Name;
            surname = ict.employee?.Surname;
            localPlus = ict.employee?.LocalPlus?.Description;
            technicalID = ict.employee?.UserId;
            workingLocation = ict.employee?.location?.Description;
            employeeCategory = ict.employee?.standardEmployeeCategory?.Description;
            costCentre = ict.employee?.workingCostCentre?.Description;
            activityStatus = ict.employee?.activityStatus?.Description;
            activeDirAccount = ict.ActiveDirAccount;
            workstaionNumber = ict.WorkstaionNumber;
            approvalLevel1 = ict.ApprovalLevel1;
            approvalLevel2 = ict.ApprovalLevel2;
            macroAggregation = ict.MacroAggregation;
            deskPhoneNumber = ict.DeskPhoneNumber;
            mobileNumber = ict.MobileNumber;
            advancedLyncProfile = ict.AdvancedLyncProfile;
            sap = ict.SAP;
            linkToAttachment = ict.LinkToAttachment;
            note = ict.Note;
            startMoveDate = ict.StartMoveDate;
            emailAddress = ict.employee?.EmailAddress;
        }

        public long employeeID { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string localPlus { get; set; }
        public string technicalID { get; set; }
        public string workingLocation { get; set; }
        public string employeeCategory { get; set; }
        public string costCentre { get; set; }
        public string activityStatus { get; set; }

        public string activeDirAccount { get; set; }
        public string workstaionNumber { get; set; }
        public string approvalLevel1 { get; set; }
        public string approvalLevel2 { get; set; }
        public string macroAggregation { get; set; }
        public string deskPhoneNumber { get; set; }
        public string mobileNumber { get; set; }
        public bool advancedLyncProfile { get; set; }
        public bool sap { get; set; }
        public string linkToAttachment { get; set; }
        public string note { get; set; }
        public DateTime? startMoveDate { get; set; }
        public string emailAddress { get; set; }


    }
}
