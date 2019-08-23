using System;
using System.Collections.Generic;

namespace BLRMIS.Web.DataAccess.Entities
{
    public partial class LrmisWebUser
    {
        public LrmisWebUser()
        {
            InverseCreatedByNavigation = new HashSet<LrmisWebUser>();
            InverseModifiedByNavigation = new HashSet<LrmisWebUser>();
            LrmisWebAttachmentCreatedByNavigation = new HashSet<LrmisWebAttachment>();
            LrmisWebAttachmentModifiedByNavigation = new HashSet<LrmisWebAttachment>();
            LrmisWebCategoryCreatedByNavigation = new HashSet<LrmisWebCategory>();
            LrmisWebCategoryModifiedByNavigation = new HashSet<LrmisWebCategory>();
            LrmisWebComplaintComplaintAssignToNavigation = new HashSet<LrmisWebComplaint>();
            LrmisWebComplaintLockedByNavigation = new HashSet<LrmisWebComplaint>();
            LrmisWebComplaintLogComplaintAssignByNavigation = new HashSet<LrmisWebComplaintLog>();
            LrmisWebComplaintLogComplaintAssignToNavigation = new HashSet<LrmisWebComplaintLog>();
            LrmisWebComplaintLogCreatedByNavigation = new HashSet<LrmisWebComplaintLog>();
            LrmisWebComplaintLogModifiedByNavigation = new HashSet<LrmisWebComplaintLog>();
            LrmisWebContentCreatedByNavigation = new HashSet<LrmisWebContent>();
            LrmisWebContentModifiedByNavigation = new HashSet<LrmisWebContent>();
            LrmisWebDepartmentCreatedByNavigation = new HashSet<LrmisWebDepartment>();
            LrmisWebDepartmentModifiedByNavigation = new HashSet<LrmisWebDepartment>();
            LrmisWebDesignationCreatedByNavigation = new HashSet<LrmisWebDesignation>();
            LrmisWebDesignationModifiedByNavigation = new HashSet<LrmisWebDesignation>();
            LrmisWebDownloadCreatedByNavigation = new HashSet<LrmisWebDownload>();
            LrmisWebDownloadModifiedByNavigation = new HashSet<LrmisWebDownload>();
            LrmisWebFaqCreatedByNavigation = new HashSet<LrmisWebFaq>();
            LrmisWebFaqModifiedByNavigation = new HashSet<LrmisWebFaq>();
            LrmisWebNewsCreatedByNavigation = new HashSet<LrmisWebNews>();
            LrmisWebNewsModifiedByNavigation = new HashSet<LrmisWebNews>();
            LrmisWebRoleCreatedByNavigation = new HashSet<LrmisWebRole>();
            LrmisWebRoleModifiedByNavigation = new HashSet<LrmisWebRole>();
            LrmisWebVerificationTokenCreatedByNavigation = new HashSet<LrmisWebVerificationToken>();
            LrmisWebVerificationTokenModifiedByNavigation = new HashSet<LrmisWebVerificationToken>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string Cnic { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int LocationId { get; set; }
        public int RoleId { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }

        public LrmisWebUser CreatedByNavigation { get; set; }
        public LrmisWebDepartment Department { get; set; }
        public LrmisWebDesignation Designation { get; set; }
        public LrmisWebLocation Location { get; set; }
        public LrmisWebUser ModifiedByNavigation { get; set; }
        public LrmisWebRole Role { get; set; }
        public ICollection<LrmisWebUser> InverseCreatedByNavigation { get; set; }
        public ICollection<LrmisWebUser> InverseModifiedByNavigation { get; set; }
        public ICollection<LrmisWebAttachment> LrmisWebAttachmentCreatedByNavigation { get; set; }
        public ICollection<LrmisWebAttachment> LrmisWebAttachmentModifiedByNavigation { get; set; }
        public ICollection<LrmisWebCategory> LrmisWebCategoryCreatedByNavigation { get; set; }
        public ICollection<LrmisWebCategory> LrmisWebCategoryModifiedByNavigation { get; set; }
        public ICollection<LrmisWebComplaint> LrmisWebComplaintComplaintAssignToNavigation { get; set; }
        public ICollection<LrmisWebComplaint> LrmisWebComplaintLockedByNavigation { get; set; }
        public ICollection<LrmisWebComplaintLog> LrmisWebComplaintLogComplaintAssignByNavigation { get; set; }
        public ICollection<LrmisWebComplaintLog> LrmisWebComplaintLogComplaintAssignToNavigation { get; set; }
        public ICollection<LrmisWebComplaintLog> LrmisWebComplaintLogCreatedByNavigation { get; set; }
        public ICollection<LrmisWebComplaintLog> LrmisWebComplaintLogModifiedByNavigation { get; set; }
        public ICollection<LrmisWebContent> LrmisWebContentCreatedByNavigation { get; set; }
        public ICollection<LrmisWebContent> LrmisWebContentModifiedByNavigation { get; set; }
        public ICollection<LrmisWebDepartment> LrmisWebDepartmentCreatedByNavigation { get; set; }
        public ICollection<LrmisWebDepartment> LrmisWebDepartmentModifiedByNavigation { get; set; }
        public ICollection<LrmisWebDesignation> LrmisWebDesignationCreatedByNavigation { get; set; }
        public ICollection<LrmisWebDesignation> LrmisWebDesignationModifiedByNavigation { get; set; }
        public ICollection<LrmisWebDownload> LrmisWebDownloadCreatedByNavigation { get; set; }
        public ICollection<LrmisWebDownload> LrmisWebDownloadModifiedByNavigation { get; set; }
        public ICollection<LrmisWebFaq> LrmisWebFaqCreatedByNavigation { get; set; }
        public ICollection<LrmisWebFaq> LrmisWebFaqModifiedByNavigation { get; set; }
        public ICollection<LrmisWebNews> LrmisWebNewsCreatedByNavigation { get; set; }
        public ICollection<LrmisWebNews> LrmisWebNewsModifiedByNavigation { get; set; }
        public ICollection<LrmisWebRole> LrmisWebRoleCreatedByNavigation { get; set; }
        public ICollection<LrmisWebRole> LrmisWebRoleModifiedByNavigation { get; set; }
        public ICollection<LrmisWebVerificationToken> LrmisWebVerificationTokenCreatedByNavigation { get; set; }
        public ICollection<LrmisWebVerificationToken> LrmisWebVerificationTokenModifiedByNavigation { get; set; }
    }
}
