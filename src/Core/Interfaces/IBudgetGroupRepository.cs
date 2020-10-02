using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetGroupRepository
    {
        Task<IReadOnlyList<BudgetGroup>> GetBudgetGroupsAsync(int? userId);
        Task<BudgetGroup> GetBudgetGroupByIdAsync(int budgetGroupId);
        Task<BudgetGroup> AddBudgetGroupAsync(BudgetGroup budgetGroup);
        Task<BudgetGroup> UpdateBudgetGroupAsync(BudgetGroup budgetGroup);
        Task<BudgetGroup> DeleteBudgetGroupByIdAsync(int budgetGroupId);
        bool BudgetGroupByIdExists(int budgetGroupId);
    }
}
