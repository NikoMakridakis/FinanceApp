﻿namespace Web.Models
{
    public class ItemDto
    {
        public int ItemId { get; set; }
        public int BudgetGroupId { get; set; }
        public string ItemTitle { get; set; }
        public decimal ItemMontlyAmount { get; set; }
    }
}
