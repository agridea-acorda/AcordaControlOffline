using System;
using System.IO;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Farm;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

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
    }
}
