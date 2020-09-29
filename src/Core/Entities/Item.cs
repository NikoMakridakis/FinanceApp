namespace Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public int BudgetGroupId { get; set; }
        public string ItemTitle { get; set; }
        public decimal ItemMontlyAmount { get; set; }

        public Item()
        {
        }
        public Item(int budgetGroupId, string itemTitle, decimal itemMonthlyAmount)
        {
            BudgetGroupId = budgetGroupId;
            ItemTitle = itemTitle;
            ItemMontlyAmount = itemMonthlyAmount;
        }
    }
}
