namespace Tracker.DAL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Tracker.DAL;
    using Tracker.Entities;
    using Entities.Identity;
    using Microsoft.Extensions.Configuration;

    public class DBSeeder
    {
        private static IConfigurationRoot Configuration { get; set; }

        public DBSeeder()
        {
            var builder = new ConfigurationBuilder()
                //.SetBasePath(System.Reflection.Assembly.GetEntryAssembly().Location)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        public void SeedMenus(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userDbContext = serviceScope.ServiceProvider.GetService<DAL.UserDbContext>();
                SeedMenus(userDbContext);
            }
        }

        public void SeedMenus(string seedData, UserDbContext userDbContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<Menu> menus = JsonConvert.DeserializeObject<List<Menu>>(seedData, settings);

            using (userDbContext)
            {
                if (!userDbContext.Menus.Any())
                {
                    userDbContext.AddRange(menus);
                    userDbContext.SaveChanges();
                }
            }
        }

        public void SeedMenus(UserDbContext userDbContext)
        {
            var filename = @"Data/MenuSeeder.json";
            var seedData = System.IO.File.ReadAllText(filename);

            SeedMenus(seedData, userDbContext);
        }

        public void SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userDbContext = serviceScope.ServiceProvider.GetService<DAL.UserDbContext>();
                SeedRoles(userDbContext);
            }
        }

        public void SeedRoles(string seedData, UserDbContext userDbContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(seedData, settings);

            using (userDbContext)
            {
                if (!userDbContext.Roles.Any())
                {
                    userDbContext.AddRange(roles);
                    userDbContext.SaveChanges();
                }
            }
        }

        public void SeedRoles(UserDbContext userDbContext)
        {
            var filename = @"Data/RoleSeeder.json";
            var seedData = System.IO.File.ReadAllText(filename);

            SeedRoles(seedData, userDbContext);
        }
    }
}
