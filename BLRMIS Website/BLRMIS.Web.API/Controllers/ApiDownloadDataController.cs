using BLRMIS.Web.API.Extenstions;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiDownloadDataController : ControllerBase
    {
        private readonly IApiDownloadedDataService _newsService;

        public ApiDownloadDataController(IApiDownloadedDataService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public ActionResult<ListResponseModel<WebApiDownloadedDataModel>> GetAllNews([FromQuery(Name = "param")]string searchParams)
        {
            return Ok(_newsService.GetWebApiDownloadedList(searchParams));
        }
    }
}