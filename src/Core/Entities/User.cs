using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class User : IdentityUser
    {
        public decimal MonthlyIncome { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BudgetGroup> BudgetGroups { get; set; }
    }
}


