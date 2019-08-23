using System;
using System.Collections.Generic;

namespace BLRMIS.Web.API.Entities
{
    public partial class LrmisWebFunctionRoleMapping
    {
        public int MappingId { get; set; }
        public int FunctionId { get; set; }
        public int RoleId { get; set; }
        public bool Include { get; set; }

        public LrmisWebFunctions Function { get; set; }
        public LrmisWebRole Role { get; set; }
    }
}
