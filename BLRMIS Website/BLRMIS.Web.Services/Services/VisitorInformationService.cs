using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;

namespace BLRMIS.Web.Services.Services
{
    public class VisitorInformationService : BaseService, IVisitorInformationService
    {
        IRepository<LrmisWebVisitorInformation> visitorInformationRepository;

        public VisitorInformationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            visitorInformationRepository = unitOfWork.GetRepository<LrmisWebVisitorInformation>();
        }

        public void ChangeStatus(int VisitorInformationId)
        {
            throw new NotImplementedException();
        }

        public WebVisitInformationModel GetVisitorInformationById(int VisitorInformationId)
        {
            throw new NotImplementedException();
        }

        public ListResponseModel<WebVisitInformationModel> GetVisitorInformationList(string searchParams)
        {
            Expression<Func<LrmisWebVisitorInformation, bool>> predicate = null;
            Func<IQueryable<LrmisWebVisitorInformation>, IOrderedQueryable<LrmisWebVisitorInformation>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebVisitorInformation>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = visitorInformationRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<WebVisitInformationModel> liWebDesignationModel = EntityMapper.Mapper.Map<List<WebVisitInformationModel>>(result.Items);

            ListResponseModel<WebVisitInformationModel> listResponseModel = new ListResponseModel<WebVisitInformationModel>();
            listResponseModel.Records = liWebDesignationModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public void SaveVisitorInformation(WebVisitInformationModel VisitorInformationModel)
        {
            visitorInformationRepository.Insert(EntityMapper.Mapper.Map<LrmisWebVisitorInformation>(VisitorInformationModel));
            visitorInformationRepository.Save();
        }

        public void UpdateVisitorInformation(int VisitorInformationId, WebVisitInformationModel VisitorInformationModel)
        {
            throw new NotImplementedException();
        }
    }
}
