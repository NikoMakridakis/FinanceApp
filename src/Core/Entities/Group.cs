﻿using System.Collections.Generic;

namespace Core.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public int BudgetId { get; set; }
        public decimal GroupMonthlyTotal { get; set; }
        public string GroupTitle { get; set; }
        public List<Item> Items { get; set; }

        public Group()
        {
        }

        public Group(int budgetId, string groupTitle)
        {
            BudgetId = budgetId;
            GroupTitle = groupTitle;
        }
    }
}
