using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IDepartmentService
    {
        ListResponseModel<WebDepartmentModel> GetDepartmentList(string searchParams);

        WebDepartmentModel GetDepartmentById(int DepartmentId);

        void SaveDepartment(WebDepartmentModel DepartmentModel);

        void UpdateDepartment(int DepartmentId, WebDepartmentModel DepartmentModel);

        void ChangeStatus(int DepartmentId);
    }
}
