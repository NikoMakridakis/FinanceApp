using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _repo;
        public BudgetController(IBudgetRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Budget
        [HttpGet]
        public async Task<ActionResult<List<Budget>>> GetBudgets()
        {
            IReadOnlyList<Budget> budget = await _repo.GetBudgetsAsync();

            return Ok(budget);
        }

        // GET: api/Budget/budgetId
        [HttpGet("{budgetId}")]
        public async Task<ActionResult<Budget>> GetBudget(int budgetId)
        {
            Budget budget = await _repo.GetBudgetByBudgetIdAsync(budgetId);

            return Ok(budget);
        }
    }
}