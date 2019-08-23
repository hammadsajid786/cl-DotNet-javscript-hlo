using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
   // [ApiController]
    public class UploadFilesController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly AppSettings AppSettings;
        private IFileHelper fileHelper; 

        public UploadFilesController(
            IHostingEnvironment hostingEnvironment, 
            IOptions<AppSettings> settings,
            IFileHelper fileHelper)
        {
            _hostingEnvironment = hostingEnvironment;
            AppSettings = settings.Value;
            this.fileHelper = fileHelper; 
        }


        [HttpPost]
        [Route("/api/UploadFiles/Post")]
        public async Task<IActionResult> Post([FromForm]List<IFormFile> files)
        {
            // Get the file from the POST request
            // var theFile = HttpContext.Request.Form.Files.GetFile("file");
            var theFile = HttpContext.Request.Form.Files[0];
            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            //var fileRoute = Path.Combine(AppSettings.UploadsPath, Constants.ContentFilePath);
            var fileRoute = AppSettings.UploadsPath;
            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files[0].ContentType;//HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString("x") + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(AppSettings.UploadsPath);
            dir.Directory.Create();
            dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            string[] imageMimeTypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
            string[] fileMimeTypes = { "text/plain", "application/msword", "application/x-pdf", "application/pdf", "application/json", "text/html" };
            string[] videoMimeTypes = { "video/mp4", "video/webm", "video/ogg" };

            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob",".jfif" };
            string[] fileExt = { ".txt", ".pdf", ".doc", ".json", ".html" };
            string[] videoExt = { ".mp4", ".webm", ".ogg" };

            // Basic validation on mime types and file extension
            string[] allMimetypes = imageMimeTypes.Concat(fileMimeTypes).Concat(videoMimeTypes).ToArray();
            string[] allExt = imageExt.Concat(fileExt).Concat(videoExt).ToArray();

            try
            {
                if (Array.IndexOf(allMimetypes, mimeType) >= 0 && (Array.IndexOf(allExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable imageUrl = new Hashtable();
                    imageUrl.Add("link", AppSettings.UploadPathIIS + name);

                    //var json = JsonConvert.SerializeObject(imageUrl);

                    return Ok(imageUrl);
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                return Ok(ex.Message);
            }
        }


        [HttpPost]
        [Route("/api/UploadImage/Post")]
        public async Task<IActionResult> PostImage([FromForm]List<IFormFile> files)
        {
            // Get the file from the POST request
            // var theFile = HttpContext.Request.Form.Files.GetFile("file");
            var theFile = HttpContext.Request.Form.Files[0];
            // Get the server path, wwwroot
            string webRootPath = _hostingEnvironment.WebRootPath;

            // Building the path to the uploads directory
            //var fileRoute = Path.Combine(AppSettings.UploadsPath, Constants.ContentFilePath);
            var fileRoute = AppSettings.UploadsPath;
            // Get the mime type
            var mimeType = HttpContext.Request.Form.Files[0].ContentType;//HttpContext.Request.Form.Files.GetFile("file").ContentType;

            // Get File Extension
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            string name = Guid.NewGuid().ToString("x") + extension;

            // Build the full path inclunding the file name
            string link = Path.Combine(fileRoute, name);

            // Create directory if it does not exist.
            FileInfo dir = new FileInfo(AppSettings.UploadsPath);
            dir.Directory.Create();
            dir = new FileInfo(fileRoute);
            dir.Directory.Create();

            string[] imageMimeTypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml" };
            string[] fileMimeTypes = { "text/plain", "application/msword", "application/x-pdf", "application/pdf", "application/json", "text/html" };
            string[] videoMimeTypes = { "video/mp4", "video/webm", "video/ogg" };

            string[] imageExt = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob" };
            string[] fileExt = { ".txt", ".pdf", ".doc", ".json", ".html" };
            string[] videoExt = { ".mp4", ".webm", ".ogg" };

            // Basic validation on mime types and file extension
            string[] allMimetypes = imageMimeTypes.Concat(fileMimeTypes).Concat(videoMimeTypes).ToArray();
            string[] allExt = imageExt.Concat(fileExt).Concat(videoExt).ToArray();

            try
            {
                if (Array.IndexOf(allMimetypes, mimeType) >= 0 && (Array.IndexOf(allExt, extension) >= 0))
                {
                    // Copy contents to memory stream.
                    Stream stream;
                    stream = new MemoryStream();
                    theFile.CopyTo(stream);
                    stream.Position = 0;
                    String serverPath = link;

                    // Save the file
                    using (FileStream writerFileStream = System.IO.File.Create(serverPath))
                    {
                        await stream.CopyToAsync(writerFileStream);
                        writerFileStream.Dispose();
                    }

                    // Return the file path as json
                    Hashtable imageUrl = new Hashtable();
                    imageUrl.Add("link", AppSettings.UploadPathIIS + name);

                    var data = new { url = AppSettings.UploadPathIIS + name};
                    var json = JsonConvert.SerializeObject(data);
                      return Ok(json);
                   // return Json(new { message = "dasd", type = "dsad" });
                }
                throw new ArgumentException("The image did not pass the validation");
            }

            catch (ArgumentException ex)
            {
                var data = new { error = true, ErrorMessage = ex.Message };
                var json = JsonConvert.SerializeObject(data);
                return Ok(json);
            }
        }

        [HttpPost]
        [Route("/api/files")]
        public async Task<IActionResult> FileUpload([FromForm]List<IFormFile> files)
        {
          var filesCreated =  await fileHelper.SaveFilesToDirectoryAsync(files);
            // this function should be used for save files name in db. 
           
           return Ok(filesCreated);
        }
    }
}
