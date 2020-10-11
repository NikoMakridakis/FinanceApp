using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ItemForUpdateDto
    {
        [Required(ErrorMessage = "BudgetGroupId is required")]
        public int BudgetGroupId { get; set; }
        public string ItemTitle { get; set; }
        public decimal ItemMontlyAmount { get; set; }
    }
}
