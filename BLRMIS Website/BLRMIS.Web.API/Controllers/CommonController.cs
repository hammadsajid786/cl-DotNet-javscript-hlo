using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        ICommonService commonService;
        IVerificationTokenService verificationTokenService;
        IComplaintService complaintService;
        IEmailHelper emailHelper; 

        public CommonController(
            ICommonService commonService,
            IVerificationTokenService verificationTokenService,
            IComplaintService complaintService,
            IEmailHelper emailHelper,
            IOptions<AppSettings> appSettings
            )
        {

            this.emailHelper = emailHelper;
            this.commonService = commonService;
            this.verificationTokenService = verificationTokenService;
            this.complaintService = complaintService;
        }

        [HttpPost]
        [Route("/api/verification-token")]
        public ActionResult<VerificationTokenModel> CreateVerificationToken(VerificationTokenModel verificationTokenModel)
        {
            //TODO: Do not return token with response. 
            return Ok(verificationTokenService.CreateToken(verificationTokenModel));
        }

    }
}