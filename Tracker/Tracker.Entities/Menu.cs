using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
