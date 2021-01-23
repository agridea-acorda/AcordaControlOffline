namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel
{
    public enum InspectionOutcome
    {
        Unset,
        Oui,
        P,
        Non,
        NC,
        NA,
    }

    public static class InspectionOutcomeExtensions
    {
        public static InspectionOutcome ToViewModel(this Domain.Inspection.InspectionOutcome outcome)
        {
            return outcome == Domain.Inspection.InspectionOutcome.Ok ? InspectionOutcome.Oui :
                   outcome == Domain.Inspection.InspectionOutcome.PartiallyOk ? InspectionOutcome.P :
                   outcome == Domain.Inspection.InspectionOutcome.NotOk ? InspectionOutcome.Non :
                   outcome == Domain.Inspection.InspectionOutcome.NotInspected ? InspectionOutcome.NC :
                   outcome == Domain.Inspection.InspectionOutcome.NotApplicable ? InspectionOutcome.NA :
                   InspectionOutcome.Unset;
        }
    }
}