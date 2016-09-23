using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tracker.Entities.Identity;

namespace Tracker.Web.Services
{
    public static class DBSeeder
    {
        public static void SeedDB(string jsonData, IServiceProvider serviceProvider)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                ContractResolver = new PrivateSetterContractResolver()
            };

            List<User> events = JsonConvert.DeserializeObject<List<User>>(jsonData, settings);
        }
    }
}
