using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Mandate : AggregateRoot
    {
        public Mandate(int farmId, IReadOnlyList<Inspection.Inspection> inspections)
        {
            if (farmId <= 0)
                throw new ArgumentOutOfRangeException(nameof(farmId), $"{nameof(farmId)} must be > 0");

            FarmId = farmId;
            Inspections = inspections;
        }
        public int FarmId { get; }
        public IReadOnlyList<Inspection.Inspection> Inspections { get; }
    }
}
