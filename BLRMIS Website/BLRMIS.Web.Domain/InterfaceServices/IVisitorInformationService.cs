using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IVisitorInformationService
    {
        ListResponseModel<WebVisitInformationModel> GetVisitorInformationList(string searchParams);

        WebVisitInformationModel GetVisitorInformationById(int VisitorInformationId);

        void SaveVisitorInformation(WebVisitInformationModel VisitorInformationModel);

        void UpdateVisitorInformation(int VisitorInformationId, WebVisitInformationModel VisitorInformationModel);

        void ChangeStatus(int VisitorInformationId);
    }
}
