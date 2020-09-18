using System.Collections.Generic;

namespace Core.Entities
{
    public class Group
    {
        public Group()
        {
        }

        public Group(int budgetId, string title)
        {
            BudgetId = budgetId;
            Title = title;
        }

        public int GroupId { get; set; }
        public int BudgetId { get; set; }
        public string Title { get; set; }
        public List<Item> Items { get; set; }
    }
}
