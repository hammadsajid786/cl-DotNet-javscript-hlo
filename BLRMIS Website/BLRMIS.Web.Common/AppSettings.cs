

namespace BLRMIS.Web.Common
{
    public class AppSettings
    {
        public string EmailUserName { get; set; }
        public string EmailPassword { get; set; }
        public string EmailHost { get; set; }
        public string EmailEnableSsl { get; set; }
        public string EmailPort { get; set; }
        public string ConnectionString { get; set; }
        public string EmailTemplatePath { get; set; }
        public string UploadsPath { get; set; }
        public string UploadPathIIS { get; set; }
        public string AllowedMIMETypes { get; set; }
        public string AllowedFileExtentions { get; set; }
        public string PasswordSecret { get; set; }
        public string SigningSecret { get; set; }
        public string ExpiryDuration { get; set; }
        public int PublicUserId { get; set; }
        public string EStampingUrl { get; set; }
        public string AuthenticationHeaderName { get; set; }
        public string AuthenticationHeaderValue { get; set; }
        public string APIBaseUrl { get; set; }
        public string LoginPath { get; set; }
        public string AccessDeniedPath { get; set; }
        public int AuthCookieExpiryInHours { get; set; }
        public string RootUrl { get; set; }
        public string EStampId { get; set; }
        public string RegistryOfDeedsId { get; set; }
        public string MarkazSahuliyatId { get; set; }
        public string GrievanceId { get; set; }
        public string EStampPageUrl { get; set; }
        public string RegistryOfDeedsPageUrl { get; set; }
        public string MarkazSahuliyatPageUrl { get; set; }
        public string GrievancePageUrl { get; set; }
        public string EStampIcon { get; set; }
        public string RegistryOfDeedsIcon { get; set; }
        public string MarkazSahuliyatIcon { get; set; }
        public string GrievanceIcon { get; set; }
    }
}
