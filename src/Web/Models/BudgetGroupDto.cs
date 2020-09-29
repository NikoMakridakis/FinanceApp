using Core.Entities;
using System.Collections.Generic;

namespace Web.Models
{
    public class BudgetGroupDto
    {
        public int BudgetGroupId { get; set; }
        public int UserId { get; set; }
        public string BudgetGroupTitle { get; set; }
        public List<Item> Items { get; set; }
    }
}
