using System;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Inspection: AggregateRoot
    {
        public Inspection(Guid inspectionId, Domain domain, Campaign campaign, InspectionReason reason, string comment)
        {
            if (inspectionId == Guid.Empty)
                throw new ArgumentNullException($"{nameof(inspectionId)} must be non-empty.");

            if (domain == null)
                throw new ArgumentNullException($"{nameof(domain)} must be defined.");

            if (campaign == null)
                throw new ArgumentNullException($"{nameof(campaign)} must be defined.");

            if (reason == null)
                throw new ArgumentNullException($"{nameof(reason)} must be defined.");

            InspectionId = inspectionId;
            Domain = domain;
            Campaign = campaign;
            Reason = reason;
            Comment = comment;
        }
        public Guid InspectionId { get; }
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
    }
}
