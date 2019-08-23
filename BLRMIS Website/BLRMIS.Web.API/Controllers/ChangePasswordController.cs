using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IChangePasswordService _changePasswordService;
        public ChangePasswordController(IChangePasswordService changePasswordService)
        {
            _changePasswordService = changePasswordService;
        }

        [HttpPost]
        public IActionResult Put([FromBody] WebUserModel UserModel)
        {
            UserModel.UserId = Convert.ToInt32(User.Identity.Name);
            _changePasswordService.ChangePassword(UserModel);
            return Ok();
        }
    }
}