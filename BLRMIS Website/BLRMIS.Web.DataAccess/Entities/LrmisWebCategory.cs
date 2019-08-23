using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebCategory
    {
        public LrmisWebCategory()
        {
            LrmisWebComplaint = new HashSet<LrmisWebComplaint>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
        public ICollection<LrmisWebComplaint> LrmisWebComplaint { get; set; }
    }
}
