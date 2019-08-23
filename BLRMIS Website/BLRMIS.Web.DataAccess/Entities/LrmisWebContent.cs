using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebContent
    {
        public int ContentId { get; set; }
        public int ContentPageId { get; set; }
        public string ContentDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }

        public LrmisWebPage ContentPage { get; set; }
        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
    }
}
