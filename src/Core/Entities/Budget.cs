using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public DateTime Date { get; set; }
        public List<Group> Groups { get; set; } = new List<Group>();

        public Budget()
        {
        }
        public Budget(DateTime date)
        {
            Date = date;
        }
    }
}


