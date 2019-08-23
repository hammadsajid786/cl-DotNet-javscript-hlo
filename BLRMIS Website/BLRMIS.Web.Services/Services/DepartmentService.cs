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
    public class DepartmentService : BaseService, IDepartmentService
    {
        IRepository<LrmisWebDepartment> departmentRepository;

        public DepartmentService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            departmentRepository = unitOfWork.GetRepository<LrmisWebDepartment>();
        }

        public ListResponseModel<WebDepartmentModel> GetDepartmentList(string searchParams)
        {
            Expression<Func<LrmisWebDepartment, bool>> predicate = null;
            Func<IQueryable<LrmisWebDepartment>, IOrderedQueryable<LrmisWebDepartment>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebDepartment>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = departmentRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<WebDepartmentModel> liWebDepartmentModel = EntityMapper.Mapper.Map<List<WebDepartmentModel>>(result.Items);

            ListResponseModel<WebDepartmentModel> listResponseModel = new ListResponseModel<WebDepartmentModel>();
            listResponseModel.Records = liWebDepartmentModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public WebDepartmentModel GetDepartmentById(int DepartmentId)
        {
            return EntityMapper.Mapper.Map<WebDepartmentModel>(departmentRepository.GetById(DepartmentId));
        }

        public void SaveDepartment(WebDepartmentModel DepartmentModel)
        {
            DepartmentModel.CreatedBy = 4;
            departmentRepository.Insert(EntityMapper.Mapper.Map<LrmisWebDepartment>(DepartmentModel));
            departmentRepository.Save();
        }

        public void UpdateDepartment(int DepartmentId, WebDepartmentModel DepartmentModel)
        {
            LrmisWebDepartment _department = departmentRepository.GetById(DepartmentId);

            WebDepartmentModel tempDepartment = new WebDepartmentModel();
            tempDepartment.CreatedDate = _department.CreatedDate;
            tempDepartment.CreatedBy = _department.CreatedBy;

            if (_department.DepartmentId > 0)
            {
                departmentRepository.SetState(_department, Microsoft.EntityFrameworkCore.EntityState.Detached);
                var department = EntityMapper.Mapper.Map<LrmisWebDepartment>(DepartmentModel);
                department.CreatedBy = tempDepartment.CreatedBy;
                department.CreatedDate = tempDepartment.CreatedDate;
                department.ModifiedDate = DateTime.Now;
                department.ModifiedBy = 4;

                departmentRepository.Update(department);
                departmentRepository.Save();
            }
        }

        public void ChangeStatus(int DepartmentId)
        {
            LrmisWebDepartment department = departmentRepository.GetById(DepartmentId);
            department.Active = !department.Active;
            departmentRepository.Update(department);
            departmentRepository.Save();
        }
    }
}
