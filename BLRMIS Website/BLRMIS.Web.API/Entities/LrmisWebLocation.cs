using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebLocation
    {
        public LrmisWebLocation()
        {
            LrmisWebComplaint = new HashSet<LrmisWebComplaint>();
            LrmisWebUser = new HashSet<LrmisWebUser>();
        }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int DigitizationProgressPercentage { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public ICollection<LrmisWebComplaint> LrmisWebComplaint { get; set; }
        public ICollection<LrmisWebUser> LrmisWebUser { get; set; }
    }
}
