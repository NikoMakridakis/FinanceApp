using System;

namespace Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
        public bool IsDone { get; set; }
        public bool IsIncome { get; set; }
    }
}
