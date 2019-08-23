using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class FAQModel
    {
        public int FaqId { get; set; }

        [Required]
        [StringLength(maximumLength:200,ErrorMessage = "FaqTitle Length should not greater than 200.")]
        public string FaqTitle { get; set; }

       // [Required]
        public string FaqDescription { get; set; }

        [ValidateFile]
        public List<IFormFile> files { get; set; }

        public List<AttachmentModel> Attachments { get; set; }

        public string ExistingAttachments { get; set; }
    }
}
