using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebComplaintStatus
    {
        public int ComplaintStatusId { get; set; }
        public string ComplaintStatus { get; set; }
        public string ComplaintStatusCode { get; set; }
    }
}
