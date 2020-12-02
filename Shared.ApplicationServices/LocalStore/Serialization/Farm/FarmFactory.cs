using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm
{
    public class FarmFactory : AggregateRootFactoryBase<Domain.Farm.Farm>
    {
        public override Domain.Farm.Farm Parse(string json)
        {
            throw new NotImplementedException();
        }

        public override string Serialize(Domain.Farm.Farm aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}
