using Core.Entities;
using System.Collections.Generic;

namespace Web.Models
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public int BudgetId { get; set; }
        public string Title { get; set; }
    }
}
