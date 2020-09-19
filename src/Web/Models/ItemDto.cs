using System;

namespace Web.Models
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public bool isIncome { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
