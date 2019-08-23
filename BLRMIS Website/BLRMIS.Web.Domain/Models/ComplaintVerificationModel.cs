using BLRMIS.Web.Domain.InterfaceServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class VerificationTokenModel
    {
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string VerificationCode { get; set; }
        public int VerificationId { get; set; }

    }
}
