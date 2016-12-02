using System;
using System.Linq;

namespace Tracker.DAL.Services
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    public class UserRoleJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            //T target = Create(objectType, jObject);

            // Populate the object properties
            //serializer.Populate(jObject.CreateReader(), target);

            //return target;

            throw new NotImplementedException();

        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Create an instance of objectType, based properties in the JSON object
        /// </summary>
        /// <param name="objectType">type of object expected</param>
        /// <param name="jObject">contents of JSON object that will be deserialized</param>
        /// <returns></returns>
        //protected override ValueTuple<string, string> Create(Type objectType, JObject jObject)
        protected List<string> Create(Type objectType, JObject jObject)
        {
            var roles = jObject.Property("Roles").ToList();

            //return ValueTuple.Create<string, string>("Madankumar", "SuperAdmin");
            return new List<string>();
        }
    }
}
