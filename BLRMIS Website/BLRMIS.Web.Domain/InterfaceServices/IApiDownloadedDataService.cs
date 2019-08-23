using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IApiDownloadedDataService
    {
        ListResponseModel<WebApiDownloadedDataModel> GetWebApiDownloadedList(string searchParams);

        WebApiDownloadedDataModel GetWebApiDownloadedById(int DesignationId);

    }
}
