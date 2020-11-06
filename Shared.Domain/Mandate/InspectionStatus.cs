namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate {
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
}