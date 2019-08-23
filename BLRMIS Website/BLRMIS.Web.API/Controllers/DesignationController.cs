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
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(_designationService.GetDesignationList(searchParams));
        }

        [HttpGet("{DesignationId}")]
        public IActionResult Get(int DesignationId)
        {
            return Ok(_designationService.GetDesignationById(DesignationId));
        }

        [HttpGet("{DesignationId}/ChangeStatus")]
        public IActionResult ChangeStatus(int DesignationId)
        {
            _designationService.ChangeStatus(DesignationId);
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody] WebDesignationModel DesignationModel)
        {
            _designationService.SaveDesignation(DesignationModel);
            return Ok();
        }

        [HttpPut("{DesignationId}")]
        public IActionResult Put(int DesignationId, [FromBody] WebDesignationModel DesignationModel)
        {
            _designationService.UpdateDesignation(DesignationId, DesignationModel);
            return Ok();
        }
    }
}