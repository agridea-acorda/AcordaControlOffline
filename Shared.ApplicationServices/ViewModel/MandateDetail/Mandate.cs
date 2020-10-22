using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Mandate: ViewModel
    {
        public Farm Farm { get; set; }
        public Badge[] Badges { get; set; }
        public string AgriculturalArea { get; set; }
        public string NonAgriculturalArea { get; set; }
        public string BovineStandardUnits { get; set; }
        public string BovineStandardUnitsFromBdta { get; set; }
        public CheckList CheckList { get; set; }
    }

    public class CheckList: MandateList.CheckList
    {
        public string Campaign { get; set; }
    }

    public class Farm: MandateList.Farm { }
}
