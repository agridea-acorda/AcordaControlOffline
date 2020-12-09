using System;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Inspection: MandateList.Inspection
    {
        public int FarmInspectionId { get; set; }
        public string Campaign { get; set; }
        public string CommentForOffice { get; set; }
        public bool HasComplianceRequirements { get; set; }
        public string ComplianceDeadline { get; set; }
        public string ClosedBy { get; set; }
        public bool CanGeneratePdfReport { get; set; }
        public bool CanDisplayPdfReport { get; set; }
        public bool CanClose { get; set; }
        public bool CanReopen { get; set; }
        public string VisitDate { get; set; }
        public string AppointmentDate { get; set; }
        public bool IsUnexpected { get; set; }
        
        public static Inspection FromDomain(Domain.Inspection.Inspection inspection)
        {
            var model = new Inspection
            {
                FarmInspectionId = inspection.FarmInspectionId,
                Campaign = inspection.Campaign.Name,
                CommentForOffice = inspection.CommentForOffice,
                HasComplianceRequirements = inspection.HasComplianceRequirements(),
                ComplianceDeadline = inspection.Compliance.DueDate?.ToShortDateString() ?? "",
                ClosedBy = inspection.CloseStatus.IsClosed ? inspection.CloseStatus.ClosedBy : "",
                CanGeneratePdfReport = inspection.CanGeneratePdfReport(),
                CanDisplayPdfReport = inspection.CanDisplayPdfReport(),
                CanClose = inspection.CanClose(),
                CanReopen = inspection.CanReopen(),
                Domain = inspection.Domain.ShortName,
                Inspector = "Mr Bean",
                Percent = (int)Math.Round(inspection.PercentComputed),
                Outcome = inspection.OutcomeComputed.ToViewModel(),
                IsClosed = inspection.CloseStatus.IsClosed,
                CloseDate = inspection.CloseStatus.CloseDate?.ToShortDateString() ?? "",
                VisitDate = inspection.Appointment.Date.HasValue 
                                ? string.Format(DomainStrings.AppointmentDateString, inspection.Appointment.Date.Value.ToShortDateString())
                                : DomainStrings.ApppointmentNotSet,
                AppointmentDate = inspection.Appointment.FirstContactDate.HasValue
                                      ? string.Format(DomainStrings.AppointmentContactDateString, inspection.Appointment.FirstContactDate.Value.ToShortDateString())
                                      : "",
                IsUnexpected = inspection.Appointment.Mode == InspectionMode.Unscheduled
            };
            return model;
        }

        
    }
}