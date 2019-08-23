using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.Common;
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
    public class DownloadController : ControllerBase
    {
        IDownloadService _downloadService;
        private IFileHelper fileHelper;

        public DownloadController(IDownloadService downloadService, IFileHelper fileHelper)
        {
            this._downloadService = downloadService;
            this.fileHelper = fileHelper;
        }

        //[HttpGet]
        //public ActionResult<ListResponseModel<DownloadModel>> GetAll(string Title)
        //{
        //    return Ok(_downloadService.GetAllDownloads(Title));
        //}

        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAll([FromQuery(Name = "SearchParams")]string SearchParams)
        {
            return Ok(_downloadService.GetAllDownloads(SearchParams));
        }

        [HttpGet("Public")]
        [AllowAnonymous]
        public ActionResult<dynamic> GetAllWithAttachments([FromQuery]int PageNumber, [FromQuery]int PageSize)
        {
            return Ok(_downloadService.GetAllDownloadsWithAttachments(PageNumber, PageSize));
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _downloadService.GetDownload(id);

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
        public async Task<IActionResult> PostAsync([FromForm] DownloadModel model)
        {
            if (ModelState.IsValid)
            {
                var downloadModel = await _downloadService.CreateDownload(model);

                return Ok(downloadModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] DownloadModel model, int id)
        {
            await _downloadService.UpdateDownload(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _downloadService.DeleteDownload(id);

            return Ok();
        }

        private void OnFileCompleted(object data)
        {
           
        }
    }
}