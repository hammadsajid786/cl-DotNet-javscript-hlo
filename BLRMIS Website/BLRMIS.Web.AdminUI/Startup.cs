using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BLRMIS.Web.AdminUI.Extensions;
using BLRMIS.Web.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BLRMIS.Web.AdminUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings =  services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            // here we define route policies
            services.AddAuthorization(ServiceConfigurations.ConfigureAuthorizationOptions);
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie((options)=> {
                options.CookieHttpOnly = false;
                options.LoginPath =  "/Login";
                options.ExpireTimeSpan = TimeSpan.FromHours(2);
                //options.SlidingExpiration = true;
                options.AccessDeniedPath = "/AccessDenied";
            });
            
            services.AddMvc()
            .AddRazorPagesOptions(ServiceConfigurations.ConfigureRazorPagesOptions)
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
