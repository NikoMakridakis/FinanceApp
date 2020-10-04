using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class BudgetGroup
    {
        [Key]
        public int BudgetGroupId { get; set; }
        public string BudgetGroupTitle { get; set; }

        public User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
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
