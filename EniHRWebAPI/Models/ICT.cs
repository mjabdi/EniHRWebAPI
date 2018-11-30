using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EniHRWebAPI.ViewModel;
using Newtonsoft.Json.Linq;

namespace EniHRWebAPI.Models
{
    public class ICT
    {

        [Key]
        public long ICTID { get; set; }

        public virtual Employee employee { get; set; }

        public string ActiveDirAccount { get; set; }
        public string WorkstaionNumber { get; set; }
        public string ApprovalLevel1 { get; set; }
        public string ApprovalLevel2 { get; set; }
        public string MacroAggregation { get; set; }
        public string DeskPhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public bool AdvancedLyncProfile { get; set; }
        public bool SAP { get; set; }
        public string LinkToAttachment { get; set; }
        public string Note { get; set; }
        public DateTime? StartMoveDate { get; set; }


        public void UpdateFromICTViewModel(MyDBContext context, JToken ictVM)
        {
            this.ActiveDirAccount =  (string)ictVM["activeDirAccount"];
            this.WorkstaionNumber = (string)ictVM["workstaionNumber"]; 
            this.ApprovalLevel1 = (string)ictVM["approvalLevel1"];
            this.ApprovalLevel2 = (string)ictVM["approvalLevel2"];
            this.MacroAggregation = (string)ictVM["macroAggregation"];
            this.DeskPhoneNumber = (string)ictVM["deskPhoneNumber"];
            this.MobileNumber = (string)ictVM["mobileNumber"];
            this.AdvancedLyncProfile = (bool)ictVM["advancedLyncProfile"];
            this.SAP = (bool)ictVM["sap"];
            //this.LinkToAttachment = ictVM.linkToAttachment;
            this.Note = (string)ictVM["note"];
            this.StartMoveDate = (DateTime?)ictVM["startMoveDate"];

            if (this.employee == null)
            {
                this.employee = context.Employees.Find(ICTID);
            }

            this.employee.EmailAddress = (string)ictVM["emailAddress"];
        }
    }
}
