using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.InterfaceServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        IAttachmentService _attachmentService;
        AppSettings _appSettings;

        public AttachmentController(IAttachmentService attachmentService, IOptions<AppSettings> appSettings)
        {
            _attachmentService = attachmentService;
            _appSettings = appSettings.Value;
        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {

            var result = _attachmentService.GetAttachment(id);
            if (result == null) return NotFound(new APIResponse("FILE_NOT_FOUND", String.Format("File not found against this id {0}", id)));
            var file = System.IO.File.OpenRead(result.AttachmentPath);
            var fileObject =  await Task.Run(() => File(file, result.Mimetype, result.OriginalFileName));
            return fileObject; 
        }

        [HttpGet("/api/attachments/source/{source}/id/{id}")]
        public IActionResult GetAttachmentsBySource(int source, int id)
        {
            var result = _attachmentService.GetAttachments(source, id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpDelete("/api/attachment/{id}")]
        public IActionResult Delete(int id)
        {
            var result = _attachmentService.GetAttachment(id);
            if (result == null) return NotFound(String.Format("No attachment is found againts this id {0}", id));
            _attachmentService.DeleteAttachment(id);
            return Ok(String.Format("Attachment has been deleted {0}", id));
        }
        //

        [HttpGet("/api/file/{id}")]
        public IActionResult Download(int id)
        {
            if (id == 0) return BadRequest("INVALID_FILE_ID");
            var file = _attachmentService.GetAttachment(id);
            if (file == null) return BadRequest("FILE_NOT_FOUND");
            var path = Path.Combine(_appSettings.UploadsPath, file.AttachmentName);
            if (System.IO.File.Exists(path))
            {

                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileStream = new FileStream(path, FileMode.Open);
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = file.OriginalFileName;
                return Ok(Response);
            }
            return NotFound(new APIResponse("FILE_NOT_FOUND", "File is not found on server."));
        }

    }
}