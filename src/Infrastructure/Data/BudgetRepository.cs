using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly FinanceAppContext _context;
        public BudgetRepository(FinanceAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Budget> GetBudgetByBudgetIdAsync(int budgetId)
        {
            return await _context.Budgets.FindAsync(budgetId);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsAsync()
        {
            return await _context.Budgets.ToListAsync();
        }

        public async Task<Budget> AddBudgetAsync(Budget budget)
        {
            if (budget == null)
            {
                throw new ArgumentNullException(nameof(budget));
            }

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> UpdateBudgetAsync(Budget budget)
        {
            await _context.SaveChangesAsync();
            return budget;
        }

        public async Task<Budget> DeleteBudgetByBudgetIdAsync(int budgetId)
        {
            Budget budget = await _context.Budgets.FindAsync(budgetId);
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return budget;
        }

        public bool BudgetByBudgetIdExists(int budgetId)
        {
            return _context.Budgets.Any(b => b.BudgetId == budgetId);
        }
    }
}
