using BLRMIS.Web.Common;
using BLRMIS.Web.Domain.InterfaceServices;
using BLRMIS.Web.Services;
using BLRMIS.Web.Services.ExternalCommunication;
using BLRMIS.Web.Services.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BLRMIS.Web.API.Extenstions
{
    public static class RegisterDependancyExtensions
    {
        public static void RegisterDependancies(this IServiceCollection services)
        {
            services.AddScoped<IFileHelper, FileHelper>();
            services.AddScoped<IEmailHelper, EmailHelper>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IContentService, ContentService>();
            services.AddScoped<IComplaintService, ComplaintService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IDownloadService, DownloadService>();
            services.AddScoped<IVerificationTokenService, VerificationTokenService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IChangePasswordService, ChangePasswordService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IConfigService, ConfigService>();
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<ExternalCommunicationService, ExternalCommunicationService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IApiDownloadedDataService, ApiDownloadedDataService>();
        }
    }
}

