using System;

namespace Core.Entities
{
    public class Fund
    {
        public int FundId { get; set; }
        public string Label { get; set; }
        public decimal GoalAmount { get; set; }
        public decimal CurrentAmount { get; set; }
        public decimal MonthlyFundAmount { get; set; }
        public DateTime GoalDate { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}
