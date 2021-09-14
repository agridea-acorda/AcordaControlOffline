using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList
{
    public class InspectionInfo : ViewModel
    {
        public int FarmInspectionId { get; set; }
        public string Domain { get; set; }
        public string Inspector { get; set; }
        public string InspectorDisplay => !string.IsNullOrWhiteSpace(Inspector) ? $"({Inspector})" : "";
        public int Percent { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public InspectionOutcome Outcome { get; set; }
        public bool IsClosed { get; set; }
        public string CloseDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public string Campaign { get; set; }
        public DateTime? DateLastVisit { get; set; }

        //public static InspectionInfo FromDomain(Domain.Inspection.Inspection inspection, Domain.Checklist.Checklist checklist)
        //{
        //    var model = new InspectionInfo
        //    {
        //        Domain = inspection.Domain.ShortName,
        //        Inspector = "Mr Bean", // get inspector
        //        Percent = (int)Math.Round(checklist.Percent * 100),
        //        Outcome = checklist.OutcomeComputed.ToViewModel(),
        //        IsClosed = inspection.CloseStatus.IsClosed,
        //        CloseDate = inspection.CloseStatus.CloseDate?.ToShortDateString() ?? ""
        //    };
        //    return model;
        //}

        public static InspectionInfo FromChecklist(Domain.Checklist.Checklist checklist)
        {
            var model = new InspectionInfo
            {
                Domain = "",
                Inspector = "", // get inspector
                Percent = (int)Math.Round(checklist.Percent * 100),
                Outcome = checklist.OutcomeComputed.ToViewModel(),
                IsClosed = false,
                CloseDate = "",
                Reason = "",
                Status = "",
                Campaign = "",
                DateLastVisit = null
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