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
    public class BudgetGroupController : ControllerBase
    {
        private readonly IBudgetGroupRepository _repo;
        private readonly IMapper _mapper;
        public BudgetGroupController(IBudgetGroupRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetGroupDto>>> GetGroups([FromQuery] int? budgetId)
        {
            IEnumerable<BudgetGroup> group = await _repo.GetGroupsAsync(budgetId);
            return Ok(_mapper.Map<IEnumerable<BudgetGroupDto>>(group));
        }

        // GET: api/group/{groupId}
        [HttpGet("{groupId}", Name = "GetGroup")]
        public async Task<ActionResult<BudgetGroupDto>> GetGroup([FromRoute] int budgetId, int groupId)
        {
            if (!_repo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            BudgetGroup group = await _repo.GetGroupByGroupIdAsync(groupId);
            return Ok(_mapper.Map<BudgetGroupDto>(group));
        }

        // POST: api/group
        [HttpPost]
        public async Task<ActionResult<BudgetGroupDto>> PostGroup([FromRoute] int budgetId, BudgetGroupForCreationDto groupForCreationDto)
        {
            if (!_repo.BudgetByBudgetIdExists(groupForCreationDto.BudgetId))
            {
                return NotFound($"Unable to find budget with ID '{groupForCreationDto.BudgetId}'.");
            }

            BudgetGroup group = _mapper.Map<BudgetGroup>(groupForCreationDto);
            await _repo.AddGroupAsync(group);
            BudgetGroupDto groupDto = _mapper.Map<BudgetGroupDto>(group);
            return CreatedAtRoute(nameof(GetGroup), new { groupId = groupDto.GroupId }, groupDto);
        }

        // PUT: api/group/{groupId}
        [HttpPut("{groupId}")]
        public async Task<ActionResult<BudgetGroupDto>> PutGroup([FromRoute] int budgetId, int groupId, BudgetGroupForUpdateDto groupForUpdateDto)
        {
            BudgetGroup group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            _mapper.Map(groupForUpdateDto, group);
            await _repo.UpdateGroupAsync(group);
            BudgetGroupDto groupDto = _mapper.Map<BudgetGroupDto>(group);
            return CreatedAtRoute(nameof(GetGroup), new { budgetId = group.BudgetId, groupId = groupDto.GroupId }, groupDto);
        }

        // DELETE: api/group/{groupId}
        [HttpDelete("{groupId}")]
        public async Task<ActionResult<BudgetGroupDto>> DeleteGroup([FromRoute] int budgetId, int groupId)
        {
            BudgetGroup group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            await _repo.DeleteGroupByGroupIdAsync(groupId);
            BudgetGroupDto groupDto = _mapper.Map<BudgetGroupDto>(group);
            return Ok(groupDto);
        }
    }
}
