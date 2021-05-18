using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home
{
    public static class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                await MigrateDatabaseAsync(scope);
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }

        private static Task MigrateDatabaseAsync(IServiceScope serviceScope)
        {
            var lifetime = serviceScope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
            var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

            return context.Database.MigrateAsync(lifetime.ApplicationStopping);
        }
    }
}
