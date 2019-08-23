using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebComplaintLog
    {
        public int ComplaintCommentId { get; set; }
        public int ComplaintId { get; set; }
        public int ComplaintStatusId { get; set; }
        public int? ComplaintAssignTo { get; set; }
        public int? ComplaintAssignBy { get; set; }
        public string ComplaintComments { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }

        public LrmisWebComplaint Complaint { get; set; }
        public LrmisWebUser ComplaintAssignByNavigation { get; set; }
        public LrmisWebUser ComplaintAssignToNavigation { get; set; }
        public LrmisWebComplaintStatus ComplaintStatus { get; set; }
        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
    }
}
