using System.Collections.Generic;

namespace Web.Models
{
    public class GroupForUpdateDto
    {
        public int BudgetId { get; set; }
        public string Title { get; set; }
        public List<ItemForCreationDto> Items { get; set; } = new List<ItemForCreationDto>();
    }
}
