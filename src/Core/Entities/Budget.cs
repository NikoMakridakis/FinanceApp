using Core.Extensions;
using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public DateTime TodaysDate { get; set; }
        public int DaysLeftInMonth { get; set; }
        public decimal MonthlyIncome { get; set; }
        public decimal MonthlySpending { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public Budget()
        {
        }
        public Budget(DateTime todaysDate, decimal monthlyIncome, decimal monthlySpending)
        {
            TodaysDate = todaysDate;
            DaysLeftInMonth = TodaysDate.GetCurrentDaysLeftInMonth();
            MonthlyIncome = monthlyIncome;
            MonthlySpending = monthlySpending;
        }
    }
}


