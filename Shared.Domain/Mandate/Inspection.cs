using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Inspection: Entity
    {
        public Guid InspectionId { get; }
        public Domain Domain { get; }
        public Campaign Campaign { get; }
        public InspectionReason Reason { get; }
        
        public InspectionMode Mode { get; private set; }
        public string Comment { get; private set; }
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

    public class Campaign : ValueObject
    {
        public Campaign(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), $"{nameof(id)} must be > 0");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} must be non-empty");

            Id = id;
            Name = name;
        }
        public int Id { get; }
        public string Name { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }

    public class Domain : ValueObject
    {
        public Domain(int id, string shortName)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), $"{nameof(id)} must be > 0");

            if (string.IsNullOrWhiteSpace(shortName))
                throw new ArgumentException($"{nameof(shortName)} must be non-empty");

            Id = id;
            ShortName = shortName;
        }
        public int Id { get; }
        public string ShortName { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return ShortName;
        }
    }

    public class FinishStatus : ValueObject
    {
        public string DoneByInspector { get; }
        public DateTime? DoneOn { get; }
        public bool IsFinished => DoneOn.HasValue;

        public static FinishStatus NotFinished => new FinishStatus(null, "");

        public FinishStatus(DateTime? doneOn, string doneByInspector)
        {
            if (doneOn.HasValue != !string.IsNullOrWhiteSpace(DoneByInspector))
                throw new InvalidOperationException("Done-date and -inspector must be either both set or both empty.");

            DoneOn = doneOn;
            DoneByInspector = doneByInspector;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DoneOn;
            yield return DoneByInspector;
        }
    }

    public class CloseStatus : ValueObject
    {
        public DateTime? CloseDate { get; }
        public string ClosedBy { get; }
        public bool IsClosed => CloseDate.HasValue;

        public static CloseStatus NotClosed = new CloseStatus(null, "");

        public CloseStatus(DateTime? closeDate, string closedBy)
        {
            if (closeDate.HasValue != !string.IsNullOrWhiteSpace(closedBy))
                throw new InvalidOperationException("Close-date and -by must be either both set or both empty.");

            CloseDate = closeDate;
            ClosedBy = closedBy;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CloseDate;
            yield return ClosedBy;
        }
    }

    public class ReopenStatus : ValueObject
    {
        public DateTime? ReopenDate { get; }
        public string ReopenedBy { get; }
        public bool IsReopened => ReopenDate.HasValue;
        
        public static ReopenStatus NotReopened = new ReopenStatus(null, "");
        
        public ReopenStatus(DateTime? reopenDate, string reopenedBy)
        {
            if (reopenDate.HasValue != !string.IsNullOrWhiteSpace(reopenedBy))
                throw new InvalidOperationException("Reopen-date and -by must be either both set or both empty.");
            
            ReopenDate = reopenDate;
            ReopenedBy = reopenedBy;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ReopenDate;
            yield return ReopenedBy;
        }
    }

    public class Compliance : ValueObject
    {
        public string ActionsOrDocuments { get; }
        public DateTime? DueDate { get; }
        public bool DueDateNotRespected { get; }
        public bool DueDateRespected { get; }
        public bool FurtherInvestigationNeeded { get; }
        public bool IncompleteOrNonCompliant { get; }
        public Compliance(string actionsOrDocuments, DateTime? dueDate, bool dueDateNotRespected, bool dueDateRespected, bool furtherInvestigationNeeded, bool incompleteOrNonCompliant)
        {
            ActionsOrDocuments = actionsOrDocuments;
            DueDate = dueDate;
            DueDateNotRespected = dueDateNotRespected;
            DueDateRespected = dueDateRespected;
            FurtherInvestigationNeeded = furtherInvestigationNeeded;
            IncompleteOrNonCompliant = incompleteOrNonCompliant;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ActionsOrDocuments;
            yield return DueDate;
            yield return DueDateNotRespected;
            yield return DueDateRespected;
            yield return FurtherInvestigationNeeded;
            yield return IncompleteOrNonCompliant;
        }
    }

    public class CodeNameValueObject : ValueObject
    {
        public CodeNameValueObject(int code, string name)
        {
            if (code < 0)
                throw new ArgumentOutOfRangeException(nameof(Code), $"{nameof(Code)} must be >= 0.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentOutOfRangeException(nameof(Name), $"{nameof(Name)} must be non-empty.");

            Code = code;
            Name = name;
        }
        public int Code { get; }
        public string Name { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }

    public class InspectionReason : CodeNameValueObject
    {
        public InspectionReason(int code, string name) : base(code, name) { }
        public static InspectionReason Routine => new InspectionReason(1, "Routine");
        public static InspectionReason FollowUp => new InspectionReason(2, "Suivi");
        public static InspectionReason Random => new InspectionReason(11, "Aléatoire");
        public static InspectionReason Other => new InspectionReason(8, "Autres");
        public static InspectionReason Change => new InspectionReason(3, "Changement");
        public static InspectionReason Suspicion => new InspectionReason(4, "Suspicion");
        public static InspectionReason Event => new InspectionReason(5, "Evénement (jusqu'à 2019)");
        public static InspectionReason Request => new InspectionReason(6, "Demande (jusqu'à 2019)");
        public static InspectionReason Defect => new InspectionReason(7, "Saisie manquement (jusqu'à 2019)");
        public static InspectionReason FollowUpShortTerm => new InspectionReason(9, "Suivi à court terme (Contrôle intermédiaire dynamique)");
        public static InspectionReason Riskier => new InspectionReason(10, "Risques accrus (dès 2020)");

    }

    public class InspectionStatus : CodeNameValueObject
    {
        public InspectionStatus(int code, string name) : base(code, name) { }
        public static InspectionStatus Planned => new InspectionStatus(1, "Planifié");
        public static InspectionStatus Suspended => new InspectionStatus(2, "Suspendu");
        public static InspectionStatus ResultsInProgress => new InspectionStatus(3, "Résultats en cours de traitement");
        public static InspectionStatus ResultsSet => new InspectionStatus(4, "Résultats saisis");
        public static InspectionStatus ResultsClosed => new InspectionStatus(5, "Résultats clôturés");
        public static InspectionStatus ResultsReleased => new InspectionStatus(6, "Résultats libérés");
        public static InspectionStatus Interrupted => new InspectionStatus(7, "Interrompu");
        public static InspectionStatus MeasuresInProgress => new InspectionStatus(8, "Mesures en cours de traitement");
        public static InspectionStatus MeasuresSet => new InspectionStatus(9, "Mesures saisies");
        public static InspectionStatus Appeal => new InspectionStatus(10, "Recours");
        public static InspectionStatus DecisionsReleased => new InspectionStatus(11, "Décisions libérées");
    }

    public class Signature : ValueObject
    {
        public string Signatory { get; }
        public string Proxy { get; }
        public string Data { get; }
        public string DataUrl { get; }
        public bool HasProxy => !string.IsNullOrWhiteSpace(Proxy);
        public bool HasSigned => !string.IsNullOrWhiteSpace(Data);
        
        public static Signature None => new Signature("", "", "", ""); 
        
        public Signature(string signatory, string proxy, string data, string dataUrl)
        {
            bool IsEmpty() =>
                string.IsNullOrWhiteSpace(signatory) &&
                string.IsNullOrWhiteSpace(proxy) &&
                string.IsNullOrWhiteSpace(data) &&
                string.IsNullOrWhiteSpace(dataUrl);

            if (!IsEmpty() && string.IsNullOrWhiteSpace(data))
                throw new ArgumentNullException(nameof(data), "Signature drawing data must be non-empty.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(dataUrl))
                throw new ArgumentNullException(nameof(dataUrl), "Signature image must be non-empty.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(Signatory))
                throw new ArgumentNullException(nameof(signatory), $"{nameof(Signatory)} must be non-empty.");

            Signatory = signatory;
            Proxy = proxy;
            Data = data;
            DataUrl = dataUrl;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Signatory;
            yield return Proxy;
            yield return Data;
        }
    }
}
