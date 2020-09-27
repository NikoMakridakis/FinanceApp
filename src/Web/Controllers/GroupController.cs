using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/budget/{budgetId}/group")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _repo;
        private readonly IBudgetRepository _budgetRepo;
        private readonly IMapper _mapper;
        public GroupController(IGroupRepository repo, IBudgetRepository budgetRepo, IMapper mapper)
        {
            _repo = repo;
            _budgetRepo = budgetRepo;
            _mapper = mapper;
        }

        // GET: api/budget/{budgetId}/group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups([FromRoute] int budgetId)
        {
            if (!_budgetRepo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            IEnumerable<Group> group = await _repo.GetGroupsAsync();

            return Ok(_mapper.Map<IEnumerable<GroupDto>>(group));
        }

        // GET: api/budget/{budgetId}/group/{groupId}
        [HttpGet("{groupId}", Name = "GetGroup")]
        public async Task<ActionResult<GroupDto>> GetGroup([FromRoute] int budgetId, int groupId)
        {
            if (!_budgetRepo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            if (!_repo.GroupByGroupIdExists(groupId))
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            return Ok(_mapper.Map<GroupDto>(group));
        }

        // POST: api/budget/{budgetId}/group
        [HttpPost]
        public async Task<ActionResult<GroupDto>> PostGroup([FromRoute] int budgetId, GroupForCreationDto groupForCreationDto)
        {
            if (!_budgetRepo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            Group group = _mapper.Map<Group>(groupForCreationDto);
            await _repo.AddGroupAsync(budgetId, group);

            GroupDto groupDto = _mapper.Map<GroupDto>(group);

            return CreatedAtRoute(nameof(GetGroup), new { budgetId = group.BudgetId, groupId = groupDto.GroupId }, groupDto);
        }

        // PUT: api/budget/{budgetId}/group/{groupId}
        [HttpPut("{groupId}")]
        public async Task<ActionResult<GroupDto>> PutGroup([FromRoute] int budgetId, int groupId, GroupForUpdateDto groupForUpdateDto)
        {
            if (!_budgetRepo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            _mapper.Map(groupForUpdateDto, group);

            await _repo.UpdateGroupAsync(group);

            GroupDto groupDto = _mapper.Map<GroupDto>(group);

            return CreatedAtRoute(nameof(GetGroup), new { budgetId = group.BudgetId, groupId = groupDto.GroupId }, groupDto);
        }

        // DELETE: api/budget/{budgetId}/group/{groupId}
        [HttpDelete("{groupId}")]
        public async Task<ActionResult<GroupDto>> DeleteGroup([FromRoute] int budgetId, int groupId)
        {
            if (!_budgetRepo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound($"Unable to find group with ID '{groupId}'.");
            }

            await _repo.DeleteGroupByGroupIdAsync(groupId);

            GroupDto groupDto = _mapper.Map<GroupDto>(group);

            return Ok(groupDto);
        }
    }
}
