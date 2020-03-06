using Infraestructura;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace Web.Helpers
{
    public static class DatabaseSeedHelper
    {
        public static IHost SeedDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var log = scope.ServiceProvider.GetRequiredService<ILogger<Startup>>();
                var appSettings = scope.ServiceProvider.GetRequiredService<IAppSettings>();
                using var appContext = scope.ServiceProvider.GetRequiredService<CablemodemContext>();
                try
                {
                    log.LogDebug("Ejecutando seed de base de datos");
                    appContext.Database.BeginTransaction();
                    var commandText = File.ReadAllText(appSettings.SqlSeedFilePath);
                    appContext.Database.ExecuteSqlRaw(commandText);
                    appContext.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    log.LogError("Se produjo un error al ejecutar el seed de base de datos", ex);
                    throw;
                }
            }

            return host;
        }
    }
}
