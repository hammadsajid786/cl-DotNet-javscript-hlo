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
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAll([FromQuery(Name = "SearchParams")]string SearchParams)
        {
            return Ok(_newsService.GetAllNews(SearchParams));
        }

        [HttpGet("topnews")]
        [AllowAnonymous]
        public ActionResult<ListResponseModel<DownloadModel>> GetTopNews()
        {
            return Ok(_newsService.GetTopNews());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult Get(string id)
        {
            var result = _newsService.GetNews(id);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm]NewsModel model)
        {
            if (ModelState.IsValid)
            {
                model =  await _newsService.CreateNews(model);

                return Ok(model);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromForm] NewsModel model)
        {
            await _newsService.UpdateNews(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _newsService.DeleteNews(id);

            return Ok();
        }

        [HttpGet]
        [Route("/api/News/GetAll")]
        [AllowAnonymous]
        public ActionResult<ListResponseModel<DownloadModel>> GetAllNews([FromQuery(Name = "SearchParams")]string Title, string Description)
        {
            return Ok(_newsService.GetAllNews(Title, Description));
        }
    }
}