using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

        public async Task<Group> PostGroupAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateGroupAsync(int groupId, Group group)
        {
            _context.Entry(group).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return group;
        }
        public async Task<Group> DeleteGroupByIdAsync(int groupId)
        {
            Group group = await _context.Groups.FindAsync(groupId);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }
    }
}
