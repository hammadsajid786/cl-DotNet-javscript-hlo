using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebPage
    {
        public LrmisWebPage()
        {
            LrmisWebContent = new HashSet<LrmisWebContent>();
        }

        public int PageId { get; set; }
        public string PageName { get; set; }

        public ICollection<LrmisWebContent> LrmisWebContent { get; set; }
    }
}
