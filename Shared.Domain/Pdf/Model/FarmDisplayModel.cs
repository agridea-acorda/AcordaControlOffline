using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public class FarmDisplayModel : IHasIndicators
    {
        #region Properties

        public string Address => String.Join(", ", AddressAddOn, AddressPart2, AddressPart3);
        public string AddressAddOn { get; set; }
        public string AddressPart2 => PostOfficeBoxNumber == null ? String.Join(" ", Street, HouseNumber) : $"Case postale {PostOfficeBoxNumber}";
        public string AddressPart3 => String.Join(" ", TownZip, TownName);
        public virtual int? BioConversionStartYear { get; set; }
        public string CompleteName => String.Join(" ", Name, NameAddOn1, NameAddOn2);
        public string FarmType => String.Join(" ", FarmTypeCode, FarmTypeName);
        public int FarmTypeCode { get; set; }
        public string FarmTypeName { get; set; }
        public string HouseNumber { get; set; }
        public int Id { get; set; }
        public int IndicatorsId { get; set; }
        public bool IndicatorsAnimal { get; set; }
        public bool IndicatorsBdNetwork { get; set; }
        public bool IndicatorsBdQuality { get; set; }
        public bool IndicatorsBees { get; set; }
        public bool IndicatorsBts { get; set; }
        public bool IndicatorsBtsNextYear { get; set; }
        public bool IndicatorsCbe { get; set; }
        public bool IndicatorsCer { get; set; }
        public bool IndicatorsCerCleaningSystem { get; set; }
        public bool IndicatorsCerCleaningSystemNextYear { get; set; }
        public bool IndicatorsCerLowEmissionFertilization { get; set; }
        public bool IndicatorsCerLowEmissionFertilizationPreviousYear { get; set; }
        public bool IndicatorsCerLowNFoodForPigs { get; set; }
        public bool IndicatorsCerLowNFoodForPigsPreviousYear { get; set; }
        public bool IndicatorsCerLowNFoodForPoultry { get; set; }
        public bool IndicatorsCerLowNFoodForPoultryPreviousYear { get; set; }
        public bool IndicatorsCerLowPesticide { get; set; }
        public bool IndicatorsCerLowPesticideBeetroot { get; set; }
        public bool IndicatorsCerLowPesticideBeetrootNextYear { get; set; }
        public bool IndicatorsCerLowPesticideFruits { get; set; }
        public bool IndicatorsCerLowPesticideFruitsNextYear { get; set; }
        public bool IndicatorsCerLowPesticideOpenCropland { get; set; }
        public bool IndicatorsCerLowPesticideOpenCroplandNextYear { get; set; }
        public bool IndicatorsCerLowPesticideWine { get; set; }
        public bool IndicatorsCerLowPesticideWineNextYear { get; set; }
        public bool IndicatorsCerMildSoilTreatment { get; set; }
        public bool IndicatorsCerMildSoilTreatmentNextYear { get; set; }
        public bool IndicatorsCerOther { get; set; }
        public bool IndicatorsCerPrecisePesticideApplication { get; set; }
        public bool IndicatorsCerPrecisePesticideApplicationNextYear { get; set; }
        public bool IndicatorsCqp { get; set; }
        public bool IndicatorsExtenso { get; set; }
        public bool IndicatorsExtensoNextYear { get; set; }
        public bool IndicatorsFish { get; set; }
        public bool IndicatorsLiveStockUnknownUmos { get; set; }
        public bool IndicatorsLiveStockWith3UgbAnd02Umos { get; set; }
        public bool IndicatorsLiveStockWithLessThan3UgbOr02Umos { get; set; }
        public bool IndicatorsOeln { get; set; }
        public bool IndicatorsOelnAromatic { get; set; }
        public bool IndicatorsOelnAromaticNextYear { get; set; }
        public bool IndicatorsOelnBerries { get; set; }
        public bool IndicatorsOelnBerriesNextYear { get; set; }
        public bool IndicatorsOelnFieldCrops { get; set; }
        public bool IndicatorsOelnFieldCropsNextYear { get; set; }
        public bool IndicatorsOelnFruits { get; set; }
        public bool IndicatorsOelnFruitsNextYear { get; set; }
        public bool IndicatorsOelnVeggies { get; set; }
        public bool IndicatorsOelnVeggiesNextYear { get; set; }
        public bool IndicatorsOelnWine { get; set; }
        public bool IndicatorsOelnWineNextYear { get; set; }
        public bool IndicatorsOpenCropland3HaPlus { get; set; }
        public bool IndicatorsOrganic { get; set; }
        public bool IndicatorsOrganicFieldCrops { get; set; }
        public bool IndicatorsOrganicFieldCropsNextYear { get; set; }
        public bool IndicatorsOrganicFruits { get; set; }
        public bool IndicatorsOrganicFruitsNextYear { get; set; }
        public bool IndicatorsOrganicMultiannualCrops { get; set; }
        public bool IndicatorsOrganicMultiannualCropsNextYear { get; set; }
        public bool IndicatorsOrganicWine { get; set; }
        public bool IndicatorsOrganicWineNextYear { get; set; }
        public bool IndicatorsPlvh { get; set; }
        public bool IndicatorsPlvhPreviousYear { get; set; }
        public bool IndicatorsRaus { get; set; }
        public bool IndicatorsRausNextYear { get; set; }
        public bool IndicatorsSummering { get; set; }
        public bool IndicatorsSummeringAnimal { get; set; }
        public bool IndicatorsSummeringCqp { get; set; }
        public bool IndicatorsPureWinemaker { get; set; }
        public bool IsBio { get; set; }
        public string Ktidb { get; set; }
        public string Name { get; set; }
        public string NameAddOn1 { get; set; }
        public string NameAddOn2 { get; set; }
        public int? PostOfficeBoxNumber { get; set; }
        public string ReturnUrl { get; set; }
        public string Street { get; set; }
        public int TotalAgriculturalArea { get; set; }
        public int TotalNonAgriculturalArea { get; set; }
        public double TotalUgb { get; set; }
        public double TotalUgbTvd { get; set; }
        public double TotalUgbOther { get; set; }
        public string TownName { get; set; }
        public int TownZip { get; set; }

        #endregion
    }
}
