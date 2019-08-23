using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface ICategoryService
    {
        IEnumerable<SelectListModel> GetCategoryListShort();

        ListResponseModel<WebCategoryModel> GetCategoryList(string searchParams);

        WebCategoryModel GetCategoryById(int CategoryId);

        void SaveCategory(WebCategoryModel CategoryModel);

        void UpdateCategory(int CategoryId, WebCategoryModel CategoryModel);

        void ChangeStatus(int CategoryId);
    }
}
