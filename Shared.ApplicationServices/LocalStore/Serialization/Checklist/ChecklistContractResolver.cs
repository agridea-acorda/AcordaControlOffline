using System;
using System.Collections.Generic;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist
{
    public class ChecklistContractResolver : AggregateRootContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p =>
                                   p.PropertyName != nameof(Result.Parent)
                        )
                        .ToList();
        }
    }
}