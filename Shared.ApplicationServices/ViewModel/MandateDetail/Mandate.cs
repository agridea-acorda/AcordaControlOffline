using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Mandate: ViewModel
    {
        public Farm.Farm Farm { get; set; }
        public Inspection[] Inspections { get; set; }
    }

    public class Mandate2 : ViewModel
    {
        public Farm.Farm Farm { get; set; }
        public InspectionDeserializationDto[] Inspections { get; set; }
    }
}
