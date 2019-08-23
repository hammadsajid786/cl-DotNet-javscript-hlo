

namespace BLRMIS.Web.Common
{
    public class Constants
    {
        public const string VerificationCode = "VerificationCode";
        public const string ContentFilePath = "ContentManagement";
        public const int PUBLIC_USER_ID = 1018;
    }

    public class DateTimeConstants
    {
        public const string DatabaseDateFormat = "yyyy-MM-dd";
        public const string DatabaseDateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        public const string EStampingDateFormat = "dd-MM-yyyy";
    }

        public static partial class ErrorCategories
    {
        #region Public Fields

        public const string Authorization = "Authorization";
        public const string BadRequest = "BadRequest";
        public const string BusinessRule = "BusinessRule";
        public const string Identification = "Identification";
        public const string InternalServer = "InternalServer";
        public const string NotFound = "NotFound";
        public const string ServiceUnavailable = "ServiceUnavailable";
        public const string Validation = "Validation";
        public const string IncorrectLoginCredentials = "IncorrectLoginCredentials";
        public const string NoFuncationsAssigned = "NoFuncationsAssigned";
        #endregion Public Fields
    }

    public static partial class FieldNames
    {
        public const string Page = "Page";
    }

    public static partial class ErrorMessages
    {
        public const string PageRequired = "The field Page is required.";
        public const string UserNamePasswordIncorrect = "UserName or Password is incorrect.";
        public const string NoFunctionisSpecified = "Functions are not defined for this user";
    }

    public static class SourceType
    {
        public const string NEWS = "NEWS";
        public const string FAQ = "FAQ";
        public const string DOWNLOAD = "DOWNLOAD";
        public const string COMPLAINT = "COMPLAINT";
        //public const string FAQ = "BLRMIS - Complaint Submitted.";
    }

    public enum SortingType
    {
        Ascending,
        Descending
    }

    public enum EmailTemplates
    {
        VerificationCodeTemplate,
        ComplaintSubmitNotificationTemplate,
        ComplaintResolvedNotificationTemplate,
        Images,
        Logo,
        Bitmap,
        FbIcon,
        TwitterIcon,
        YouTubeIcon,
        Googleplay,
        LinkedinIcon
    }
    public enum TokenEnums
    {
        EXPIRED,
        VERIFIED,
        CONSUMED,
        INVALID,
        CREATED
    }

    public enum SourceTypeEnums
    {
        NULL,
        DOWNLOAD,
        FAQ,
        NEWS,
        CONTENT_MANAGEMENT,
        COMPLAINT,
        COMPLAINT_COMMENT
    }
    public enum ComplaintStatusEnum
    {
        OPEN = 1,
        REOPEN = 2,
        CLOSED = 3,
        RESOLVED = 4 ,
        PENDING = 5,
        IN_PROGRESS = 6
    }

    public class ComplaintStatus
    {
        const string OPEN = "OPEN";
        const string REOPEN = "RE_OPEN";
        const string PENDING = "PENDING";
        const string INPROGRESS = "IN_PROGESS";
        const string CLOSED = "CLOSED";
        const string RESOLVED = "RESOLVED";
    }
    public enum UserRolesEnum
    {
        ADMIN,
        PUBLIC_USER,
        REIVEWER = 10,
        RESOLVER = 11,
        SUPER_REVIEWER = 12
    }
    public class EmailMessages
    {
        public const string EMAIL_SUBJECT_VERIFICATION_CODE = "BLRMIS - Complaint verification code.";
        public const string EMAIL_SUBJECT_COMPLAINT_SUBMITTED = "BLRMIS - Complaint Submitted.";
        public const string EMAIL_SUBJECT_COMPLAINT_RESOLVED = "BLRMIS - Complaint Submitted.";
    }

    public enum UserRoleFunctionEnum {
        ADMIN = 0,
        PUBLIC_USER = 1,
        REVIEWER = 10,
        RESOLVER ,
        SUPER_REVIEWER = 12
    }

    public enum FunctionCodeEnum
    {
        USER_MANAGEMENT,
        FAQ_MANAGEMENT,
        ROLE_MANAGEMENT,
        CONTENT_MANAGEMENT,
        CATEGORY_MANAGEMENT,
        DEPARTMENT_MANAGEMENT,
        DESIGNATION_MANAGEMENT,
        DOWNLOAD_MANAGEMENT,
        COMPLAINT_ADMIN,
        COMPLAINT_REVIWER,
        COMPLAINT_RESOLVER,
        COMPLAINT_SR_REVIWER,
        NEWS_MANAGEMENT,
        DIGITIZATION_PROGRESS_MANAGEMENT
    }

    public enum PublicUserOpinionEnum
    {
        SATISFIED = 1,
        NOT_SATISFIED = 2
    }
    public enum ClaimEnum
    {
        FUNCTION,
        USER_NAME
    }

    public class ExternalCommunicationParameters
    {
        public const string Post = "POST";
        public const string Put = "PUT";
        public const string Get = "GET";
        public const string Delete = "DELETE";
        public const string HttpMethod = "HttpMethod";
        public const string FromDate = "fromDate";
        public const string ToDate = "toDate";
        public const string DistrictId = "districtId";


    }
    public enum ErrorCodeEnum {
        INVALID_CREDENTIALS,
        SOMETHING_WENT_WRONG
    }
}
