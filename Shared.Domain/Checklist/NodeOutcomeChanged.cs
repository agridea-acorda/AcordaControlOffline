using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class NodeOutcomeChanged : ValueObject, IDomainEvent
    {
        public NodeOutcomeChanged(InspectionOutcome outcome, double percent, int farmInspectionId)
        {
            Outcome = outcome;
            Percent = percent;
            FarmInspectionId = farmInspectionId;
        }
        public InspectionOutcome Outcome { get; }
        public double Percent { get; }
        public int FarmInspectionId { get; }
            
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FarmInspectionId;
            yield return Outcome;
            yield return Percent;
        }
    }
}