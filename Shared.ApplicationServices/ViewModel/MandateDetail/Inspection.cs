using System;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Inspection: MandateList.InspectionInfo
    {
        public string Comment { get; set; }
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
        public int ModeId { get; set; }
        public bool IsUnexpected { get; set; }
        public bool IsPastDeadline { get; set; }
        public bool DueDateRespected { get; set; }
        public bool IsLateOrNotCompliant { get; set; }
        
        public static Inspection FromDomain(Domain.Inspection.Inspection inspection)
        {
            var model = new Inspection
            {
                FarmInspectionId = inspection.FarmInspectionId,
                Reason = inspection.Reason.Name,
                Campaign = inspection.Campaign.Name,
                Comment = inspection.Comment,
                CommentForOffice = inspection.CommentForOffice,
                HasComplianceRequirements = inspection.HasComplianceRequirements(),
                ComplianceDeadline = inspection.Compliance.DueDate?.ToShortDateString() ?? "",
                ClosedBy = inspection.CloseStatus.IsClosed ? inspection.CloseStatus.ClosedBy : "",
                CanGeneratePdfReport = inspection.CanGeneratePdfReport(),
                CanDisplayPdfReport = inspection.CanDisplayPdfReport(),
                CanClose = inspection.CanClose(),
                CanReopen = inspection.CanReopen(),
                Domain = inspection.Domain.ShortName,
                Percent = (int)Math.Round(inspection.PercentComputed),
                Outcome = inspection.OutcomeComputed.ToViewModel(),
                IsClosed = inspection.CloseStatus.IsClosed,
                CloseDate = inspection.CloseStatus.CloseDate?.ToShortDateString() ?? "",
                VisitDate = inspection.Appointment.Date.HasValue 
                                ? string.Concat("Agendé le ", inspection.Appointment.Date.Value.ToString("dd.MM.yyyy"), " à ", inspection.Appointment.Date.Value.ToString("t"))
                                : DomainStrings.ApppointmentNotSet,
                AppointmentDate = inspection.Appointment.FirstContactDate.HasValue
                                      ? string.Concat("Contacté le ", inspection.Appointment.FirstContactDate.Value.ToString("dd.MM.yyyy"))
                                      : "",
                ModeId = inspection.Appointment.Mode.Value,
                IsUnexpected = inspection.Appointment.Mode == InspectionMode.Unscheduled,
                IsPastDeadline = inspection.Compliance.IsPastDeadline,
                DueDateRespected = inspection.Compliance.DueDateRespected,
                IsLateOrNotCompliant = inspection.Compliance.IsLateOrNotCompliant                
            };
            return model;
        }
    }
}