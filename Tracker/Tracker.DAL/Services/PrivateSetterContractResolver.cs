﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Tracker.DAL.Services
{
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }

    public class PrivateSetterCamelCasePropertyNamesContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.Writable)
                return jProperty;

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }

    public class UserContractResolver : PrivateSetterContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.PropertyName == "Roles")
            {
                jProperty.ShouldDeserialize = instance => false;
            }
            if (jProperty.Writable)
            {
                return jProperty;
            }

            jProperty.Writable = member.IsPropertyWithSetter();

            return jProperty;
        }
    }

    internal static class MemberInfoExtensions
    {
        internal static bool IsPropertyWithSetter(this MemberInfo member)
        {
            var property = member as PropertyInfo;

            return property?.GetSetMethod(true) != null;
        }
    }
}
