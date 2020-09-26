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
        private readonly IMapper _mapper;
        public GroupController(IGroupRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/budget/{budgetId}/group
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            IEnumerable<Group> group = await _repo.GetGroupsAsync();

            return Ok(_mapper.Map<IEnumerable<GroupDto>>(group));
        }

        // GET: api/budget/{budgetId}/group/{groupId}
        [HttpGet("{groupId}", Name = "GetGroup")]
        public async Task<ActionResult<GroupDto>> GetGroup(int groupId)
        {
            if (!_repo.GroupByGroupIdExists(groupId))
            {
                return NotFound();
            }

            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            return Ok(_mapper.Map<GroupDto>(group));
        }

        // POST: api/budget/{budgetId}/group
        [HttpPost]
        public async Task<ActionResult<GroupDto>> PostGroup(GroupForCreationDto groupForCreationDto)
        {
            Group group = _mapper.Map<Group>(groupForCreationDto);
            await _repo.AddGroupAsync(group);

            GroupDto groupDto = _mapper.Map<GroupDto>(group);

            return CreatedAtRoute(nameof(GetGroup), new { groupId = groupDto.GroupId }, groupDto);
        }

        // PUT: api/budget/{budgetId}/group/{groupId}
        [HttpPut("{groupId}")]
        public async Task<ActionResult> PutGroup(int groupId, GroupForUpdateDto groupForUpdateDto)
        {
            if (!_repo.GroupByGroupIdExists(groupId))
            {
                return NotFound();
            }

            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }

            _mapper.Map(groupForUpdateDto, group);

            await _repo.UpdateGroupAsync(group);

            GroupDto groupDto = _mapper.Map<GroupDto>(group);

            return CreatedAtRoute(nameof(GetGroup), new { groupId = groupDto.GroupId }, groupDto);
        }

        // DELETE: api/budget/{budgetId}/group/{groupId}
        [HttpDelete("{groupId}")]
        public async Task<ActionResult> DeleteGroup(int groupId)
        {
            if (!_repo.GroupByGroupIdExists(groupId))
            {
                return NotFound();
            }

            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }

            await _repo.DeleteGroupByGroupIdAsync(groupId);

            GroupDto groupDto = _mapper.Map<GroupDto>(group);

            return Ok(groupDto);
        }
    }
}
