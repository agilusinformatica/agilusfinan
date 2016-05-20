using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace AgilusFinan.Web.Areas.Bases
{
    public class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;
            if (propInfo != null)
            {
                if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal)
                {
                    prop.ShouldSerialize = obj => false;
                }
            }
            return prop;
        }
    }
}