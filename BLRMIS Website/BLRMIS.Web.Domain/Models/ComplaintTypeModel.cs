using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
   public class ComplaintTypeModel
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public bool? Active { get; set; }
    }
}
