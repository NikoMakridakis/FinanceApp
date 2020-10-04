using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extensions
{
    public static class IdentityServiceExtension
    {
        public static void AddIdentityConfiguration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<FinanceAppDbContext>()
                .AddDefaultTokenProviders()
                .AddSignInManager<SignInManager<User>>();
        }
    }
}
