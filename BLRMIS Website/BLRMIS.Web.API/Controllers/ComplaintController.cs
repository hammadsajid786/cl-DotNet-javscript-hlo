using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BLRMIS.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ComplaintController : ControllerBase
    {
        IComplaintService complaintService;
        IAttachmentService attachmentService;
        IVerificationTokenService verificationTokenService;
        IEmailHelper emailHelper;
      
        public ComplaintController(
            IComplaintService ComplaintService,
            IVerificationTokenService verificationTokenService,
            IOptions<AppSettings> appSettings,
            IAttachmentService attachmentService
            
            )
        {
            complaintService = ComplaintService;
            this.verificationTokenService = verificationTokenService;
            this.attachmentService = attachmentService;
            this.emailHelper = new EmailHelper(appSettings);
        }
        [HttpGet]
        [Route("/api/reviewer/complaints")]
        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAllComplaintsForReviwer([FromQuery(Name = "SearchParams")]string SearchParams)
        {
            var UserId = User.Identity.Name;
            return Ok(complaintService.GetAllComplaints(SearchParams, UserRolesEnum.REIVEWER, Convert.ToInt32(UserId)));
        }


        [HttpGet]
        [Route("/api/resolver/complaints")]
        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAllComplaintsForResolver([FromQuery(Name = "SearchParams")]string SearchParams)
        {
            var UserId = User.Identity.Name;           
            
            return Ok(complaintService.GetAllComplaints(SearchParams, UserRolesEnum.RESOLVER,Convert.ToInt32(UserId)));
        }

        [HttpGet]
        [Route("/api/seniorreviewer/complaints")]
        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAllComplaintsForSeniorReviewer([FromQuery(Name = "SearchParams")]string SearchParams)
         {
            var UserId = User.Identity.Name;

            return Ok(complaintService.GetAllComplaints(SearchParams, UserRolesEnum.SUPER_REVIEWER, Convert.ToInt32(UserId)));
        }


        [HttpGet]
        [Route("/api/admin/complaints")]
        [HttpGet]
        public ActionResult<ListResponseModel<DownloadModel>> GetAllComplaintsForAdmin([FromQuery(Name = "SearchParams")]string SearchParams)
        {
            var UserId = User.Identity.Name;
            return Ok(complaintService.GetAllComplaints(SearchParams, UserRolesEnum.ADMIN));
        }
        [HttpGet]
        [Route("/api/complaint/{accessToken}")]
        public ActionResult<ComplaintModel> GetByAccessToken(string accessToken)
        {
            try
            {
                var complaint = complaintService.GetComplaintByAccessToken(accessToken);
                if (complaint == null) return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", string.Format("Complaint not found againts this code {0}", accessToken)));
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse("COMPLAINT_NOT_FOUND",ex.Message));
            }
          
        }

        [HttpGet]
        [Route("/api/complaint/seniorreviewer/{accessToken}")]
        public ActionResult<ComplaintModel> GetByAccessTokenForSeniorReviewer(string accessToken)
        {
            try
            {
                var UserId = User.Identity.Name;
                var complaint = complaintService.GetComplaintByAccessToken(accessToken,UserRolesEnum.SUPER_REVIEWER,Convert.ToInt32(UserId));
                if (complaint == null) return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", string.Format("Complaint not found againts this code {0}", accessToken)));
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", ex.Message));
            }

        }

        [HttpGet]
        [Route("/api/complaint/reviewer/{accessToken}")]
        public ActionResult<ComplaintModel> GetByAccessTokenForReviewer(string accessToken)
        {
            try
            {
                var UserId = User.Identity.Name;
                var complaint = complaintService.GetComplaintByAccessToken(accessToken, UserRolesEnum.REIVEWER, Convert.ToInt32(UserId));
                if (complaint == null) return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", string.Format("Complaint not found againts this code {0}", accessToken)));
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", ex.Message));
            }

        }

        [HttpGet]
        [Route("/api/complaint/resolver/{accessToken}")]
        public ActionResult<ComplaintModel> GetByAccessTokenForResolver(string accessToken)
        {
            try
            {
                var UserId = User.Identity.Name;
                var complaint = complaintService.GetComplaintByAccessToken(accessToken, UserRolesEnum.RESOLVER, Convert.ToInt32(UserId));
                if (complaint == null) return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", string.Format("Complaint not found againts this code {0}", accessToken)));
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", ex.Message));
            }

        }
        [HttpGet]
        [Route("/api/complaint/public/{accessToken}")]
        public ActionResult<ComplaintModel> GetPublicByAccessToken(string accessToken)
        {
            try
            {
                var complaint = complaintService.GetPublicComplaintByAccessToken(accessToken);
                if (complaint == null) return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", string.Format("Complaint not found againts this code {0}", accessToken)));
                return Ok(complaint);
            }
            catch (Exception ex)
            {
                return NotFound(new APIResponse("COMPLAINT_NOT_FOUND", ex.Message));
            }

        }
        [HttpGet]
        [Route("/api/complaints/status")]
        public ActionResult<SelectListModel> GetComplaintStatusList()
        {
            return Ok(complaintService.GetComplaintStatusList());
        }
        [HttpGet]
        [Route("/api/complaints/{type}/{number}/token/{token}")]
        public ActionResult<ListResponseModel<ComplaintModel>> GetComplaintsByPhoneOrCNIC(string type, string number, string token, [FromQuery(Name = "param")]string searchParams)
        {
            // var _token = verificationTokenService.FindToken(token);
            // if (_token == null) return BadRequest("INVALID_VERIFICATION_CODE");
            ListResponseModel<ComplaintModel> complaints = null;
            if (type.ToUpper() == "PH") complaints = complaintService.GetAllComplaintsByPhoneNumber(number, searchParams);
            if (type.ToUpper() == "CNIC") complaints = complaintService.GetAllComplaintsByCNIC(number, searchParams);
            if (complaints == null  ) return NotFound("COMPLAINT_NOT_FOUND");
            return Ok(complaints);
        }
        [HttpGet]
        [Route("/api/complaint/{complaintId}/logs")]
        public IActionResult GetComplaintLogs(int complaintId)
        {
            return Ok(complaintService.GetComplaintLogs(complaintId)); 
        }
        [HttpGet]
        [Route("/api/complaint-status")]
        public ActionResult<IEnumerable<SelectListModel>> GetAllComplaintStatus()
        {
            return Ok(complaintService.GetALLComplaintStatus());
        }
        [HttpGet]
        [Route("/api/complaint/{complaintCode}/verify/{verificationCode}")]
        public IActionResult GetComplaintAccessToken(string verificationCode, string complaintCode)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(verificationCode)) return BadRequest("NULL_VERIFICATION_CODE");
                var accessToken = complaintService.GetComplaintAccessTokenByVerificationCode(verificationCode, complaintCode);
                return Ok(accessToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

       

        [HttpGet]
        [Route("/api/complaint/search/{complaintNo}")]
        public IActionResult VerifyComplaintCode(string complaintNo)
        {
            if (String.IsNullOrWhiteSpace(complaintNo)) return BadRequest("NULL_COMPLAINT_NO");
            var complaint = complaintService.GetComplaint(complaintNo);
            if (complaint == null) return NotFound("NOT_FOUND_COMPLAINT");
            var token = verificationTokenService.CreateToken(new VerificationTokenModel
            {
                EmailAddress = complaint.CitizenEmailAddress,
                PhoneNumber = complaint.CitizenPhoneNumber
            });
            if (token == null) return BadRequest("NULL_VERIFICATION_CODE");
            return Ok(token);
        }
        [HttpGet]
        [Route("/api/complaint/search/{type}/{number}")]
        public IActionResult VerifyPhoneOrCNIC(string type,string number)
        {
            if (String.IsNullOrWhiteSpace(number)) return BadRequest("NULL_COMPLAINT_NO");
            ComplaintModel complaint = null;
            if(type.ToUpper() == "PH") complaint = complaintService.GetComplaintByPhoneNumber(number);
            if (type.ToUpper() == "CNIC") complaint = complaintService.GetComplaintByCNIC(number);
            if (complaint == null) return NotFound("NOT_FOUND_COMPLAINT");
            var token = verificationTokenService.CreateToken(new VerificationTokenModel
            {
                EmailAddress = complaint.CitizenEmailAddress,
                PhoneNumber = complaint.CitizenPhoneNumber
            });
            if (token == null) return BadRequest("NULL_VERIFICATION_CODE");
            return Ok(token);
        }

        [HttpPost]
        [Route("/api/complaint")]
        public ActionResult<ComplaintModel> Post([FromForm]ComplaintModel model)
        {
            if (ModelState.IsValid)
            {
                var hasToken = verificationTokenService.FindToken(model.VerificationCode);
                if (hasToken == null) return BadRequest("INVALID_VERIFICATION_CODE");
                var tokenStatus = verificationTokenService.VerifyToken(hasToken.VerificationCode, hasToken.PhoneNumber, hasToken.EmailAddress);
                verificationTokenService.DeleteToken(hasToken.VerificationId);
                if (tokenStatus == Common.TokenEnums.EXPIRED.ToString()) return Ok("EXPIRED_VERIFICATION_CODE");
                return Ok(complaintService.CreateComplaint(model));
            }
            return BadRequest(ModelState);
        }
        [HttpPut]
        [Route("/api/complaint/{complaintId}")]
        public ActionResult<ComplaintModel> Update(int complaintId, [FromForm]ComplaintLogModel complaintLogModel)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.Identity.Name;
                var status = complaintService.AddComplaintLog(complaintId, complaintLogModel,Convert.ToInt32(UserId));

                if(status == 1)
                 return Ok(new APIResponse("SUCCESS", String.Format("Complaint is successfully assigned to {0}", complaintLogModel.ComplaintAssignTo)));
                else
                    return Ok(new APIResponse("ERROR", String.Format("This complaint is locked by another user.")));
                // return Ok(new APIResponse("SUCCESS", String.Format("Complaint is successfully assigned to {0}", complaintLogModel.ComplaintAssignTo)));
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("/api/complaint/unlock/{complaintId}")]
        public ActionResult<ComplaintModel> ReleaseComplaintLock(int complaintId, [FromForm]ComplaintLogModel complaintLogModel)
        {
            if (ModelState.IsValid)
            {
                var UserId = User.Identity.Name;
                complaintService.ReleaseComplaintLock(complaintId, complaintLogModel, Convert.ToInt32(UserId));
                return Ok(new APIResponse("SUCCESS", String.Format("Complaint lock is successfully assigned to released.")));
            }
            return BadRequest(ModelState);
        }


    }
}