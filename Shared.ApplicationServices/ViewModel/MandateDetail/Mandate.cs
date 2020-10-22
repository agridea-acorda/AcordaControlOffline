using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Mandate: ViewModel
    {
        public Farm Farm { get; set; }
        public CheckList[] CheckLists { get; set; }
    }

    public class CheckList: MandateList.CheckList
    {
        public int FarmInspectionId { get; set; }
        public string Campaign { get; set; }
        public bool HasComplianceRequirements { get; set; }
        public string ComplianceDeadline { get; set; }
        public bool isClosed { get; set; }
        public string closeDate { get; set; }
        public string closedBy { get; set; }
        public bool CanGeneratePdfReport { get; set; }
        public bool CanDisplayPdfReport { get; set; }
        public bool CanClose { get; set; }
        public bool CanReopen { get; set; }
    }

    public class Farm : MandateList.Farm
    {
        public string AgriculturalArea { get; set; }
        public string NonAgriculturalArea { get; set; }
        public string BovineStandardUnits { get; set; }
        public string BovineStandardUnitsFromBdta { get; set; }
        public Badge[] Badges { get; set; }
    }
}
