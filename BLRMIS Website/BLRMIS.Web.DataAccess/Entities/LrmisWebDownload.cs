using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebDownload
    {
        public int DownloadId { get; set; }
        public string DownloadTitle { get; set; }
        public string DownloadDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
    }
}
