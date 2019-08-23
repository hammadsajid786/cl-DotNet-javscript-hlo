using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebDepartment
    {
        public LrmisWebDepartment()
        {
            LrmisWebUser = new HashSet<LrmisWebUser>();
        }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public bool Active { get; set; }
        public string DepartmentDescription { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
        public ICollection<LrmisWebUser> LrmisWebUser { get; set; }
    }
}
