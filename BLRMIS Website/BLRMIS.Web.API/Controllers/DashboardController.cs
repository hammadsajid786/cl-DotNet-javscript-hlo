using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.API.Extenstions;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            this._dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]DashboardModel model)
        {
            dynamic actionResult;

            var response = _dashboardService.GetChallanDetails(model);
            actionResult = response.Result.IsError ? response.Result.Error.ToHttpResponse() : Ok(response.Result.Model);
            return actionResult;
        }

        [HttpGet]
        [Route("/api/Dashboard/GetComplaintStats")]
        public IActionResult GetComplaintStats([FromQuery]DashboardModel model)
        {
            //dynamic actionResult;

            var response = _dashboardService.GetComplaintStats(model);
            //actionResult = response.Result.IsError ? response.Result.Error.ToHttpResponse() : Ok(response.Result.Model);
            return Ok(response);
        }
    }
}