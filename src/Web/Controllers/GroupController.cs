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
        public async Task<ActionResult<List<Budget>>> GetGroups()
        {
            IReadOnlyList<Group> group = await _repo.GetGroupsAsync();

            return Ok(group);
        }

        // GET: api/Group/groupId
        [HttpGet("{groupId}")]
        public async Task<ActionResult<Budget>> GetGroup(int groupId)
        {
            Group group = await _repo.GetGroupByIdAsync(groupId);

            return Ok(group);
        }

        // POST: api/Group
        [HttpPost]
        public async Task<ActionResult<Budget>> PostGroup(int groupId)
        {
            _repo.GetGroupByIdAsync(groupId);

            return Ok(group);
        }

        //// PUT: api/Group/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
