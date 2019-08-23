using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class AttachmentModel
    {
        public int AttachmentId { get; set; }
        public string AttachmentPath { get; set; }
        public string AttachmentName { get; set; }
        public int SourceType { get; set; }
        public int SourceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public string OriginalFileName { get; set; }
        public string Mimetype { get; set; }
        public string Filesize { get; set; }
    }
}
