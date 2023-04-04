using InsuranceCorp.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace InsuranceCorp.API.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public LogRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, InsCorpDbContext context)
        {
            // ziskani informace o requestu
            var url = httpContext.Request.Path;
            var ip = httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            context.LogRequests.Add(new Model.RequestLog()
            {
                Date = DateTime.Now,
                Url = url,
                IP = ip
            });
            context.SaveChanges();

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LogRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogRequestMiddleware>();
        }
    }
}
