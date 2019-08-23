using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
   public class MenuItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public List<MenuItem> SubItems { get; set; }

        public string Icon { get; set; }
        public string DetailMenuItemName { get; set; }

    }
}
