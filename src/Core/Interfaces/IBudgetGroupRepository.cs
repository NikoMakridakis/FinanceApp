using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetGroupRepository
    {
        Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsForUserAsync(int userId);
        Task<BudgetGroup> GetBudgetGroupByIdForUserAsync(int budgetGroupId, int userId);
        Task AddBudgetGroupForUserAsync(BudgetGroup budgetGroup);
        Task UpdateBudgetGroupForUserAsync(BudgetGroup budgetGroup);
        Task DeleteBudgetGroupByIdForUserAsync(int budgetGroupId);
        Task<bool> BudgetGroupByIdForUserExists(int budgetGroupId, int userId);
    }
}
