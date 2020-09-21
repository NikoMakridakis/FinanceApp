using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class BudgetForCreationDto
    {
        public DateTime Date { get; set; }
        public List<GroupForCreationDto> Groups { get; set; } = new List<GroupForCreationDto>();
    }
}
