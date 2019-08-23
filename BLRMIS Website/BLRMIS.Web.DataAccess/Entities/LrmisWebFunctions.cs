using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebFunctions
    {
        public LrmisWebFunctions()
        {
            LrmisWebComplaint = new HashSet<LrmisWebComplaint>();
            LrmisWebFunctionRoleMapping = new HashSet<LrmisWebFunctionRoleMapping>();
        }

        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public string FunctionDescription { get; set; }
        public string FunctionCode { get; set; }

        public ICollection<LrmisWebComplaint> LrmisWebComplaint { get; set; }
        public ICollection<LrmisWebFunctionRoleMapping> LrmisWebFunctionRoleMapping { get; set; }
    }
}
