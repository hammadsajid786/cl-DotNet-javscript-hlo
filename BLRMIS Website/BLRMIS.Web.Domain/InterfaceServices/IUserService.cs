using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Domain.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IUserService
    {
        ListResponseModel<WebUserModel> GetUsersList(string SearchParams);

        WebUserModel GetUserById(int UserId);

        bool SaveUser(WebUserModel UserModel);

        void UpdateUser(int UserId, WebUserModel UserModel);

        void ChangeStatus(int UserId);

        IEnumerable<SelectListModel> GetDesignationList();

        IEnumerable<SelectListModel> GetDepartmentList();

        IEnumerable<SelectListModel> GetLocationList();

        IEnumerable<SelectListModel> GetRoleList();

        IEnumerable<SelectListModel> GetAllUsersShortList();
        void TestFilter(string SearchKeyWords);
    }
}
