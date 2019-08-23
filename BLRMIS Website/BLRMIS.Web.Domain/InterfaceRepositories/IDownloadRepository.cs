using BLRMIS.Web.Domain.Models;
using System.Collections.Generic;
using BLRMIS.Web.DataAccess.Entities;
using System;
using System.Linq.Expressions;

namespace BLRMIS.Web.Domain.InterfaceRepositories
{
    public interface IDownloadRepository : IRepository<LrmisWebDownload>
    {
        dynamic GetAllDownloadsWithAttachments(int PageNumber, int PageSize);
    }
}
