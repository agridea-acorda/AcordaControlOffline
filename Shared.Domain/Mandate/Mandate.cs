using System;
using System.Collections.Generic;
using System.Text;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Mandate : AggregateRoot
    {
        public Mandate(int farmId)
        {
            if (farmId <= 0)
                throw new ArgumentOutOfRangeException(nameof(FarmId), $"{nameof(FarmId)} must be > 0");

            FarmId = farmId;
        }
        public int FarmId { get; }
        public IReadOnlyList<Inspection> Inspections { get; }
    }
}
