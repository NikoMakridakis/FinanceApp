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

        public async Task<Group> GetGroupByIdAsync(int id)
        {
            return await _context.Groups.FindAsync(id);
        }

        public async Task<List<Group>> GetGroupsAsync()
        {
            return await _context.Groups.ToListAsync();
        }
    }
}
