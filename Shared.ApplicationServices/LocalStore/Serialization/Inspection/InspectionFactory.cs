using System;
using Newtonsoft.Json;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection
{
    public class InspectionFactory : AggregateRootFactoryBase<Domain.Inspection.Inspection>
    {
        public override Domain.Inspection.Inspection Parse(string json)
        {
            throw new NotImplementedException();
        }

        public override string Serialize(Domain.Inspection.Inspection aggregateRoot)
        {
            return JsonConvert.SerializeObject(aggregateRoot,
                                               Formatting.Indented,
                                               new JsonSerializerSettings
                                               {
                                                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                   ContractResolver = new AggregateRootContractResolver()
                                               });
        }
    }
}
