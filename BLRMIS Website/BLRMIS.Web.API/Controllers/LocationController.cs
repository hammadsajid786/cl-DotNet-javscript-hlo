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
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(_locationService.GetLocationList(searchParams));
        }

        [HttpGet("{LocationId}")]
        public IActionResult Get(int LocationId)
        {
            return Ok(_locationService.GetLocationById(LocationId));
        }

        [HttpGet("{LocationId}/ChangeStatus")]
        public IActionResult ChangeStatus(int LocationId)
        {
            _locationService.ChangeStatus(LocationId);
            return Ok();
        }

        //[HttpPost]
        //public IActionResult Post([FromBody] WebLocationModel LocationModel)
        //{
        //    _locationService.SaveLocation(LocationModel);
        //    return Ok();
        //}

        [HttpPut("{LocationId}")]
        public IActionResult Put(int LocationId, [FromBody] WebLocationModel LocationModel)
        {
            _locationService.UpdateLocation(LocationId, LocationModel);
            return Ok();
        }
    }
}