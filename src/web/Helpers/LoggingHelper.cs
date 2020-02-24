using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebAPI.Helpers
{
    public static class LoggingHelper
    {
        public static IHostBuilder AddLogging(this IHostBuilder host)
        {
            return host.ConfigureLogging(log =>
            {
                log.ClearProviders();
                log.AddDebug();
                log.AddConsole();
            });
        }
    }
}
