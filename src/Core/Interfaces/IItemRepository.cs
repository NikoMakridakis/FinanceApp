using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> GetItemByIdAsync(int itemId);
        Task<List<Item>> GetItemsAsync();
    }
}
