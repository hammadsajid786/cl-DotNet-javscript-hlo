using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BRLMIS.Web.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BRLMIS.Web.UI
{
    public class Startup
    {
        public static string apiBaseUrl;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = Configuration.GetSection("AppSettings");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.Configure<AppSettings>(appSettings);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession();
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

            app.UseSession();
            //app.UseMiddleware<MyMiddleware>();
            app.UseMvc();
        }


    }

    //public class MyMiddleware
    //{
    //    private readonly RequestDelegate _next;
    //    private readonly ILogger _logger;
    //    private readonly IOptions<AppSettings> _config;

    //    public MyMiddleware(RequestDelegate next, ILoggerFactory logFactory, IOptions<AppSettings> config)
    //    {
    //        _next = next;

    //        _logger = logFactory.CreateLogger("MyMiddleware");

    //        _config = config;
    //    }

    //    public async Task Invoke(HttpContext httpContext)
    //    {
    //        string IpAddress = httpContext.Connection.RemoteIpAddress.ToString();
    //        string name = httpContext.Connection.Id;
    //        StringContent Content = new StringContent("{\"MachineName\":\"\",\"IpAddress\":\"" + IpAddress + "\",\"UserAgent\":\"\"}",
    //                                Encoding.UTF8,
    //                                "application/json");

    //        HttpClient client = new HttpClient();

    //        var response = await client.PostAsync(_config.Value.APIBaseUrl + "/Dashboard/VisitorInformation/Add", Content);
    //        var responseString = await response.Content.ReadAsStringAsync();

    //        await _next(httpContext); // calling next middleware

    //    }
    //}
    //public static class MyMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<MyMiddleware>();
    //    }
    //}
}
