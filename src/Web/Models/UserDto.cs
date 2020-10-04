using Core.Entities;
using System.Collections.Generic;

namespace Web.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BudgetGroup> BudgetGroups { get; set; }
    }
}
