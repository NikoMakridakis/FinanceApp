using Core.Entities;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByGroupIdAsync(int groupId);
        Task<IReadOnlyList<Group>> GetGroupsAsync();
        Task<Group> PostGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(int groupId, Group group);
        Task<Group> DeleteGroupByGroupIdAsync(int groupId);
    }
}
