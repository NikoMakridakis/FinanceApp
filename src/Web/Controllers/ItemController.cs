using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _repo;
        private readonly IMapper _mapper;
        public ItemController(IItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/item
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetItems([FromQuery] int? budgetGroupId)
        {
            if (budgetGroupId != null && !_repo.BudgetGroupByBudgetGroupIdExists(budgetGroupId))
            {
                return NotFound($"Unable to find budget group with ID '{budgetGroupId}'.");
            }

            IReadOnlyList<Item> item = await _repo.GetItemsAsync(budgetGroupId);
            return Ok(_mapper.Map<IReadOnlyList<ItemDto>>(item));
        }

        // GET: api/item/{itemId}
        [HttpGet("{itemId}", Name = "GetItem")]
        public async Task<ActionResult<ItemDto>> GetItem(int itemId)
        {
            if (!_repo.ItemByItemIdExists(itemId))
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);
            return Ok(_mapper.Map<ItemDto>(item));
        }

        // POST: api/item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(ItemForCreationDto itemForCreationDto)
        {
            int budgetGroupId = itemForCreationDto.BudgetGroupId;

            if (!_repo.BudgetGroupByBudgetGroupIdExists(budgetGroupId))
            {
                return NotFound($"Unable to find budget group with ID '{budgetGroupId}'.");
            }

            Item item = _mapper.Map<Item>(itemForCreationDto);
            await _repo.AddItemAsync(item);
            ItemDto itemDto = _mapper.Map<ItemDto>(item);
            return CreatedAtRoute(nameof(GetItem), new { itemId = itemDto.ItemId }, itemDto);
        }

        // PUT: api/item/{itemId}
        [HttpPut("{itemId}")]
        public async Task<ActionResult<ItemDto>> PutItem(int itemId, ItemForUpdateDto itemForUpdateDto)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            _mapper.Map(itemForUpdateDto, item);
            await _repo.UpdateItemAsync(item);
            ItemDto itemDto = _mapper.Map<ItemDto>(item);
            return CreatedAtRoute(nameof(GetItem), new { itemId = itemDto.ItemId }, itemDto);
        }

        // DELETE: api/item/{itemId}
        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ItemDto>> DeleteItem(int itemId)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            await _repo.DeleteItemByItemIdAsync(itemId);
            ItemDto itemDto = _mapper.Map<ItemDto>(item);
            return Ok(itemDto);
        }
    }
}