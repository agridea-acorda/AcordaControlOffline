using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm
{
    public class FarmFactory : AggregateRootFactoryBase<Domain.Farm.Farm>
    {
        public override Domain.Farm.Farm Parse(string json)
        {
            var dto = JsonConvert.DeserializeObject<FarmDeserializationDto.Root>(json);
            return Parse(dto);
        }

        public override string Serialize(Domain.Farm.Farm aggregateRoot)
        {
            return JsonConvert.SerializeObject(aggregateRoot,
                                               Formatting.Indented,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new AggregateRootContractResolver()
                                               });
        }

        private Domain.Farm.Farm Parse(FarmDeserializationDto.Root dto)
        {
            if (dto == null) return null;

            var targetInstance = (Domain.Farm.Farm)FormatterServices.GetUninitializedObject(typeof(Domain.Farm.Farm));
            return targetInstance;
        }
    }
}
