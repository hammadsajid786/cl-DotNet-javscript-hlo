using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Repositories;
namespace BLRMIS.Web.Services.Services
{
    public class CommonService : BaseService, ICommonService
    {
        public CommonService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
     
    }
}
