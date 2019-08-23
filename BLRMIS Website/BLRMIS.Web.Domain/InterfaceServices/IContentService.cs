using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface IContentService
    {
        WebContentModel GetContent(string Id);

        List<WebContentModel> GetContentForDashboard();

        List<WebPageModel> GetAllPages();

        void PostContent(WebContentModel model);
    }
}
