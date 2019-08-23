using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class DownloadListModel
    {
        public WebDownloadModel Download { get; set; }
        public List<AttachmentModel> Attachments { get; set; }
    }
}
