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
    public class FinanceAppRepository : IFinanceAppRepository
    {
        private readonly FinanceAppContext _context;
        public FinanceAppRepository(FinanceAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); ;
        }

        #region Budget Repository Methods
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

        public async Task<Budget> UpdateBudgetAsync(int budgetId, Budget budget)
        {
            _context.Entry(budget).State = EntityState.Modified;
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
        #endregion

        #region Group Repository Methods
        public async Task<Group> GetGroupByGroupIdAsync(int groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateGroupAsync(int groupId, Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> DeleteGroupByGroupIdAsync(int groupId)
        {
            Group group = await _context.Groups.FindAsync(groupId);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public bool GroupByGroupIdExists(int groupId)
        {
            return _context.Groups.Any(g => g.GroupId == groupId);
        }
        #endregion

        #region Item Repository Methods
        public async Task<Item> GetItemByItemIdAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> AddItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> UpdateItemAsync(int itemId, Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Item> DeleteItemByItemIdAsync(int itemId)
        {
            Item item = await _context.Items.FindAsync(itemId);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public bool ItemByItemIdExists(int itemId)
        {
            return _context.Items.Any(i => i.ItemId == itemId);
        }
        #endregion
    }
}
