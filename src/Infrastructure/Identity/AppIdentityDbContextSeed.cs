using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
        {
            try
            {
                var defaultUser = new AppUser { MonthlyIncome = 5000, FirstName = "Niko", LastName = "Makridakis", UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
                await userManager.CreateAsync(defaultUser, "Pass@word1");
            }
            catch (Exception ex)
            {
                var log = loggerFactory.CreateLogger<AppIdentityDbContextSeed>();
                log.LogError(ex.Message);
            }
        }
    }
}
