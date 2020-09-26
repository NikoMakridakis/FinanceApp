using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/budget/{budgetId}/group/{groupId}/item")]
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

        // GET: api/budget/{budgetId}/group/{groupId}/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            IEnumerable<Item> item = await _repo.GetItemsAsync();

            return Ok(_mapper.Map<IEnumerable<ItemDto>>(item));
        }

        // GET: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpGet("{itemId}", Name = "GetItem")]
        public async Task<ActionResult<ItemDto>> GetItem(int itemId)
        {
            if (!_repo.ItemByItemIdExists(itemId))
            {
                return NotFound();
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);

            return Ok(_mapper.Map<ItemDto>(item));
        }

        // POST: api/budget/{budgetId}/group/{groupId}/item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(ItemForCreationDto itemForCreationDto)
        {
            Item item = _mapper.Map<Item>(itemForCreationDto);
            await _repo.AddItemAsync(item);

            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            return CreatedAtRoute(nameof(GetItem), new { itemId = itemDto.ItemId }, itemDto);
        }

        // PUT: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpPut("{itemId}")]
        public async Task<ActionResult> PutItem(int itemId, ItemForUpdateDto itemForUpdateDto)
        {
            if (!_repo.ItemByItemIdExists(itemId))
            {
                return NotFound();
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            _mapper.Map(itemForUpdateDto, item);

            await _repo.UpdateItemAsync(item);

            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            return CreatedAtRoute(nameof(GetItem), new { itemId = itemDto.ItemId }, itemDto);
        }

        // DELETE: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpDelete("{itemId}")]
        public async Task<ActionResult> DeleteItem(int itemId)
        {
            if (!_repo.ItemByItemIdExists(itemId))
            {
                return NotFound();
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound();
            }

            await _repo.DeleteItemByItemIdAsync(itemId);

            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            return Ok(itemDto);
        }
    }
}