using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FinanceAppDbContextSeed
    {
        public static async Task SeedAsync(FinanceAppDbContext context, UserManager<User> userManager, ILoggerFactory logger)
        {
            try
            {
                context.Database.Migrate();

                if (!await context.Users.AnyAsync())
                {
                    User user = new User
                    {
                        MonthlyIncome = 5000,
                        FirstName = "Niko",
                        LastName = "Makridakis",
                        UserName = "demouser",
                        Email = "demouser@microsoft.com",
                    };

                    await userManager.CreateAsync(user, "Pass@word1");
                }

                if (!await context.BudgetGroups.AnyAsync())
                {
                    await context.BudgetGroups.AddRangeAsync(GetPreconfiguredBudgetGroups());
                    await context.SaveChangesAsync();
                }

                if (!await context.Items.AnyAsync())
                {
                    await context.Items.AddRangeAsync(GetPreconfiguredItems());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var log = logger.CreateLogger<FinanceAppDbContextSeed>();
                log.LogError(ex.Message);
            }
        }

        static List<BudgetGroup> GetPreconfiguredBudgetGroups()
        {
            return new List<BudgetGroup>()
            {
                new BudgetGroup(1, "Housing"),
                new BudgetGroup(1, "Transportation"),
                new BudgetGroup(1, "Food"),
                new BudgetGroup(1, "Personal"),
                new BudgetGroup(1, "Health"),
                new BudgetGroup(1, "Insurance"),
                new BudgetGroup(1, "Debt"),
            };
        }

        static List<Item> GetPreconfiguredItems()
        {
            return new List<Item>()
            {
                new Item(1, "Mortgage/Rent", 0),
                new Item(1, "Water", 0),
                new Item(1, "Electricity", 0),
                new Item(1, "Natural Gas", 0),
                new Item(1, "Internet/Cable", 0),
                new Item(1, "Trash", 0),
                new Item(2, "Gas", 0),
                new Item(2, "Maintenance", 0),
                new Item(3, "Groceries", 0),
                new Item(3, "Restaurants", 0),
                new Item(4, "Clothing", 0),
                new Item(4, "Phone", 0),
                new Item(4, "Subscriptions", 0),
                new Item(4, "Fun Money", 0),
                new Item(5, "Gym", 0),
                new Item(5, "Medicine/Vitamins", 0),
                new Item(5, "Doctor Visits", 0),
                new Item(6, "Health Insurance", 0),
                new Item(6, "Dental Insurance", 0),
                new Item(6, "Life Insurance", 0),
                new Item(6, "Auto Insurance", 0),
                new Item(6, "Homeowner/Renter Insurance", 0),
                new Item(7, "Credit Card", 0),
                new Item(7, "Car Payment", 0),
                new Item(7, "Student Loan", 0),
                new Item(7, "Medical Bill", 0),
            };
        }
    }
}
