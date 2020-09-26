namespace Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string ItemTitle { get; set; }
        public decimal ItemMontlyAmount { get; set; }

        public Item()
        {
        }
        public Item(int groupId, string itemTitle, decimal itemMonthlyAmount)
        {
            GroupId = groupId;
            ItemTitle = itemTitle;
            ItemMontlyAmount = itemMonthlyAmount;
        }
    }
}
