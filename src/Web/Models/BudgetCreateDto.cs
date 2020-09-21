using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BudgetCreateDto
    {
        public DateTime Date { get; set; }
        public List<GroupCreateDto> Groups { get; set; } = new List<GroupCreateDto>();
    }
}
