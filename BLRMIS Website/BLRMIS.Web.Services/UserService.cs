using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Domain.Paging;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BLRMIS.Web.Services
{
    public class UserService : BaseService, IUserService
    {
        IRepository<LrmisWebUser> userRepository;
        IRepository<LrmisWebDesignation> designationRepository;
        IRepository<LrmisWebDepartment> departmentRepository;
        IRepository<LrmisWebLocation> locationRepository;
        IRepository<LrmisWebRole> roleRepository;
        private readonly AppSettings appSettings;

        public UserService(IUnitOfWork unitOfWork, IOptions<AppSettings> AppSettings) : base(unitOfWork)
        {
            userRepository = unitOfWork.GetRepository<LrmisWebUser>();
            designationRepository = unitOfWork.GetRepository<LrmisWebDesignation>();
            departmentRepository = unitOfWork.GetRepository<LrmisWebDepartment>();
            locationRepository = unitOfWork.GetRepository<LrmisWebLocation>();
            roleRepository = unitOfWork.GetRepository<LrmisWebRole>();
            appSettings = AppSettings.Value;
        }

        public WebUserModel GetUserById(int UserId)
        {
            var user = userRepository.Single(predicate: x => x.UserId == UserId,
                include: source => source.Include(t => t.Role).Include(t => t.Location).Include(t => t.Department).Include(t => t.Designation));

            user.Password = CryptoEngine.Decrypt(user.Password, appSettings.PasswordSecret);
            return EntityMapper.Mapper.Map<WebUserModel>(user);
        }

        public ListResponseModel<WebUserModel> GetUsersList(string searchParams)
        {
            Expression<Func<LrmisWebUser, bool>> predicate = null;
            Func<IQueryable<LrmisWebUser>, IOrderedQueryable<LrmisWebUser>> sortexp = null;
            int index = 0;
            int size = 0;
            IQueryableExtensions.GetFilters<LrmisWebUser>(searchParams, ref predicate, ref sortexp, ref index, ref size);
            var result = userRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size,
                include: source => source.Include(t => t.Role).Include(t => t.Location).Include(t => t.Designation).Include(t => t.Department));
            List<WebUserModel> liWebUserModel = EntityMapper.Mapper.Map<List<WebUserModel>>(result.Items);
            ListResponseModel<WebUserModel> listResponseModel = new ListResponseModel<WebUserModel>();
            //listResponseModel.Records = liWebUserModel.Where(x => x.UserId != (int)Constants.PUBLIC_USER_ID).ToList();
            listResponseModel.Records = liWebUserModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
        }

        public bool SaveUser(WebUserModel UserModel)
        {
            UserModel.Password = CryptoEngine.Encrypt(UserModel.Password, appSettings.PasswordSecret);
            var users = userRepository.GetAll(x => x.UserName.Trim().ToLower() == UserModel.UserName.Trim().ToLower());
            if (users.Count > 0)
                return false;

            UserModel.CreatedBy = 4;
            userRepository.Insert(EntityMapper.Mapper.Map<LrmisWebUser>(UserModel));
            userRepository.Save();
            return true;
        }

        public void UpdateUser(int UserId, WebUserModel UserModel)
        {
            LrmisWebUser user = userRepository.GetById(UserId);

            WebUserModel tempUser = new WebUserModel();
            tempUser.CreatedDate = user.CreatedDate;
            tempUser.CreatedBy = user.CreatedBy;
            tempUser.UserName = user.UserName;
            //tempUser.Password = user.Password;

            if (user.UserId > 0)
            {
                userRepository.SetState(user, Microsoft.EntityFrameworkCore.EntityState.Detached);
                var userModel = EntityMapper.Mapper.Map<LrmisWebUser>(UserModel);

                userModel.Password = CryptoEngine.Encrypt(userModel.Password, appSettings.PasswordSecret);
                userModel.UserName = tempUser.UserName;
                //userModel.Password = tempUser.Password;
                userModel.CreatedBy = tempUser.CreatedBy;
                userModel.CreatedDate = tempUser.CreatedDate;
                userModel.ModifiedDate = DateTime.Now;
                UserModel.ModifiedBy = 4;

                userRepository.Update(userModel);
                userRepository.Save();
            }
        }

        public void ChangeStatus(int UserId)
        {
            LrmisWebUser user = userRepository.GetById(UserId);
            user.Active = !user.Active;
            userRepository.Update(user);
            userRepository.Save();
        }

        public IEnumerable<SelectListModel> GetDesignationList()
        {
            return CommonMapper.MapDesignationList(designationRepository.GetAll(x => x.Active));
        }

        public IEnumerable<SelectListModel> GetDepartmentList()
        {
            return CommonMapper.MapDepartmentList(departmentRepository.GetAll(x => x.Active));
        }

        public IEnumerable<SelectListModel> GetLocationList()
        {
            return CommonMapper.MapLocationList(locationRepository.GetAll());
        }

        public IEnumerable<SelectListModel> GetRoleList()
        {
            return CommonMapper.MapRoleList(roleRepository.GetAll(x => x.Active));
        }

        public void TestFilter(string SearchKeyWords)
        {
            //KeyValuePair<string, SortingType> sort = new KeyValuePair<string, SortingType>("UserName", SortingType.Descending);
            //var sortexp = IQueryableExtensions.GetOrderByFunc<LrmisWebUser>(sort);

            //var predicate = IQueryableExtensions.GetFilterByFunc<LrmisWebUser>("FatherName-Ahmed,cnic-2244");
            Expression<Func<LrmisWebUser, bool>> predicate = null;
            Func<IQueryable<LrmisWebUser>, IOrderedQueryable<LrmisWebUser>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebUser>("filter:UserName!=usman123,CreatedDate=||CreatedDate=,LocationName=;orderby:UserName=Descending;size:10;index:1",
               ref predicate, ref sortexp, ref index, ref size);

            var result = userRepository.GetList(predicate: predicate, orderBy: sortexp, size: 10, index: 0,
                 include: source => source.Include(t => t.Role).Include(t => t.Location));
        }

        public IEnumerable<SelectListModel> GetAllUsersShortList()
        {
            return Mapper.CommonMapper.MapUserSelectList(userRepository.GetAll().Where(x => x.Active == true && x.UserId != Constants.PUBLIC_USER_ID).ToList());
        }
    }
}
