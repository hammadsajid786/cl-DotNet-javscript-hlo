using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Domain.Models;
using BLRMIS.Web.Repositories;
using System;
using System.Collections.Generic;
using BLRMIS.Web.DataAccess.Entities;
using AutoMapper;
using BLRMIS.Web.Services.Mapper;
using BLRMIS.Web.Domain.InterfaceRepositories;
using System.Linq;
using BLRMIS.Web.Common;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.Extensions.Options;
namespace BLRMIS.Web.Services.Services
{


    public class ComplaintService : BaseService, IComplaintService
    {


        private IComplaintRepository complaintRepository;
        private IRepository<LrmisWebComplaintLog> complaintLogRepository;
        private IRepository<LrmisWebAttachment> attachmentRepository;
        private IRepository<LrmisWebComplaintStatus> complaintStatusRepository;
        private ICommonService commonService;
        private IFileHelper fileHelper;
        private IEmailHelper emailHelper;
        private IAttachmentService attachmentService;
        private IVerificationTokenService verificationService;
        public event ComplaintSubmittedEventHanlder ComplaintSubmitted;
        public event ComplaintVerifiedEventHanlder ComplaintVerified;
        public event ComplaintResolvedEventHandler ComplaintResolved;
        public bool enabledSendEmailVerification = true; // READ FROM CONFIG
        public bool enabledSendSMSVerification = true; // READ FROM CONFIG
        private readonly AppSettings appSettings;
        public ComplaintService(
            IUnitOfWork unitOfWork,
            ICommonService commonService,
            IFileHelper fileHelper,
            IAttachmentService attachmentService,
            IEmailHelper emailHelper,
            IVerificationTokenService verificationService,
            IOptions<AppSettings> AppSettings
            ) : base(unitOfWork)
        {
            this.commonService = commonService;
            complaintRepository = unitOfWork.GetInstance<ComplaintRepository>();
            complaintLogRepository = unitOfWork.GetRepository<LrmisWebComplaintLog>();
            attachmentRepository = unitOfWork.GetRepository<LrmisWebAttachment>();
            complaintStatusRepository = unitOfWork.GetRepository<LrmisWebComplaintStatus>();
            appSettings = AppSettings.Value;

            this.fileHelper = fileHelper;
            this.emailHelper = emailHelper;
            this.attachmentService = attachmentService;
            this.verificationService = verificationService;
            //  ComplaintSubmitted += OnComplaintSubmitted_AddAttachments;
            if (enabledSendEmailVerification)
            {
                ComplaintSubmitted += OnComplaintSubmitted_SendEmail;
                ComplaintResolved += OnComplaintResolved_SendEmail;
            }
            if (enabledSendSMSVerification)
            {
                ComplaintSubmitted += OnComplaintSubmitted_SendSMS;
            }


        }
        public ComplaintModel CreateComplaint(ComplaintModel model)
        {
            var entity = EntityMapper.Mapper.Map<LrmisWebComplaint>(model);
            entity.ComplaintStatusId = (int)ComplaintStatusEnum.OPEN;
            entity.ComplaintCode = GetComplaintCode();//DateTime.Now.ToString("ffssmmhhddMMyy");
            entity.FunctionId = (int)UserRoleFunctionEnum.REVIEWER;
            entity.ModifiedDate = DateTime.Now;
            complaintRepository.Insert(entity);
            complaintRepository.Save();
            var mappedEntity = EntityMapper.Mapper.Map<ComplaintModel>(entity);
            if (model != null && model.Files != null && model.Files.Count > 0)
                attachmentService.AddAttachments(model.Files, entity.ComplaintId, (int)SourceTypeEnums.COMPLAINT);
            OnComplaintSubmitted(mappedEntity);
            return mappedEntity;
        }

        private void OnComplaintSubmitted(object source)
        {
            if (ComplaintSubmitted != null) ComplaintSubmitted(source, EventArgs.Empty);
        }

        public IEnumerable<ComplaintModel> GetAllComplaints()
        {
            var listComplaints = complaintRepository.GetAll();
            foreach (var item in listComplaints)
            {
                yield return EntityMapper.Mapper.Map<ComplaintModel>(item);
            }
        }


