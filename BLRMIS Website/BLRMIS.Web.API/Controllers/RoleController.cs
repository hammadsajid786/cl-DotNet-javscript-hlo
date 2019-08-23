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
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(_roleService.GetRolesList(searchParams));
        }

        [HttpGet("{RoleId}")]
        public IActionResult Get(int RoleId)
        {
            return Ok(_roleService.GetRoleById(RoleId));
        }

        [HttpGet("{RoleId}/ChangeStatus")]
        public IActionResult ChangeStatus(int RoleId)
        {
            bool flag = _roleService.ChangeStatus(RoleId);
            return Ok(flag);
        }

        [HttpGet("{RoleId}/RoleFunctions")]
        public IActionResult RoleFunctionMappings([FromQuery(Name = "SearchParams")]string SearchParams, int RoleId)
        {
            return Ok(_roleService.GetRoleFunctions(SearchParams, RoleId));
        }

        [HttpPost("RoleFunctions")]
        public IActionResult MapRoleFunctions([FromBody] List<WebFunctionRoleMapping> mappings)
        {
            _roleService.MapRoleFunctions(mappings);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WebRoleModel RoleModel)
        {
            _roleService.SaveRole(RoleModel);
            return Ok();
        }

        [HttpPut("{RoleId}")]
        public IActionResult Put(int RoleId, [FromBody] WebRoleModel RoleModel)
        {
            bool flag = _roleService.UpdateRole(RoleId, RoleModel);
            return Ok(flag);

        }
    }
}