using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebLocationModel
    {
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int DigitizationProgressPercentage { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
