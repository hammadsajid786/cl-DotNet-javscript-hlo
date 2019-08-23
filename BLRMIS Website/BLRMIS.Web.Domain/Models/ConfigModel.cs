using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class ConfigModel
    {
        public List<UserFunctionModel> Functions;
        public ConfigModel(){
            Functions = new List<UserFunctionModel>();
        }

    }
}
