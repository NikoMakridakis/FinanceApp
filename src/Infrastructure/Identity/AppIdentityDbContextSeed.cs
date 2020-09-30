using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var user = new User
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                var log = loggerFactory.CreateLogger<FinanceAppContextSeed>();
                log.LogError(ex.Message);
            }
        }

        static List<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User(4000, "nmak@gmail.com", "password"),
                new User(5000, "abar@gmail.com", "password")
            };
        }
    }
    }
}
