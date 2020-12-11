using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;

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
                case InspectionOutcome.NA: return "<i class=\"far fa-circle\"></i>";
                case InspectionOutcome.NC: return "<i class=\"far fa-circle\"></i>";
            }

            return "";
        }

        public static string BackgroundCssClass(this InspectionOutcome outcome)
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

        public static string OutcomeCssClass(this IResult rubric, AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome outcome)
        {
            return rubric.Outcome == outcome && outcome == AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome.Ok ? "btn-success" :
                   rubric.Outcome == outcome && outcome == AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome.PartiallyOk ? "btn-warning" :
                   rubric.Outcome == outcome && outcome == AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome.NotOk ? "btn-danger" :
                   rubric.Outcome == outcome && outcome == AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome.NotInspected ? "btn-primary" :
                   rubric.Outcome == outcome && outcome == AcordaControlOffline.Shared.Domain.Inspection.InspectionOutcome.NotApplicable ? "btn-primary" :
                   "btn-default";
        }

        public static string BackgroundCssClass(this int farmTypeCode)
        {
            switch (farmTypeCode)
            {
                case 1: return "list-group-item-yearly";
                case 2: return "list-group-item-production-unit";
                case 4: return "list-group-item-summering";
                case 5: return "list-group-item-summering";
                case 6: return "list-group-item-community";
                case 14: return "list-group-item-per-partial-community";
                case 15: return "list-group-item-hobby-breeding";
                case 16: return "list-group-item-per-partial-community";
                case 99: return "list-group-item-disabled";
                case 100: return "list-group-item-disabled";
            }

            return "";
        }

        public static string CurateAsElementId(this string elementCode)
        {
            return elementCode.Replace(" ", "_").Replace(".", "_").Replace(",", "_");
        }

        public static string CurateAsReadableText(this string pointDescription)
        {
            return pointDescription.Replace("\\n", "<br/>") // curate newline characters
                                   .Replace("¿", "'"); // curate upside-down question mark characters
        }
    }
}
