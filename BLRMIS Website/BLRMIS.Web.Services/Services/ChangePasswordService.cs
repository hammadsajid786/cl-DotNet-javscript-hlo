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
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class ChangePasswordService : BaseService, IChangePasswordService
    {
        IRepository<LrmisWebUser> userRepository;
        private readonly AppSettings appSettings;
        public ChangePasswordService(IUnitOfWork unitOfWork, IOptions<AppSettings> AppSettings) : base(unitOfWork)
        {
            userRepository = unitOfWork.GetRepository<LrmisWebUser>();
            appSettings = AppSettings.Value;
        }
        public void ChangePassword(WebUserModel user)
        {
            var userModal = userRepository.GetAll(x => x.UserId == user.UserId)[0];
            userModal.Password = CryptoEngine.Encrypt(user.Password, appSettings.PasswordSecret);
            userRepository.Update(EntityMapper.Mapper.Map<LrmisWebUser>(userModal));
            userRepository.Save();
        }
    }
}
