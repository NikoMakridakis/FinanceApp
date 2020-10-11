using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<ItemController> _logger;
        private readonly IMapper _mapper;
        public ItemController(IItemRepository repo, ILogger<ItemController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/item
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ItemDto>>> GetItems([FromQuery] int budgetGroupId)
        {
            if (!_repo.BudgetGroupByBudgetGroupIdExists(budgetGroupId))
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
                return NotFound();
            }

            IReadOnlyList<Item> item = await _repo.GetItemsAsync(budgetGroupId);

            _logger.LogInformation($"Returned all Items associated with the BudgetGroupId '{budgetGroupId}'.");
            return Ok(_mapper.Map<IReadOnlyList<ItemDto>>(item));
        }

        // GET: api/item/{itemId}
        [HttpGet("{itemId}", Name = "GetItem")]
        public async Task<ActionResult<ItemDto>> GetItem(int itemId)
        {
            if (!_repo.ItemByItemIdExists(itemId))
            {
                _logger.LogError($"Unable to find Item with the ItemId '{itemId}'.");
                return NotFound();
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);

            _logger.LogInformation($"Returned Item from the ItemId '{itemId}'.");
            return Ok(_mapper.Map<ItemDto>(item));
        }

        // POST: api/item
        [HttpPost]
        public async Task<ActionResult> PostItem(ItemForCreationDto itemForCreationDto)
        {
            int budgetGroupId = itemForCreationDto.BudgetGroupId;

            if (!_repo.BudgetGroupByBudgetGroupIdExists(budgetGroupId))
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
                return NotFound();
            }

            Item item = _mapper.Map<Item>(itemForCreationDto);
            await _repo.AddItemAsync(item);
            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            _logger.LogInformation($"Created Item with the ItemId '{itemDto.ItemId}'.");
            return CreatedAtRoute(nameof(GetItem), new { itemId = itemDto.ItemId }, itemDto);
        }

        // PUT: api/item/{itemId}
        [HttpPut("{itemId}")]
        public async Task<ActionResult> PutItem(int itemId, ItemForUpdateDto itemForUpdateDto)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            _mapper.Map(itemForUpdateDto, item);
            await _repo.UpdateItemAsync(item);
            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            _logger.LogInformation($"Updated Item with the ItemId '{itemDto.ItemId}'.");
            return CreatedAtRoute(nameof(GetItem), new { itemId = itemDto.ItemId }, itemDto);
        }

        // DELETE: api/item/{itemId}
        [HttpDelete("{itemId}")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            await _repo.DeleteItemByItemIdAsync(itemId);
            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            _logger.LogInformation($"Deleted Item with the ItemId '{itemId}'.");
            return NoContent();
        }
    }
}