using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebDigitizationProgress
    {
        public int DigitizationId { get; set; }
        public int LocationId { get; set; }
        public int ProgressPercentage { get; set; }

        public LrmisWebLocation Location { get; set; }
    }
}
