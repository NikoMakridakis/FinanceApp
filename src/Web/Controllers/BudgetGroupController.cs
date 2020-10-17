using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<BudgetGroupController> _logger;
        private readonly IMapper _mapper;
        public BudgetGroupController(IBudgetGroupRepository repo, UserManager<User> userManager, ILogger<BudgetGroupController> logger, IMapper mapper)
        {
            _repo = repo;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/budgetGroup
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BudgetGroupDto>>> GetBudgetGroups([FromQuery] int userId)
        {
            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the UserId '{userId}'.");
                return NotFound();
            }

            IReadOnlyList<BudgetGroup> budgetGroups = await _repo.GetBudgetGroupsAsync(userId);

            _logger.LogInformation($"Returned all BudgetGroups associated with the UserId '{userId}'.");
            return Ok(_mapper.Map<IReadOnlyList<BudgetGroupDto>>(budgetGroups));
            
        }

        // GET: api/budgetGroup/{budgetGroupId}
        [HttpGet("{budgetGroupId}", Name = "GetBudgetGroup")]
        public async Task<ActionResult<BudgetGroupDto>> GetBudgetGroup(int budgetGroupId)
        {
            if (!_repo.BudgetGroupByIdExists(budgetGroupId))
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
                return NotFound();
            }

            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdAsync(budgetGroupId);

            _logger.LogInformation($"Returned BudgetGroup from the BudgetGroupId '{budgetGroupId}'.");
            return Ok(_mapper.Map<BudgetGroupDto>(budgetGroup));
        }

        // POST: api/budgetGroup
        [HttpPost]
        public async Task<ActionResult> PostBudgetGroup(BudgetGroupForCreationDto budgetGroupForCreationDto)
        {
            int userId = budgetGroupForCreationDto.UserId;

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the UserId '{userId}'.");
                return NotFound();
            }

            BudgetGroup budgetGroup = _mapper.Map<BudgetGroup>(budgetGroupForCreationDto);
            await _repo.AddBudgetGroupAsync(budgetGroup);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);

            _logger.LogInformation($"Created BudgetGroup with the BudgetGroupId '{budgetGroupDto.BudgetGroupId}'.");
            return CreatedAtRoute(nameof(GetBudgetGroup), new { budgetGroupId = budgetGroupDto.BudgetGroupId }, budgetGroupDto);
        }

        // PUT: api/budgetGroup/{budgetGroupId}
        [HttpPut("{budgetGroupId}")]
        public async Task<ActionResult> PutBudgetGroup(int budgetGroupId, BudgetGroupForUpdateDto budgetGroupForUpdateDto)
        {
            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdAsync(budgetGroupId);

            if (budgetGroup == null)
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
                return NotFound();
            }

            _mapper.Map(budgetGroupForUpdateDto, budgetGroup);
            await _repo.UpdateBudgetGroupAsync(budgetGroup);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);

            _logger.LogInformation($"Updated BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
            return CreatedAtRoute(nameof(GetBudgetGroup), new { budgetGroupId = budgetGroupDto.BudgetGroupId }, budgetGroupDto);
        }

        // DELETE: api/budgetGroup/{budgetGroupId}
        [HttpDelete("{budgetGroupId}")]
        public async Task<ActionResult> DeleteBudgetGroup(int budgetGroupId)
        {
            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdAsync(budgetGroupId);

            if (budgetGroup == null)
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
                return NotFound();
            }

            await _repo.DeleteBudgetGroupByIdAsync(budgetGroupId);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);

            _logger.LogInformation($"Deleted BudgetGroup with the BudgetGroupId '{budgetGroupId}'.");
            return NoContent();
        }
    }
}
