using System;
using System.Collections.Generic;

namespace BLRMIS.Web.Domain.Models
{
    public partial class WebPageModel
    {
        public string PageId { get; set; }
        public string PageName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
