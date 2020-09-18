using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _repo;
        public GroupController(IGroupRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Group
        [HttpGet]
        public async Task<ActionResult<List<Group>>> GetGroups()
        {
            IReadOnlyList<Group> group = await _repo.GetGroupsAsync();

            return Ok(group);
        }

        // GET: api/Group/groupId
        [HttpGet("{groupId}")]
        public async Task<ActionResult<Group>> GetGroup(int groupId)
        {
            Group group = await _repo.GetGroupByIdAsync(groupId);

            if (group == null)
            {
                return NotFound();
            }

            return Ok(group);
        }

        // POST: api/Group
        [HttpPost]
        public async Task<ActionResult<Group>> PostGroup(Group group)
        {
            await _repo.PostGroupAsync(group);

            return Ok(group);
        }

        // PUT: api/Group/groupId
        [HttpPut("{groupId}")]
        public async Task<ActionResult<Group>> PutGroup(int groupId, Group group)
        {
            await _repo.UpdateGroupAsync(groupId, group);

            return Ok(group);
        }

        // DELETE: api/ApiWithActions/groupId
        [HttpDelete("{groupId}")]
        public async Task<ActionResult<Group>> DeleteGroup(int groupId)
        {
            await _repo.DeleteGroupByIdAsync(groupId);

            return Ok();
        }
    }
}
