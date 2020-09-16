using System;

namespace Core.Entities
{
    public class Item
    {
        public Item(int groupId, string label)
        {
            GroupId = groupId;
            Label = label;
        }

        public int ItemId { get; set; }
        public string Label { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Notes { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
