namespace Tracker.DAL.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;

    using Tracker.DAL;
    using Tracker.Entities;

    public static class DBSeeder
    {
        public static void SeedMenus(string jsonData, IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DAL.UserContext>();
               SeedMenus(context);
            }
        }

        public static void SeedMenus(string jsonData, UserContext userContext)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<Menu> menus = JsonConvert.DeserializeObject<List<Menu>>(jsonData, settings);

            using (userContext)
            {
                if (!userContext.Menus.Any())
                {
                    userContext.AddRange(menus);
                    userContext.SaveChanges();
                }
            }
        }

        public static void SeedMenus(UserContext userContext)
        {
            var filename = @"Data/MenuSeeder.json";
            var dataText = System.IO.File.ReadAllText(filename);

            SeedMenus(dataText, userContext);
        }
    }
}
