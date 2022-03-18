using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace HomeworkTwo_NadirKorkut.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AppVersionControlMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _appversion;

        public AppVersionControlMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _appversion = configuration;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var version = new Version(_appversion.GetValue<string>("VersionControl"));
            var requestHeader = new Version(httpContext.Request.Headers["version"]);

            try
            {
                if(httpContext.Request.Path == "/login" || httpContext.Request.Path == "/register")
                {
                    await _next(httpContext);
                }
                else
                {
                    if (version.CompareTo(requestHeader) < 0)
                        await VersionControlExceptionHandle(httpContext, new Exception());
                }
            }
            catch (Exception ex)
            {
                await VersionControlExceptionHandle(httpContext, ex);
            }
        }
        private async Task VersionControlExceptionHandle(HttpContext httpContext,Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await httpContext.Response.WriteAsync($"Unauthorized : {ex.Message}");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AppVersionControlMiddlewareExtensions
    {
        public static IApplicationBuilder UseAppVersionControlMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppVersionControlMiddleware>();
        }
    }
}
