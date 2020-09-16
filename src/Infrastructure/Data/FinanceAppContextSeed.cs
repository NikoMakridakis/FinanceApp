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

        static Budget GetPreconfiguredBudget()
        {
            return new Budget(DateTime.Now);
        }

        static IEnumerable<Group> GetPreconfiguredGroups()
        {
            return new List<Group>()
            {
                new Group(1, "Income"),
                new Group(1, "Savings"),
                new Group(1, "Housing"),
                new Group(1, "Transportation"),
                new Group(1, "Food"),
                new Group(1, "Personal"),
                new Group(1, "Health"),
                new Group(1,"Insurance"),
                new Group(1, "Debt")
            };
        }

        static IEnumerable<Item> GetPreconfiguredItems()
        {
            return new List<Item>()
            {
                new Item(1, "Paycheck"),
                new Item(2, "Emergency Fund"),
                new Item(3, "Mortgage/Rent"),
                new Item(3, "Water"),
                new Item(3, "Electricity"),
                new Item(3, "Natural Gas"),
                new Item(3, "Internet/Cable"),
                new Item(3, "Trash"),
                new Item(4, "Gas"),
                new Item(4, "Maintenance"),
                new Item(5, "Groceries"),
                new Item(5, "Restaurants"),
                new Item(6, "Clothing"),
                new Item(6, "Phone"),
                new Item(6, "Subscriptions"),
                new Item(6, "Fun Money"),
                new Item(7, "Gym"),
                new Item(7, "Medicine/Vitamins"),
                new Item(7, "Doctor Visits"),
                new Item(8, "Health Insurance"),
                new Item(8, "Dental Insurance"),
                new Item(8, "Life Insurance"),
                new Item(8, "Auto Insurance"),
                new Item(8, "Homeowner/Renter Insurance"),
                new Item(9, "Credit Card"),
                new Item(9, "Car Payment"),
                new Item(9, "Student Loan"),
                new Item(9, "Medical Bill"),
            };
        }
    }
}
