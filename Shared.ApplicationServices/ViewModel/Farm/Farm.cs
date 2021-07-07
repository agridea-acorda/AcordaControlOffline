using System.Linq;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.Farm
{
    public class Farm : ViewModel, IFarm<Badge.Badge>
    {
        public int Id { get; set; }
        public string Ktidb { get; set; }
        public string FarmName { get; set; }
        public string PersonName { get; set; }
        public int? TvdNumber { get; set; }
        public string Address { get; set; }
        public string FarmType { get; set; }
        public int FarmTypeCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AgriculturalArea { get; set; }
        public string NonAgriculturalArea { get; set; }
        public string BovineStandardUnits { get; set; }
        public string BovineStandardUnitsFromBdta { get; set; }
        public Badge.Badge[] Badges { get; set; }

        public static Farm FromDomain(Domain.Farm.Farm farm)
        {
            var model = new Farm
            {
                Id = (int) farm.Id,
                Ktidb = farm.Ktidb,
                FarmName = farm.FarmName,
                Address = farm.Address,
                FarmType = farm.FarmType,
                FarmTypeCode = farm.FarmTypeCode,
                Email = farm.Email,
                PhoneNumber = farm.PhoneNumber,
                AgriculturalArea = farm.AgriculturalArea,
                NonAgriculturalArea = farm.NonAgriculturalArea,
                BovineStandardUnits = farm.BovineStandardUnits,
                BovineStandardUnitsFromBdta = farm.BovineStandardUnitsFromBdta,
                Badges = farm.Badges?.Select(Badge.Badge.FromDomain).ToArray(),
                TvdNumber = farm.TvdNumber,
                PersonName = farm.PersonName
            };
            return model;
        }
    }
}