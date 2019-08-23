using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IDashboardService
    {
        Task<SingleResponseModel<EStampingModel>> GetChallanDetails(DashboardModel model);

        DashboardComplaintModel GetComplaintStats(DashboardModel model);
    }
}
