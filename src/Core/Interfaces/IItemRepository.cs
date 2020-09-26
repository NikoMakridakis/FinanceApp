using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> GetItemByItemIdAsync(int itemId);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task<Item> AddItemAsync(Item item);
        Task<Item> UpdateItemAsync(int itemId, Item item);
        void DeleteItemByItemIdAsync(int itemId);
        bool ItemByItemIdExists(int itemId);
    }
}
