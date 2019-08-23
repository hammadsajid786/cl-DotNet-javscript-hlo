using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class DashboardModel
    {
        public string DistrictId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }
    }

    public class DashboardComplaintModel
    {
        public int OpenComplaints { get; set; }
        public int ReOpenComplaints { get; set; }
        public int ClosedComplaints { get; set; }
        public int ResolvedComplaints { get; set; }
        public int PendingComplaints { get; set; }
        public int InProgressComplaints { get; set; }
        public int TotalComplaints { get; set; }

        public int SatisfiedComplaints { get; set; }

        public int UnSatisfiedComplaints { get; set; }

    }
}
