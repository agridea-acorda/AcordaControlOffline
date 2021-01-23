using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate
{
    public class MandateDeserializationDto
    {
        public int FarmId { get; set; }
        public InspectionDeserializationDto.Root[] Inspections { get; set; }
    }
}
