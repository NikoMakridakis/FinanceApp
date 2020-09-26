using System;

namespace Web.Models
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        public DateTime TodaysDate { get; set; }
        public int DaysLeftInMonth { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlySpending { get; set; }
    }
}
