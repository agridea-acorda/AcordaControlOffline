using System;
using System.IO;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Pdf.Model;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class InspectionTests
    {
        [Fact]
        public void Can_construct_new_inspection()
        {
            var inspection = TestDataHelper.ConstructInspection();
            TestDataHelper.InspectionShouldBeSuchAsConstructed(inspection);
        }

        [Fact]
        public void Can_generate_pdf_nok_with_compliance_due_and_auto_na_nc()
        {
            var inspection = TestDataHelper.ConstructInspection();
            var farm = TestDataHelper.ConstructFarm();
            var checklist = TestDataHelper.ConstructChecklist();
            checklist.SetOutcome("R1,P1", InspectionOutcome.Ok);
            checklist.Find("R1,P2")
                     .SetOutcome(InspectionOutcome.NotOk)
                     .SetInspectorComment("Livret des sorties pas à jour => à remplir sous délai.")
                     .SetDefect(new Defect("Livret des sorties pas à jour", Defect.Measurement.Unspecified), DefectSeriousness.Small);
            checklist.SetOutcome("R1", InspectionOutcome.NotOk);
            checklist.SetOutcome("R2,G1,P1", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1,P2", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1,P3", InspectionOutcome.Ok);
            checklist.SetOutcome("R2,G1", InspectionOutcome.Ok);
            checklist.Find("R2,G2,SG1,P3").SetOutcome(InspectionOutcome.NotInspected).SetAuto();
            checklist.Find("R2,G2,SG1,P4").SetOutcome(InspectionOutcome.NotInspected).SetAuto();
            checklist.Find("R2,G2").SetOutcome(InspectionOutcome.NotApplicable).SetAuto();
            checklist.SetOutcome("R2", InspectionOutcome.Ok);
            
            inspection.RequireActionOfDocuments("Livret des sorties à jour", new DateTime(2021, 01, 29));
            inspection.InspectorSigns(TestDataHelper.ConstructSignature("Joe l'inspecteur"));
            inspection.FarmerSigns(TestDataHelper.ConstructSignature(farm.FarmName));
            inspection.Finish(new FinishStatus(new DateTime(2021, 01, 22), "Joe the inspector"));
            
            string organizationName = Organization.Ajapi.Name;
            string userName = "username";
            string logoPath = AppDomain.CurrentDomain.BaseDirectory + "\\img\\focaa.png";
            var model = InspectionPdfModel.FromDomain(inspection,
                                                      farm,
                                                      checklist,
                                                      organizationName,
                                                      logoPath);
            var pdf = new InspectionPdf(model, userName);
            File.WriteAllBytes(Path.GetTempFileName() + ".pdf", pdf.CreatePdf());
        }
    }
}
