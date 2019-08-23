using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class DesignationService : BaseService, IDesignationService
    {
        IRepository<LrmisWebDesignation> designationRepository;

        public DesignationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            designationRepository = unitOfWork.GetRepository<LrmisWebDesignation>();
        }

        public ListResponseModel<WebDesignationModel> GetDesignationList(string searchParams)
        {
            Expression<Func<LrmisWebDesignation, bool>> predicate = null;
            Func<IQueryable<LrmisWebDesignation>, IOrderedQueryable<LrmisWebDesignation>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebDesignation>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = designationRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<WebDesignationModel> liWebDesignationModel = EntityMapper.Mapper.Map<List<WebDesignationModel>>(result.Items);

            ListResponseModel<WebDesignationModel> listResponseModel = new ListResponseModel<WebDesignationModel>();
            listResponseModel.Records = liWebDesignationModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public WebDesignationModel GetDesignationById(int DesignationId)
        {
            return EntityMapper.Mapper.Map<WebDesignationModel>(designationRepository.GetById(DesignationId));
        }

        public void SaveDesignation(WebDesignationModel DesignationModel)
        {
            DesignationModel.CreatedBy = 4;
            designationRepository.Insert(EntityMapper.Mapper.Map<LrmisWebDesignation>(DesignationModel));
            designationRepository.Save();
        }

        public void UpdateDesignation(int DesignationId, WebDesignationModel DesignationModel)
        {
            LrmisWebDesignation _designation = designationRepository.GetById(DesignationId);

            WebDesignationModel tempDesignation = new WebDesignationModel();
            tempDesignation.CreatedDate = _designation.CreatedDate;
            tempDesignation.CreatedBy = _designation.CreatedBy;

            if (_designation.DesignationId > 0)
            {
                designationRepository.SetState(_designation, Microsoft.EntityFrameworkCore.EntityState.Detached);
                var designation = EntityMapper.Mapper.Map<LrmisWebDesignation>(DesignationModel);
                designation.CreatedBy = tempDesignation.CreatedBy;
                designation.CreatedDate = tempDesignation.CreatedDate;
                designation.ModifiedDate = DateTime.Now;
                designation.ModifiedBy = 4;

                designationRepository.Update(designation);
                designationRepository.Save();
            }
        }

        public void ChangeStatus(int DesignationId)
        {
            LrmisWebDesignation designation = designationRepository.GetById(DesignationId);
            designation.Active = !designation.Active;
            designationRepository.Update(designation);
            designationRepository.Save();
        }
    }
}
