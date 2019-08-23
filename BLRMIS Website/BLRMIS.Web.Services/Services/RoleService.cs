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
    public class RoleService : BaseService, IRoleService
    {
        IRepository<LrmisWebRole> roleRepository;
        IRepository<LrmisWebFunctions> functionRepository;
        IRepository<LrmisWebFunctionRoleMapping> mappingRepository;
        IRepository<LrmisWebUser> userRepository;

        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            roleRepository = unitOfWork.GetRepository<LrmisWebRole>();
            functionRepository = unitOfWork.GetRepository<LrmisWebFunctions>();
            mappingRepository = unitOfWork.GetRepository<LrmisWebFunctionRoleMapping>();
            userRepository = unitOfWork.GetRepository<LrmisWebUser>();
        }

        public ListResponseModel<WebRoleModel> GetRolesList(string searchParams)
        {
            Expression<Func<LrmisWebRole, bool>> predicate = null;
            Func<IQueryable<LrmisWebRole>, IOrderedQueryable<LrmisWebRole>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebRole>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = roleRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<WebRoleModel> liWebRoleModel = EntityMapper.Mapper.Map<List<WebRoleModel>>(result.Items);

            ListResponseModel<WebRoleModel> listResponseModel = new ListResponseModel<WebRoleModel>();
            listResponseModel.Records = liWebRoleModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public WebRoleModel GetRoleById(int RoleId)
        {
            return EntityMapper.Mapper.Map<WebRoleModel>(roleRepository.GetById(RoleId));
        }

        public void SaveRole(WebRoleModel RoleModel)
        {
            //RoleModel.Active = true;
            RoleModel.CreatedBy = 4;
            roleRepository.Insert(EntityMapper.Mapper.Map<LrmisWebRole>(RoleModel));
            roleRepository.Save();
        }

        public bool UpdateRole(int RoleId, WebRoleModel RoleModel)
        {
            LrmisWebRole _role = roleRepository.GetById(RoleId);

            WebRoleModel tempRole = new WebRoleModel();
            tempRole.CreatedDate = _role.CreatedDate;
            tempRole.CreatedBy = _role.CreatedBy;
            //tempRole.Active = _role.Active;

            if(_role.Active && !RoleModel.Active)
            {
                var user = userRepository.GetByCondition(x => x.RoleId == _role.RoleId && x.Active == true);
                if (user != null)
                    return false;
            }

            if (_role.RoleId > 0)
            {
                roleRepository.SetState(_role, Microsoft.EntityFrameworkCore.EntityState.Detached);
                var role = EntityMapper.Mapper.Map<LrmisWebRole>(RoleModel);
                //role.Active = tempRole.Active;
                role.CreatedBy = tempRole.CreatedBy;
                role.CreatedDate = tempRole.CreatedDate;
                role.ModifiedDate = DateTime.Now;
                role.ModifiedBy = 4;

                roleRepository.Update(role);
                roleRepository.Save();
            }
            return true;
        }

        public bool ChangeStatus(int RoleId)
        {
            LrmisWebRole role = roleRepository.GetById(RoleId);
            if (role.Active)
            {
                var user = userRepository.GetByCondition(x => x.RoleId == role.RoleId && x.Active == true);
                if (user != null)
                    return false;
            }
            role.Active = !role.Active;
            roleRepository.Update(role);
            roleRepository.Save();
            return true;
        }

        public ListResponseModel<WebFunctionRoleMapping> GetRoleFunctions(string SearchParams, int RoleId)
        {
            Expression<Func<LrmisWebFunctions, bool>> predicate = null;
            Func<IQueryable<LrmisWebFunctions>, IOrderedQueryable<LrmisWebFunctions>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebFunctions>(SearchParams, ref predicate, ref sortexp, ref index, ref size);

            List<WebFunctionRoleMapping> listMappings = new List<WebFunctionRoleMapping>();

            var result = functionRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);
            //var listFunctions = functionRepository.GetAll().OrderBy(x => x.FunctionName);

            List<LrmisWebFunctionRoleMapping> listRoleFunctionMappings = mappingRepository.GetAll(x => x.RoleId == RoleId);

            foreach (LrmisWebFunctions item in result.Items)
            {
                LrmisWebFunctionRoleMapping selectedMapping = listRoleFunctionMappings.Find(x => x.FunctionId == item.FunctionId);

                WebFunctionRoleMapping roleFunctionMapping = new WebFunctionRoleMapping();
                roleFunctionMapping.MappingId = selectedMapping != null ? selectedMapping.MappingId : 0;
                roleFunctionMapping.FunctionId = item.FunctionId;
                roleFunctionMapping.RoleId = RoleId;
                roleFunctionMapping.FunctionName = item.FunctionName;
                roleFunctionMapping.Include = selectedMapping != null ? selectedMapping.Include : false;
                listMappings.Add(roleFunctionMapping);
            }

            ListResponseModel<WebFunctionRoleMapping> listResponseModel = new ListResponseModel<WebFunctionRoleMapping>();
            listResponseModel.Records = listMappings;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public void MapRoleFunctions(List<WebFunctionRoleMapping> mappings)
        {
            foreach (WebFunctionRoleMapping item in mappings)
            {
                if (item.MappingId > 0)
                {
                    LrmisWebFunctionRoleMapping functionRoleMapping = mappingRepository.GetById(item.MappingId);
                    functionRoleMapping.Include = item.Include;
                    mappingRepository.Update(functionRoleMapping);
                    mappingRepository.Save();
                }
                else
                {
                    mappingRepository.Insert(EntityMapper.Mapper.Map<LrmisWebFunctionRoleMapping>(item));
                    mappingRepository.Save();
                }
            }
        }
    }
}
