using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IFAQService
    {
        FAQModel GetFAQ(string Id);

        ListResponseModel<FAQModel>GetAll(string SearchParam);

        ListResponseModel<FAQModel> GetAllFAQs(string Title, string Description);

        Task<FAQModel> CreateFAQ(FAQModel model);

        Task UpdateFAQ(int id, FAQModel model);

        void DeleteFAQ(int Id);
    }
}
