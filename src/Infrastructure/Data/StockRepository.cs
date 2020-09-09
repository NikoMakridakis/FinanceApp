using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    class StockRepository : IStockRepository
    {
        public Task<Stock> GetStockByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Stock>> GetStocksAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
