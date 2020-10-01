using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(AppIdentityDbContext context, UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
        {
            try
            {
                var defaultUser = new AppUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
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
