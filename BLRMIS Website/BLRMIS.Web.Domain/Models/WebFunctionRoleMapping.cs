using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebFunctionRoleMapping
    {
        public int MappingId { get; set; }
        public int FunctionId { get; set; }
        public string FunctionName { get; set; }
        public int RoleId { get; set; }
        public bool Include { get; set; }
    }
}
