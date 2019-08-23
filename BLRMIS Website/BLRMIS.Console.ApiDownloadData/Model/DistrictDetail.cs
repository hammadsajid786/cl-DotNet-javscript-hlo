using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Console.ApiDownloadData.Model
{
    public class DistrictDetail
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public bool IsActive { get; set; }
        public int OperationStatus { get; set; }
    }
}
