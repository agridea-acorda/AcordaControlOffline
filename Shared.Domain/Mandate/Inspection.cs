using System;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Inspection: AggregateRoot
    {
        public Inspection(int farmInspectionId, Guid inspectionId, Domain domain, Campaign campaign, InspectionReason reason, string comment, long checklistId)
        {
            if (farmInspectionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(farmInspectionId), $"{nameof(farmInspectionId)} must be > 0");

            if (inspectionId == Guid.Empty)
                throw new ArgumentNullException($"{nameof(inspectionId)} must be non-empty.");

            if (domain == null)
                throw new ArgumentNullException($"{nameof(domain)} must be defined.");

            if (campaign == null)
                throw new ArgumentNullException($"{nameof(campaign)} must be defined.");

            if (reason == null)
                throw new ArgumentNullException($"{nameof(reason)} must be defined.");

            if (checklistId <= 0)
                throw new ArgumentOutOfRangeException(nameof(checklistId), $"{nameof(checklistId)} must be > 0");

            FarmInspectionId = farmInspectionId;
            InspectionId = inspectionId;
            Domain = domain;
            Campaign = campaign;
            Reason = reason;
            Comment = comment;
            ChecklistId = checklistId;

            Appointment = Appointment.None;
            CommentForFarmer = CommentForOffice = "";
            Status = InspectionStatus.Planned;
            InspectorSignature = Inspector2Signature = FarmerSignature = Signature.None;
            Compliance = Compliance.Empty;
            FinishStatus = FinishStatus.NotFinished;
            CloseStatus = CloseStatus.NotClosed;
            ReopenStatus = ReopenStatus.NotReopened;
            PercentComputed = 0;
            DateComputed = DateTime.MinValue;
            OutcomeComputed = InspectionOutcome.NotInspected;
        }
        public int FarmInspectionId { get; }
        public Guid InspectionId { get; }
        public long ChecklistId { get; set; } 
        public Domain Domain { get; }
        public Campaign Campaign { get; }
        public InspectionReason Reason { get; }
        public string Comment { get; }

        public Appointment Appointment { get; private set; }
        public string CommentForFarmer { get; private set; }
        public string CommentForOffice { get; private set; }
        public InspectionStatus Status { get; private set; }
        public double PercentComputed { get; private set; }
        public DateTime DateComputed { get; private set; }
        public InspectionOutcome OutcomeComputed { get; private set; }
        public Signature InspectorSignature { get; private set; }
        public Signature Inspector2Signature { get; private set; }
        public Signature FarmerSignature { get; private set; }
        public Compliance Compliance { get; private set; }
        public FinishStatus FinishStatus { get; private set; }
        public CloseStatus CloseStatus { get; private set; }
        public ReopenStatus ReopenStatus { get; private set; }

        public Inspection SetAppointment(Appointment appointment)
        {
            Appointment = appointment;
            return this;
        }

        public Inspection SetCommentForFarmer(string comment)
        {
            CommentForFarmer = comment;
            return this;
        }

        public Inspection SetCommentForOffice(string comment)
        {
            CommentForOffice = comment;
            return this;
        }

        public Inspection InspectorSigns(Signature signature)
        {
            InspectorSignature = signature;
            return this;
        }
    }
}
