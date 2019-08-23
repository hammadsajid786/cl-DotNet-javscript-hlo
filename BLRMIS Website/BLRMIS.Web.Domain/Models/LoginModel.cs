using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class LoginModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public bool Authenticated { get; set; }
        public string Message { get; set; }

        public string LoginUserName { get; set; }
    }
}
