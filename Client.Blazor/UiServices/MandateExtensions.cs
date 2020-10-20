using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;

namespace Agridea.Acorda.AcordaControlOffline.Client.Blazor.UiServices
{
    public static class MandateExtensions
    {
        public static string Icon(this InspectionOutcome outcome)
        {
            switch (outcome)
            {
                case InspectionOutcome.Oui: return "<i class=\"far fa-thumbs-up\"></i>";
                case InspectionOutcome.P: return "<i class=\"far fa-thumbs-up\"></i><i class=\"far fa-thumbs-down\"></i>";
                case InspectionOutcome.Non: return "<i class=\"far fa-thumbs-down\"></i>";
                case InspectionOutcome.NA: return "<i class=\"fas fa-genderless\"></i>";
                case InspectionOutcome.NC: return "<i class=\"fas fa-genderless\"></i>";
            }

            return "";
        }

        public static string BackgroupCssClass(this InspectionOutcome outcome)
        {
            switch (outcome)
            {
                case InspectionOutcome.Oui: return "bg-gradient-success";
                case InspectionOutcome.P: return "bg-gradient-orange";
                case InspectionOutcome.Non: return "bg-gradient-danger";
                case InspectionOutcome.NA: return "bg-gradient-cyan";
                case InspectionOutcome.NC: return "bg-gradient-cyan";
            }

            return "";
        }
    }
}
