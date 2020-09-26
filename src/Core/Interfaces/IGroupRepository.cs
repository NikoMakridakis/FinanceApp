using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByGroupIdAsync(int groupId);
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Group> AddGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(Group group);
        Task<Group> DeleteGroupByGroupIdAsync(int groupId);
        bool GroupByGroupIdExists(int groupId);
    }
}
