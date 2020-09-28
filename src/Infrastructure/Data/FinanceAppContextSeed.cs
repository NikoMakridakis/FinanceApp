using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FinanceAppContextSeed
    {
        public static async Task SeedAsync(FinanceAppContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                context.Database.Migrate();

                if(!await context.Budgets.AnyAsync())
                {
                    await context.Budgets.AddRangeAsync(GetPreconfiguredBudget());
                    await context.SaveChangesAsync();
                }

                if (!await context.Groups.AnyAsync())
                {
                    await context.Groups.AddRangeAsync(GetPreconfiguredGroups());
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
                var log = loggerFactory.CreateLogger<FinanceAppContextSeed>();
                log.LogError(ex.Message);
            }
        }

        static User GetPreconfiguredBudget()
        {
            return new User(4000, 2000);
        }

        static IEnumerable<BudgetGroup> GetPreconfiguredGroups()
        {
            return new List<BudgetGroup>()
            {
                new BudgetGroup(1, "Housing"),
                new BudgetGroup(1, "Transportation"),
                new BudgetGroup(1, "Food"),
                new BudgetGroup(1, "Personal"),
                new BudgetGroup(1, "Health"),
                new BudgetGroup(1, "Insurance"),
                new BudgetGroup(1, "Debt")
            };
        }

        static IEnumerable<Item> GetPreconfiguredItems()
        {
            return new List<Item>()
            {
                new Item(1, "Mortgage/Rent", 1000),
                new Item(1, "Water", 50),
                new Item(1, "Electricity", 50),
                new Item(1, "Natural Gas", 50),
                new Item(1, "Internet/Cable", 25),
                new Item(1, "Trash", 25),
                new Item(2, "Gas", 180),
                new Item(2, "Maintenance", 20),
                new Item(3, "Groceries", 700),
                new Item(3, "Restaurants", 100),
                new Item(4, "Clothing", 100),
                new Item(4, "Phone", 50),
                new Item(4, "Subscriptions", 10),
                new Item(4, "Fun Money", 100),
                new Item(5, "Gym", 0),
                new Item(5, "Medicine/Vitamins", 0),
                new Item(5, "Doctor Visits", 0),
                new Item(6, "Health Insurance", 50),
                new Item(6, "Dental Insurance", 25),
                new Item(6, "Life Insurance", 0),
                new Item(6, "Auto Insurance", 60),
                new Item(6, "Homeowner/Renter Insurance", 10),
                new Item(7, "Credit Card", 0),
                new Item(7, "Car Payment", 0),
                new Item(7, "Student Loan", 0),
                new Item(7, "Medical Bill", 0),
            };
        }
    }
}
