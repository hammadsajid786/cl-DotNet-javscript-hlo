using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using Microsoft.Extensions.Options;

namespace BLRMIS.Web.Services
{
    public class ContentService : BaseService, IContentService
    {

        private readonly AppSettings appSettings;
        public ContentService(IUnitOfWork unitOfWork, IOptions<AppSettings> AppSettings) : base(unitOfWork)
        {
            appSettings = AppSettings.Value;
        }

        public WebContentModel GetContent(string Id)
        {

            // var result = _unitOfWorkFactory.GetRepository<LrmisWebContent>().GetById(1);
            WebContentModel webContentModel = new WebContentModel();
            var result = unitOfWork.GetRepository<LrmisWebContent>().GetByCondition(x => x.ContentPageId == Convert.ToInt32(Id));

            if (result != null)
            {
                webContentModel.ContentPageId = result.ContentPageId;
                webContentModel.ContentDescription = result.ContentDescription;
                webContentModel.ContentId = result.ContentId;
                //WebContentModel webContentModel = Mapper.Map<LrmisWebContent, WebContentModel>(result);
                return webContentModel;
            }
            else
            {
                return null;
            }


        }

        public List<WebContentModel> GetContentForDashboard()
        {
            string[] arrContentPagesIds = new string[4]
            {
                appSettings.EStampId,
                appSettings.RegistryOfDeedsId,
                appSettings.MarkazSahuliyatId,
                appSettings.GrievanceId
            };

            var result = unitOfWork.GetRepository<LrmisWebContent>().GetAll(x => Array.IndexOf(arrContentPagesIds, x.ContentPageId.ToString()) >= 0);
            var contentPages = unitOfWork.GetRepository<LrmisWebPage>().GetAll();
            List<WebContentModel> liWebContentModel = new List<WebContentModel>();

            foreach (var item in result)
            {
                WebContentModel webContentModel = new WebContentModel();
                webContentModel.ContentPageId = item.ContentPageId;
                webContentModel.ContentDescription = item.ContentDescription;
                webContentModel.ContentId = item.ContentId;
                webContentModel.ContentPageName = contentPages.Find(x => x.PageId == item.ContentPageId).PageName;

                webContentModel.ContentPageUrl = item.ContentPageId == Convert.ToInt32(appSettings.EStampId) ? appSettings.EStampPageUrl :
                    item.ContentPageId == Convert.ToInt32(appSettings.RegistryOfDeedsId) ? appSettings.RegistryOfDeedsPageUrl :
                    item.ContentPageId == Convert.ToInt32(appSettings.MarkazSahuliyatId) ? appSettings.MarkazSahuliyatPageUrl :
                    item.ContentPageId == Convert.ToInt32(appSettings.GrievanceId) ? appSettings.GrievancePageUrl : "";

                webContentModel.ContentPageIcon = item.ContentPageId == Convert.ToInt32(appSettings.EStampId) ? appSettings.EStampIcon :
                    item.ContentPageId == Convert.ToInt32(appSettings.RegistryOfDeedsId) ? appSettings.RegistryOfDeedsIcon :
                    item.ContentPageId == Convert.ToInt32(appSettings.MarkazSahuliyatId) ? appSettings.MarkazSahuliyatIcon :
                    item.ContentPageId == Convert.ToInt32(appSettings.GrievanceId) ? appSettings.GrievanceIcon : "";

                liWebContentModel.Add(webContentModel);
            }

            return liWebContentModel;
        }

        public List<WebPageModel> GetAllPages()
        {
            List<WebPageModel> webPageModels = new List<WebPageModel>();
            List<LrmisWebPage> lrmisWebPages = unitOfWork.GetRepository<LrmisWebPage>().GetAll(); //.UserRepository.AddUser(user);

            foreach (var lrmisWebPage in lrmisWebPages)
            {
                WebPageModel webPageModel = new WebPageModel();
                webPageModel.PageId = lrmisWebPage.PageId.ToString();
                webPageModel.PageName = lrmisWebPage.PageName;

                webPageModels.Add(webPageModel);
            }

            return webPageModels;
        }

        //TODO: Rename Post Content with Submit Content. 
        public void PostContent(WebContentModel model)
        {
            // check if page content already exists
            var entity = unitOfWork.GetRepository<LrmisWebContent>().GetByCondition(x => x.ContentPageId == model.ContentPageId);

            if (entity != null)
            {
                entity.ModifiedDate = DateTime.Now;
                // entity.modified_by = ;
                entity.ContentDescription = model.ContentDescription;

                unitOfWork.GetRepository<LrmisWebContent>().Update(entity);
            }
            else
            {
                LrmisWebContent lrmisWebContent = new LrmisWebContent();
                lrmisWebContent.CreatedDate = DateTime.Now;
                lrmisWebContent.CreatedBy = 4;           // HARD CODE
                lrmisWebContent.ContentPageId = model.ContentPageId;
                lrmisWebContent.ContentDescription = model.ContentDescription;

                unitOfWork.GetRepository<LrmisWebContent>().Insert(lrmisWebContent);
            }

            unitOfWork.GetRepository<LrmisWebContent>().Save();
        }
    }
}
