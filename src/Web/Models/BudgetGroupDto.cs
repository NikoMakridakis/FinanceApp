using Core.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BudgetGroupDto
    {
        public int BudgetGroupId { get; set; }
        public string BudgetGroupTitle { get; set; }
        public List<Item> Items { get; set; }
    }
}
