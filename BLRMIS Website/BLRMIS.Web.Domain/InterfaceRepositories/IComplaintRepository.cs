using BLRMIS.Web.Domain.Models;
using System.Collections.Generic;
using BLRMIS.Web.DataAccess.Entities;
using System;
using System.Linq.Expressions;

namespace BLRMIS.Web.Domain.InterfaceRepositories
{
    public interface IComplaintRepository:IRepository<LrmisWebComplaint>
    {
        IEnumerable<LrmisWebComplaintStatus> GetAllComplaintStatus();
        IEnumerable<LrmisWebComplaintLog> GetComplaintLogs(int complaintId);
        void AddComplaintLog(LrmisWebComplaintLog complaintLogModel);
        IEnumerable<LrmisWebComplaint> GetAllComplaints(Expression<Func<LrmisWebComplaint, bool>> condition);

    }
}
