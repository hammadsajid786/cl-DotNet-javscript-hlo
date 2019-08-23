using BLRMIS.Web.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class SortModel
    {
        public string ColumnName { get; set; }

        public SortingType SortType { get; set; }
    }
}
