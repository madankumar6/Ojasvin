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
    using Newtonsoft.Json.Converters;
    using System.Dynamic;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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
            }
        }

        public void SeedUsers(string seedData, UserDbContext userDbContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<UserRoleSeeder> userRolesSeeder = JsonConvert.DeserializeObject<List<UserRoleSeeder>>(seedData, settings);

            var roles = userDbContext.Roles.ToList();
            var users = new List<User>();
            var identityUserRoles = new List<IdentityUserRole<int>>();

            foreach (var userRoleSeedObj in userRolesSeeder)
            {
                users.Add(new User { UserName = userRoleSeedObj.UserName, PasswordHash = userRoleSeedObj.PasswordHash, Email = userRoleSeedObj.Email });
            }

            if (!userDbContext.Users.Any())
            {
                userDbContext.AddRange(users);
                userDbContext.SaveChanges();
            }

            foreach (var userRoleSeedObj in userRolesSeeder)
            {
                identityUserRoles.Add(new IdentityUserRole<int> { RoleId = roles.First(role => role.Name == userRoleSeedObj.RoleName).Id, UserId = users.First(user => user.UserName == userRoleSeedObj.UserName).Id });
            }

            if (!userDbContext.UserRoles.Any())
            {
                userDbContext.AddRange(identityUserRoles);
                userDbContext.SaveChanges();
            }
        }
    }
}
