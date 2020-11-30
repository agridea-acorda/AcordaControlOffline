using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate
{
    public class MandateFactory : IAggregateRootFactory<Domain.Mandate.Mandate>
    {
        public Domain.Mandate.Mandate Parse(string json)
        {
            throw new NotImplementedException();
        }

        public string Serialize(Domain.Mandate.Mandate aggregateRoot)
        {
            throw new NotImplementedException();
        }
    }
}
