using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

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

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.RequireUniqueEmail = false;
            });
        }
    }
}
