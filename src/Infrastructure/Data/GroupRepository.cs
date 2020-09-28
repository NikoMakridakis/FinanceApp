﻿using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GroupRepository : IGroupRepository
    {
        private readonly FinanceAppContext _context;
        public GroupRepository(FinanceAppContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(int? budgetId)
        {
            if (budgetId == null)
            {
                return await _context.Groups.ToListAsync();
            }

            return await _context.Groups.Where(g => g.BudgetId == budgetId).ToListAsync();
        }

        public async Task<Group> GetGroupByGroupIdAsync(int groupId)
        {
            return await _context.Groups.FindAsync(groupId);
        }

        public async Task<Group> AddGroupAsync(Group group)
        {
            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> UpdateGroupAsync(Group group)
        {
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<Group> DeleteGroupByGroupIdAsync(int groupId)
        {
            Group group = await _context.Groups.FindAsync(groupId);
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public bool GroupByGroupIdExists(int groupId)
        {
            return _context.Groups.Any(g => g.GroupId == groupId);
        }

        public bool BudgetByBudgetIdExists(int budgetId)
        {
            return _context.Budgets.Any(b => b.BudgetId == budgetId);
        }
    }
}
