using System.Collections.Generic;

namespace Core.Entities
{
    public class BudgetGroup
    {
        public int BudgetGroupId { get; set; }
        public int UserId { get; set; }
        public string BudgetGroupTitle { get; set; }
        public List<Item> Items { get; set; }

        public BudgetGroup()
        {
        }

        public BudgetGroup(int userId, string budgetGroupTitle)
        {
            UserId = userId;
            BudgetGroupTitle = budgetGroupTitle;
        }
    }
}
