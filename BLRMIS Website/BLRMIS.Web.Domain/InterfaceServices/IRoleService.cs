using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IRoleService
    {
        ListResponseModel<WebRoleModel> GetRolesList(string searchParams);

        WebRoleModel GetRoleById(int RoleId);

        void SaveRole(WebRoleModel RoleModel);

        bool UpdateRole(int RoleId, WebRoleModel RoleModel);

        bool ChangeStatus(int RoleId);

        ListResponseModel<WebFunctionRoleMapping> GetRoleFunctions(string SearchParams, int RoleId);

        void MapRoleFunctions(List<WebFunctionRoleMapping> mapping);
    }
}
