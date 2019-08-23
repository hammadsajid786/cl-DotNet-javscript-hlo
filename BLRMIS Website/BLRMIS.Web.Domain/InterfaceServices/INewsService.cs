using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public interface INewsService
    {
        NewsModel GetNews(string Id);

        ListResponseModel<NewsModel> GetAllNews(string SearchParam);

        List<NewsModel> GetTopNews();

        ListResponseModel<NewsModel> GetAllNews(string Title, string Description);

        Task<NewsModel> CreateNews(NewsModel model);

        Task UpdateNews(int id, NewsModel model);

        void DeleteNews(int Id);
    }
}
