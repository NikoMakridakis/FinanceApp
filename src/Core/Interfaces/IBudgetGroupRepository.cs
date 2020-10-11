using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetGroupRepository
    {
        Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsAsync(int userId);
        Task<BudgetGroup> GetBudgetGroupByIdAsync(int budgetGroupId);
        Task AddBudgetGroupAsync(BudgetGroup budgetGroup);
        Task UpdateBudgetGroupAsync(BudgetGroup budgetGroup);
        Task DeleteBudgetGroupByIdAsync(int budgetGroupId);
        bool BudgetGroupByIdExists(int budgetGroupId);
    }
}
