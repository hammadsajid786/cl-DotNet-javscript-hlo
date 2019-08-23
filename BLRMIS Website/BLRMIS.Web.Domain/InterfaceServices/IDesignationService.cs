using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IDesignationService
    {
        ListResponseModel<WebDesignationModel> GetDesignationList(string searchParams);

        WebDesignationModel GetDesignationById(int DesignationId);

        void SaveDesignation(WebDesignationModel DesignationModel);

        void UpdateDesignation(int DesignationId, WebDesignationModel DesignationModel);

        void ChangeStatus(int DesignationId);
    }
}
