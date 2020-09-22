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
        private readonly IFinanceAppRepository _repo;
        private readonly IMapper _mapper;
        public GroupController(IFinanceAppRepository repo, IMapper mapper)
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
        [HttpGet("{groupId}")]
        public async Task<ActionResult<GroupDto>> GetGroup(int groupId)
        {
            Group group = await _repo.GetGroupByGroupIdAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<GroupDto>(group));
        }

        // POST: api/budget/{budgetId}/group
        [HttpPost]
        public async Task<ActionResult<GroupDto>> PostGroup(Group group)
        {
            await _repo.AddGroupAsync(group);

            return Ok(group);
        }

        // PUT: api/budget/{budgetId}/group/{groupId}
        //[HttpPut("{groupId}")]
        //public async Task<ActionResult> PutGroup(int groupId, Group group)
        //{
        //    _repo.UpdateGroupAsync(groupId, group);

        //    return Ok(group);
        //}

        // DELETE: api/budget/{budgetId}/group/{groupId}
        //[HttpDelete("{groupId}")]
        //public async Task<ActionResult<Group>> DeleteGroup(int groupId)
        //{
        //    await _repo.DeleteGroupByGroupIdAsync(groupId);

        //    return Ok();
        //}
    }
}
