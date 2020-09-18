using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _repo;
        public ItemController(IItemRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Item
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            IReadOnlyList<Item> item = await _repo.GetItemsAsync();

            return Ok(item);
        }

        // GET: api/Item/itemId
        [HttpGet("{itemId}")]
        public async Task<ActionResult<Item>> GetItem(int itemId)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            return Ok(item);
        }
    }
}