using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

using Newtonsoft.Json;

using Tracker.Entities;

namespace Tracker.Web.Services
{
    using Tracker.DAL;

    public static class DBSeeder
    {
        public static void SeedDB(string jsonData, IServiceProvider serviceProvider)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<Menu> menus = JsonConvert.DeserializeObject<List<Menu>>(jsonData, settings);

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DAL.UserContext>();
                if (!context.Menus.Any())
                {
                    context.AddRange(menus);
                    context.SaveChanges();
                }
            }
        }
    }
}
