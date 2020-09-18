using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(int groupId);
        Task<IReadOnlyList<Group>> GetGroupsAsync();
        Task<Group> PostGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(int groupId, Group group);
        Task<Group> DeleteGroupByIdAsync(int groupId);

    }
}
