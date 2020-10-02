using System.Collections.Generic;

namespace Core.Entities
{
    public class BudgetGroup
    {
        public int BudgetGroupId { get; set; }
        public int AppUserId { get; set; }
        public string BudgetGroupTitle { get; set; }
        public AppUser AppUser { get; set; }
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
