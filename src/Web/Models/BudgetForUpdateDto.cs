using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class BudgetForUpdateDto
    {
        public DateTime Date { get; set; }
        public List<GroupForCreationDto> Groups { get; set; } = new List<GroupForCreationDto>();
    }
}
