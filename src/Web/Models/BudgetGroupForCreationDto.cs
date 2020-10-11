using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BudgetGroupForCreationDto
    {
        [Required(ErrorMessage = "UserId is required")]
        public int UserId { get; set; }
        public string BudgetGroupTitle { get; set; }
    }
}
