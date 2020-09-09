using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> GetStockByIdAsync(int id);
        Task<IReadOnlyList<Stock>> GetStocksAsync();
    }
}
