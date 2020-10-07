using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public BudgetGroupController(IBudgetGroupRepository repo, UserManager<User> userManager, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _mapper = mapper;
        }

        // GET: api/budgetGroup
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BudgetGroupDto>>> GetBudgetGroups([FromQuery] int? userId)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (userId != null && user == null)
            {
                return NotFound($"Unable to find user with ID '{userId}'.");
            }

            IReadOnlyList<BudgetGroup> budgetGroups = await _repo.GetBudgetGroupsAsync(userId);

            return Ok(_mapper.Map<IReadOnlyList<BudgetGroupDto>>(budgetGroups));
        }

        // GET: api/budgetGroup/{budgetGroupId}
        [HttpGet("{budgetGroupId}", Name = "GetBudgetGroup")]
        public async Task<ActionResult<BudgetGroupDto>> GetBudgetGroup(int budgetGroupId)
        {
            if (!_repo.BudgetGroupByIdExists(budgetGroupId))
            {
                return NotFound($"Unable to find budget group with ID '{budgetGroupId}'.");
            }

            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdAsync(budgetGroupId);
            return Ok(_mapper.Map<BudgetGroupDto>(budgetGroup));
        }

        // POST: api/budgetGroup
        [HttpPost]
        public async Task<ActionResult<BudgetGroupDto>> PostBudgetGroup(BudgetGroupForCreationDto budgetGroupForCreationDto)
        {
            int userId = budgetGroupForCreationDto.UserId;

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return NotFound($"Unable to find user with ID '{userId}'.");
            }

            BudgetGroup budgetGroup = _mapper.Map<BudgetGroup>(budgetGroupForCreationDto);
            await _repo.AddBudgetGroupAsync(budgetGroup);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);
            return CreatedAtRoute(nameof(GetBudgetGroup), new { budgetGroupId = budgetGroupDto.BudgetGroupId }, budgetGroupDto);
        }

        // PUT: api/budgetGroup/{budgetGroupId}
        [HttpPut("{budgetGroupId}")]
        public async Task<ActionResult<BudgetGroupDto>> PutBudgetGroup(int budgetGroupId, BudgetGroupForUpdateDto budgetGroupForUpdateDto)
        {
            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdAsync(budgetGroupId);

            if (budgetGroup == null)
            {
                return NotFound($"Unable to find budget group with ID '{budgetGroupId}'.");
            }

            _mapper.Map(budgetGroupForUpdateDto, budgetGroup);
            await _repo.UpdateBudgetGroupAsync(budgetGroup);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);
            return CreatedAtRoute(nameof(GetBudgetGroup), new { budgetGroupId = budgetGroupDto.BudgetGroupId }, budgetGroupDto);
        }

        // DELETE: api/budgetGroup/{budgetGroupId}
        [HttpDelete("{budgetGroupId}")]
        public async Task<ActionResult<BudgetGroupDto>> DeleteBudgetGroup(int budgetGroupId)
        {
            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdAsync(budgetGroupId);

            if (budgetGroup == null)
            {
                return NotFound($"Unable to find budget group with ID '{budgetGroupId}'.");
            }

            await _repo.DeleteBudgetGroupByIdAsync(budgetGroupId);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);
            return Ok(budgetGroupDto);
        }
    }
}
