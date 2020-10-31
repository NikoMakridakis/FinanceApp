using Core.Entities;
using Web.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            //The using statement defines a scope at the end of which an object will be disposed.
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                try
                {
                    Log.Information("Seeding the database.");
                    var context = services.GetRequiredService<FinanceAppDbContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var logger = services.GetRequiredService<ILoggerFactory>();
                    await FinanceAppDbContextSeed.SeedAsync(context, userManager, logger);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "Error seeding the database.");
                }
            }

            Log.Information("Application starting up.");
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
