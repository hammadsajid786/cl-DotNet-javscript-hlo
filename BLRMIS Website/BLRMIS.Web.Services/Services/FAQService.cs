using AutoMapper;
using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Services.Services
{
    public class FAQService : BaseService, IFAQService
    {
        IRepository<LrmisWebFaq> fAQRepository;
        IAttachmentService _attachmentService;

        public FAQService(IUnitOfWork unitOfWork, IAttachmentService attachmentService) : base(unitOfWork)
        {
            fAQRepository = unitOfWork.GetRepository<LrmisWebFaq>();
            this._attachmentService = attachmentService;
        }

        public async Task<FAQModel> CreateFAQ(FAQModel model)
        {
            //var mapModel = Mapper.Map<FAQModel, LrmisWebFaq>(model);
            var entity = EntityMapper.Mapper.Map<LrmisWebFaq>(model);
            fAQRepository.Insert(entity);
            entity.CreatedBy = 4;           // HARD CODE
            fAQRepository.Save();
            if (model.files != null)
            {
                await _attachmentService.AddAttachments(model.files, entity.FaqId, (int)SourceTypeEnums.FAQ);
            }
            model = EntityMapper.Mapper.Map<FAQModel>(entity);
            return model;

        }

        public void DeleteFAQ(int Id)
        {
            fAQRepository.Delete(Id);

            _attachmentService.DeleteAttachments((int)SourceTypeEnums.FAQ, Id);

            fAQRepository.Save();
        }

        public ListResponseModel<FAQModel> GetAll(string SearchParams)
        {
            Expression<Func<LrmisWebFaq, bool>> predicate = null;
            Func<IQueryable<LrmisWebFaq>, IOrderedQueryable<LrmisWebFaq>> sortexp = null;
            ListResponseModel<FAQModel> listResponseModel = new ListResponseModel<FAQModel>();
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebFaq>(SearchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = fAQRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<FAQModel> modelList = EntityMapper.Mapper.Map<List<FAQModel>>(result.Items);

            listResponseModel.Records = modelList;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }


        public ListResponseModel<FAQModel> GetAllFAQs(string Title, string Description)
        {
            Expression<Func<LrmisWebFaq, bool>> predicate = null;
            Func<IQueryable<LrmisWebFaq>, IOrderedQueryable<LrmisWebFaq>> sortexp = null;
            ListResponseModel<FAQModel> listResponseModel = new ListResponseModel<FAQModel>();
            int index = 0;
            int size = 0;

           // IQueryableExtensions.GetFilters<LrmisWebFaq>(SearchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = fAQRepository.GetList(predicate: x=> (string.IsNullOrEmpty(Title) || x.FaqTitle.Contains(Title) ) || (string.IsNullOrEmpty(Description) || x.FaqDescription.Contains(Description)));

            List<FAQModel> modelList = EntityMapper.Mapper.Map<List<FAQModel>>(result.Items);

            listResponseModel.Records = modelList;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public IEnumerable<FAQModel> GetFAQsByTitle(string Title)
        {
            var listFAQ = fAQRepository.GetAll(x => x.FaqTitle.Contains(Title));

            foreach (var entity in listFAQ)
                yield return EntityMapper.Mapper.Map<FAQModel>(entity);
        }

        public FAQModel GetFAQ(string Id)
        {
            FAQModel fAQModel = new FAQModel();
            var result = fAQRepository.GetById(Convert.ToInt32(Id));

            if (result != null)
            {
                fAQModel = EntityMapper.Mapper.Map<FAQModel>(result);
                fAQModel.Attachments = _attachmentService.GetAttachments((int)SourceTypeEnums.FAQ, fAQModel.FaqId);
                return fAQModel;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateFAQ(int id, FAQModel model)
        {
            LrmisWebFaq faq = fAQRepository.GetById(id);

            if (faq != null)
            {
                var listAttachments = _attachmentService.GetAttachments((int)SourceTypeEnums.FAQ, faq.FaqId);

                if (listAttachments.Any())
                {
                    model.Attachments = JsonConvert.DeserializeObject<List<AttachmentModel>>(model.ExistingAttachments);
                    var removingAttachments = listAttachments.Where(c => !model.Attachments.Any(s => s.AttachmentId == c.AttachmentId));  // item to be removed

                    _attachmentService.DeleteAttachments(removingAttachments.ToList());
                }

                if (model.files != null)
                {
                    await _attachmentService.AddAttachments(model.files, faq.FaqId, (int)SourceTypeEnums.FAQ);
                }

                faq.ModifiedDate = DateTime.Now;
                faq.FaqTitle = model.FaqTitle;
                faq.FaqDescription = model.FaqDescription;

                fAQRepository.Update(faq);
                fAQRepository.Save();
            }
        }
    }
}
