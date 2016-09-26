namespace Tracker.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public int? MenuOrder { get; set; }
        public string CssClass { get; set; }
        public bool Enabled { get; set; }

        public int? ParentMenuId { get; set; }
        public virtual MenuItem ParentMenu { get; set; }
        public virtual ICollection<MenuItem> SubMenuItems { get; set; }
        public int? MenuId { get; set; }
        public virtual Menu Menu { get; set; }

        public MenuItem()
        {
            SubMenuItems = new List<MenuItem>();
        }
    }
}
