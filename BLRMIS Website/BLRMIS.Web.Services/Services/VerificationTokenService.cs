using BLRMIS.Web.Common;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.InterfaceRepositories;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using BLRMIS.Web.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BLRMIS.Web.Services.Services
{
    public class VerificationTokenService : BaseService, IVerificationTokenService
    {
        IRepository<LrmisWebVerificationToken> veriTokenRepository;
        IEmailHelper emailHelper; 
        public event TokenCreatedEventHanlder TokenCreated;

        public VerificationTokenService(IUnitOfWork unitOfWork, IEmailHelper emailHelper) : base(unitOfWork)
        {
            veriTokenRepository = unitOfWork.GetRepository<LrmisWebVerificationToken>();
            this.emailHelper = emailHelper;
            TokenCreated += OnTokenCreated_SendEmail;
        }

       

        public VerificationTokenModel CreateToken(VerificationTokenModel verificationTokenModel)
        {
            //-- Delete previous verification codes which are created less than 20 minutes
            DeleteVerificationCodeByPhoneAndEmail(verificationTokenModel.PhoneNumber,verificationTokenModel.EmailAddress);
            //-- End ----------------------------------------------------------
            var token = Utility.RandomNumber6Digit();
            var entity = EntityMapper.Mapper.Map<LrmisWebVerificationToken>(verificationTokenModel);
            entity.VerificationCode = token;
            veriTokenRepository.Insert(entity);
            veriTokenRepository.Save();
            var model = EntityMapper.Mapper.Map<VerificationTokenModel>(entity);
            OnTokenCreated(model);
            return model;
        }
        public string VerifyToken(string token, string phoneNumber, string emailAddress)
        {

            var hasToken = veriTokenRepository.GetByCondition(i =>
             i.VerificationCode == token
                && i.PhoneNumber == phoneNumber
                && i.EmailAddress == emailAddress
                && i.VerificationCode != null
            );
            if (hasToken == null) return TokenEnums.INVALID.ToString();
            var isTokenExpired = (hasToken.CreatedDate.Value.AddMinutes(20) < DateTime.Now);
            if (isTokenExpired) return TokenEnums.EXPIRED.ToString();
            return hasToken.VerificationCode;
        }
      
        public void DeleteToken(int tokenId)
        {
            veriTokenRepository.Delete(tokenId);
            veriTokenRepository.Save();
        }

        public VerificationTokenModel FindToken(string token)
        {

            var tokenModel = veriTokenRepository.GetByCondition(i => i.VerificationCode == token && i.VerificationCode != null);
            if (tokenModel != null) return EntityMapper.Mapper.Map<VerificationTokenModel>(tokenModel);
            return null;
        }

        private void OnTokenCreated(object sender)
        {
            if(TokenCreated != null)
            {
                TokenCreated(sender, EventArgs.Empty);
            }
        }

        public void DeleteVerificationCodeByPhoneAndEmail(string phoneNumber, string emailAddress)
        {
            var hasToken = veriTokenRepository.GetAll(i =>
                 i.PhoneNumber == phoneNumber
              && i.EmailAddress == emailAddress
              && i.VerificationCode != null
              && i.CreatedDate.Value > DateTime.Now.AddMinutes(-20)
             );

            if(hasToken != null)
            {
                
                 foreach (LrmisWebVerificationToken verTokens in hasToken)
                 {
                   // var objVerificiationToken = veriTokenRepository.GetById(verTokens.VerificationId);
                    veriTokenRepository.Delete(verTokens.VerificationId);
                    veriTokenRepository.Save();
                 }
            }
        }


        private void OnTokenCreated_SendEmail(object sender, EventArgs args)
        {
            try
            {
                // if model has complaint code, then fetch and update email and phone againts complaint code. 
                var model = (VerificationTokenModel)sender;
                var placeHolders = new Dictionary<string, string>();
                foreach (PropertyInfo p in typeof(VerificationTokenModel).GetProperties())
                {
                    placeHolders.Add("{" + p.Name + "}", p.GetValue(model, null)?.ToString());
                }
                var subject = Common.EmailMessages.EMAIL_SUBJECT_VERIFICATION_CODE + " " + model.VerificationCode;
                emailHelper.SendEmail(placeHolders, EmailTemplates.VerificationCodeTemplate.ToString(), model.EmailAddress, subject);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}
