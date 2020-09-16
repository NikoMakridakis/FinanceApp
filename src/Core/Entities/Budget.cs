using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Budget
    {
        public Budget(DateTime date)
        {
            Date = date;
        }

        public int BudgetId { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}


