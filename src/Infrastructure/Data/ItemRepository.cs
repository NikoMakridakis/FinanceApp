using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ItemRepository : IItemRepository
    {
        private readonly FinanceAppContext _context;
        public ItemRepository(FinanceAppContext context)
        {
            _context = context;
        }

        public async Task<Item> GetItemByIdAsync(int itemId)
        {
            return await _context.Items.FindAsync(itemId);
        }

        public async Task<List<Item>> GetItemsAsync()
        {
            return await _context.Items.ToListAsync();
        }
    }
}
