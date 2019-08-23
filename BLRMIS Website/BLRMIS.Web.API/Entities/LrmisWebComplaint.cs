using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebComplaint
    {
        public LrmisWebComplaint()
        {
            LrmisWebComplaintLog = new HashSet<LrmisWebComplaintLog>();
        }

        public int ComplaintId { get; set; }
        public string ComplaintTitle { get; set; }
        public string ComplaintDescription { get; set; }
        public string CitizenName { get; set; }
        public string CitizenPhoneNumber { get; set; }
        public string CitizenEmailAddress { get; set; }
        public string CitizenCnic { get; set; }
        public int LocationId { get; set; }
        public int ComplaintCategoryId { get; set; }
        public int ComplaintStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedBy { get; set; }
        public string ComplaintCode { get; set; }
        public string ComplaintAccessToken { get; set; }
        public int? FunctionId { get; set; }
        public int? ComplaintAssignTo { get; set; }

        public LrmisWebUser ComplaintAssignToNavigation { get; set; }
        public LrmisWebCategory ComplaintCategory { get; set; }
        public LrmisWebComplaintStatus ComplaintStatus { get; set; }
        public LrmisWebFunctions Function { get; set; }
        public LrmisWebLocation Location { get; set; }
        public LrmisWebUser LockedByNavigation { get; set; }
        public ICollection<LrmisWebComplaintLog> LrmisWebComplaintLog { get; set; }
    }
}
