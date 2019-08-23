using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Common
{
    public delegate void FileSavedEventHanlder(object sender, EventArgs ars);
    public  interface IFileHelper
    {
        Task<List<string>> SaveFilesToDirectoryAsync(List<IFormFile> files);
        event FileSavedEventHanlder FilesSaveCompleted;
    }
}

