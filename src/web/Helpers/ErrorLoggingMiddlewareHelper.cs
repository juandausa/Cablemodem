using Microsoft.AspNetCore.Builder;
using Web.Middlewares;

namespace Web.Helpers
{
    public static class ErrorLoggingMiddlewareHelper
    {
        public static void UseErrorLoggingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ErrorLoggingMiddleware>();
        }
    }
}
