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

                if(!await context.Users.AnyAsync())
                {
                    await context.Users.AddRangeAsync(GetPreconfiguredUsers());
                    await context.SaveChangesAsync();
                }

                if (!await context.BudgetGroups.AnyAsync())
                {
                    await context.BudgetGroups.AddRangeAsync(GetPreconfiguredGroups());
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

        static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User(4000, "nmak@gmail.com", "password"),
                new User(5000, "abar@gmail.com", "password")
            };
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
                new BudgetGroup(1, "Debt"),

                new BudgetGroup(2, "Housing"),
                new BudgetGroup(2, "Transportation"),
                new BudgetGroup(2, "Food"),
                new BudgetGroup(2, "Personal"),
                new BudgetGroup(2, "Health"),
                new BudgetGroup(2, "Insurance"),
                new BudgetGroup(2, "Debt")
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

                new Item(8, "Mortgage/Rent", 800),
                new Item(8, "Water", 30),
                new Item(8, "Electricity", 30),
                new Item(8, "Natural Gas", 30),
                new Item(8, "Internet/Cable", 20),
                new Item(8, "Trash", 20),
                new Item(9, "Gas", 100),
                new Item(9, "Maintenance", 10),
                new Item(10, "Groceries", 400),
                new Item(10, "Restaurants", 200),
                new Item(11, "Clothing", 200),
                new Item(11, "Phone", 20),
                new Item(11, "Subscriptions", 20),
                new Item(11, "Fun Money", 200),
                new Item(12, "Gym", 10),
                new Item(12, "Medicine/Vitamins", 10),
                new Item(12, "Doctor Visits", 10),
                new Item(13, "Health Insurance", 40),
                new Item(13, "Dental Insurance", 20),
                new Item(13, "Life Insurance", 10),
                new Item(13, "Auto Insurance", 40),
                new Item(13, "Homeowner/Renter Insurance", 10),
                new Item(14, "Credit Card", 200),
                new Item(14, "Car Payment", 200),
                new Item(14, "Student Loan", 200),
                new Item(14, "Medical Bill", 200),
            };
        }
    }
}
