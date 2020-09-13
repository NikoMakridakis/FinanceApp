using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly FinanceAppContext _context;
        public BudgetRepository(FinanceAppContext context)
        {
            _context = context;
        }

        public async Task<Budget> GetBudgetByIdAsync(int id)
        {
            return await _context.Budgets.FindAsync(id);
        }

        public async Task<IReadOnlyList<Budget>> GetBudgetsAsync()
        {
            return await _context.Budgets.ToListAsync();
        }
    }
}
