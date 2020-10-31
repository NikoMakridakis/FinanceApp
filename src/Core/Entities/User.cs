using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User : IdentityUser<int>
    {
        public decimal MonthlyIncome { get; set; }
        public string FullName { get; set; }

        public List<BudgetGroup> BudgetGroups { get; set; }
    }
}


