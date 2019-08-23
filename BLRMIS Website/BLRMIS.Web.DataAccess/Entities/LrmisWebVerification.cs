using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebVerification
    {
        public int VerificationId { get; set; }
        public string VarificationCode { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int? CreatedBy { get; set; }
    }
}
