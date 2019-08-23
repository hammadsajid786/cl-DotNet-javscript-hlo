using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class NewsModel
    {
        public int NewsId { get; set; }

        [Required]
        public string NewsTitle { get; set; }
        public DateTime? NewsDate { get; set; }

        //[Required]
        public string NewsDescription { get; set; }

        public string Date { get; set; }

        [ValidateFile]
        public List<IFormFile> files { get; set; }

        public List<AttachmentModel> Attachments { get; set; }

        public string ExistingAttachments { get; set; }
    }
}
