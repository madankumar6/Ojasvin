using System.Collections.Generic;

namespace Tracker.Entities
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
