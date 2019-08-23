using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BLRMIS.Web.Domain.Models
{
    public class ValidateFileAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)

        {

            // int maxContent = 1024 * 1024; //1 MB

            string[] sAllowedExt = new string[] { ".pdf", ".doc",".docx",".xlsx",".png",".jpeg",".jpg" };

            string[] allowedContentType = new string[] { "image/png", "image/jpeg", "application/pdf" };

            var files = value as List<IFormFile>;
            if (files != null)
            {

                string FileSize = "1000";    //ConfigurationManager.AppSettings["AttachmentFileSize"];
                string FilesCount = "6";        // ConfigurationManager.AppSettings["AttachmentFileCount"];
                int maxFileSize = 0;
                int maxFiles = 0;

                if (!string.IsNullOrEmpty(FileSize))
                {
                    maxFileSize = Convert.ToInt32(FileSize);
                }

                if (!string.IsNullOrEmpty(FilesCount))
                {
                    maxFiles = Convert.ToInt32(FilesCount);
                }
                if (files.Count <= maxFiles)
                {
                    foreach (var file in files)
                    {
                        if (!allowedContentType.Contains(file.ContentType))
                        {
                            ErrorMessage = "Please do not upload attachmnet of type other than : " + string.Join(", ", allowedContentType);

                            return false;
                        }

                        else if (file.Length > (maxFileSize * 1024))
                        {
                            ErrorMessage = "Your attachment is too large, maximum allowed size is : " + (FileSize).ToString() + "KB";

                            return false;
                        }
                    }
                }
                else
                {
                    ErrorMessage = "Maximum " + FilesCount + " files Allowed.";

                    return false;
                }
            }
            return true;
        }

    }
}
