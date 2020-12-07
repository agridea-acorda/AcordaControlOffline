using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate
{
    public class MandateFactory: AggregateRootFactoryBase<Domain.Mandate.Mandate>
    {
        public override Domain.Mandate.Mandate Parse(string json)
        {
            var dto = JsonConvert.DeserializeObject<MandateDeserializationDto>(json);
            return Parse(dto);
        }

        public override string Serialize(Domain.Mandate.Mandate aggregateRoot)
        {
            return JsonConvert.SerializeObject(aggregateRoot,
                                               Formatting.Indented,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new AggregateRootContractResolver()
                                               });
        }

        private Domain.Mandate.Mandate Parse(MandateDeserializationDto dto)
        {
            if (dto == null) return null;
            int farmId = dto.FarmId;
            var inspectionFactory = new InspectionFactory();
            var inspections = new List<Domain.Inspection.Inspection>();
            foreach (var inspection in dto.Inspections)
            {
                inspections.Add(inspectionFactory.Parse(inspection));
            }
            var mandate = new Domain.Mandate.Mandate(farmId, inspections);
            return mandate;
        }
    }
}
