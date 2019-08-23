using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BLRMIS.Web.Services.Services
{
   public class ConfigService: BaseService, IConfigService
    {
        private IRepository<LrmisWebFunctionRoleMapping> _functionRoleMappingRepository;
        private IRepository<LrmisWebFunctions> _functionRoleRepository;

        public ConfigService(IUnitOfWork unitOfWork):base(unitOfWork)
        {
            _functionRoleMappingRepository = unitOfWork.GetRepository<LrmisWebFunctionRoleMapping>();
            _functionRoleRepository = unitOfWork.GetRepository<LrmisWebFunctions>();
        }
        
         List<UserFunctionModel> IConfigService.GetUserFunctions(int UserRoleId)
        {
            var findAllAccessibleResources = _functionRoleMappingRepository.GetAll(i => i.RoleId == UserRoleId && i.Include == true); 
            if (findAllAccessibleResources == null) throw new Exception(String.Format("There is no function assigned to the user. please check role function mapping table with this id {0}", UserRoleId));
            var functionIds = findAllAccessibleResources.Select(i => i.FunctionId).ToList();
            var findFunctions = _functionRoleRepository.GetAll(i => functionIds.Contains(i.FunctionId)); 

            var functionList = new List<UserFunctionModel>();
            foreach (var item in findFunctions) {
                functionList.Add(new UserFunctionModel
                {
                    FunctionId = item.FunctionId,
                    FunctionName = item.FunctionName,
                    FunctionDescription = item.FunctionDescription
                }); 
            }
            return functionList; 
        }
    }
}
