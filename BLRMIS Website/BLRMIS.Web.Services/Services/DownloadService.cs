using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Services.Services
{
    public class DownloadService : BaseService, IDownloadService
    {
        IRepository<LrmisWebDownload> downloadRepository;
        private IDownloadRepository _downloadRepository;
        private IFileHelper fileHelper;
        IAttachmentService _attachmentService;

        public DownloadService(IUnitOfWork unitOfWork, IFileHelper fileHelper, IAttachmentService attachmentService) : base(unitOfWork)
        {
            downloadRepository = unitOfWork.GetRepository<LrmisWebDownload>();
            _downloadRepository = unitOfWork.GetInstance<DownloadRepository>();
            this.fileHelper = fileHelper;
            this._attachmentService = attachmentService;
        }

        public async Task<DownloadModel> CreateDownload(DownloadModel model)
        {
            var entity = EntityMapper.Mapper.Map<LrmisWebDownload>(model);

            downloadRepository.Insert(entity);
            entity.CreatedBy = 4;           // HARD CODE
            downloadRepository.Save();
            if (model.files != null)
            {
                await _attachmentService.AddAttachments(model.files, entity.DownloadId, (int)SourceTypeEnums.DOWNLOAD);
            }

            return model;
        }

        public void DeleteDownload(int Id)
        {
            downloadRepository.Delete(Id);

            _attachmentService.DeleteAttachments((int)SourceTypeEnums.FAQ, Id);

            downloadRepository.Save();
        }

        public ListResponseModel<DownloadModel> GetAllDownloads(string searchParams)
        {
            Expression<Func<LrmisWebDownload, bool>> predicate = null;
            Func<IQueryable<LrmisWebDownload>, IOrderedQueryable<LrmisWebDownload>> sortexp = null;
            ListResponseModel<DownloadModel> listResponseModel = new ListResponseModel<DownloadModel>();
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebDownload>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = downloadRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<DownloadModel> listDownload = EntityMapper.Mapper.Map<List<DownloadModel>>(result.Items);

            listResponseModel.Records = listDownload;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public dynamic GetAllDownloadsWithAttachments(int PageNumber, int PageSize)
        {
            return _downloadRepository.GetAllDownloadsWithAttachments(PageNumber, PageSize);
        }

        public DownloadModel GetDownload(string Id)
        {
            DownloadModel downloadModel = new DownloadModel();
            var entity = downloadRepository.GetById(Convert.ToInt32(Id));

            if (entity != null)
            {
                downloadModel = EntityMapper.Mapper.Map<DownloadModel>(entity);

                downloadModel.Attachments = _attachmentService.GetAttachments((int)SourceTypeEnums.DOWNLOAD, downloadModel.DownloadId);

                return downloadModel;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateDownload(int id, DownloadModel model)
        {
            LrmisWebDownload download = downloadRepository.GetById(id);

            if (download != null)
            {
                //_attachmentService.DeleteAttachments((int)SourceTypeEnums.DOWNLOAD, id);

                var listAttachments = _attachmentService.GetAttachments((int)SourceTypeEnums.DOWNLOAD, download.DownloadId);

                if (listAttachments.Any())
                {
                    model.Attachments = JsonConvert.DeserializeObject<List<AttachmentModel>>(model.ExistingAttachments);
                    var removingAttachments = listAttachments.Where(c => !model.Attachments.Any(s => s.AttachmentId == c.AttachmentId));  // item to be removed

                    _attachmentService.DeleteAttachments(removingAttachments.ToList());
                }

                if (model.files != null)
                {
                    await _attachmentService.AddAttachments(model.files, download.DownloadId, (int)SourceTypeEnums.DOWNLOAD);
                }

                download.ModifiedDate = DateTime.Now;
                download.DownloadTitle = model.DownloadTitle;
                download.DownloadDescription = model.DownloadDescription;

                downloadRepository.Update(download);
                downloadRepository.Save();
            }


        }
    }
}
