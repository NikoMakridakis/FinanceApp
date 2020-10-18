using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class BudgetGroupRepository : IBudgetGroupRepository
    {
        private readonly FinanceAppDbContext _context;
        public BudgetGroupRepository(FinanceAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsForUserAsync(int userId)
        {
            return await _context.BudgetGroups.Where(budgetGroup => budgetGroup.UserId == userId).ToListAsync();
        }

        public async Task<BudgetGroup> GetBudgetGroupByIdForUserAsync(int budgetGroupId, int userId)
        {
            return await _context.BudgetGroups.Where(budgetGroup => budgetGroup.BudgetGroupId == budgetGroupId && budgetGroup.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task AddBudgetGroupForUserAsync(BudgetGroup budgetGroup)
        {
            if (budgetGroup == null)
            {
                throw new ArgumentNullException(nameof(budgetGroup));
            }

            _context.BudgetGroups.Add(budgetGroup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBudgetGroupForUserAsync(BudgetGroup budgetGroup)
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBudgetGroupByIdForUserAsync(int budgetGroupId)
        {
            BudgetGroup budgetGroup = await _context.BudgetGroups.FindAsync(budgetGroupId);
            _context.BudgetGroups.Remove(budgetGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BudgetGroupByIdForUserExists(int budgetGroupId, int userId)
        {
            return await _context.BudgetGroups.AnyAsync(budgetGroup => budgetGroup.BudgetGroupId == budgetGroupId && budgetGroup.UserId == userId);
        }
    }
}
