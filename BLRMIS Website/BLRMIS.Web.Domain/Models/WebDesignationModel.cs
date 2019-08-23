using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebDesignationModel
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public bool Active { get; set; }
        public string DesignationDescription { get; set; }
    }
}
