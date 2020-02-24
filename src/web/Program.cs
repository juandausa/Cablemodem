using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using WebAPI.Helpers;

namespace WebAPI
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
