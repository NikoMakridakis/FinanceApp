using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class BudgetGroup
    {
        public int BudgetGroupId { get; set; }
        public string BudgetGroupTitle { get; set; }
        public User User { get; set; }
        public List<Item> Items { get; set; }

        public BudgetGroup()
        {
        }

        public BudgetGroup(string budgetGroupTitle)
        {
            BudgetGroupTitle = budgetGroupTitle;
        }
    }
}
