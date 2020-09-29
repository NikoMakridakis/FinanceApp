using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IItemRepository
    {
        Task<IReadOnlyList<Item>> GetItemsAsync(int? budgetGroupId);
        Task<Item> GetItemByItemIdAsync(int itemId);
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(Item item);
        Task<Item> DeleteItemByItemIdAsync(int itemId);
        bool ItemByItemIdExists(int itemId);
        bool BudgetGroupByBudgetGroupIdExists(int budgetGroupId);
    }
}
