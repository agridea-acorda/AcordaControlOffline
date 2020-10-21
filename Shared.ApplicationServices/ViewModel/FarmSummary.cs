using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel
{
    public class FarmSummary: ViewModel
    {
        public FarmBase Farm { get; set; }
        public Badge[] Badges { get; set; }
        public string AgriculturalArea { get; set; }
        public string NonAgriculturalArea { get; set; }
        public string BovineStandardUnits { get; set; }
        public string BovineStandardUnitsFromBdta { get; set; }
    }
}
