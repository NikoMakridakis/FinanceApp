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

        public async Task<IEnumerable<BudgetGroup>> GetGroupsAsync(int? budgetId)
        {
            if (budgetId == null)
            {
                return await _context.Groups.ToListAsync();
            }

            return await _context.Groups.Where(g => g.BudgetId == budgetId).ToListAsync();
        }

        public async Task<BudgetGroup> GetGroupByGroupIdAsync(int groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<BudgetGroup> AddGroupAsync(BudgetGroup group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<BudgetGroup> UpdateGroupAsync(BudgetGroup group)
        {
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<BudgetGroup> DeleteGroupByGroupIdAsync(int groupId)
        {
            BudgetGroup group = await _context.Groups.FindAsync(groupId);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public bool GroupByGroupIdExists(int groupId)
        {
            return _context.Groups.Any(g => g.GroupId == groupId);
        }

        public bool BudgetByBudgetIdExists(int budgetId)
        {
            return _context.Budgets.Any(b => b.BudgetId == budgetId);
        }
    }
}
