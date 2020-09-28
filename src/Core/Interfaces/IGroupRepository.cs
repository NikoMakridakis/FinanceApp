using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> GetGroupsAsync(int? budgetId);
        Task<Group> GetGroupByGroupIdAsync(int groupId);
        Task<Group> AddGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Group group);
        Task<Group> DeleteGroupByGroupIdAsync(int groupId);
        bool GroupByGroupIdExists(int groupId);
        bool BudgetByBudgetIdExists(int budgetId);
    }
}
