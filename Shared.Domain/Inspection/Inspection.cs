using System;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class Inspection: AggregateRoot
    {
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
            FinishStatus = FinishStatus.NotFinished;
            CloseStatus = CloseStatus.NotClosed;
            ReopenStatus = ReopenStatus.NotReopened;
            PercentComputed = 0;
            DateComputed = DateTime.MinValue;
            OutcomeComputed = InspectionOutcome.NotInspected;
        }

        //public Inspection(int farmInspectionId, Guid inspectionId, Domain domain, Campaign campaign, InspectionReason reason, string comment, long checklistId, long farmId)
        //{
        //    if (farmInspectionId <= 0)
        //        throw new ArgumentOutOfRangeException(nameof(farmInspectionId), $"{nameof(farmInspectionId)} must be > 0");

        //    if (inspectionId == Guid.Empty)
        //        throw new ArgumentNullException($"{nameof(inspectionId)} must be non-empty.");

        //    if (domain == null)
        //        throw new ArgumentNullException($"{nameof(domain)} must be defined.");

        //    if (campaign == null)
        //        throw new ArgumentNullException($"{nameof(campaign)} must be defined.");

        //    if (reason == null)
        //        throw new ArgumentNullException($"{nameof(reason)} must be defined.");

        //    if (checklistId <= 0)
        //        throw new ArgumentOutOfRangeException(nameof(checklistId), $"{nameof(checklistId)} must be > 0");

        //    if (farmId <= 0)
        //        throw new ArgumentOutOfRangeException(nameof(farmId), $"{nameof(farmId)} must be > 0");

        //    FarmInspectionId = farmInspectionId;
        //    InspectionId = inspectionId;
        //    Domain = domain;
        //    Campaign = campaign;
        //    Reason = reason;
        //    Comment = comment;
        //    ChecklistId = checklistId;
        //    FarmId = farmId;

        //    Appointment = Appointment.None;
        //    CommentForFarmer = CommentForOffice = "";
        //    Status = InspectionStatus.Planned;
        //    InspectorSignature = Inspector2Signature = FarmerSignature = Signature.None;
        //    Compliance = Compliance.Empty;
        //    FinishStatus = FinishStatus.NotFinished;
        //    CloseStatus = CloseStatus.NotClosed;
        //    ReopenStatus = ReopenStatus.NotReopened;
        //    PercentComputed = 0;
        //    DateComputed = DateTime.MinValue;
        //    OutcomeComputed = InspectionOutcome.NotInspected;
        //}
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
