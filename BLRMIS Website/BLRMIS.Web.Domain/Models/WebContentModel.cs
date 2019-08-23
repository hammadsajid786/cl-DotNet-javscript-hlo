using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLRMIS.Web.Domain.Models
{
    public partial class WebContentModel
    {
        public int ContentId { get; set; }

        [Required]
        public int ContentPageId { get; set; }

        [Required]
        public string ContentDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? CreatedBy { get; set; }
        public string ContentPageName { get; set; }
        public string ContentPageUrl { get; set; }
        public string ContentPageIcon { get; set; }
    }
}
