using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BLRMIS.Web.Common
{
    public class FileHelper : IFileHelper
    {
        public event FileSavedEventHanlder FilesSaveCompleted;
        private readonly AppSettings appSettings;
        private string _uploadDirectoryPath;
        private string[] _allowedMIMETypes;
        private string[] _allowedFileExtensions;
        string[] AllowedMIMETypes;
        string[] AllowedFileExtentions;
        string[] defaultAllowedMIMETypes = { "image/gif", "image/jpeg", "image/pjpeg", "image/x-png", "image/png", "image/svg+xml", "text/plain", "application/msword", "application/x-pdf", "application/pdf", "application/json", "text/html", "video/mp4", "video/webm", "video/ogg" };
        string[] defaultAllowedFileExtentions = { ".gif", ".jpeg", ".jpg", ".png", ".svg", ".blob", ".txt", ".pdf", ".doc", ".json", ".html", ".mp4", ".webm", ".ogg" };



        public string uploadDirectoryPath
        {
            get
            {
                if (String.IsNullOrEmpty(_uploadDirectoryPath)) return appSettings.UploadsPath;
                return _uploadDirectoryPath;
            }
            set
            {
                _uploadDirectoryPath = System.AppDomain.CurrentDomain.BaseDirectory + value;
            }
        }
        public string[] allowedMIMETypes
        {
            get
            {
                if (_allowedMIMETypes == null || _allowedMIMETypes.Length == 0) return AllowedMIMETypes;
                return _allowedMIMETypes;
            }
            set
            {
                _allowedMIMETypes = value;
            }
        }
        public string[] allowedFileExtensions
        {
            get
            {
                if (_allowedFileExtensions == null || _allowedFileExtensions.Length == 0) return AllowedFileExtentions;
                return _allowedFileExtensions;
            }
            set
            {
                _allowedFileExtensions = value;
            }
        }


        public FileHelper(IOptions<AppSettings> AppSettings)
        {
            appSettings = AppSettings.Value;
            AllowedMIMETypes = appSettings.AllowedMIMETypes == null ? defaultAllowedMIMETypes : appSettings.AllowedMIMETypes.Split(',');
            AllowedFileExtentions = appSettings.AllowedFileExtentions == null ? defaultAllowedFileExtentions : appSettings.AllowedFileExtentions.Split(',');
        }


        public async Task<List<string>> SaveFilesToDirectoryAsync(List<IFormFile> files)
        {
            try
            {
                if (!Directory.Exists(uploadDirectoryPath))
                    Directory.CreateDirectory(uploadDirectoryPath);
                var savedFiles = new List<string>();
                var savedFilesObject = new List<FileModel>();
                foreach (var file in files)
                {
                    var fileExtension = Path.GetExtension(file.FileName);
                    var newFileName = Guid.NewGuid().ToString("N") + fileExtension;
                    var mimeType = file.ContentType;
                    if (isValidFile(file))
                    {
                        var filePath = Path.Combine(uploadDirectoryPath, newFileName);
                        using (var fileStream = new FileStream(Path.Combine(uploadDirectoryPath, newFileName), FileMode.Create))
                        {
                            file.CopyTo(fileStream);

                            FileModel fileModel = new FileModel();
                            fileModel.FileName = newFileName;
                            fileModel.FilePath = filePath;
                            fileModel.Filesize = file.Length.ToString();
                            fileModel.Mimetype = mimeType;
                            fileModel.OriginalFileName = file.FileName;
                            savedFilesObject.Add(fileModel);
                        }

                        //Below Code Commented By Haris Dar Due To Memory Stream Error When Uploading Multiple Files
                        //Stream stream = new MemoryStream();
                        //file.CopyTo(stream);
                        //stream.Position = 0;
                        //var filePath = Path.Combine(uploadDirectoryPath, newFileName);
                        //// Save the file
                        //using (FileStream writerFileStream = File.Create(filePath))
                        //{
                        //    await stream.CopyToAsync(writerFileStream);

                        //    writerFileStream.Dispose();

                        //    FileModel fileModel = new FileModel();
                        //    fileModel.FileName = newFileName;
                        //    fileModel.FilePath = filePath;
                        //    fileModel.Filesize = file.Length.ToString();
                        //    fileModel.Mimetype = mimeType;
                        //    fileModel.OriginalFileName = file.FileName;
                        //    savedFilesObject.Add(fileModel);

                        //    savedFiles.Add(newFileName);
                        //}
                    }
                    else
                    {
                        // TODO
                        // LOG file which is not uploaded. 
                    }
                }
                onFileSaveCompleted(savedFilesObject);
                return savedFiles;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private bool isValidFile(IFormFile file)
        {
            return Array.IndexOf(AllowedMIMETypes, file.ContentType) >= 0 && (Array.IndexOf(AllowedFileExtentions, Path.GetExtension(file.FileName)) >= 0);
        }

        private void onFileSaveCompleted(object data)
        {
            if (FilesSaveCompleted != null)
            {
                FilesSaveCompleted(data, EventArgs.Empty);
            }
        }
    }



}
