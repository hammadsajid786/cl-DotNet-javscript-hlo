using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IDownloadService
    {
        DownloadModel GetDownload(string Id);

        ListResponseModel<DownloadModel> GetAllDownloads(string Title);

        dynamic GetAllDownloadsWithAttachments(int PageNumber, int PageSize);

        Task<DownloadModel> CreateDownload(DownloadModel model);

        Task UpdateDownload(int id,DownloadModel model);

        void DeleteDownload(int Id);
    }
}
