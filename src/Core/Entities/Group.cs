using System.Collections.Generic;

namespace Core.Entities
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Title { get; set; }
        public List<Item> Items { get; set; }
        public List<Fund> Funds { get; set; }
    }
}
