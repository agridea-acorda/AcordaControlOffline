using System;
using System.Collections.Generic;
using System.Linq;
using Agridea.DomainDrivenDesign;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization
{
    public class EntityContractResolver : ExcludeCalculatedPropertiesContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p =>
                                   p.PropertyName != nameof(Entity.CreatedBy) &&
                                   p.PropertyName != nameof(Entity.CreationDate) &&
                                   p.PropertyName != nameof(Entity.ModifiedBy) &&
                                   p.PropertyName != nameof(Entity.ModificationDate)
                        )
                        .ToList();
        }
    }
}