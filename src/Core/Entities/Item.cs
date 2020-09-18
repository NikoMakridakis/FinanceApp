using System;

namespace Core.Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public int GroupId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public bool isIncome { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public Item()
        {
        }
        public Item(int groupId, string label)
        {
            GroupId = groupId;
            Label = label;
        }
    }
}
