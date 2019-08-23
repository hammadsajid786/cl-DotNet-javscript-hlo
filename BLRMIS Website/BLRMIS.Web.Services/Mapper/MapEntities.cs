using AutoMapper;
using BLRMIS.Web.DataAccess.Entities;
using BLRMIS.Web.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLRMIS.Web.Services.Mapper
{
    public class MapEntities : Profile
    {
        public MapEntities()
        {
            CreateMap<ComplaintModel, LrmisWebComplaint>();
            CreateMap<LrmisWebComplaint, ComplaintModel>();
            CreateMap<FAQModel, LrmisWebFaq>();
            CreateMap<LrmisWebFaq, FAQModel>();
            CreateMap<DownloadModel, LrmisWebDownload>();
            CreateMap<WebUserModel, LrmisWebUser>();
            CreateMap<LrmisWebUser, WebUserModel>();
            CreateMap<VerificationTokenModel, LrmisWebVerificationToken>();
            CreateMap<LrmisWebVerificationToken, VerificationTokenModel>();
            CreateMap<NewsModel, LrmisWebNews>();
            CreateMap<LrmisWebNews, NewsModel>();
            CreateMap<WebRoleModel, LrmisWebRole>();
            CreateMap<LrmisWebRole, WebRoleModel>();
            CreateMap<WebCategoryModel, LrmisWebCategory>();
            CreateMap<LrmisWebCategory, WebCategoryModel>();
            CreateMap<LrmisWebDownload, DownloadModel>();
            CreateMap<LrmisWebAttachment, AttachmentModel>();
            CreateMap<AttachmentModel, LrmisWebAttachment>();
            CreateMap<WebDepartmentModel, LrmisWebDepartment>();
            CreateMap<LrmisWebDepartment, WebDepartmentModel>();
            CreateMap<WebDesignationModel, LrmisWebDesignation>();
            CreateMap<LrmisWebDesignation, WebDesignationModel>();
            CreateMap<LrmisWebComplaintLog, ComplaintLogModel>();
            CreateMap<ComplaintLogModel, LrmisWebComplaintLog>();
            CreateMap<WebFunctionRoleMapping, LrmisWebFunctionRoleMapping>();
            CreateMap<LrmisWebAttachment, FileList>();
            CreateMap<LrmisWebComplaintStatus, WebComplaintStatus>();
            CreateMap<LrmisWebApiDownloadedData, WebApiDownloadedDataModel>();
        }
    }
}
