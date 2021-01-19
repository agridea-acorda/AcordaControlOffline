using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList
{
    public class Inspection : ViewModel
    {
        public string Domain { get; set; }
        public string Inspector { get; set; }
        public string InspectorDisplay => !string.IsNullOrWhiteSpace(Inspector) ? $"({Inspector})" : "";
        public int Percent { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public InspectionOutcome Outcome { get; set; }
        public bool IsClosed { get; set; }
        public string CloseDate { get; set; }

        public static Inspection FromDomain(Domain.Inspection.Inspection inspection, Domain.Checklist.Checklist checklist)
        {
            var model = new Inspection
            {
                Domain = inspection.Domain.ShortName,
                Inspector = "Mr Bean", // get inspector
                Percent = (int)Math.Round(checklist.Percent * 100), // use property from Checklist, not Inspection
                Outcome = checklist.OutcomeComputed.ToViewModel(), // use property from Checklist, not Inspection
                IsClosed = inspection.CloseStatus.IsClosed,
                CloseDate = inspection.CloseStatus.CloseDate?.ToShortDateString() ?? ""
            };
            return model;
        }
        public static Inspection FromDomain(Domain.Checklist.Checklist checklist)
        {
            var model = new Inspection
            {
                Domain = "",
                Inspector = "", // get inspector
                Percent = (int)Math.Round(checklist.Percent * 100), // use property from Checklist, not Inspection
                Outcome = checklist.OutcomeComputed.ToViewModel(), // use property from Checklist, not Inspection
                IsClosed = false,
                CloseDate = ""
            };
            return model;
        }


        public void Progress(double percent)
        {
            Percent = (int) Math.Round(percent * 100);
        }

        public void SetOutcome(Domain.Inspection.InspectionOutcome outcome)
        {
            Outcome = outcome.ToViewModel();
        }
    }
}