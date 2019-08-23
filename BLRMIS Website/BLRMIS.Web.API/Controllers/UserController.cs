using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.DataAccess.Entities;
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
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(_userService.GetUsersList(searchParams));
        }

        [HttpGet("{UserId}")]
        public IActionResult Get(int UserId)
        {
            return Ok(_userService.GetUserById(UserId));
        }

        [HttpGet("{UserId}/ChangeStatus")]
        public IActionResult ChangeStatus(int UserId)
        {
            _userService.ChangeStatus(UserId);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WebUserModel UserModel)
        {
            bool result = _userService.SaveUser(UserModel);
            if (!result)
                return BadRequest("UserNameAlreadyExists");
            return Ok();
        }

        [HttpPut("{UserId}")]
        public IActionResult Put(int UserId, [FromBody] WebUserModel UserModel)
        {
            _userService.UpdateUser(UserId, UserModel);
            return Ok();
        }

        [HttpGet("/api/Locations")]
        [AllowAnonymous]
        public IActionResult GetLocations()
        {
            return Ok(_userService.GetLocationList());
        }
       
        [HttpGet("/api/Roles")]
        public IActionResult GetRoles()
        {
            return Ok(_userService.GetRoleList());
        }

        [HttpGet("/api/Designations")]
        public IActionResult GetDesignations()
        {
            return Ok(_userService.GetDesignationList());
        }

        [HttpGet("/api/Departments")]
        public IActionResult GetDepartments()
        {
            return Ok(_userService.GetDepartmentList());
        }
        [HttpGet("/api/users/short-list")]
        [AllowAnonymous]
        public IActionResult GetUserShortList()
        {
            var userList = _userService.GetAllUsersShortList();
            if (userList == null) return NotFound("NOT_FOUND_USERS");
            return Ok(userList);
        }

        [HttpGet("TestUser")]
        [AllowAnonymous]
        public IActionResult TestUser()
        {
            //User.Identity.Name;
            _userService.TestFilter("");
            return Ok();
        }
    }
}
