using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public delegate void TokenCreatedEventHanlder(object sender, EventArgs ars);

    public interface IVerificationTokenService
    {
        VerificationTokenModel CreateToken(VerificationTokenModel verificationTokenModel);
        string VerifyToken(string token, string phoneNumber, string emailAddress);
        void DeleteToken(int id);
        void DeleteVerificationCodeByPhoneAndEmail(string phoneNumber, string emailAddress);
        VerificationTokenModel FindToken(string token);
        event TokenCreatedEventHanlder TokenCreated;

    }
}

