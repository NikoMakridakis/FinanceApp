using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFundRepository
    {
        Task<Fund> GetFundByIdAsync(int id);
        Task<List<Fund>> GetFundsAsync();
    }
}
