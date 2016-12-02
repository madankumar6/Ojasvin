using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Tracker.DAL.Services
{
    public class UserRoleContractResolver : PrivateSetterContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var jProperty = base.CreateProperty(member, memberSerialization);
            if (jProperty.PropertyName == "Roles" || jProperty.PropertyName == "UserName")
            {
                jProperty.ShouldDeserialize = instance => true;
            }
            else
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
}
