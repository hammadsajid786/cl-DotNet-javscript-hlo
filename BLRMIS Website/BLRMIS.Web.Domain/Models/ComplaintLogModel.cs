using BLRMIS.Web.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class ComplaintLogModel
    {
        public int ComplaintCommentId { get; set; }
        public int ComplaintId { get; set; }
        public int ComplaintStatusId { get; set; }
        public int? ComplaintAssignTo { get; set; }
        public int? ComplaintAssignBy { get; set; }
        public string ComplaintComments { get; set; }
        public int UserOpinion { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public List<IFormFile> Files { get; set; }
        public List<FileList> FileList { get; set; }

        //public LrmisWebComplaint Complaint { get; set; }
        public WebUserModel ComplaintAssignByNavigation { get; set; }
        //public LrmisWebUser ComplaintAssignToNavigation { get; set; }
        //public LrmisWebComplaintStatus ComplaintStatus { get; set; }
        public WebUserModel CreatedByNavigation { get; set; }
       
        //public LrmisWebUser ModifiedByNavigation { get; set; }
    }

    public class FileList
    {
        public int AttachmentId { get; set; }
        public string OriginalFileName { get; set; }
    }
}
