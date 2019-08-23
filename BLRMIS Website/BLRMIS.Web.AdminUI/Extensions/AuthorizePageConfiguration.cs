using BLRMIS.Web.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLRMIS.Web.AdminUI.Extensions
{
    public class AuthorizePageConfiguration
    {
        private List<KeyValuePair<string, string>> AuthorizePageDictionary;
        public AuthorizePageConfiguration()
        {
            // Mapping for page with policy for authorization
            // 1. @Pram1: policy name
            // 2. @Param2: page name 
            AuthorizePageDictionary = new List<KeyValuePair<string, string>>{

                new KeyValuePair<string, string>(FunctionCodeEnum.USER_MANAGEMENT.ToString(),          "/users" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.ROLE_MANAGEMENT.ToString(),          "/role/role" ),
                new KeyValuePair<string, string>(FunctionCodeEnum.FAQ_MANAGEMENT.ToString(),           "/faq/index" ),
                new KeyValuePair<string, string>(FunctionCodeEnum.NEWS_MANAGEMENT.ToString(),          "/news/index" ),
                new KeyValuePair<string, string>(FunctionCodeEnum.DOWNLOAD_MANAGEMENT.ToString(),      "/download/index" ),
                new KeyValuePair<string, string>(FunctionCodeEnum.CATEGORY_MANAGEMENT.ToString(),      "/category/category" ),
                new KeyValuePair<string, string>(FunctionCodeEnum.CONTENT_MANAGEMENT.ToString(),       "/webcontent/content" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.DEPARTMENT_MANAGEMENT.ToString(),    "/department/department" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.DIGITIZATION_PROGRESS_MANAGEMENT.ToString(),   "/digitizationProgress/digitizationProgress" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.DESIGNATION_MANAGEMENT.ToString(),   "/designation/designation" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_ADMIN.ToString(),          "/complaint/AdminComplaintListing"),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_ADMIN.ToString(),          "/complaint/AdminComplaintDetails" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_REVIWER.ToString(),        "/complaint/ReviewerComplaintListing" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_REVIWER.ToString(),        "/complaint/ReviewerComplaintDetails" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_RESOLVER.ToString(),       "/complaint/ResolverComplaintListing" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_RESOLVER.ToString(),       "/complaint/ResolverComplaintDetails" ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_SR_REVIWER.ToString(),     "/complaint/SeniorReviewerComplaintListing"  ),
                new KeyValuePair<string, string>( FunctionCodeEnum.COMPLAINT_SR_REVIWER.ToString(),     "/complaint/SeniorReviewerComplaintDetails"  )
            };
        }
        public List<KeyValuePair<string, string>> GetAuthorizePageCollection
        {
            get
            {
                return AuthorizePageDictionary;
            }
        }
    }
}
