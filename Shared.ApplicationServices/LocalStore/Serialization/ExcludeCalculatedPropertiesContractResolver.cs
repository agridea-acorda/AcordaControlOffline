using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization
{
    /// <remarks>
    /// Thanks to https://stackoverflow.com/a/34112601
    /// </remarks>
    public class ExcludeCalculatedPropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = _ => ShouldSerialize(member);
            return property;
        }

        internal static bool ShouldSerialize(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            if (propertyInfo == null)
            {
                return false;
            }

            if (propertyInfo.SetMethod != null)
            {
                return true;
            }

            var getMethod = propertyInfo.GetMethod;
            return Attribute.GetCustomAttribute(getMethod, typeof(CompilerGeneratedAttribute)) != null;
        }
    }
}
