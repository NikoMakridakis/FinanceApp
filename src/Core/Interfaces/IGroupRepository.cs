using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGroupRepository
    {
        Task<Group> GetGroupByIdAsync(int id);
        Task<List<Group>> GetGroupsAsync();
    }
}
