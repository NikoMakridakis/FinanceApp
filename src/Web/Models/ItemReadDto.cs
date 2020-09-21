using System;

namespace Web.Models
{
    public class ItemReadDto
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public bool isIncome { get; set; }
    }
}
