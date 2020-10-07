using Core.Entities;
using System.Collections.Generic;

namespace Web.Models
{
    public class UserDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BudgetGroup> BudgetGroups { get; set; } =

        new List<BudgetGroup>()
        {
            new BudgetGroup(1, "Housing"),
            new BudgetGroup(1, "Transportation"),
            new BudgetGroup(1, "Food"),
            new BudgetGroup(1, "Personal"),
            new BudgetGroup(1, "Health"),
            new BudgetGroup(1, "Insurance"),
            new BudgetGroup(1, "Debt"),
        };
    }
}
