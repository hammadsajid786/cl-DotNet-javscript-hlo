using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class LocationService : BaseService, ILocationService
    {
        IRepository<LrmisWebLocation> locationRepository;

        public LocationService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            locationRepository = unitOfWork.GetRepository<LrmisWebLocation>();
        }

        public ListResponseModel<WebLocationModel> GetLocationList(string searchParams)
        {
            Expression<Func<LrmisWebLocation, bool>> predicate = null;
            Func<IQueryable<LrmisWebLocation>, IOrderedQueryable<LrmisWebLocation>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebLocation>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            dynamic result = null;
            if (string.IsNullOrEmpty(searchParams))
                result = locationRepository.GetList();
            else
                result = locationRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size);

            List<WebLocationModel> liWebDepartmentModel = EntityMapper.Mapper.Map<List<WebLocationModel>>(result.Items);

            ListResponseModel<WebLocationModel> listResponseModel = new ListResponseModel<WebLocationModel>();
            listResponseModel.Records = liWebDepartmentModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public WebLocationModel GetLocationById(int LocationId)
        {
            return EntityMapper.Mapper.Map<WebLocationModel>(locationRepository.GetById(LocationId));
        }

        public void SaveLocation(WebLocationModel LocationModel)
        {
            LocationModel.CreatedDate = DateTime.Now;
            locationRepository.Insert(EntityMapper.Mapper.Map<LrmisWebLocation>(LocationModel));
            locationRepository.Save();
        }

        public void UpdateLocation(int LocationId, WebLocationModel LocationModel)
        {
            LrmisWebLocation location = locationRepository.GetById(LocationId);

            if (location.LocationId > 0)
            {
                locationRepository.SetState(location, Microsoft.EntityFrameworkCore.EntityState.Detached);

                location.DigitizationProgressPercentage = LocationModel.DigitizationProgressPercentage;
                location.ModifiedDate = DateTime.Now;

                locationRepository.Update(location);
                locationRepository.Save();
            }
        }

        public void ChangeStatus(int LocationId)
        {
            LrmisWebLocation location = locationRepository.GetById(LocationId);
            location.Active = !location.Active;
            locationRepository.Update(location);
            locationRepository.Save();
        }
    }
}
