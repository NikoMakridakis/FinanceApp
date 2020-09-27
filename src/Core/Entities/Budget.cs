using System.Collections.Generic;

namespace Core.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlySpending { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public Budget()
        {
        }
        public Budget(decimal monthlyIncome, decimal monthlySpending)
        {
            MonthlyIncome = monthlyIncome;
            MonthlySpending = monthlySpending;
        }
    }
}


