using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class LoginService : BaseService, ILoginService
    {
        IRepository<LrmisWebUser> UserRepository;
        IRepository<LrmisWebFunctionRoleMapping> MappingRepository;
        private readonly AppSettings appSettings;
        public LoginService(IUnitOfWork unitOfWork, IOptions<AppSettings> AppSettings) : base(unitOfWork)
        {
            UserRepository = unitOfWork.GetRepository<LrmisWebUser>();
            MappingRepository = unitOfWork.GetRepository<LrmisWebFunctionRoleMapping>();
            appSettings = AppSettings.Value;
        }

        public bool Login()
        {
            return true;
        }

        public LoginResponseModel Login(string Username, string Password)
        {
            LoginResponseModel model = new LoginResponseModel();
            string encryptedPass = CryptoEngine.Encrypt(Password, appSettings.PasswordSecret);
            var user = UserRepository.Single(predicate: x => x.UserName == Username && x.Password == encryptedPass &&
            x.Active == true,
                include: source => source.Include(t => t.Role));

            if (user == null)
            {
                model.Authenticated = false;
                model.Message = ErrorMessages.UserNamePasswordIncorrect;
                return model;
            }

            var functions = MappingRepository.GetListObject(predicate: x => x.RoleId == user.RoleId && x.Include == true,
                include: source => source.Include(t => t.Role).Include(t => t.Function));

            string token = string.Empty;

            //if(functions.Count > 0)
            //{
            var signingKey = Encoding.ASCII.GetBytes(appSettings.SigningSecret);
            var expiryDuration = int.Parse(appSettings.ExpiryDuration);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim("UserName", user.UserName));
            //claims.Add(new Claim("Name", user.UserId.ToString()));
            claims.Add(new Claim("UserID", user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Role, user.Role.RoleName));
            claims.Add(new Claim("FirstName", user.FirstName));
            claims.Add(new Claim("LastName", user.LastName));
            foreach (var function in functions)
            {
                claims.Add(new Claim(ClaimEnum.FUNCTION.ToString(), function.Function.FunctionCode));
            }

            //System.Security.Principal.GenericIdentity objidentity = new System.Security.Principal.GenericIdentity(user.UserId.ToString());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(expiryDuration),
                Subject = new ClaimsIdentity(new System.Security.Principal.GenericIdentity(user.UserId.ToString()), claims),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)

            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            token = jwtTokenHandler.WriteToken(jwtToken);
            model.Authenticated = true;
            model.Token = token;
            return model;
            //}

            //model.Authenticated = false;
            //model.Message = ErrorMessages.NoFunctionisSpecified;
            //return model;
        }
    }
}
