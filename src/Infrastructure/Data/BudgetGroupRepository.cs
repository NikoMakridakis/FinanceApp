using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BudgetGroupRepository : IBudgetGroupRepository
    {
        private readonly FinanceAppDbContext _context;
        public BudgetGroupRepository(FinanceAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsAsync(int userId)
        {
            return await _context.BudgetGroups.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<BudgetGroup> GetBudgetGroupByIdAsync(int budgetGroupId)
        {
            return await _context.BudgetGroups.FindAsync(budgetGroupId);
        }

        public async Task AddBudgetGroupAsync(BudgetGroup budgetGroup)
        {
            if (budgetGroup == null)
            {
                throw new ArgumentNullException(nameof(budgetGroup));
            }

            _context.BudgetGroups.Add(budgetGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBudgetGroupAsync(BudgetGroup budgetGroup)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBudgetGroupByIdAsync(int budgetGroupId)
        {
            BudgetGroup budgetGroup = await _context.BudgetGroups.FindAsync(budgetGroupId);
            _context.BudgetGroups.Remove(budgetGroup);
            await _context.SaveChangesAsync();
        }

        public bool BudgetGroupByIdExists(int budgetGroupId)
        {
            return _context.BudgetGroups.Any(g => g.BudgetGroupId == budgetGroupId);
        }
    }
}
