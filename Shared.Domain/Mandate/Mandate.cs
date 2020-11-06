using System;
using System.Collections.Generic;
using System.Text;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Mandate : AggregateRoot
    {
        public Mandate(IReadOnlyList<Inspection> inspections, Farm farm)
        {
            Inspections = inspections;
            Farm = farm;
        }

        private Mandate() { }

        private Farm Farm { get; }
        private IReadOnlyList<Inspection> Inspections { get; }
    }
}
