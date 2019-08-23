using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Services.Services
{
    public class AttachmentService : BaseService, IAttachmentService
    {
        IRepository<LrmisWebAttachment> attachmentRepository;
        private IFileHelper fileHelper;
        

        public AttachmentService(IUnitOfWork unitOfWork, IFileHelper fileHelper) : base(unitOfWork)
        {
            attachmentRepository = unitOfWork.GetRepository<LrmisWebAttachment>();
            this.fileHelper = fileHelper;
        }

        public AttachmentModel AddAttachment(AttachmentModel model)
        {
            var entity = EntityMapper.Mapper.Map<LrmisWebAttachment>(model);
            entity.CreatedBy = 4; // HARD CODE
            attachmentRepository.Insert(entity);
            attachmentRepository.Save();
            model = EntityMapper.Mapper.Map<AttachmentModel>(entity);
            return model;
        }

        public async Task AddAttachments(List<IFormFile> FileModels, int SourceId, int SourceType)
        {
            fileHelper.FilesSaveCompleted += (data, args) =>
            {
                var files = (List<FileModel>)data;
                List<AttachmentModel> listAttachmentModel = new List<AttachmentModel>();
                foreach (var file in files)
                {
                    AttachmentModel attachmentModel = new AttachmentModel();
                    attachmentModel.AttachmentName = file.FileName;
                    attachmentModel.AttachmentPath = file.FilePath;
                    attachmentModel.Filesize = file.Filesize;
                    attachmentModel.Mimetype = file.Mimetype;
                    attachmentModel.OriginalFileName = Utility.LimitString(file.OriginalFileName, 255);
                    attachmentModel.SourceId = SourceId;
                    attachmentModel.SourceType = SourceType;
                    listAttachmentModel.Add(attachmentModel);
                    var entity = EntityMapper.Mapper.Map<LrmisWebAttachment>(attachmentModel);
                    entity.CreatedBy = 4; // HARD CODE
                    attachmentRepository.Insert(entity);
                }
                attachmentRepository.Save();
            };

            var filesCreated = await fileHelper.SaveFilesToDirectoryAsync(FileModels);

        }

        public List<AttachmentModel> GetAttachments(int SourceType, int SourceId)
        {
            List<LrmisWebAttachment> listAttachment = new List<LrmisWebAttachment>();
           // listResponseModel.Records = new List<AttachmentModel>();

            listAttachment = attachmentRepository.GetAll(x=>x.SourceId == SourceId && x.SourceType == SourceType);

            List<AttachmentModel> listAttachmentsModel = EntityMapper.Mapper.Map<List<AttachmentModel>>(listAttachment);

            return listAttachmentsModel;
        }

        public AttachmentModel GetAttachment(int Id)
        {
            var entity = attachmentRepository.GetById(Id);

            if (entity != null)
            {
                var model = EntityMapper.Mapper.Map<AttachmentModel>(entity);

                return model;
            }
            else
            {
                return null;
            }
        }

        

        public void DeleteAttachment(int Id)
        {
            attachmentRepository.Delete(Id);

            attachmentRepository.Save();
        }

        public void DeleteAttachments(List <AttachmentModel> attachments)
        {
            if (attachments.Count > 0)
            {
                foreach (var attachment in attachments.ToList())
                {
                    DeleteAttachment(attachment.AttachmentId);
                    if ((System.IO.File.Exists(attachment.AttachmentPath)))
                    {
                        System.IO.File.Delete(attachment.AttachmentPath);
                    }
                }
            }
        }

        public void DeleteAttachments(int SourceType, int SourceId)
        {
            var attachments = GetAttachments(SourceType, SourceId);

            if (attachments.Count > 0)            
            {
                foreach (var attachment in attachments.ToList())
                {
                    DeleteAttachment(attachment.AttachmentId);
                    if ((System.IO.File.Exists(attachment.AttachmentPath)))
                    {
                        System.IO.File.Delete(attachment.AttachmentPath);
                    }
                }
            }
        }

        private void OnFileSaveCompleted(object source, EventArgs args) {
        }

    }
}
