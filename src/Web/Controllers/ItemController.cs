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
        private readonly IGroupRepository _groupRepo;
        private readonly IMapper _mapper;
        public ItemController(IItemRepository repo, IGroupRepository groupRepo, IMapper mapper)
        {
            _repo = repo;
            _groupRepo = groupRepo;
            _mapper = mapper;
        }

        // GET: api/budget/{budgetId}/group/{groupId}/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems([FromRoute] int groupId)
        {
            if (!_groupRepo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            IEnumerable<Item> item = await _repo.GetItemsAsync();

            return Ok(_mapper.Map<IEnumerable<ItemDto>>(item));
        }

        // GET: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpGet("{itemId}", Name = "GetItem")]
        public async Task<ActionResult<ItemDto>> GetItem([FromRoute] int groupId, int itemId)
        {
            if (!_groupRepo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            if (!_repo.ItemByItemIdExists(itemId))
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);

            return Ok(_mapper.Map<ItemDto>(item));
        }

        // POST: api/budget/{budgetId}/group/{groupId}/item
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem([FromRoute] int groupId, ItemForCreationDto itemForCreationDto)
        {
            if (!_groupRepo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            Item item = _mapper.Map<Item>(itemForCreationDto);
            await _repo.AddItemAsync(groupId, item);

            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            return CreatedAtRoute(nameof(GetItem), new { groupId = item.GroupId, itemId = itemDto.ItemId }, itemDto);
        }

        // PUT: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpPut("{itemId}")]
        public async Task<ActionResult<ItemDto>> PutItem([FromRoute] int groupId, int itemId, ItemForUpdateDto itemForUpdateDto)
        {
            if (!_groupRepo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            Item item = await _repo.GetItemByItemIdAsync(itemId);

            if (item == null)
            {
                return NotFound($"Unable to find item with ID '{itemId}'.");
            }

            _mapper.Map(itemForUpdateDto, item);

            await _repo.UpdateItemAsync(item);

            ItemDto itemDto = _mapper.Map<ItemDto>(item);

            return CreatedAtRoute(nameof(GetItem), new { groupId = item.GroupId, itemId = itemDto.ItemId }, itemDto);
        }

        // DELETE: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ItemDto>> DeleteItem([FromRoute] int groupId, int itemId)
        {
            if (!_groupRepo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

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