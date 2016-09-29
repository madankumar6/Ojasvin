namespace Tracker.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        public MenuItem ParentMenu { get; set; }
        public ICollection<MenuItem> SubMenuItems { get; set; }
        public int? MenuId { get; set; }
        public Menu Menu { get; set; }

        public MenuItem()
        {
            this.SubMenuItems = new List<MenuItem>();
        }
    }
}
