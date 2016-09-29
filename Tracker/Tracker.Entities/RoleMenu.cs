using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracker.Entities
{
    public class RoleMenu
    {
        public int RoleMenuId { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsEnabled { get; set; }
    }
}