        private string GetComplaintCode()
        {
            string complaintCode = string.Empty;
            string todayDate = DateTime.Now.ToString("Mddyyyy");
            Int64 seedValue = 1;            
            var complaintEntity = complaintRepository.GetAll().OrderByDescending(i => i.CreatedDate).Any() ? complaintRepository.GetAll().OrderByDescending(i => i.CreatedDate).Where(i => i.CreatedDate.Date == DateTime.Now.Date).FirstOrDefault() : null;
            if(complaintEntity != null)
            {
                string lastComplaintCode = complaintEntity.ComplaintCode;
                string[] seed = lastComplaintCode.Split('-');
                if(seed != null && seed.Length > 1 && seed[1] != null)
                {
                    seedValue = Convert.ToInt64(seed[1]);
                    seedValue++;
                }
            }
            
            complaintCode = todayDate + "-" + seedValue.ToString().PadLeft(3, '0');
            


            return complaintCode;
        }

        public ListResponseModel<ComplaintModel> GetAllComplaints(string searchParams, UserRolesEnum userRoleName)
        {
            //int LoggedInUserId = 4;
            Expression<Func<LrmisWebComplaint, bool>> predicate = null;

            Func<IQueryable<LrmisWebComplaint>, IOrderedQueryable<LrmisWebComplaint>> sortexp = null;
            int index = 0;
            int size = 0;
            IQueryableExtensions.GetFilters<LrmisWebComplaint>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            Expression<Func<LrmisWebComplaint, bool>> ComplaintListAccess = null;
            if (userRoleName == UserRolesEnum.REIVEWER) ComplaintListAccess = fo => fo.FunctionId == (int)UserRoleFunctionEnum.REVIEWER;
            if (userRoleName == UserRolesEnum.PUBLIC_USER) ComplaintListAccess = fo => fo.FunctionId == (int)UserRoleFunctionEnum.PUBLIC_USER;
            if (userRoleName == UserRolesEnum.ADMIN) ComplaintListAccess = fo => true;


            var result =
               complaintRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size,
               include: source => source.Where(ComplaintListAccess)
               .Include(t => t.ComplaintCategory)
               .Include(t => t.ComplaintStatus)
               .Include(t => t.Location)
               .Include(t => t.LockedByNavigation)
               .Include(t => t.Function)
               .Include(t => t.ComplaintAssignToNavigation));
            List<ComplaintModel> modelList = EntityMapper.Mapper.Map<List<ComplaintModel>>(result.Items);
            ListResponseModel<ComplaintModel> listResponseModel = new ListResponseModel<ComplaintModel>();
            listResponseModel.Records = modelList;
            //listResponseModel.TotalRecords = result.Count;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;
            

        }


