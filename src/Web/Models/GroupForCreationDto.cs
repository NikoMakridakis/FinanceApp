using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class GroupForCreationDto
    {
        public int BudgetId { get; set; }
        public string Title { get; set; }
        public List<ItemForCreationDto> Items { get; set; } = new List<ItemForCreationDto>();
    }
}
