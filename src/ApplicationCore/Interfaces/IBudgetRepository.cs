using ApplicationCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> GetBudgetByIdAsync(int id);
        Task<IReadOnlyList<Budget>> GetBudgetsAsync();
    }
}
