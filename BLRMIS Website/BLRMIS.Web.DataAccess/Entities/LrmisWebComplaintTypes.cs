using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebComplaintTypes
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool? Active { get; set; }
    }
}
