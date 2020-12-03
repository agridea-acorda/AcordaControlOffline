using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Badge;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Farm
{
    public interface IFarm<T> : IFarmBase where T:IBadge
    {
        string AgriculturalArea { get; set; }
        string NonAgriculturalArea { get; set; }
        string BovineStandardUnits { get; set; }
        string BovineStandardUnitsFromBdta { get; set; }
        T[] Badges { get; set; }
    }
}