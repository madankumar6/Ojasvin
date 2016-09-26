using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tracker.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //TMSEntities entities = new TMSEntities();
            //var menusList =
            //    entities.Menus.Join(entities.RoleMenus.Where(rm => rm.RoleId == userRoleId), m => m.MenuId,
            //        r => r.MenuId, (menu, role) => new { menuList = menu })
            //        .SelectMany(i => i.menuList.MenuItems)
            //        .Where(i => i.ParentMenuId == null)
            //        .Include(j => j.Children)
            //        .ToList();

            //var menus = (from m in entities.Menus
            //    join r in entities.RoleMenus on m.MenuId equals r.MenuId
            //    where r.RoleId == 1 && r.Enabled == true && m.ParentMenuId == null
            //    select m).Include(i => i.Children).ToList();
        }
    }
}
