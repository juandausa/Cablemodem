using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorLoggingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ha ocurrido un error no controlado");
                throw;
            }
        }
    }
}
