using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain;
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

namespace BLRMIS.Web.Services
{
    public class NewsService : BaseService, INewsService
    {
        IRepository<LrmisWebNews> newsRepository;
        IAttachmentService _attachmentService;

        public NewsService(IUnitOfWork unitOfWork, IAttachmentService attachmentService) : base(unitOfWork)
        {
            newsRepository = unitOfWork.GetRepository<LrmisWebNews>();
            this._attachmentService = attachmentService;
        }

        public async Task<NewsModel> CreateNews(NewsModel model)
        {
            model.NewsDate = Convert.ToDateTime(DateTime.Now.ToString(DateTimeConstants.DatabaseDateFormat));
            var entity = EntityMapper.Mapper.Map<LrmisWebNews>(model);
            entity.CreatedBy = 4;           // HARD CODE
            newsRepository.Insert(entity);
            newsRepository.Save();
            if (model.files != null)
            {
                await _attachmentService.AddAttachments(model.files, entity.NewsId, (int)SourceTypeEnums.NEWS);
            }
            model = EntityMapper.Mapper.Map<NewsModel>(entity);
            return model;

        }

        public void DeleteNews(int Id)
        {
            newsRepository.Delete(Id);
            _attachmentService.DeleteAttachments((int)SourceTypeEnums.NEWS, Id);
            newsRepository.Save();
        }

        public List<NewsModel> GetTopNews()
        {
            var result = newsRepository.GetAll().OrderByDescending(x => x.CreatedDate).Take(3);
            var newsModel = EntityMapper.Mapper.Map<List<NewsModel>>(result);
            return newsModel;
        }

        public ListResponseModel<NewsModel> GetAllNews(string SearchParams)
        {
            Expression<Func<LrmisWebNews, bool>> predicate = null;
            Func<IQueryable<LrmisWebNews>, IOrderedQueryable<LrmisWebNews>> sortexp = null;
            ListResponseModel<NewsModel> listResponseModel = new ListResponseModel<NewsModel>();
            listResponseModel.Records = new List<NewsModel>();
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebNews>(SearchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = newsRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            foreach (var entity in result.Items)
            {
               // NewsModel newsModel = new NewsModel();
                var newsModel = EntityMapper.Mapper.Map<NewsModel>(entity);
                newsModel.Date = newsModel.NewsDate?.ToString("dd-MMM-yy");
                listResponseModel.Records.Add(newsModel);
            }

           // List<NewsModel> modelList = EntityMapper.Mapper.Map<List<NewsModel>>(result.Items);

           // listResponseModel.Records = modelList;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public ListResponseModel<NewsModel> GetAllNews(string SearchParams, string Description)
        {
            ListResponseModel<NewsModel> listResponseModel = new ListResponseModel<NewsModel>();
            listResponseModel.Records = new List<NewsModel>();
            Expression<Func<LrmisWebNews, bool>> predicate = null;
            Func<IQueryable<LrmisWebNews>, IOrderedQueryable<LrmisWebNews>> sortexp = null;
            int index = 0;
            int size = 0;
           // string SearchParams = 
            IQueryableExtensions.GetFilters<LrmisWebNews>(SearchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = newsRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

           // var result = newsRepository.GetList(predicate: x=>x.NewsTitle == Title || x.NewsDescription == Description);

            foreach (var entity in result.Items)
            {
                // NewsModel newsModel = new NewsModel();
                var newsModel = EntityMapper.Mapper.Map<NewsModel>(entity);
                newsModel.Date = newsModel.NewsDate?.ToString("dd-MMM-yy");
                listResponseModel.Records.Add(newsModel);
            }

            // List<NewsModel> modelList = EntityMapper.Mapper.Map<List<NewsModel>>(result.Items);

            // listResponseModel.Records = modelList;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public NewsModel GetNews(string Id)
        {
            NewsModel newsModel = new NewsModel();
            var result = newsRepository.GetById(Convert.ToInt32(Id));

            if (result != null)
            {
                newsModel = EntityMapper.Mapper.Map<NewsModel>(result);
                newsModel.Attachments = _attachmentService.GetAttachments((int)SourceTypeEnums.NEWS, newsModel.NewsId);
                return newsModel;
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateNews(int id, NewsModel model)
        {
            LrmisWebNews news = newsRepository.GetById(id);

            if (news != null)
            {
                var listAttachments = _attachmentService.GetAttachments((int)SourceTypeEnums.NEWS, news.NewsId);

                if (listAttachments.Any())
                {
                    model.Attachments = JsonConvert.DeserializeObject<List<AttachmentModel>>(model.ExistingAttachments);
                    var removingAttachments = listAttachments.Where(c => !model.Attachments.Any(s => s.AttachmentId == c.AttachmentId));  // item to be removed

                    _attachmentService.DeleteAttachments(removingAttachments.ToList());
                }

                if (model.files != null)
                {
                    await _attachmentService.AddAttachments(model.files, news.NewsId, (int)SourceTypeEnums.NEWS);
                }

                news.ModifiedDate = DateTime.Now;
                news.NewsTitle = model.NewsTitle;
                news.NewsDescription = model.NewsDescription;

                newsRepository.Update(news);
                newsRepository.Save();
            }
        }
    }
}
