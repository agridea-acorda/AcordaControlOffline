using System;
using System.Collections.Generic;
using System.Text;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model
{
    public interface IHasIndicators
    {
        bool IndicatorsAnimal { get; set; }

        bool IndicatorsBdNetwork { get; set; }

        bool IndicatorsBdQuality { get; set; }

        bool IndicatorsBees { get; set; }

        bool IndicatorsBts { get; set; }

        bool IndicatorsBtsNextYear { get; set; }

        bool IndicatorsCbe { get; set; }

        bool IndicatorsCer { get; set; }

        bool IndicatorsCerCleaningSystem { get; set; }

        bool IndicatorsCerCleaningSystemNextYear { get; set; }

        bool IndicatorsCerLowEmissionFertilization { get; set; }

        bool IndicatorsCerLowEmissionFertilizationPreviousYear { get; set; }

        bool IndicatorsCerLowNFoodForPigs { get; set; }

        bool IndicatorsCerLowNFoodForPigsPreviousYear { get; set; }

        bool IndicatorsCerLowNFoodForPoultry { get; set; }

        bool IndicatorsCerLowNFoodForPoultryPreviousYear { get; set; }

        bool IndicatorsCerLowPesticide { get; set; }

        bool IndicatorsCerLowPesticideBeetroot { get; set; }

        bool IndicatorsCerLowPesticideBeetrootNextYear { get; set; }

        bool IndicatorsCerLowPesticideFruits { get; set; }

        bool IndicatorsCerLowPesticideFruitsNextYear { get; set; }

        bool IndicatorsCerLowPesticideOpenCropland { get; set; }

        bool IndicatorsCerLowPesticideOpenCroplandNextYear { get; set; }

        bool IndicatorsCerLowPesticideWine { get; set; }

        bool IndicatorsCerLowPesticideWineNextYear { get; set; }

        bool IndicatorsCerMildSoilTreatment { get; set; }

        bool IndicatorsCerMildSoilTreatmentNextYear { get; set; }

        bool IndicatorsCerOther { get; set; }

        bool IndicatorsCerPrecisePesticideApplication { get; set; }

        bool IndicatorsCerPrecisePesticideApplicationNextYear { get; set; }

        bool IndicatorsCqp { get; set; }

        bool IndicatorsExtenso { get; set; }

        bool IndicatorsExtensoNextYear { get; set; }

        bool IndicatorsFish { get; set; }

        bool IndicatorsLiveStockUnknownUmos { get; set; }

        bool IndicatorsLiveStockWith3UgbAnd02Umos { get; set; }

        bool IndicatorsLiveStockWithLessThan3UgbOr02Umos { get; set; }

        bool IndicatorsOeln { get; set; }

        bool IndicatorsOelnAromatic { get; set; }

        bool IndicatorsOelnAromaticNextYear { get; set; }

        bool IndicatorsOelnBerries { get; set; }

        bool IndicatorsOelnBerriesNextYear { get; set; }

        bool IndicatorsOelnFieldCrops { get; set; }

        bool IndicatorsOelnFieldCropsNextYear { get; set; }

        bool IndicatorsOelnFruits { get; set; }

        bool IndicatorsOelnFruitsNextYear { get; set; }

        bool IndicatorsOelnVeggies { get; set; }

        bool IndicatorsOelnVeggiesNextYear { get; set; }

        bool IndicatorsOelnWine { get; set; }

        bool IndicatorsOelnWineNextYear { get; set; }

        bool IndicatorsOpenCropland3HaPlus { get; set; }

        bool IndicatorsOrganic { get; set; }

        bool IndicatorsOrganicFieldCrops { get; set; }

        bool IndicatorsOrganicFieldCropsNextYear { get; set; }

        bool IndicatorsOrganicFruits { get; set; }

        bool IndicatorsOrganicFruitsNextYear { get; set; }

        bool IndicatorsOrganicMultiannualCrops { get; set; }

        bool IndicatorsOrganicMultiannualCropsNextYear { get; set; }

        bool IndicatorsOrganicWine { get; set; }

        bool IndicatorsOrganicWineNextYear { get; set; }

        bool IndicatorsPlvh { get; set; }

        bool IndicatorsPlvhPreviousYear { get; set; }

        bool IndicatorsRaus { get; set; }

        bool IndicatorsRausNextYear { get; set; }

        bool IndicatorsSummering { get; set; }

        bool IndicatorsSummeringAnimal { get; set; }

        bool IndicatorsSummeringCqp { get; set; }

        bool IndicatorsPureWinemaker { get; set; }
    }
}
