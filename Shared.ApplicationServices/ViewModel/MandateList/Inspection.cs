using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList
{
    public class Inspection : ViewModel
    {
        public string Domain { get; set; }
        public string Inspector { get; set; }
        public int Percent { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public InspectionOutcome Outcome { get; set; }
        public bool IsClosed { get; set; }
        public string CloseDate { get; set; }

        public static Inspection FromDomain(Domain.Inspection.Inspection inspection)
        {
            var model = new Inspection
            {
                Domain = inspection.Domain.ShortName,
                Inspector = "Mr Bean", // get inspector
                Percent = (int)Math.Round(inspection.PercentComputed), // use property from Checklist, not Inspection
                Outcome = inspection.OutcomeComputed.ToViewModel(), // use property from Checklist, not Inspection
                IsClosed = inspection.CloseStatus.IsClosed,
                CloseDate = inspection.CloseStatus.CloseDate?.ToShortDateString() ?? ""
            };
            return model;
        }
    }
}