        public ListResponseModel<ComplaintModel> GetAllComplaints(string searchParams, UserRolesEnum userRoleName, int LoggedInUserId)
        {
            // int LoggedInUserId = 4;          
            Expression<Func<LrmisWebComplaint, bool>> predicate = null;

            Func<IQueryable<LrmisWebComplaint>, IOrderedQueryable<LrmisWebComplaint>> sortexp = null;
            int index = 0;
            int size = 0;
            IQueryableExtensions.GetFilters<LrmisWebComplaint>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            Expression<Func<LrmisWebComplaint, bool>> ComplaintListAccess = null;
            if (userRoleName == UserRolesEnum.REIVEWER) ComplaintListAccess = fo => (fo.FunctionId == (int)UserRoleFunctionEnum.REVIEWER || fo.ComplaintAssignTo == LoggedInUserId);
            if (userRoleName == UserRolesEnum.PUBLIC_USER) ComplaintListAccess = fo => fo.FunctionId == (int)UserRoleFunctionEnum.PUBLIC_USER;
            if (userRoleName == UserRolesEnum.RESOLVER) ComplaintListAccess = fo => fo.ComplaintAssignTo == LoggedInUserId;
            if (userRoleName == UserRolesEnum.SUPER_REVIEWER) ComplaintListAccess = fo => true; //(fo.ComplaintAssignTo == LoggedInUserId || fo.FunctionId == (int)UserRoleFunctionEnum.SUPER_REVIEWER) ;
            if (userRoleName == UserRolesEnum.ADMIN) ComplaintListAccess = fo => true;


            var result =
               complaintRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size,
               include: source => source.Where(ComplaintListAccess)
               .Include(t => t.ComplaintCategory)
               .Include(t => t.ComplaintStatus)
               .Include(t => t.Location)
               .Include(t => t.LockedByNavigation)
               .Include(t=> t.Function)
               .Include(t=> t.ComplaintAssignToNavigation)
               );
            List<ComplaintModel> modelList = EntityMapper.Mapper.Map<List<ComplaintModel>>(result.Items);
            ListResponseModel<ComplaintModel> listResponseModel = new ListResponseModel<ComplaintModel>();
            listResponseModel.Records = modelList;
            //listResponseModel.TotalRecords = result.Count;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;
            return listResponseModel;


        }

        public ComplaintModel GetComplaint(string complaintNo)
        {
            var entity = complaintRepository.GetByCondition(i => i.ComplaintCode == complaintNo);
            return EntityMapper.Mapper.Map<ComplaintModel>(entity);
        }
        public IEnumerable<SelectListModel> GetALLComplaintStatus()
        {
            return CommonMapper.MapComplaintStatusSelectList(complaintRepository.GetAllComplaintStatus().ToList()).OrderBy(i => i.Text);
        }
        public string GetComplaintAccessTokenByVerificationCode(string verificationCode, string complaintCode)
        {
            var token = verificationService.FindToken(verificationCode);
            if (token == null) throw new Exception("Invalid verification code.");
            var complaint = complaintRepository.GetByCondition(
                i => i.CitizenEmailAddress == token.EmailAddress
                && i.CitizenPhoneNumber == token.PhoneNumber
                && i.ComplaintCode == complaintCode
                );
            if (complaint == null) throw new Exception("Complaint not found.");
            return complaint.ComplaintAccessToken;

        }
        private void OnComplaintSubmitted_AddAttachments(object source, EventArgs args)
        {
            if (source != null)
            {
                var model = (ComplaintModel)source;
                if (model != null && model.Files != null && model.Files.Count > 0)
                    attachmentService.AddAttachments(model.Files, model.ComplaintId, (int)SourceTypeEnums.COMPLAINT);
            }
            else
            {
                // LOG HERE.
            }
        }
        private void OnComplaintSubmitted_SendEmail(object sender, EventArgs ars)
        {
            var model = (ComplaintModel)sender;
            var placeHolders = new Dictionary<string, string>();
            foreach (PropertyInfo p in typeof(ComplaintModel).GetProperties())
            {
                placeHolders.Add("{" + p.Name + "}", p.GetValue(model, null)?.ToString());
            }
            var subject = Common.EmailMessages.EMAIL_SUBJECT_COMPLAINT_SUBMITTED + " " + model.ComplaintCode;
            emailHelper.SendEmail(placeHolders, EmailTemplates.ComplaintSubmitNotificationTemplate.ToString(), model.CitizenEmailAddress, subject);
        }

        private void OnComplaintSubmitted_SendSMS(object sender, EventArgs ars)
        {
        }
        public ComplaintModel GetComplaintByAccessToken(string accessToken)
        {
            var entity = complaintRepository.Single(
                i => i.ComplaintAccessToken == accessToken
                && i.ComplaintAccessToken != null,
                include: source => source
                .Include(t => t.ComplaintCategory)
                .Include(t => t.ComplaintStatus)
                .Include(t => t.Location)
                .Include(t => t.LockedByNavigation)

                );

            if (entity == null) throw new Exception(String.Format("Complaint not found against this code {0}", accessToken));
            if (entity.ComplaintStatusId == (int)ComplaintStatusEnum.RESOLVED && entity.ComplaintAssignTo != appSettings.PublicUserId /*Constants.PUBLIC_USER_ID*/) throw new Exception(String.Format("This complaint has been resolved and assigned to the complainant."));
            //if ((entity.IsLocked != null && entity.IsLocked == true) && (entity.LockedBy != null && entity.LockedBy > 0)) throw new Exception(String.Format("This complaint is locked by another user."));

            //entity.IsLocked = true;
            //entity.LockedBy = 4;
            //entity.ModifiedBy = 4;
            //entity.ModifiedDate = DateTime.Now;
            //complaintRepository.Update(entity);
            //complaintRepository.Save();


            return EntityMapper.Mapper.Map<ComplaintModel>(entity);
        }


        public ComplaintModel GetComplaintByAccessToken(string accessToken, UserRolesEnum userRoleName, int loggedInUserId)
        {
            var entity = complaintRepository.Single(
                i => i.ComplaintAccessToken == accessToken
                && i.ComplaintAccessToken != null,
                include: source => source
                .Include(t => t.ComplaintCategory)
                .Include(t => t.ComplaintStatus)
                .Include(t => t.Location)
                .Include(t => t.LockedByNavigation)
                .Include(t=> t.Function)
                .Include(t=> t.ComplaintAssignToNavigation)

                );

            if (entity == null) throw new Exception(String.Format("Complaint not found against this code {0}", accessToken));
            if (entity.ComplaintStatusId == (int)ComplaintStatusEnum.RESOLVED && entity.ComplaintAssignTo != appSettings.PublicUserId /*Constants.PUBLIC_USER_ID*/) throw new Exception(String.Format("This complaint has been resolved and assigned to the complainant."));
            /* Locked complaints */
            if ((userRoleName != UserRolesEnum.SUPER_REVIEWER) && (entity.IsLocked != null && entity.IsLocked == true) && (entity.LockedBy != null && entity.LockedBy > 0 && entity.LockedBy != loggedInUserId)) throw new Exception(String.Format("This complaint is locked by another user."));

           var  updateentity = complaintRepository.GetById(entity.ComplaintId);
            updateentity.IsLocked = true;
            updateentity.LockedBy = loggedInUserId;
            updateentity.ModifiedBy = loggedInUserId;
            updateentity.ModifiedDate = DateTime.Now;
            complaintRepository.Update(updateentity);
            complaintRepository.Save();
            
            /* End */

            


            return EntityMapper.Mapper.Map<ComplaintModel>(entity);
        }
        public ComplaintModel GetPublicComplaintByAccessToken(string accessToken)
        {
            var entity = complaintRepository.Single(
                i => i.ComplaintAccessToken == accessToken
                && i.ComplaintAccessToken != null,
                include: source => source
                .Include(t => t.ComplaintCategory)
                .Include(t => t.ComplaintStatus)
                .Include(t => t.Location)
                .Include(t => t.LockedByNavigation)
                .Include(t=> t.Function)
                .Include(t=> t.ComplaintAssignToNavigation)
               
                );
            if (entity == null) throw new Exception(String.Format("Complaint not found against this code {0}", accessToken));
            //if (entity.ComplaintStatusId == (int)ComplaintStatusEnum.CLOSED) throw new Exception(String.Format("This complaint has been resolved and closed."));
            //if (entity.ComplaintStatusId == (int)ComplaintStatusEnum.REOPEN) throw new Exception(String.Format("This complaint is assigned to the Reviewer."));
            //if(entity.ComplaintStatusId  != (int)ComplaintStatusEnum.RESOLVED) throw new Exception(String.Format("This complaint is not resolved yet, and in progress by Reviewer."));
            return EntityMapper.Mapper.Map<ComplaintModel>(entity);
        }

        public IEnumerable<ComplaintLogModel> GetComplaintLogs(int complaintId)
        {
            var complaintLogs = complaintRepository.GetComplaintLogs(complaintId);
            var complaintLogIds = complaintLogs.Select(x => x.ComplaintCommentId);
            var complaintAttachments = attachmentRepository.GetAll(x =>
            x.SourceType == (int)SourceTypeEnums.COMPLAINT_COMMENT && complaintLogIds.Contains(x.SourceId));

            List<ComplaintLogModel> listComplaintLogModel = new List<ComplaintLogModel>();
            foreach (var item in complaintLogs)
            {
                listComplaintLogModel.Add(EntityMapper.Mapper.Map<ComplaintLogModel>(item));
                listComplaintLogModel[listComplaintLogModel.Count - 1].FileList = EntityMapper.Mapper.
                    Map<List<FileList>>(complaintAttachments.Where(x => x.SourceId == item.ComplaintCommentId));
            }
            return listComplaintLogModel;
        }
        public void AddComplaintLog(int complaintId, ComplaintLogModel complaintLogModel)
        {
            int loggedInUserId = 4;
            int complaintStatusId = complaintLogModel.ComplaintStatusId;
            int? functionId = null;


            int ModifiedByUserId = (Convert.ToInt32(complaintLogModel.ComplaintAssignBy) != 0) ? Convert.ToInt32(complaintLogModel.ComplaintAssignBy) : loggedInUserId; // TODO: fetch it from login user.
            var complaintEntity = complaintRepository.GetById(complaintId);
            if (complaintEntity == null) throw new Exception(String.Format("Unable to find complaint againts this complaint Id {0}", complaintId));

            // condition will be applied on public user opinion selection.

            if (complaintLogModel.UserOpinion == (int)PublicUserOpinionEnum.SATISFIED)
            {
                complaintStatusId = (int)ComplaintStatusEnum.CLOSED;
            }
            else if (complaintLogModel.UserOpinion == (int)PublicUserOpinionEnum.NOT_SATISFIED)
            {
                complaintStatusId = (int)ComplaintStatusEnum.REOPEN;
                var complaintLogs = complaintLogRepository.GetAll(x => x.ComplaintId == complaintId && x.ComplaintStatusId == (int)ComplaintStatusEnum.REOPEN);
                if (complaintLogs.Count < 2)
                    functionId = (int)UserRolesEnum.REIVEWER;
                else
                    functionId = (int)UserRolesEnum.SUPER_REVIEWER;
            }
            complaintLogModel.ComplaintStatusId = complaintStatusId;

            // if complaint status is resolved then complaint will be send to the complainiant,
            // otherwise it will be assigned to the selected user by reviewer or resolver. 
            int? complaintAssignTo = (complaintStatusId == (int)ComplaintStatusEnum.RESOLVED) ? appSettings.PublicUserId /*Constants.PUBLIC_USER_ID*/ :
                functionId == (int)UserRolesEnum.SUPER_REVIEWER ? null : complaintLogModel.ComplaintAssignTo;

            complaintLogModel.ComplaintId = complaintId;
            complaintLogModel.ModifiedDate = DateTime.Now;
            complaintLogModel.CreatedBy = ModifiedByUserId;
            complaintLogModel.ComplaintAssignBy = ModifiedByUserId;
            complaintLogModel.ComplaintAssignTo = complaintAssignTo;

            if (complaintLogModel.UserOpinion == (int)PublicUserOpinionEnum.NOT_SATISFIED ||
                complaintLogModel.UserOpinion == (int)PublicUserOpinionEnum.SATISFIED)
                complaintLogModel.ComplaintAssignTo = appSettings.PublicUserId;//Constants.PUBLIC_USER_ID;


            var entity = EntityMapper.Mapper.Map<LrmisWebComplaintLog>(complaintLogModel);

            // adding complaint logs
            complaintRepository.AddComplaintLog(entity);
            // updating complaint status. 
            complaintEntity.ModifiedDate = DateTime.Now;
            complaintEntity.ModifiedBy = ModifiedByUserId;
            complaintEntity.ComplaintStatusId = complaintStatusId;
            complaintEntity.FunctionId = functionId;
            complaintEntity.ComplaintAssignTo = complaintAssignTo;
            complaintRepository.Update(complaintEntity);
            complaintRepository.Save();
            var mappedEntity = EntityMapper.Mapper.Map<ComplaintModel>(complaintEntity);
            if (complaintStatusId == (int)ComplaintStatusEnum.RESOLVED)
            {
                OnComplaintResolved(mappedEntity);
            }

            if (complaintLogModel != null && complaintLogModel.Files != null && complaintLogModel.Files.Count > 0)
                attachmentService.AddAttachments(complaintLogModel.Files, entity.ComplaintCommentId, (int)SourceTypeEnums.COMPLAINT_COMMENT);
        }

        public int AddComplaintLog(int complaintId, ComplaintLogModel complaintLogModel, int userID)
        {
            int loggedInUserId = userID;
            int complaintStatusId = complaintLogModel.ComplaintStatusId;
            int? functionId = null;


            int ModifiedByUserId = (Convert.ToInt32(complaintLogModel.ComplaintAssignBy) != 0) ? Convert.ToInt32(complaintLogModel.ComplaintAssignBy) : loggedInUserId; // TODO: fetch it from login user.
            var complaintEntity = complaintRepository.GetById(complaintId);
            if (complaintEntity == null) throw new Exception(String.Format("Unable to find complaint againts this complaint Id {0}", complaintId));
            // condition will be applied on public user opinion selection.

            //** Cannot update complaint if super reviewer taken lock **//
            if ((complaintEntity.IsLocked != null && complaintEntity.IsLocked == true) && (complaintEntity.LockedBy != null && complaintEntity.LockedBy > 0 && complaintEntity.LockedBy != loggedInUserId))
                return 0;//  String.Format("Cannot update complaint. This complaint is locked by super reviewer.")
                //throw new Exception(String.Format("Cannot update complaint. This complaint is locked by super reviewer."));
                //** End **//

            if (complaintLogModel.UserOpinion == (int)PublicUserOpinionEnum.SATISFIED)
            {
                complaintStatusId = (int)ComplaintStatusEnum.CLOSED;
            }
            else if (complaintLogModel.UserOpinion == (int)PublicUserOpinionEnum.NOT_SATISFIED)
            {

                complaintStatusId = (int)ComplaintStatusEnum.REOPEN;
               // functionId = (int)UserRolesEnum.REIVEWER;
            }
            complaintLogModel.ComplaintStatusId = complaintStatusId;

            // if complaint status is resolved then complaint will be send to the complainiant,
            // otherwise it will be assigned to the selected user by reviewer or resolver. 
            int? complaintAssignTo = (complaintStatusId == (int)ComplaintStatusEnum.RESOLVED) ? appSettings.PublicUserId /*Constants.PUBLIC_USER_ID*/ : complaintLogModel.ComplaintAssignTo;


            //if (complaintStatusId == (int)ComplaintStatusEnum.RESOLVED || complaintStatusId == (int)ComplaintStatusEnum.REOPEN) { complaintAssignTo = complaintEntity.ComplaintAssignTo; } else complaintAssignTo = complaintLogModel.ComplaintAssignTo;
          


            complaintLogModel.ComplaintId = complaintId;
            complaintLogModel.ModifiedDate = DateTime.Now;
            complaintLogModel.CreatedBy = ModifiedByUserId;
            complaintLogModel.ComplaintAssignBy = ModifiedByUserId;
            if(complaintAssignTo > 0)
              complaintLogModel.ComplaintAssignTo = complaintAssignTo;
            var entity = EntityMapper.Mapper.Map<LrmisWebComplaintLog>(complaintLogModel);

            // adding complaint logs
            complaintRepository.AddComplaintLog(entity);
            // updating complaint status. 
            complaintEntity.ModifiedDate = DateTime.Now;
            complaintEntity.ModifiedBy = ModifiedByUserId;
            complaintEntity.ComplaintStatusId = complaintStatusId;
            complaintEntity.FunctionId = functionId;
            complaintEntity.ComplaintAssignTo = complaintAssignTo;
            /* release lock */
            complaintEntity.IsLocked = null;
            complaintEntity.LockedBy = null;
            /* End */
            complaintRepository.Update(complaintEntity);
            complaintRepository.Save();
            var mappedEntity = EntityMapper.Mapper.Map<ComplaintModel>(complaintEntity);
            if (complaintStatusId == (int)ComplaintStatusEnum.RESOLVED)
            {
                OnComplaintResolved(mappedEntity);
            }

            if (complaintLogModel != null && complaintLogModel.Files != null && complaintLogModel.Files.Count > 0)
                attachmentService.AddAttachments(complaintLogModel.Files, entity.ComplaintCommentId, (int)SourceTypeEnums.COMPLAINT_COMMENT);

            return 1;
        }



        public void ReleaseComplaintLock(int complaintId, ComplaintLogModel complaintLogModel, int loggedInUserId)
        {
          

          //  int ModifiedByUserId = (complaintLogModel.ComplaintAssignBy != 0) ? complaintLogModel.ComplaintAssignBy : loggedInUserId; // TODO: fetch it from login user.
            var complaintEntity = complaintRepository.GetById(complaintId);
            if (complaintEntity == null) throw new Exception(String.Format("Unable to find complaint againts this complaint Id {0}", complaintId));
            // condition will be applied on public user opinion selection.

            
            // updating complaint status. 
            complaintEntity.ModifiedDate = DateTime.Now;
            complaintEntity.ModifiedBy = loggedInUserId ;       
            /* release lock */
            complaintEntity.IsLocked = null;
            complaintEntity.LockedBy = null;
            /* End */
            complaintRepository.Update(complaintEntity);
            complaintRepository.Save();
         
        }


        public ComplaintModel GetComplaintByPhoneNumber(string phoneNumber)
        {
            var entity = complaintRepository.GetByCondition(i =>
            i.CitizenPhoneNumber == phoneNumber
            || i.CitizenPhoneNumber == phoneNumber.Replace("-", "")
            || i.CitizenPhoneNumber == phoneNumber.Replace(" ", ""));
            return EntityMapper.Mapper.Map<ComplaintModel>(entity);
        }

        public ComplaintModel GetComplaintByCNIC(string cnic)
        {
            var entity = complaintRepository.GetByCondition(i => i.CitizenCnic == cnic || i.CitizenCnic == cnic.Replace("-", ""));
            return EntityMapper.Mapper.Map<ComplaintModel>(entity);
        }

        public List<SelectListModel> GetComplaintStatusList()
        {
            return CommonMapper.MapComplaintStatusList(complaintStatusRepository.GetAll());
        }

        public ListResponseModel<ComplaintModel> GetAllComplaintsByPhoneNumber(string phoneNumber, string searchParams)
        {
            //var lst = complaintRepository.GetAllComplaints(
            //         i => i.CitizenPhoneNumber == phoneNumber
            //    || i.CitizenPhoneNumber == phoneNumber.Replace("-", "")
            //    || i.CitizenPhoneNumber == phoneNumber.Replace(" ", ""));
            //foreach (var item in lst)
            //{
            //    yield return EntityMapper.Mapper.Map<ComplaintModel>(item);
            //}

            Expression<Func<LrmisWebComplaint, bool>> predicate = null;
            Func<IQueryable<LrmisWebComplaint>, IOrderedQueryable<LrmisWebComplaint>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebComplaint>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = complaintRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size, 
                include: source => source.Include(x => x.ComplaintStatus).Include(x => x.ComplaintCategory));

            List<ComplaintModel> liComplaintModel = EntityMapper.Mapper.Map<List<ComplaintModel>>(result.Items);

            ListResponseModel<ComplaintModel> listResponseModel = new ListResponseModel<ComplaintModel>();
            listResponseModel.Records = liComplaintModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public ListResponseModel<ComplaintModel> GetAllComplaintsByCNIC(string cnic, string searchParams)
        {
            //var lst = complaintRepository.GetAllComplaints(
            //          i => i.CitizenCnic == cnic
            //     || i.CitizenCnic == cnic.Replace("-", "")
            //     || i.CitizenCnic == cnic.Replace(" ", ""));
            //foreach (var item in lst)
            //{
            //    yield return EntityMapper.Mapper.Map<ComplaintModel>(item);
            //}

            Expression<Func<LrmisWebComplaint, bool>> predicate = null;
            Func<IQueryable<LrmisWebComplaint>, IOrderedQueryable<LrmisWebComplaint>> sortexp = null;
            int index = 0;
            int size = 0;

            IQueryableExtensions.GetFilters<LrmisWebComplaint>(searchParams, ref predicate, ref sortexp, ref index, ref size);

            var result = complaintRepository.GetList(predicate: predicate, orderBy: sortexp, index: index, size: size,
                include: source => source.Include(x => x.ComplaintStatus).Include(x => x.ComplaintCategory));

            List<ComplaintModel> liComplaintModel = EntityMapper.Mapper.Map<List<ComplaintModel>>(result.Items);

            ListResponseModel<ComplaintModel> listResponseModel = new ListResponseModel<ComplaintModel>();
            listResponseModel.Records = liComplaintModel;
            listResponseModel.TotalPages = result.Pages;
            listResponseModel.TotalRecords = result.Count;

            return listResponseModel;
        }

        public void OnComplaintResolved_SendEmail(object sender, EventArgs args)
        {
            var model = (ComplaintModel)sender;
            var placeHolders = new Dictionary<string, string>();
            foreach (PropertyInfo p in typeof(ComplaintModel).GetProperties())
                placeHolders.Add("{" + p.Name + "}", p.GetValue(model, null)?.ToString());
            var subject = Common.EmailMessages.EMAIL_SUBJECT_COMPLAINT_RESOLVED + " " + model.ComplaintCode;
            emailHelper.SendEmail(placeHolders, EmailTemplates.ComplaintResolvedNotificationTemplate.ToString(), model.CitizenEmailAddress, subject);
        }

        public void OnComplaintResolved(object data)
        {
            if (ComplaintResolved != null)
            {
                ComplaintResolved(data, EventArgs.Empty);
            }
        }


    }
}
