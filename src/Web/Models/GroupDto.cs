namespace Web.Models
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public int BudgetId { get; set; }
        public decimal GroupMonthlyTotal { get; set; }
        public string GroupTitle { get; set; }
    }
}
