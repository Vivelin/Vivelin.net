using Microsoft.EntityFrameworkCore;

using Vivelin.Web.Data;

namespace Vivelin.Web.Home;

public static class Program
{
    public static async Task Main(string[] args)
    {
        IHost host = CreateHostBuilder(args).Build();
        using (IServiceScope scope = host.Services.CreateScope())
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

    private static async Task MigrateDatabaseAsync(IServiceScope serviceScope)
    {
        IHostApplicationLifetime lifetime = serviceScope.ServiceProvider.GetRequiredService<IHostApplicationLifetime>();
        DataContext context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();

        await context.Database.MigrateAsync(lifetime.ApplicationStopping).ConfigureAwait(false);
    }
}
