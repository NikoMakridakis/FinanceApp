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
        private readonly FinanceAppContext _context;
        public ItemRepository(FinanceAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
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
        public async void DeleteItemByItemIdAsync(int itemId)
        {
            Item item = await _context.Items.FindAsync(itemId);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }

        public bool ItemByItemIdExists(int itemId)
        {
            return _context.Items.Any(i => i.ItemId == itemId);
        }
    }
}
