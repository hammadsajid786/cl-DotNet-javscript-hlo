using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Domain.InterfaceServices
{
    public delegate void ComplaintSubmittedEventHanlder(object sender, EventArgs args);
    public delegate void ComplaintVerifiedEventHanlder(object sender, EventArgs args);
    public delegate void ComplaintResolvedEventHandler(object sender, EventArgs args);
    
    public interface IComplaintService
    {
        ComplaintModel CreateComplaint(ComplaintModel complaintModel);
        IEnumerable<ComplaintModel> GetAllComplaints();
        ListResponseModel<ComplaintModel> GetAllComplaints(string searchParams, UserRolesEnum userRoleName);
        ListResponseModel<ComplaintModel> GetAllComplaints(string searchParams, UserRolesEnum userRoleName, int LoggedInUserId);
       
        ListResponseModel<ComplaintModel> GetAllComplaintsByPhoneNumber(string phoneNumber, string searchParams);
        ListResponseModel<ComplaintModel> GetAllComplaintsByCNIC(string cnic, string searchParams);

        ComplaintModel GetComplaint(string complaintNo);
        ComplaintModel GetComplaintByPhoneNumber(string phoneNumber);
        ComplaintModel GetComplaintByCNIC(string cnic);
        ComplaintModel GetComplaintByAccessToken(string accessToken);

        ComplaintModel GetComplaintByAccessToken(string accessToken, UserRolesEnum userRoleName, int loggedInUserId);
        ComplaintModel GetPublicComplaintByAccessToken(string accessToken);
        IEnumerable<ComplaintLogModel> GetComplaintLogs(int complaintId);
        void ReleaseComplaintLock(int complaintId, ComplaintLogModel complaintLogModel, int loggedInUserId);
        string GetComplaintAccessTokenByVerificationCode(string verificationCode, string complaintCode);
        
        
        void AddComplaintLog(int complaintId, ComplaintLogModel complaintLogModel);
        int AddComplaintLog(int complaintId, ComplaintLogModel complaintLogModel, int userID);

        IEnumerable<SelectListModel> GetALLComplaintStatus();
        event ComplaintSubmittedEventHanlder ComplaintSubmitted;
        event ComplaintVerifiedEventHanlder ComplaintVerified;

        List<SelectListModel> GetComplaintStatusList();

    }
}
