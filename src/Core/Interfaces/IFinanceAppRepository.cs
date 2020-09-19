﻿using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IFinanceAppRepository
    {
        Task<Budget> GetBudgetByBudgetIdAsync(int budgetId);
        Task<IEnumerable<Budget>> GetBudgetsAsync();
        Task<Budget> AddBudgetAsync(Budget budget);
        Task<Budget> UpdateBudgetAsync(int budgetId, Budget budget);
        Task<Budget> DeleteBudgetByBudgetIdAsync(int budgetId);

        Task<Group> GetGroupByGroupIdAsync(int groupId);
        Task<IEnumerable<Group>> GetGroupsAsync();
        Task<Group> AddGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(int groupId, Group group);
        Task<Group> DeleteGroupByGroupIdAsync(int groupId);

        Task<Item> GetItemByItemIdAsync(int itemId);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(int itemId, Item item);
        Task<Item> DeleteItemByItemIdAsync(int itemId);
    }
}
