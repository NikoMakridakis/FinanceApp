using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly FinanceAppContext _context;
        public GroupRepository(FinanceAppContext context)
        {
            _context = context;
        }

        public async Task<Group> GetGroupByIdAsync(int groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<IReadOnlyList<Group>> GetGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }
    }
}
