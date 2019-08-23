using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebDownloadModel
    {
        public int DownloadId { get; set; }
        public string DownloadTitle { get; set; }
        public string DownloadDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
    }
}
