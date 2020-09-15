using System.Collections.Generic;

namespace Core.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Title { get; set; }

        public ICollection<Item> Items { get; set; }
        public ICollection<Fund> Funds { get; set; }
    }
}
