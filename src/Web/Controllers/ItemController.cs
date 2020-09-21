﻿using AutoMapper;
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
        private readonly IFinanceAppRepository _repo;
        private readonly IMapper _mapper;
        public ItemController(IFinanceAppRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/budget/{budgetId}/group/{groupId}/item
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemReadDto>>> GetItems()
        {
            IEnumerable<Item> item = await _repo.GetItemsAsync();

            return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(item));
        }

        // GET: api/budget/{budgetId}/group/{groupId}/item/{itemId}
        [HttpGet("{itemId}")]
        public async Task<ActionResult<ItemReadDto>> GetItem(int itemId)
        {
            Item item = await _repo.GetItemByItemIdAsync(itemId);

            return Ok(_mapper.Map<ItemReadDto>(item));
        }
    }
}