using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Console.ApiDownloadData.Model
{
    public class DeliveryStats
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public int TotalVisited { get; set; }
        public int TotalLandTransfered { get; set; }
        public int TotalRegistries { get; set; }
        public int TotalAmount { get; set; }
        public int IssuanceCount { get; set; }
    }
}
