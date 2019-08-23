using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebVisitInformationModel
    {
        public int VisitorId { get; set; }
        public string MachineName { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
