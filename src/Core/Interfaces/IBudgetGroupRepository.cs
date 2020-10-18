using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetGroupRepository
    {
        Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsForUserAsync();
        Task AddBudgetGroupForUserAsync(BudgetGroup budgetGroup);
        Task UpdateBudgetGroupAsync(BudgetGroup budgetGroup);
        Task DeleteBudgetGroupByIdAsync(int budgetGroupId);
        bool BudgetGroupByIdExists(int budgetGroupId);
    }
}
