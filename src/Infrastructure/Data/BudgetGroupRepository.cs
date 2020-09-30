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
        private readonly FinanceAppContext _context;
        public BudgetGroupRepository(FinanceAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsAsync(int? userId)
        {
            if (userId == null)
            {
                return await _context.BudgetGroups.ToListAsync();
            }

            return await _context.BudgetGroups.Where(b => b.UserId == userId).ToListAsync();
        }

        public async Task<BudgetGroup> GetBudgetGroupByIdAsync(int budgetGroupId)
        {
            return await _context.BudgetGroups.FindAsync(budgetGroupId);
        }

        public async Task<BudgetGroup> AddBudgetGroupAsync(BudgetGroup budgetGroup)
        {
            if (budgetGroup == null)
            {
                throw new ArgumentNullException(nameof(budgetGroup));
            }

            _context.BudgetGroups.Add(budgetGroup);
            await _context.SaveChangesAsync();
            return budgetGroup;
        }

        public async Task<BudgetGroup> UpdateBudgetGroupAsync(BudgetGroup budgetGroup)
        {
            await _context.SaveChangesAsync();
            return budgetGroup;
        }

        public async Task<BudgetGroup> DeleteBudgetGroupByIdAsync(int budgetGroupId)
        {
            BudgetGroup budgetGroup = await _context.BudgetGroups.FindAsync(budgetGroupId);
            _context.BudgetGroups.Remove(budgetGroup);
            await _context.SaveChangesAsync();
            return budgetGroup;
        }

        public bool BudgetGroupByIdExists(int budgetGroupId)
        {
            return _context.BudgetGroups.Any(g => g.BudgetGroupId == budgetGroupId);
        }

        public bool UserByUserIdExists(int? userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }
    }
}
