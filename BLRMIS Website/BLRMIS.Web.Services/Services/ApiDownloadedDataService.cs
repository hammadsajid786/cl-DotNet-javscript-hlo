using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class ApiDownloadedDataService : BaseService, IApiDownloadedDataService
    {
        IRepository<LrmisWebApiDownloadedData> apiDataDownloadRepository;
        AppSettings appSettings;
        public ApiDownloadedDataService(IUnitOfWork unitOfWork, IOptions<AppSettings> settings) : base(unitOfWork)
        {
            apiDataDownloadRepository = unitOfWork.GetRepository<LrmisWebApiDownloadedData>();
            appSettings = settings.Value;
        }

        public WebApiDownloadedDataModel GetWebApiDownloadedById(int DesignationId)
        {
            throw new NotImplementedException();
        }

        public ListResponseModel<WebApiDownloadedDataModel> GetWebApiDownloadedList(string searchParams)
        {

            DateTime dtFrom;

            if (searchParams == "1")
            {
                dtFrom = DateTime.Now.AddDays(-7);
            }
            else if (searchParams == "2")
            {
                dtFrom = DateTime.Now.AddMonths(-1);
            }
            else if (searchParams == "3")
            {
                dtFrom = DateTime.Now.AddYears(-1);
            }
            else
            {
                dtFrom = DateTime.Now.AddDays(-1);
            }

            DateTime dtTo = DateTime.Now;

            var result =apiDataDownloadRepository.GetAll().Where(x => x.DownloadedDate >= dtFrom && x.DownloadedDate <= dtTo);
            List<WebApiDownloadedDataModel> liWebDesignationModel = EntityMapper.Mapper.Map<List<WebApiDownloadedDataModel>>(result);

            ListResponseModel<WebApiDownloadedDataModel> listResponseModel = new ListResponseModel<WebApiDownloadedDataModel>();
            listResponseModel.Records = liWebDesignationModel;
            listResponseModel.TotalPages =0;
            listResponseModel.TotalRecords = 0;

            return listResponseModel;
        }
    }
}
