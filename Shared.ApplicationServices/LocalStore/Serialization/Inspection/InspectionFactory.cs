using System;
using System.Collections.Generic;
using System.Text;

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
            throw new NotImplementedException();
        }
    }
}
