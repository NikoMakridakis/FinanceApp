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
            if (!_repo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound();
            }

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
        public async Task<ActionResult> PutBudget(int budgetId, BudgetForUpdateDto budgetForUpdateDto)
        {
            if (!_repo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound();
            }

            Budget budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            if (budget == null)
            {
                return NotFound();
            }

            _mapper.Map(budgetForUpdateDto, budget);

            await _repo.UpdateBudgetAsync(budget);

            BudgetDto budgetDto = _mapper.Map<BudgetDto>(budget);

            return CreatedAtRoute(nameof(GetBudget), new { budgetId = budgetDto.BudgetId }, budgetDto);
        }

        // DELETE: api/budget/{budgetId}
        [HttpDelete("{budgetId}")]
        public async Task<ActionResult> DeleteBudget(int budgetId)
        {
            if (!_repo.BudgetByBudgetIdExists(budgetId))
            {
                return NotFound();
            }

            Budget budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            if (budget == null)
            {
                return NotFound();
            }

            await _repo.DeleteBudgetByBudgetIdAsync(budgetId);

            BudgetDto budgetDto = _mapper.Map<BudgetDto>(budget);

            return Ok(budgetDto);
        }
    }
}