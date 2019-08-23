using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BLRMIS.Web.Domain.Models
{
    public partial class ComplaintModel
    {

        public int ComplaintId { get; set; }
        [Required]
        public string ComplaintTitle { get; set; }
        [Required]
        public string ComplaintDescription { get; set; }
        [Required]
        public string CitizenName { get; set; }
        [Required]
        public string CitizenPhoneNumber { get; set; }
        [Required]
        public string CitizenEmailAddress { get; set; }
        [Required]
        public string CitizenCnic { get; set; }
        [Required]
        public int LocationId { get; set; }
       
        public int? FunctionId { get; set; }
        public int? ComplaintAssignTo { get; set; }
        [Required]
        public int ComplaintCategoryId { get; set; }
        [Required]

        public string VerificationCode { get; set; }
        public int ComplaintStatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public int CreatedBy { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedBy { get; set; }
        public string ComplaintCode { get; set; }
        public string ComplaintAccessToken { get; set; }


        public List<IFormFile> Files { get; set; }

        public WebCategoryModel ComplaintCategory { get; set; }
        public ComplaintStatusModel ComplaintStatus { get; set; }
        public WebLocationModel Location { get; set; }

        public WebFunctionModel  Function { get; set; }
        
        public WebUserModel ComplaintAssignToNavigation { get; set; }

    }
}
