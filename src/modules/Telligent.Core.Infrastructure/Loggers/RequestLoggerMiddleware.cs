using Microsoft.AspNetCore.Http;
using Serilog;

namespace Telligent.Core.Infrastructure.Loggers
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Request.Path.Value is "/health")
                    return;

                Log.Information(
                    "request: {method} {url} {statusCode} {userAgent}",
                    context.Request.Method,
                    context.Request.Path.Value,
                    context.Response.StatusCode,
                    context.Request.Headers["User-Agent"]);
            }
            catch (Exception ex)
            {
                Log.Error(ex,
                    "request: {method} {url} {statusCode} {userAgent}",
                    context.Request.Method,
                    context.Request.Path.Value,
                    context.Response.StatusCode,
                    context.Request.Headers["User-Agent"]);

                throw;
            }
        }
    }
}
