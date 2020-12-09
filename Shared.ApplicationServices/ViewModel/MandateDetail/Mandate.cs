using System.Linq;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateDetail
{
    public class Mandate: ViewModel
    {
        public Farm.Farm Farm { get; set; }
        public Inspection[] Inspections { get; set; }

        public static Mandate FromDomain(Domain.Farm.Farm farm, Domain.Mandate.Mandate mandate)
        {
            return new Mandate
            {
                Farm = ApplicationServices.ViewModel.Farm.Farm.FromDomain(farm),
                Inspections = mandate.Inspections.Select(Inspection.FromDomain).ToArray()
            };
        }
    }
}
