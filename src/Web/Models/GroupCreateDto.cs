using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class GroupCreateDto
    {
        public int BudgetId { get; set; }
        public string Title { get; set; }
        public List<ItemCreateDto> Items { get; set; } = new List<ItemCreateDto>();
    }
}
