using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public DateTime Date { get; set; }
    }
}


