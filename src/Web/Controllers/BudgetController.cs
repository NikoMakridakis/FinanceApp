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
        public async Task<ActionResult<IEnumerable<BudgetReadDto>>> GetBudgets()
        {
            IEnumerable<Budget> budget = await _repo.GetBudgetsAsync();

            return Ok(_mapper.Map<IEnumerable<BudgetReadDto>>(budget));
        }

        // GET: api/budget/{budgetId}
        [HttpGet("{budgetId}", Name = "GetBudget")]
        public async Task<ActionResult<BudgetReadDto>> GetBudget(int budgetId)
        {
            Budget budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            return Ok(_mapper.Map<BudgetReadDto>(budget));
        }

        // POST: api/budget
        [HttpPost]
        public async Task<ActionResult<BudgetReadDto>> PostGroup(BudgetCreateDto budgetCreateDto)
        {
            Budget budget = _mapper.Map<Budget>(budgetCreateDto);
            await _repo.AddBudgetAsync(budget);

            BudgetReadDto budgetReadDto = _mapper.Map<BudgetReadDto>(budget);

            return CreatedAtRoute(nameof(GetBudget), new { budgetId = budgetReadDto.BudgetId }, budgetReadDto);
        }

        // PUT: api/budget/{budgetId}
        [HttpPut("{budgetId}")]
        public async Task<ActionResult<BudgetReadDto>> PutBudget(int budgetId, BudgetCreateDto budgetCreateDto)
        {
            Budget budget = _mapper.Map<Budget>(budgetCreateDto);
            await _repo.UpdateBudgetAsync(budgetId, budget);

            BudgetReadDto budgetReadDto = _mapper.Map<BudgetReadDto>(budget);

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