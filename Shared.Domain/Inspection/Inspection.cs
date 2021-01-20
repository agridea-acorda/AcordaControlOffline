using System;
using System.IO;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model;
using Agridea.DomainDrivenDesign;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class Inspection: AggregateRoot
    {
        public Inspection(InitObject initValues)
        {
            if (initValues.FarmInspectionId <= 0)
                throw new ArgumentOutOfRangeException(nameof(initValues.FarmInspectionId), $"{nameof(initValues.FarmInspectionId)} must be > 0");

            if (initValues.InspectionId == Guid.Empty)
                throw new ArgumentNullException($"{nameof(initValues.InspectionId)} must be non-empty.");

            if (initValues.Domain == null)
                throw new ArgumentNullException($"{nameof(initValues.Domain)} must be defined.");

            if (initValues.Campaign == null)
                throw new ArgumentNullException($"{nameof(initValues.Campaign)} must be defined.");

            if (initValues.Reason == null)
                throw new ArgumentNullException($"{nameof(initValues.Reason)} must be defined.");

            if (initValues.ChecklistId <= 0)
                throw new ArgumentOutOfRangeException(nameof(initValues.ChecklistId), $"{nameof(initValues.ChecklistId)} must be > 0");

            if (initValues.FarmId <= 0)
                throw new ArgumentOutOfRangeException(nameof(initValues.FarmId), $"{nameof(initValues.FarmId)} must be > 0");

            FarmInspectionId = initValues.FarmInspectionId;
            InspectionId = initValues.InspectionId;
            Domain = initValues.Domain;
            Campaign = initValues.Campaign;
            Reason = initValues.Reason;
            Comment = initValues.Comment;
            ChecklistId = initValues.ChecklistId;
            FarmId = initValues.FarmId;

            Appointment = Appointment.None;
            CommentForFarmer = CommentForOffice = "";
            Status = InspectionStatus.Planned;
            InspectorSignature = Inspector2Signature = FarmerSignature = Signature.None;
            Compliance = Compliance.Empty;
            PdfReport = PdfReport.None;
            FinishStatus = FinishStatus.NotFinished;
            CloseStatus = CloseStatus.NotClosed;
            ReopenStatus = ReopenStatus.NotReopened;
            PercentComputed = 0;
            DateComputed = DateTime.MinValue;
            OutcomeComputed = InspectionOutcome.NotInspected;
        }
        public int FarmInspectionId { get; }
        public Guid InspectionId { get; }
        public long ChecklistId { get; set; }
        public long FarmId { get; }
        public Domain Domain { get; }
        public Campaign Campaign { get; }
        public InspectionReason Reason { get; }
        public string Comment { get; }
        
        public Appointment Appointment { get; private set; }
        public string CommentForFarmer { get; private set; }
        public string CommentForOffice { get; private set; }
        public InspectionStatus Status { get; private set; }
        public double PercentComputed { get; private set; }
        public DateTime DateComputed { get; private set; }
        public InspectionOutcome OutcomeComputed { get; private set; }
        public Signature InspectorSignature { get; private set; }
        public Signature Inspector2Signature { get; private set; }
        public Signature FarmerSignature { get; private set; }
        public Compliance Compliance { get; private set; }
        public PdfReport PdfReport { get; private set; }
        public FinishStatus FinishStatus { get; private set; }
        public CloseStatus CloseStatus { get; private set; }
        public ReopenStatus ReopenStatus { get; private set; }

        public Inspection SetAppointment(Appointment appointment)
        {
            Appointment = appointment;
            return this;
        }

        public Inspection SetCommentForFarmer(string comment)
        {
            CommentForFarmer = comment;
            return this;
        }

        public Inspection SetCommentForOffice(string comment)
        {
            CommentForOffice = comment;
            return this;
        }

        public Inspection SetCompliance(Compliance compliance)
        {
            Compliance = compliance;
            return this;
        }

        public Inspection InspectorSigns(Signature signature)
        {
            Console.WriteLine($"Saving inspector signature for Inspection {FarmInspectionId}");
            InspectorSignature = signature;
            return this;
        }

        public Inspection FarmerSigns(Signature signature)
        {
            Console.WriteLine($"Saving farmer signature for Inspection {FarmInspectionId}");
            FarmerSignature = signature;
            return this;
        }

        public Inspection Inspector2Signs(Signature signature)
        {
            Console.WriteLine($"Saving additional inspector signature for Inspection {FarmInspectionId}");
            Inspector2Signature = signature;
            return this;
        }

        public bool HasComplianceRequirements()
        {
            return new HasComplianceRequirements().IsSatisfiedBy(this);
        }

        public bool CanGeneratePdfReport()
        {
            return new CanGeneratePdfReport().IsSatisfiedBy(this);
        }

        public bool CanDisplayPdfReport()
        {
            return new CanDisplayPdfReport().IsSatisfiedBy(this);
        }

        public bool CanClose()
        {
            return new CanClose().IsSatisfiedBy(this);
        }

        public bool CanReopen()
        {
            return new CanReopen().IsSatisfiedBy(this);
        }

        public byte[] GenerateInspectionPdf(Inspection inspection, Farm.Farm farm)
        {

            var farmDisplay = GetFarmDisplay(farm);
            //TODO recuperer les données
            string cantonCode = "JU";//AcordaControlSession.Canton.Code;
            string userName = "DefaultUserName";//AcordaControlSession.UserSecurityContext.UserName;
            string logoPath = "";//Server.MapPath("~/Content/Images/focaa.png");
            var model = InspectionPdfModel.FromInspection(inspection,
                farmDisplay,
                cantonCode,
                logoPath);
            var pdf = new InspectionPdf(model, userName, true);
            return pdf.CreatePdf();


        }

        public FarmDisplayModel GetFarmDisplay(Farm.Farm f)
        {
           return new FarmDisplayModel
                        {
                            Id = (int)f.Id,
                            Ktidb = f.Ktidb,
                            Name = f.FarmName,
                            //TODO get farm datas
                            /*NameAddOn1 = f.NameAddOn1,
                            NameAddOn2 = f.NameAddOn2,
                            Street = f.Street,
                            HouseNumber = f.HouseNumber,
                            PostOfficeBoxNumber = f.PostOfficeBoxNumber,
                            TownZip = f.Town.Zip,
                            TownName = f.Town.Name,
                            AddressAddOn = f.AddressAddOn,
                            FarmTypeCode = f.FarmTypeCode,
                            FarmTypeName = f.FarmTypeName,
                            
                            TotalAgriculturalArea = f.ParcelList.Sum(p => (int?)p.AgriculturalArea) ?? 0,
                            TotalNonAgriculturalArea = f.ParcelList.Sum(p => (int?)p.NonAgriculturalArea) ?? 0,
                            TotalUgb = f.AnimalList.Sum(a => a.UgbComputed) ?? 0.0,
                            TotalUgbTvd = f.AnimalList.Where(a => tvdCategoryCodes.Contains(a.AnimalTypeAnimalCategoryCode)).Sum(a => a.UgbComputed) ?? 0.0,
                            TotalUgbOther = f.AnimalList.Where(a => !tvdCategoryCodes.Contains(a.AnimalTypeAnimalCategoryCode)).Sum(a => a.UgbComputed) ?? 0.0,
                            BioConversionStartYear = f.BioConversionStartYear,
                            IsBio = f.BioRegistrationList.Any(x => x.HasCurrentYearInscription),
                            ReturnUrl = returnUrl,

                            IndicatorsId = f.Indicators != null ? f.Indicators.Id : default(int),
                            // UGB
                            IndicatorsLiveStockUnknownUmos = f.Indicators != null && f.Indicators.LiveStockUnknownUmos,
                            IndicatorsLiveStockWith3UgbAnd02Umos = f.Indicators != null && f.Indicators.LiveStockWith3UgbAnd02Umos,
                            IndicatorsLiveStockWithLessThan3UgbOr02Umos = f.Indicators != null && f.Indicators.LiveStockWithLessThan3UgbOr02Umos,
                            IndicatorsAnimal = f.Indicators != null && f.Indicators.Animal,
                            // Bees Fish
                            IndicatorsBees = f.Indicators != null && f.Indicators.Bees,
                            IndicatorsFish = f.Indicators != null && f.Indicators.Fish,
                            // Biodiversity Cbe       
                            IndicatorsBdNetwork = f.Indicators != null && f.Indicators.BdNetwork,
                            IndicatorsBdQuality = f.Indicators != null && f.Indicators.BdQuality,
                            IndicatorsCbe = f.Indicators != null && f.Indicators.Cbe,
                            // Bts Raus       
                            IndicatorsBts = f.Indicators != null && f.Indicators.Bts,
                            IndicatorsRaus = f.Indicators != null && f.Indicators.Raus,
                            IndicatorsBtsNextYear = f.Indicators != null && f.Indicators.BtsNextYear,
                            IndicatorsRausNextYear = f.Indicators != null && f.Indicators.RausNextYear,
                            // Cer
                            IndicatorsCerCleaningSystem = f.Indicators != null && f.Indicators.CerCleaningSystem,
                            IndicatorsCerCleaningSystemNextYear = f.Indicators != null && f.Indicators.CerCleaningSystemNextYear,
                            IndicatorsCerLowEmissionFertilization = f.Indicators != null && f.Indicators.CerLowEmissionFertilization,
                            IndicatorsCerLowEmissionFertilizationPreviousYear = f.Indicators != null && f.Indicators.CerLowEmissionFertilizationPreviousYear,
                            IndicatorsCerLowPesticideBeetroot = f.Indicators != null && f.Indicators.CerLowPesticideBeetroot,
                            IndicatorsCerLowPesticideBeetrootNextYear = f.Indicators != null && f.Indicators.CerLowPesticideBeetrootNextYear,
                            IndicatorsCerLowPesticideFruits = f.Indicators != null && f.Indicators.CerLowPesticideFruits,
                            IndicatorsCerLowPesticideFruitsNextYear = f.Indicators != null && f.Indicators.CerLowPesticideFruitsNextYear,
                            IndicatorsCerLowPesticideWine = f.Indicators != null && f.Indicators.CerLowPesticideWine,
                            IndicatorsCerLowPesticideWineNextYear = f.Indicators != null && f.Indicators.CerLowPesticideWineNextYear,
                            IndicatorsCerMildSoilTreatment = f.Indicators != null && f.Indicators.CerMildSoilTreatment,
                            IndicatorsCerMildSoilTreatmentNextYear = f.Indicators != null && f.Indicators.CerMildSoilTreatmentNextYear,
                            IndicatorsCerPrecisePesticideApplication = f.Indicators != null && f.Indicators.CerPrecisePesticideApplication,
                            IndicatorsCerPrecisePesticideApplicationNextYear = f.Indicators != null && f.Indicators.CerPrecisePesticideApplicationNextYear,
                            IndicatorsCerLowPesticide = f.Indicators != null && f.Indicators.CerLowPesticide,
                            IndicatorsCerOther = f.Indicators != null && f.Indicators.CerOther,
                            IndicatorsCer = f.Indicators != null && f.Indicators.Cer,
                            // Extenso
                            IndicatorsExtenso = f.Indicators != null && f.Indicators.Extenso,
                            IndicatorsExtensoNextYear = f.Indicators != null && f.Indicators.ExtensoNextYear,
                            // Cqp
                            IndicatorsCqp = f.Indicators != null && f.Indicators.Cqp,
                            // Organic
                            IndicatorsOrganicFieldCrops = f.Indicators != null && f.Indicators.OrganicFieldCrops,
                            IndicatorsOrganicFieldCropsNextYear = f.Indicators != null && f.Indicators.OrganicFieldCropsNextYear,
                            IndicatorsOrganicFruits = f.Indicators != null && f.Indicators.OrganicFruits,
                            IndicatorsOrganicFruitsNextYear = f.Indicators != null && f.Indicators.OrganicFruitsNextYear,
                            IndicatorsOrganicMultiannualCrops = f.Indicators != null && f.Indicators.OrganicMultiannualCrops,
                            IndicatorsOrganicMultiannualCropsNextYear = f.Indicators != null && f.Indicators.OrganicMultiannualCropsNextYear,
                            IndicatorsOrganicWine = f.Indicators != null && f.Indicators.OrganicWine,
                            IndicatorsOrganicWineNextYear = f.Indicators != null && f.Indicators.OrganicWineNextYear,
                            IndicatorsOrganic = f.Indicators != null && f.Indicators.Organic,
                            // Oeln       
                            IndicatorsOelnAromatic = f.Indicators != null && f.Indicators.OelnAromatic,
                            IndicatorsOelnAromaticNextYear = f.Indicators != null && f.Indicators.OelnAromaticNextYear,
                            IndicatorsOelnBerries = f.Indicators != null && f.Indicators.OelnBerries,
                            IndicatorsOelnBerriesNextYear = f.Indicators != null && f.Indicators.OelnBerriesNextYear,
                            IndicatorsOelnFieldCrops = f.Indicators != null && f.Indicators.OelnFieldCrops,
                            IndicatorsOelnFieldCropsNextYear = f.Indicators != null && f.Indicators.OelnFieldCropsNextYear,
                            IndicatorsOelnFruits = f.Indicators != null && f.Indicators.OelnFruits,
                            IndicatorsOelnFruitsNextYear = f.Indicators != null && f.Indicators.OelnFruitsNextYear,
                            IndicatorsOelnVeggies = f.Indicators != null && f.Indicators.OelnVeggies,
                            IndicatorsOelnVeggiesNextYear = f.Indicators != null && f.Indicators.OelnVeggiesNextYear,
                            IndicatorsOelnWine = f.Indicators != null && f.Indicators.OelnWine,
                            IndicatorsOelnWineNextYear = f.Indicators != null && f.Indicators.OelnWineNextYear,
                            IndicatorsOeln = f.Indicators != null && f.Indicators.Oeln,
                            // PLVH
                            IndicatorsPlvh = f.Indicators != null && f.Indicators.Plvh,
                            IndicatorsPlvhPreviousYear = f.Indicators != null && f.Indicators.PlvhPreviousYear,
                            // Summering
                            IndicatorsSummering = f.Indicators != null && f.Indicators.Summering,
                            IndicatorsSummeringAnimal = f.Indicators != null && f.Indicators.SummeringAnimal,
                            IndicatorsSummeringCqp = f.Indicators != null && f.Indicators.SummeringCqp,
                            // TO
                            IndicatorsOpenCropland3HaPlus = f.Indicators != null && f.Indicators.OpenCropland3HaPlus,
                            // Pure winemaker
                            IndicatorsPureWinemaker = f.Indicators != null && f.Indicators.PureWinemaker*/
                        };
        }

        public class InitObject
        {
            public int FarmInspectionId { get; set; }
            public Guid InspectionId { get; set; }
            public long ChecklistId { get; set; }
            public long FarmId { get; set; }
            public Domain Domain { get; set; }
            public Campaign Campaign { get; set; }
            public InspectionReason Reason { get; set; }
            public string Comment { get; set; }
        }
    }
}
