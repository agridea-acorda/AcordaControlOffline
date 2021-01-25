using System;
using System.IO;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Mandate;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.ViewModel.MandateList;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using Mandate = Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate.Mandate;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Tests
{
    public class SerializationTests
    {
        private readonly Checklist checklist_;
        private readonly Inspection inspection_;
        private readonly Domain.Farm.Farm farm_;
        private readonly ITestOutputHelper testOutputHelper_;

        public SerializationTests(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper_ = testOutputHelper;
            Console.SetOut(new TestOutputWriter(testOutputHelper_));
            checklist_ = TestDataHelper.ConstructChecklist();
            inspection_ = TestDataHelper.ConstructInspection();
            farm_ = TestDataHelper.ConstructFarm();
        }

        [Fact]
        public void Can_serialize_checklist()
        {
            string json = new ChecklistFactory().Serialize(checklist_);
            json.Should().NotBeEmpty();
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "checklist.json"), json);
        }

        [Fact]
        public void Can_parse_checklist()
        {
            var json = File.ReadAllText("./Data/checklist.json");
            var checklist = new ChecklistFactory().Parse(json);
            TestDataHelper.ChecklistTreeStructureShouldBeConsistent(checklist);
        }

        [Fact]
        public void Can_serialize_then_parse_checklist()
        {
            TestDataHelper.ChecklistTreeStructureShouldBeConsistent(checklist_);
            var checklistFactory = new ChecklistFactory();
            string json = checklistFactory.Serialize(checklist_);
            var checklist = checklistFactory.Parse(json);
            TestDataHelper.ChecklistTreeStructureShouldBeConsistent(checklist);
        }

        [Fact]
        public void Can_serialize_then_parse_autoset()
        {
            TestDataHelper.ChecklistTreeStructureShouldBeConsistent(checklist_);
            checklist_.Find("R1,P2").SetAuto();
            var checklistFactory = new ChecklistFactory();
            string json = checklistFactory.Serialize(checklist_);
            var checklist = checklistFactory.Parse(json);
            TestDataHelper.ChecklistTreeStructureShouldBeConsistent(checklist);
            checklist.Find("R1,P2").IsAutoSet.Should().Be(true);
        }

        [Fact]
        public void Can_serialize_inspection()
        {
            string json = new InspectionFactory().Serialize(inspection_);
            json.Should().NotBeEmpty();
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "inspection.json"), json);
        }

        [Fact]
        public void Can_serialize_then_parse_inspection()
        {
            TestDataHelper.InspectionShouldBeSuchAsConstructed(inspection_);
            var factory = new InspectionFactory();
            string json = factory.Serialize(inspection_);
            var inspection = factory.Parse(json);
            TestDataHelper.InspectionShouldBeSuchAsConstructed(inspection);
        }

        [Fact]
        public void Can_serialize_then_parse_inspection_with_pdf_report()
        {
            TestDataHelper.InspectionShouldBeSuchAsConstructed(inspection_);
            inspection_.SetPdfReport(new PdfReport(inspection_.GenerateInspectionPdf(farm_, false)));
            var factory = new InspectionFactory();
            string json = factory.Serialize(inspection_);
            var inspection = factory.Parse(json);
            TestDataHelper.InspectionShouldBeSuchAsConstructed(inspection);
            inspection.PdfReport.Should().Be(inspection_.PdfReport);
        }

        [Fact]
        public void Can_serialize_farm()
        {
            string json = new FarmFactory().Serialize(farm_);
            json.Should().NotBeEmpty();
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "farm.json"), json);
        }

        [Fact]
        public void Can_serialize_then_parse_farm()
        {
            TestDataHelper.FarmShouldBeSuchAsConstructed(farm_);
            var factory = new FarmFactory();
            string json = factory.Serialize(farm_);
            var farm = factory.Parse(json);
            TestDataHelper.FarmShouldBeSuchAsConstructed(farm);
        }

        [Fact]
        public void Can_serialize_empty_byte_array()
        {
            var bytes = Enumerable.Empty<byte[]>().ToArray();
            var json = JsonConvert.SerializeObject(bytes);
            json.Should().Be("[]");
        }

        [Fact]
        public void Can_Serialize_PdfReport_None()
        {
            var pdfReport = PdfReport.None;
            var json = JsonConvert.SerializeObject(pdfReport);
            json.Should().Be("{\"Bytes\":\"\"}");
        }

        [Fact]
        public void Can_deserialize_PdfReport_None()
        {
            string json = "{\"Bytes\":\"\"}";
            var pdfReport = JsonConvert.DeserializeObject<PdfReport>(json);
            pdfReport.Should().NotBeNull();
            pdfReport.Bytes.Should().NotBeNull();
            pdfReport.Bytes.Should().HaveCount(0);
            pdfReport.Should().Be(PdfReport.None);
        }

        [Fact]
        public void Can_deserialize_PdfReport_with_empty_byte_array_alternative_json()
        {
            string json = "{\"Bytes\":[]}";
            var pdfReport = JsonConvert.DeserializeObject<PdfReport>(json);
            pdfReport.Should().NotBeNull();
            pdfReport.Bytes.Should().NotBeNull();
            pdfReport.Bytes.Should().HaveCount(0);
            pdfReport.Should().Be(PdfReport.None);
        }


        [Fact]
        public void Can_find_nodes_in_real_checklist()
        {
            var json = File.ReadAllText("./Data/checklist_real_FarmInspectionId83811.json");
            var checklist = new ChecklistFactory().Parse(json);
            var node = checklist.Find("12.02_2018");
            node.Should().NotBeNull();
            node.ConjunctElementCode.Should().Be("12.02_2018");
        }

        [Fact]
        void Can_prepare_merge_package()
        {
            // client end
            var inspection = TestDataHelper.ConstructInspection();
            var checklist = TestDataHelper.ConstructChecklist();
            var mandate = new Mandate(1, new[] { inspection });
            var mergePackage = MergePackage.FromDomain(mandate, new[] { checklist, checklist });
            string filename = Path.GetTempFileName() + ".json";
            File.WriteAllText(filename, JsonConvert.SerializeObject(mergePackage));

            // server end
            var receivedMergePackage = JsonConvert.DeserializeObject<MergePackage>(File.ReadAllText(filename));
            receivedMergePackage.Should().NotBeNull();
            var mandateOnServer = JsonConvert.DeserializeObject<MandateDeserializationDto>(receivedMergePackage.Mandate);
            mandateOnServer.Should().NotBeNull();
            var checklistsOnServer = JsonConvert.DeserializeObject<ChecklistDeserializationDto[]>(receivedMergePackage.Checklists);
            checklistsOnServer.Should().NotBeNull();
            checklistsOnServer.Length.Should().Be(2);
        }

        [Fact]
        void Can_deserialize_transmitted_checklist_array()
        {
            string json = File.ReadAllText(Path.Combine("C:\\Users\\nde\\Downloads", "exemple-checklists.json"));
            var checklistsOnServer = JsonConvert.DeserializeObject<ChecklistDeserializationDto[]>(json);
            checklistsOnServer.Should().NotBeNull();
            checklistsOnServer.Length.Should().Be(1);
        }

        [Fact]
        void Can_deserialize_transmitted_checklist_array_short()
        {
            string json = File.ReadAllText(Path.Combine("C:\\Users\\nde\\Downloads", "exemple-checklists-short.json"));
            var checklistsOnServer = JsonConvert.DeserializeObject<ChecklistDeserializationDto[]>(json);
            checklistsOnServer.Should().NotBeNull();
            checklistsOnServer.Length.Should().Be(1);
        }
    }
}
