using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public partial class WebApiDownloadedDataModel
    {
        public int Id { get; set; }
        public int? TotalVisited { get; set; }
        public int? TotalLandTransfered { get; set; }
        public int? TotalRegistries { get; set; }
        public int? TotalAmount { get; set; }
        public int? IssuanceCount { get; set; }
        public int? ReportedGrievance { get; set; }
        public int? ResolvedGrievance { get; set; }
        public DateTime? DownloadedDate { get; set; }
    }
}
