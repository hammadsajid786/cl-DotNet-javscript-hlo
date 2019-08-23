using BLRMIS.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BLRMIS.Web.AdminUI.Extensions
{
    // IServiceCollection extension class. 
    public static class ServiceConfigurations
    {
        public static void ConfigureAuthorizationOptions(AuthorizationOptions options)
        {
            var AuthorizePageConfiguration = new AuthorizePageConfiguration();
            foreach (var item in AuthorizePageConfiguration.GetAuthorizePageCollection)
            {
                options.AddPolicy(item.Key, policy => policy.RequireClaim(ClaimEnum.FUNCTION.ToString(), item.Key));
            }
        }
        // this method is used to configure routes based on configured policies. 
        public static void ConfigureRazorPagesOptions(RazorPagesOptions options)
        {
            var AuthorizePageConfiguration = new AuthorizePageConfiguration();
            options.Conventions.AuthorizePage("/Index");
            options.Conventions.AuthorizePage("/ChangePassword/ChangePassword");
            foreach (var item in AuthorizePageConfiguration.GetAuthorizePageCollection)
            {
                options.Conventions.AuthorizePage(item.Value, item.Key);
            }
        }
    }
}
