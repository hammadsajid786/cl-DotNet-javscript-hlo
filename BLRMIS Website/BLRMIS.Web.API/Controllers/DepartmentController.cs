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
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(_departmentService.GetDepartmentList(searchParams));
        }

        [HttpGet("{DepartmentId}")]
        public IActionResult Get(int DepartmentId)
        {
            return Ok(_departmentService.GetDepartmentById(DepartmentId));
        }

        [HttpGet("{DepartmentId}/ChangeStatus")]
        public IActionResult ChangeStatus(int DepartmentId)
        {
            _departmentService.ChangeStatus(DepartmentId);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WebDepartmentModel DepartmentModel)
        {
            _departmentService.SaveDepartment(DepartmentModel);
            return Ok();
        }

        [HttpPut("{DepartmentId}")]
        public IActionResult Put(int DepartmentId, [FromBody] WebDepartmentModel DepartmentModel)
        {
            _departmentService.UpdateDepartment(DepartmentId, DepartmentModel);
            return Ok();
        }
    }
}