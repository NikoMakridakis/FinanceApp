using System.Collections.Generic;

namespace Core.Entities
{
    public class BudgetGroup
    {
        public int GroupId { get; set; }
        public int BudgetId { get; set; }
        public string GroupTitle { get; set; }
        public List<Item> Items { get; set; }

        public BudgetGroup()
        {
        }

        public BudgetGroup(int budgetId, string groupTitle)
        {
            BudgetId = budgetId;
            GroupTitle = groupTitle;
        }
    }
}
