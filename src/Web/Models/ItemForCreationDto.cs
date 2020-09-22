using System;

namespace Web.Models
{
    public class ItemForCreationDto
    {
        public int GroupId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public bool IsIncome { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
