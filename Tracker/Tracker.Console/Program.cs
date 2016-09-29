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
        private static UserContext userContext;
        private static IConfigurationRoot Configuration { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(System.Reflection.Assembly.GetEntryAssembly().Location)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
            BuildDBMenus();
        }

        public static void BuildDBMenus()
        {
            var filename = @"Data/DBSeeders/MenuSeeder.json";
            var dataText = System.IO.File.ReadAllText(filename);

            var connectionString = Configuration["ConnectionStrings:Tracker"];

            userContext = UserContextFactory.Create(TrackerDatabase.SqlServer, connectionString);
            DBSeeder.SeedMenus(dataText, userContext);

            var menusList =
                userContext.Menus.Join(
                        userContext.RoleMenus.Where(rm => rm.RoleId == 1),
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
