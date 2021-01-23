using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public interface IOutcomable
    {
        InspectionOutcome OutcomeComputed { get; }
    }
}