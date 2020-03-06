using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Web.Helpers;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
            .AddLogging()
            .Build()
            .SeedDatabase()
            .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
