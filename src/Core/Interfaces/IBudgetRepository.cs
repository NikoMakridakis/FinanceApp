using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetRepository
    {
        Task<Budget> GetBudgetByBudgetIdAsync(int budgetId);
        Task<IEnumerable<Budget>> GetBudgetsAsync();
        Task<Budget> AddBudgetAsync(Budget budget);
        Task<Budget> UpdateBudgetAsync(Budget budget);
        Task<Budget> DeleteBudgetByBudgetIdAsync(int budgetId);
        bool BudgetByBudgetIdExists(int budgetId);
    }
}
