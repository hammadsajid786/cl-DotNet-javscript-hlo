using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebDesignation
    {
        public LrmisWebDesignation()
        {
            LrmisWebUser = new HashSet<LrmisWebUser>();
        }

        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public bool Active { get; set; }
        public string DesignationDescription { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
        public ICollection<LrmisWebUser> LrmisWebUser { get; set; }
    }
}
