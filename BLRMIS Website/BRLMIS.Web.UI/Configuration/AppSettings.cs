using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRLMIS.Web.UI.Configuration
{
    public class AppSettings
    {
        public string APIBaseUrl { get; set; }
        public string Host { get; set; }
        public string APIKey { get; set; }
        public string PublicUserId { get; set; }
        public string StampDutyPercentage { get; set; }
        public string DistrictCouncilFeePercentage { get; set; }
        public string CapitalValueTaxPercentage { get; set; }
        public string RegistrationFee { get; set; }
        public string PlaystoreLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string YoutubeLink { get; set; }
        public string LinkedInLink { get; set; }
        public string CalculateDCRateLink { get; set; }

    }
}
