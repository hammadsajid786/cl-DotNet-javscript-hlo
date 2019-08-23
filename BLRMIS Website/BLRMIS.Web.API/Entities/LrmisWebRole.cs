using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebRole
    {
        public LrmisWebRole()
        {
            LrmisWebFunctionRoleMapping = new HashSet<LrmisWebFunctionRoleMapping>();
            LrmisWebUser = new HashSet<LrmisWebUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
        public ICollection<LrmisWebFunctionRoleMapping> LrmisWebFunctionRoleMapping { get; set; }
        public ICollection<LrmisWebUser> LrmisWebUser { get; set; }
    }
}
