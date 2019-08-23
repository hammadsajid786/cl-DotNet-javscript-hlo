using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.ExternalCommunication;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Services.Services
{
    public class DashboardService : BaseService, IDashboardService
    {
        ExternalCommunicationService _externalCommunicationService;
        IRepository<LrmisWebComplaint> complaintRepository;
        IRepository<LrmisWebComplaintLog> complaintLogRepository;

        public DashboardService(IUnitOfWork unitOfWork, ExternalCommunicationService externalCommunicationService) : base(unitOfWork)
        {

            this._externalCommunicationService = externalCommunicationService;
            complaintRepository = unitOfWork.GetRepository<LrmisWebComplaint>();
            complaintLogRepository = unitOfWork.GetRepository<LrmisWebComplaintLog>();
        }

        public async Task<SingleResponseModel<EStampingModel>> GetChallanDetails(DashboardModel model)
        {
            var _params = new Dictionary<string, string>();
            var response = new SingleResponseModel<EStampingModel>();

            if (!string.IsNullOrEmpty(model.FromDate))
            {
                model.FromDate = DateTime.ParseExact(model.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
            }

            if (!string.IsNullOrEmpty(model.ToDate))
            {
                model.ToDate = DateTime.ParseExact(model.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString("dd-MM-yyyy");
            }

            _params.Add(ExternalCommunicationParameters.FromDate, !string.IsNullOrEmpty(model.FromDate) ? model.FromDate : string.Empty);
            _params.Add(ExternalCommunicationParameters.ToDate, !string.IsNullOrEmpty(model.ToDate) ? model.ToDate : string.Empty);
            _params.Add(ExternalCommunicationParameters.DistrictId, !string.IsNullOrEmpty(model.DistrictId) ? model.DistrictId : string.Empty);

            var internalResponse = await _externalCommunicationService.GetTotalChallanAmount(_params);

            if (!internalResponse.IsError)
            {
                response.Model = internalResponse.Model;
            }
            else
            {
                response.IsError = true;
                response.Error = internalResponse.Error;
            }

            return response;
        }

        public DashboardComplaintModel GetComplaintStats(DashboardModel model)
        {
            var response = new DashboardComplaintModel();
            DateTime toDate = new DateTime();
            DateTime fromDate = new DateTime();
            if (!string.IsNullOrEmpty(model.FromDate))
            {
                fromDate = Convert.ToDateTime(DateTime.ParseExact(model.FromDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            }
            if (!string.IsNullOrEmpty(model.ToDate))
            {
                toDate = Convert.ToDateTime(DateTime.ParseExact(model.ToDate, "MM/dd/yyyy", CultureInfo.InvariantCulture));
            }
            var result = complaintRepository.GetAll(x => (string.IsNullOrEmpty(model.FromDate) || x.CreatedDate >= fromDate) && (string.IsNullOrEmpty(model.ToDate) || x.CreatedDate <= toDate) && (string.IsNullOrEmpty(model.DistrictId) || x.LocationId == Convert.ToInt32(model.DistrictId)));
            response.TotalComplaints = result.Count;
            response.OpenComplaints = result.FindAll(x=>x.ComplaintStatusId == (int)ComplaintStatusEnum.OPEN).Count;
            response.ReOpenComplaints = result.FindAll(x => x.ComplaintStatusId == (int)ComplaintStatusEnum.REOPEN).Count;
            response.ResolvedComplaints = result.FindAll(x => x.ComplaintStatusId == (int)ComplaintStatusEnum.RESOLVED).Count;
            response.ClosedComplaints = result.FindAll(x => x.ComplaintStatusId == (int)ComplaintStatusEnum.CLOSED).Count;
            response.InProgressComplaints = result.FindAll(x => x.ComplaintStatusId == (int)ComplaintStatusEnum.IN_PROGRESS).Count;
            response.PendingComplaints = result.FindAll(x => x.ComplaintStatusId == (int)ComplaintStatusEnum.PENDING).Count;

            response.SatisfiedComplaints = result.FindAll(x => x.ComplaintStatusId == (int)ComplaintStatusEnum.CLOSED).Count;
            response.UnSatisfiedComplaints = result.FindAll(x => x.ComplaintStatusId != (int)ComplaintStatusEnum.CLOSED && x.LrmisWebComplaintLog.Contains(complaintLogRepository.GetByCondition(a=>a.ComplaintStatusId == (int)ComplaintStatusEnum.REOPEN && a.ComplaintId == x.ComplaintId))).Count;

            return response;
        }
    }
}
