using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class WebUserModel
    {
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
        public WebDepartmentModel Department { get; set; }
        public WebDesignationModel Designation { get; set; }
        public WebLocationModel Location { get; set; }
        public WebRoleModel Role { get; set; }
    }
}
