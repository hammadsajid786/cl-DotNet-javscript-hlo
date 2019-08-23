using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
  public  class SelectListModel
    {
        public bool Disabled { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
    }
}
