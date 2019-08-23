using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebVisitorInformation
    {
        public int VisitorId { get; set; }
        public string MachineName { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
