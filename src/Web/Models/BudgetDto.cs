using Core.Entities;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class BudgetDto
    {
        public int BudgetId { get; set; }
        public DateTime Date { get; set; }
        public List<Group> Groups { get; set; }
    }
}
