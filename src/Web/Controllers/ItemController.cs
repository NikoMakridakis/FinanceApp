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
        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemDto>> GetItem(int itemId)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            return Ok(_mapper.Map<ItemDto>(item));
        }
    }
}