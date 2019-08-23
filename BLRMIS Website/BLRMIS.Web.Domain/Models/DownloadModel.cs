using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class DownloadModel
    {
        public int DownloadId { get; set; }
        public string DownloadTitle { get; set; }
        public string DownloadDescription { get; set; }

        [ValidateFile]
        public List<IFormFile> files { get; set; }

        public List<AttachmentModel> Attachments { get; set; }

        public string ExistingAttachments { get; set; }
    }
}
