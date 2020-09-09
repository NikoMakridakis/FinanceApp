using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class StockRepository : IStockRepository
    {
        private readonly CatalogContext _context;
        public StockRepository(CatalogContext context)
        {
            _context = context;
        }

        public async Task<Stock> GetStockByIdAsync(int id)
        {
            return await _context.Stocks.FindAsync(id);
        }

        public async Task<IReadOnlyList<Stock>> GetStocksAsync()
        {
            return await _context.Stocks.ToListAsync();
        }
    }
}
