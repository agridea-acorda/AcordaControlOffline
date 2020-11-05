using System;
using System.Collections.Generic;
using System.Text;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate
{
    public class Checklist: Entity
    {
        public string Comment { get; set; }
        public string CommentForFarmer { get; set; }
        public string CommentForOffice { get; set; }
        public DateTime DateComputed { get; /*private*/ set; }
        public Guid InspectionId { get; set; }
        public Signature InspectorSignature { get; set; }
        public Signature Inspector2Signature { get; set; }
        public Signature FarmerSignature { get; set; }
        public InspectionMode Mode { get; set; }
        public InspectionOutcome OutcomeComputed { get; set; }
        public double PercentComputed { get; set; }
        public FinishStatus FinishStatus { get; set; }
        public Compliance Compliance { get; set; }
        public InspectionReason Reason { get; set; }
    }

    public class FinishStatus : ValueObject
    {
        public DateTime? ReopenDate { get; set; }
        public string ReopenedBy { get; set; }
        public DateTime? CloseDate { get; set; }
        public string ClosedBy { get; set; }
        public string DoneByInspector { get; set; }
        public string DoneByOrganization { get; set; }
        public DateTime? DoneOn { get; set; }
        public InspectionStatus Status { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }

    public class Compliance : ValueObject
    {
        public string ActionsOrDocuments { get; set; }
        public DateTime? DueDate { get; set; }
        public bool DueDateNotRespected { get; set; }
        public bool DueDateRespected { get; set; }
        public bool FurtherInvestigationNeeded { get; set; }
        public bool IncompleteOrNonCompliant { get; set; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }

    public class CodeNameValueObject : ValueObject
    {
        public CodeNameValueObject(int code, string name)
        {
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
        public Signature(string signatory, string proxy, string data, string dataUrl)
        {
            Signatory = signatory;
            Proxy = proxy;
            Data = data;
            DataUrl = dataUrl;
        }
        public string Signatory { get; }
        public string Proxy { get; }
        public string Data { get; }
        public string DataUrl { get; }
        public bool HasProxy => !string.IsNullOrWhiteSpace(Proxy);
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Signatory;
            yield return Proxy;
            yield return Data;
        }
        public static Signature None => new Signature("", "", "", "");
    }
}
