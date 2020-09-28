using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBudgetGroupRepository
    {
        Task<IEnumerable<BudgetGroup>> GetGroupsAsync(int? budgetId);
        Task<BudgetGroup> GetGroupByGroupIdAsync(int groupId);
        Task<BudgetGroup> AddGroupAsync(BudgetGroup group);
        Task<BudgetGroup> UpdateGroupAsync(BudgetGroup group);
        Task<BudgetGroup> DeleteGroupByGroupIdAsync(int groupId);
        bool GroupByGroupIdExists(int groupId);
        bool BudgetByBudgetIdExists(int budgetId);
    }
}
