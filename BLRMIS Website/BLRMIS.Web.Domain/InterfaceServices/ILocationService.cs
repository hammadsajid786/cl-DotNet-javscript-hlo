using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface ILocationService
    {
        ListResponseModel<WebLocationModel> GetLocationList(string searchParams);

        WebLocationModel GetLocationById(int LocationId);

        void SaveLocation(WebLocationModel LocationModel);

        void UpdateLocation(int LocationId, WebLocationModel LocationModel);

        void ChangeStatus(int LocationId);
    }
}
