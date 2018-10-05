using System;
namespace EniHRWebAPI.ViewModel
{
    public class LeaveSearchViewModel
    {
        public long? employeeID { get; set; }
        public DateTime? fromDate { get; set; }
        public DateTime? untilDate { get; set; }
        public string leaveType { get; set; }

        public bool MeetLeave(LeaveViewModel leaveVM)
        {
            if (employeeID.HasValue && employeeID.Value > 0)
            {
                if (leaveVM.employeeID != employeeID.Value)
                    return false;
            }



            if (fromDate.HasValue)
            {
                if (leaveVM.fromDate < fromDate.Value && leaveVM.untilDate < fromDate.Value)
                    return false;
            }
            if (untilDate.HasValue)
            {
                if (leaveVM.fromDate > untilDate.Value && leaveVM.untilDate > untilDate.Value)
                    return false;
            }

            if (!String.IsNullOrWhiteSpace(leaveType))
            {
                if (leaveType != leaveVM.leaveType)
                    return false;
            }

            return true;
        }
    }
}
