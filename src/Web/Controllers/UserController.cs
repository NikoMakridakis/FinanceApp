using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("api/budget")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/budget
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetBudgets()
        {
            IEnumerable<User> budget = await _repo.GetBudgetsAsync();

            return Ok(_mapper.Map<IEnumerable<UserDto>>(budget));
        }

        // GET: api/budget/{budgetId}
        [HttpGet("{budgetId}", Name = "GetBudget")]
        public async Task<ActionResult<UserDto>> GetBudget(int budgetId)
        {
            if (!_repo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            User budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            return Ok(_mapper.Map<UserDto>(budget));
        }

        // POST: api/budget
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostBudget(UserForCreationDto budgetForCreationDto)
        {
            User budget = _mapper.Map<User>(budgetForCreationDto);
            await _repo.AddBudgetAsync(budget);

            UserDto budgetDto = _mapper.Map<UserDto>(budget);

            return CreatedAtRoute(nameof(GetBudget), new { budgetId = budgetDto.BudgetId }, budgetDto);
        }

        // PUT: api/budget/{budgetId}
        [HttpPut("{budgetId}")]
        public async Task<ActionResult<UserDto>> PutBudget(int budgetId, UserForUpdateDto budgetForUpdateDto)
        {
            User budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            if (budget == null)
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            _mapper.Map(budgetForUpdateDto, budget);

            await _repo.UpdateBudgetAsync(budget);

            UserDto budgetDto = _mapper.Map<UserDto>(budget);

            return CreatedAtRoute(nameof(GetBudget), new { budgetId = budgetDto.BudgetId }, budgetDto);
        }

        // DELETE: api/budget/{budgetId}
        [HttpDelete("{budgetId}")]
        public async Task<ActionResult<UserDto>> DeleteBudget(int budgetId)
        {
            User budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            if (budget == null)
            {
                return NotFound($"Unable to find budget with ID '{budgetId}'.");
            }

            await _repo.DeleteBudgetByBudgetIdAsync(budgetId);

            UserDto budgetDto = _mapper.Map<UserDto>(budget);

            return Ok(budgetDto);
        }
    }
}