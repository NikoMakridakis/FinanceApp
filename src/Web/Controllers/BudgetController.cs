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
    public class BudgetController : ControllerBase
    {
        private readonly IFinanceAppRepository _repo;
        private readonly IMapper _mapper;
        public BudgetController(IFinanceAppRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/budget
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BudgetDto>>> GetBudgets()
        {
            IEnumerable<Budget> budget = await _repo.GetBudgetsAsync();

            return Ok(_mapper.Map<IEnumerable<BudgetDto>>(budget));
        }

        // GET: api/budget/{budgetId}
        [HttpGet("{budgetId}", Name = "GetBudget")]
        public async Task<ActionResult<BudgetDto>> GetBudget(int budgetId)
        {
            Budget budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            return Ok(_mapper.Map<BudgetDto>(budget));
        }

        // POST: api/budget
        [HttpPost]
        public async Task<ActionResult<BudgetDto>> PostGroup(BudgetForCreationDto budgetForCreationDto)
        {
            Budget budget = _mapper.Map<Budget>(budgetForCreationDto);
            await _repo.AddBudgetAsync(budget);

            BudgetDto budgetDto = _mapper.Map<BudgetDto>(budget);

            return CreatedAtRoute(nameof(GetBudget), new { budgetId = budgetDto.BudgetId }, budgetDto);
        }

        // PUT: api/budget/{budgetId}
        [HttpPut("{budgetId}")]
        public async Task<ActionResult<BudgetDto>> PutBudget(int budgetId, BudgetForCreationDto budgetCreateDto)
        {
            Budget budget = _mapper.Map<Budget>(budgetCreateDto);
            await _repo.UpdateBudgetAsync(budgetId, budget);

            BudgetDto budgetReadDto = _mapper.Map<BudgetDto>(budget);

            return CreatedAtRoute(nameof(GetBudget), new { budgetId = budgetReadDto.BudgetId }, budgetReadDto);
        }

        // DELETE: api/budget/{budgetId}
        [HttpDelete("{budgetId}")]
        public async Task<ActionResult<Budget>> DeleteBudget(int budgetId)
        {
            Budget budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            if (budget == null)
            {
                return NotFound();
            }
            
            await _repo.DeleteGroupByGroupIdAsync(budgetId);

            return NoContent();
        }
    }
}