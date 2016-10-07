namespace Tracker.Console
{
    using System.Linq;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;

    using Tracker.Common;
    using Tracker.DAL;
    using Tracker.DAL.Services;

    public class Program
    {
        private static UserDbContext userDbContext;
        private static DbSeeder dbSeeder;

        private static string connectionString;

        public static void Main(string[] args)
        {
            var connectionString = "Data Source=LAPTOP-NV8CBL0N\\SQLEXPRESS;Initial Catalog=TrackerCore;Trusted_Connection=True;User ID = sa;Password = welcome;Persist Security Info = True;Integrated Security =True;";

            // var connectionString = "Data Source=INL-3J8R6C2;Initial Catalog=TrackerCore;Trusted_Connection=True;User ID = sa;Password = welcome;Persist Security Info = True;Integrated Security =True;";
            dbSeeder = new DbSeeder();

            userDbContext = UserDbContextFactory.Create(TrackerDatabase.SqlServer, connectionString);


            // BuildDbMenus();
            BuildDbRoles();
            BuildDbUsers();
        }

        private static void BuildDbRoles()
        {
            var filename = @"Data/RoleSeeder.json";
            var roleSeedData = System.IO.File.ReadAllText(filename);

            dbSeeder.SeedRoles(roleSeedData, userDbContext);
        }

        private static void BuildDbUsers()
        {
            var filename = @"Data/UserSeeder.json";
            var roleSeedData = System.IO.File.ReadAllText(filename);

            dbSeeder.SeedUsers(roleSeedData, userDbContext);
        }

        public static void BuildDbMenus()
        {
            
            var filename = @"Data/MenuSeeder.json";
            var menuSeedData = System.IO.File.ReadAllText(filename);
            
            dbSeeder.SeedMenus(menuSeedData, userDbContext);

            var menusList =
                userDbContext.Menus.Join(
                        userDbContext.RoleMenus.Where(rm => rm.RoleId == 1),
                        m => m.MenuId,
                        r => r.MenuId,
                        (menu, role) => new { menuList = menu }).SelectMany(i => i.menuList.MenuItems)
                    .Where(i => i.ParentMenuId == null)
                    .Include(j => j.SubMenuItems)
                    .ToList();

            //var menus = (from m in entities.Menus
            //    join r in entities.RoleMenus on m.MenuId equals r.MenuId
            //    where r.RoleId == 1 && r.Enabled == true && m.ParentMenuId == null
            //    select m).Include(i => i.Children).ToList();
        }
    }
}
