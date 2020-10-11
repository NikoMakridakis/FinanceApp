using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly FinanceAppDbContext _context;
        public ItemRepository(FinanceAppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IReadOnlyList<Item>> GetItemsAsync(int budgetGroupId)
        {
            return await _context.Items.Where(b => b.BudgetGroupId == budgetGroupId).ToListAsync();
        }

        public async Task<Item> GetItemByItemIdAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }

        public async Task AddItemAsync(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemAsync(Item item)
        {
            await _context.SaveChangesAsync();
        }
        public async Task DeleteItemByItemIdAsync(int itemId)
        {
            Item item = await _context.Items.FindAsync(itemId);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public bool ItemByItemIdExists(int itemId)
        {
            return _context.Items.Any(i => i.ItemId == itemId);
        }

        public bool BudgetGroupByBudgetGroupIdExists(int budgetGroupId)
        {
            return _context.BudgetGroups.Any(g => g.BudgetGroupId == budgetGroupId);
        }
    }
}
