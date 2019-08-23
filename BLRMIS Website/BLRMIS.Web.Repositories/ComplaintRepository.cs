using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BLRMIS.Web.Repositories
{
    public class ComplaintRepository : Repository<LrmisWebComplaint>, IComplaintRepository
    {
        public ComplaintRepository(BLRMIS_WebsiteContext context) : base(context) { }

        public void AddComplaintLog(LrmisWebComplaintLog complaintLogModel)
        {
            _context.Set<LrmisWebComplaintLog>().Add(complaintLogModel);
        }

        public IEnumerable<LrmisWebComplaintStatus> GetAllComplaintStatus()
        {
            return this._context.Set<LrmisWebComplaintStatus>().ToList();
        }
        public IEnumerable<LrmisWebComplaintLog> GetComplaintLogs(int complaintId)
        {
            return _context.Set<LrmisWebComplaintLog>()
                .Where(i => i.ComplaintId == complaintId)
                .Include(user => user.CreatedByNavigation)
                // .Include(user=> user.ComplaintAssignByNavigation)
                .ThenInclude(userRole => userRole.Role)
                .ToList();
        }

        public IEnumerable<LrmisWebComplaint> GetAllComplaints(Expression<Func<LrmisWebComplaint, bool>> condition)
        {
            return _context.Set<LrmisWebComplaint>()
                .Where(condition)
                .Include(category => category.ComplaintCategory)
                .Include(status => status.ComplaintStatus).ToList();
        }
    }
}
