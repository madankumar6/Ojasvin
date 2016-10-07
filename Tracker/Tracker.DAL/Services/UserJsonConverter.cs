using System;
using System.Linq;

namespace Tracker.DAL.Services
{
    using Newtonsoft.Json.Linq;
    

    public class UserRoleJsonConverter : JsonCreationConverter<ValueTuple<string, string>>
    {
        protected override ValueTuple<string, string> Create(Type objectType, JObject jObject)
        {
            var roles = jObject.Property("Roles").ToList();

            return ValueTuple.Create<string, string>("Madankumar", "SuperAdmin");
        }
    }
}
