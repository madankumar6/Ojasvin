namespace Tracker.DAL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Entities.Identity;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Tracker.DAL;
    using Tracker.Entities;

    public class DbSeeder
    {
        public void SeedMenus(string seedData, IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userDbContext = serviceScope.ServiceProvider.GetService<DAL.UserDbContext>();
                this.SeedMenus(seedData, userDbContext);
            }
        }

        public void SeedMenus(string seedData, UserDbContext userDbContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<Menu> menus = JsonConvert.DeserializeObject<List<Menu>>(seedData, settings);
            if (!userDbContext.Menus.Any())
            {
                userDbContext.AddRange(menus);
                userDbContext.SaveChanges();
            }
        }

        public void SeedRoles(string seedData, IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userDbContext = serviceScope.ServiceProvider.GetService<DAL.UserDbContext>();
                this.SeedRoles(seedData, userDbContext);
            }
        }

        public void SeedRoles(string seedData, UserDbContext userDbContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(seedData, settings);
            if (!userDbContext.Roles.Any())
            {
                userDbContext.AddRange(roles);
                userDbContext.SaveChanges();
            }
        }

        public void SeedUsers(string seedData, IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var userDbContext = serviceScope.ServiceProvider.GetService<DAL.UserDbContext>();
                this.SeedRoles(seedData, userDbContext);
            }
        }

        public void SeedUsers(string seedData, UserDbContext userDbContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new UserContractResolver()
            };

            List<User> users = JsonConvert.DeserializeObject<List<User>>(seedData, settings);
            if (!userDbContext.Users.Any())
            {
                userDbContext.AddRange(users);
                userDbContext.SaveChanges();
            }

            var userRoles = JsonConvert.DeserializeObject<ValueTuple<string, string>>(seedData, new UserRoleJsonConverter());
           
        }
    }
}
