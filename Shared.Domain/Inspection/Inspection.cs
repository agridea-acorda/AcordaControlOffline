using System;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class Inspection: AggregateRoot
    {
        public Inspection(InitObject initValues)
        {
            if (initValues.FarmInspectionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(initValues.FarmInspectionId), $"{nameof(initValues.FarmInspectionId)} must be > 0");

            if (initValues.InspectionId == Guid.Empty)
                throw new ArgumentNullException($"{nameof(initValues.InspectionId)} must be non-empty.");

            if (initValues.Domain == null)
                throw new ArgumentNullException($"{nameof(initValues.Domain)} must be defined.");

            if (initValues.Campaign == null)
                throw new ArgumentNullException($"{nameof(initValues.Campaign)} must be defined.");

            if (initValues.Reason == null)
                throw new ArgumentNullException($"{nameof(initValues.Reason)} must be defined.");

            if (initValues.ChecklistId <= 0)
                throw new ArgumentOutOfRangeException(nameof(initValues.ChecklistId), $"{nameof(initValues.ChecklistId)} must be > 0");

            if (initValues.FarmId <= 0)
                throw new ArgumentOutOfRangeException(nameof(initValues.FarmId), $"{nameof(initValues.FarmId)} must be > 0");

            FarmInspectionId = initValues.FarmInspectionId;
            InspectionId = initValues.InspectionId;
            Domain = initValues.Domain;
            Campaign = initValues.Campaign;
            Reason = initValues.Reason;
            Comment = initValues.Comment;
            ChecklistId = initValues.ChecklistId;
            FarmId = initValues.FarmId;

            Appointment = Appointment.None;
            CommentForFarmer = CommentForOffice = "";
            Status = InspectionStatus.Planned;
            InspectorSignature = Inspector2Signature = FarmerSignature = Signature.None;
            Compliance = Compliance.Empty;
            PdfReport = PdfReport.None;
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
        public long FarmId { get; }
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
        public PdfReport PdfReport { get; private set; }
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

        public Inspection SetCompliance(Compliance compliance)
        {
            Compliance = compliance;
            return this;
        }

        public Inspection InspectorSigns(Signature signature)
        {
            Console.WriteLine($"Saving inspector signature for Inspection {FarmInspectionId}");
            InspectorSignature = signature;
            return this;
        }

        public bool HasComplianceRequirements()
        {
            return new HasComplianceRequirements().IsSatisfiedBy(this);
        }

        public bool CanGeneratePdfReport()
        {
            return new CanGeneratePdfReport().IsSatisfiedBy(this);
        }

        public bool CanDisplayPdfReport()
        {
            return new CanDisplayPdfReport().IsSatisfiedBy(this);
        }

        public bool CanClose()
        {
            return new CanClose().IsSatisfiedBy(this);
        }

        public bool CanReopen()
        {
            return new CanReopen().IsSatisfiedBy(this);
        }

        public class InitObject
        {
            public int FarmInspectionId { get; set; }
            public Guid InspectionId { get; set; }
            public long ChecklistId { get; set; }
            public long FarmId { get; set; }
            public Domain Domain { get; set; }
            public Campaign Campaign { get; set; }
            public InspectionReason Reason { get; set; }
            public string Comment { get; set; }
        }
    }
}
