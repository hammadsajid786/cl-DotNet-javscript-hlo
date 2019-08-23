using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceRepositories
{
   public interface IComplaintLogRepository
    {
        IEnumerable<LrmisWebComplaintLog> GetComplaintLogs(int complaintId);
        void AddComplaintLog(ComplaintLogModel complaintLogModel);
    }
}
