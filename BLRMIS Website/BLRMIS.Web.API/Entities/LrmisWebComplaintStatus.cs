using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebComplaintStatus
    {
        public LrmisWebComplaintStatus()
        {
            LrmisWebComplaint = new HashSet<LrmisWebComplaint>();
            LrmisWebComplaintLog = new HashSet<LrmisWebComplaintLog>();
        }

        public int ComplaintStatusId { get; set; }
        public string ComplaintStatus { get; set; }
        public string ComplaintStatusCode { get; set; }

        public ICollection<LrmisWebComplaint> LrmisWebComplaint { get; set; }
        public ICollection<LrmisWebComplaintLog> LrmisWebComplaintLog { get; set; }
    }
}
