using System.Collections.Generic;

namespace Core.Entities
{
    public class User
    {
        public int BudgetId { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlySpending { get; set; }
        public List<BudgetGroup> Groups { get; set; } = new List<BudgetGroup>();

        public User()
        {
        }
        public User(decimal monthlyIncome, decimal monthlySpending)
        {
            MonthlyIncome = monthlyIncome;
            MonthlySpending = monthlySpending;
        }
    }
}


