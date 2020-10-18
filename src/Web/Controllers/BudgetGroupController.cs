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
using Web.Extensions;
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
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<BudgetGroupDto>>> GetBudgetGroups()
        {
            string email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the email '{email}'.");
                return NotFound();
            }

            int userId = user.Id;

            IReadOnlyList<BudgetGroup> budgetGroups = await _repo.GetBudgetGroupsForUserAsync(userId);

            _logger.LogInformation($"Returned all BudgetGroups associated with the user Id '{userId}'.");
            return Ok(_mapper.Map<IReadOnlyList<BudgetGroupDto>>(budgetGroups));
            
        }

        // GET: api/budgetGroup/{budgetGroupId}
        [HttpGet("{budgetGroupId}", Name = "GetBudgetGroupForUser")]
        public async Task<ActionResult<BudgetGroupDto>> GetBudgetGroupForUser(int budgetGroupId)
        {
            string email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the email '{email}'.");
                return NotFound();
            }

            int userId = user.Id;

            bool budgetGroupExistsForUser = await _repo.BudgetGroupByIdForUserExists(budgetGroupId, userId);

            if (!budgetGroupExistsForUser)
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}' for user with the user Id '{userId}'.");
                return NotFound();
            }

            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdForUserAsync(budgetGroupId, userId);

            _logger.LogInformation($"Returned BudgetGroup with the BudgetGroupId '{budgetGroupId}'from the user with user Id '{userId}'.");
            return Ok(_mapper.Map<BudgetGroupDto>(budgetGroup));
        }

        // POST: api/budgetGroup
        [HttpPost]
        public async Task<ActionResult> PostBudgetGroup(BudgetGroupForCreationDto budgetGroupForCreationDto)
        {
            string email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the email '{email}'.");
                return NotFound();
            }

            int userId = user.Id;

            string budgetGroupTitle = budgetGroupForCreationDto.BudgetGroupTitle;

            BudgetGroup budgetGroupForUser = new BudgetGroup(userId, budgetGroupTitle);
            await _repo.AddBudgetGroupForUserAsync(budgetGroupForUser);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroupForUser);

            _logger.LogInformation($"Created BudgetGroup with the BudgetGroupId '{budgetGroupDto.BudgetGroupId}'for user with user Id '{userId}'.");
            return CreatedAtRoute(nameof(GetBudgetGroupForUser), new { budgetGroupId = budgetGroupDto.BudgetGroupId }, budgetGroupDto);
        }

        // PUT: api/budgetGroup/{budgetGroupId}
        [HttpPut("{budgetGroupId}")]
        public async Task<ActionResult> PutBudgetGroup(int budgetGroupId, BudgetGroupForUpdateDto budgetGroupForUpdateDto)
        {
            string email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the email '{email}'.");
                return NotFound();
            }

            int userId = user.Id;

            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdForUserAsync(budgetGroupId, userId);

            if (budgetGroup == null)
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}' for user with user Id '{userId}'.");
                return NotFound();
            }

            _mapper.Map(budgetGroupForUpdateDto, budgetGroup);
            await _repo.UpdateBudgetGroupForUserAsync(budgetGroup);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);

            _logger.LogInformation($"Updated BudgetGroup with the BudgetGroupId '{budgetGroupId}' for user with the user Id '{userId}'.");
            return CreatedAtRoute(nameof(GetBudgetGroupForUser), new { budgetGroupId = budgetGroupDto.BudgetGroupId }, budgetGroupDto);
        }

        // DELETE: api/budgetGroup/{budgetGroupId}
        [HttpDelete("{budgetGroupId}")]
        public async Task<ActionResult> DeleteBudgetGroup(int budgetGroupId)
        {
            string email = HttpContext.User.RetrieveEmailFromClaimsPrincipal();

            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                _logger.LogError($"Unable to find User with the email '{email}'.");
                return NotFound();
            }

            int userId = user.Id;

            BudgetGroup budgetGroup = await _repo.GetBudgetGroupByIdForUserAsync(budgetGroupId, userId);

            if (budgetGroup == null)
            {
                _logger.LogError($"Unable to find BudgetGroup with the BudgetGroupId '{budgetGroupId}' for user with the user Id '{userId}'.");
                return NotFound();
            }

            await _repo.DeleteBudgetGroupByIdForUserAsync(budgetGroupId);
            BudgetGroupDto budgetGroupDto = _mapper.Map<BudgetGroupDto>(budgetGroup);

            _logger.LogInformation($"Deleted BudgetGroup with the BudgetGroupId '{budgetGroupId}' for user with the user Id '{userId}'.");
            return NoContent();
        }
    }
}
