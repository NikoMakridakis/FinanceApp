using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetBudgetByBudgetIdAsync(int budgetId);
        Task<IEnumerable<User>> GetBudgetsAsync();
        Task<User> AddBudgetAsync(User budget);
        Task<User> UpdateBudgetAsync(User budget);
        Task<User> DeleteBudgetByBudgetIdAsync(int budgetId);
        bool BudgetByBudgetIdExists(int budgetId);
    }
}
