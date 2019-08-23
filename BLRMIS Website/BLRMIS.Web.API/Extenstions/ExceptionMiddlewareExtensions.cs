using BLRMIS.Web.Domain.Infrastructure;
using log4net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BLRMIS.Web.API.Extenstions
{
    public static class ExceptionMiddlewareExtensions
    {
        private static readonly log4net.ILog log =
        log4net.LogManager.GetLogger(typeof(ExceptionMiddlewareExtensions));
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        //ILoggerFactory.LogError($"Something went wrong: {contextFeature.Error}");
                        log.Error($"Exception Message @ {contextFeature.Error.Message}, Inner Exception @ {contextFeature.Error.InnerException}, Stack Trace @ {contextFeature.Error.StackTrace}");
                        // log.Info("Application - Main is invoked");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });

        }
    }
}
