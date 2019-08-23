using BLRMIS.Web.Domain;
using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLRMIS.Web.Services.Mapper
{
    public static class PagingMapper
    {
        public static async Task<ListResponseModel<TReturn>> CreatePagedResults<TReturn>(
    IQueryable<TReturn> queryable,
    int? page,
    int? pageSize,
    string orderBy,
    bool ascending)
        {
            try
            {
                IQueryable<TReturn> projection;

                if (pageSize == null || pageSize == 0)
                {
                    pageSize = 10;
                }

                if (page != null)
                {
                    var skipAmount = pageSize.Value * (page.Value - 1);

                    projection = queryable
                        .OrderByPropertyOrField(orderBy, ascending)
                        .Skip(skipAmount)
                        .Take(pageSize.Value);
                }
                else
                {
                    projection = queryable
                        .OrderByPropertyOrField(orderBy, ascending);
                }

                var totalNumberOfRecords = queryable.Count();
                var results = projection.ToList();
                var mod = totalNumberOfRecords % pageSize.Value;
                var totalPageCount = (totalNumberOfRecords / pageSize.Value) + (mod == 0 ? 0 : 1);

                return new ListResponseModel<TReturn>
                {
                    Records = results,
                    PageNumber = page ?? 0,
                    PageSize = results.Count,
                    TotalPages = page == null ? 1 : totalPageCount,
                    TotalRecords = totalNumberOfRecords
                    //NextPageUrl = nextPageUrl
                };
            }
            catch (Exception ex)
            {
                return new ListResponseModel<TReturn>();
            }
        }

        public static async Task<ListResponseModel<TReturn>> GetPaginatedResultsAsync<TReturn>(PagingModel pagingModel, List<TReturn> results)
        {
            ListResponseModel<TReturn> response;

            response = await PagingMapper
                .CreatePagedResults(results.AsQueryable(),
                    pagingModel.Page, pagingModel.PageSize, pagingModel.OrderBy, pagingModel.Ascending)
                .ConfigureAwait(false);

            if (response.Records == null || !response.Records.Any())
            {
                response.IsError = true;
                response.Error = new ErrorResponseModel()
                    .PaginationError();
            }

            return response;
        }

    }
}
