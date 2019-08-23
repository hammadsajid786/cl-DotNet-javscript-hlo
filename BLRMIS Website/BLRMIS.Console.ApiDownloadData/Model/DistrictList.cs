using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Console.ApiDownloadData.Model
{
    public class DistrictList
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<DistrictDetail> Data { get; set; }
    }
}
