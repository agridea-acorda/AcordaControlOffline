namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
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
}