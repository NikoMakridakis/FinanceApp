using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class ItemCreateDto
    {
        public int GroupId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public bool isIncome { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }
    }
}
