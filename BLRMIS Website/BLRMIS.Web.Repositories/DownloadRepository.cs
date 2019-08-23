using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLRMIS.Web.Repositories
{
    public class DownloadRepository : Repository<LrmisWebDownload>, IDownloadRepository
    {
        public DownloadRepository(BLRMIS_WebsiteContext context) : base(context) { }

        public dynamic GetAllDownloadsWithAttachments(int PageNumber, int PageSize)
        {
            var downloads = _context.Set<LrmisWebDownload>().ToList();
            var result = downloads.Select(x =>
            new
            {
                Download = x,
                Attachments = _context.Set<LrmisWebAttachment>().Where(a => a.SourceId == x.DownloadId && a.SourceType == (int)SourceTypeEnums.DOWNLOAD).ToList()
            });
            return new
            {
                TotalRecords = downloads.Count,
                TotalPages = (int)Math.Ceiling(Convert.ToDouble(downloads.Count) /Convert.ToDouble(PageSize)),
                Records = result.Skip(PageNumber * PageSize).Take(PageSize)
            };
        }
    }
}
