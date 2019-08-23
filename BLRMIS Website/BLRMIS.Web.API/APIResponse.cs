using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLRMIS.Web.API
{
    public class APIResponse
    {
        public string status { get; set; }
        public string description { get; set; }
        public APIResponse(string status , string description)
        {
            this.status = status;
            this.description = description;

        }
    }
}
