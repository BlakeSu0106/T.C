using Microsoft.AspNetCore.Builder;

namespace Telligent.Core.Infrastructure.Loggers
{
    public static class RequestLoggerExtensions
    {
        public static IApplicationBuilder UseRequestLogger(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggerMiddleware>();
        }
    }
}
