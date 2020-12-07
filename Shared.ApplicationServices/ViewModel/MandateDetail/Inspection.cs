namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Inspection: MandateList.Inspection
    {
        public int FarmInspectionId { get; set; }
        public string Campaign { get; set; }
        public bool HasComplianceRequirements { get; set; }
        public string ComplianceDeadline { get; set; }
        public string ClosedBy { get; set; }
        public bool CanGeneratePdfReport { get; set; }
        public bool CanDisplayPdfReport { get; set; }
        public bool CanClose { get; set; }
        public bool CanReopen { get; set; }

        public static Inspection FromDomain(Domain.Inspection.Inspection inspection)
        {
            var model = new Inspection
            {
                // todo fill this
            };
            return model;
        }
    }
}