using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebVerificationToken
    {
        public int VerificationId { get; set; }
        public string VerificationCode { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? CreatedBy { get; set; }
        public bool? Expired { get; set; }
        public string PhoneNumber { get; set; }
        public bool? Consumed { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
    }
}
