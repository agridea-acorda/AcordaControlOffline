using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.LocalStore.Serialization;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests;
using Agridea.DomainDrivenDesign;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Tests
{
    public class SerializationTests
    {
        private readonly Checklist checklist_;
        private readonly ITestOutputHelper testOutputHelper_;

        public SerializationTests(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper_ = testOutputHelper;
            var converter = new Converter(testOutputHelper_);
            Console.SetOut(converter);
            checklist_ = ChecklistTestHelper.BuildChecklist();
        }

        [Fact]
        public void Can_serialize()
        {
            string json = JsonConvert.SerializeObject(checklist_,
                                                      Formatting.Indented,
                                                      new JsonSerializerSettings
                                                      {
                                                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                          ContractResolver = new ChecklistContractResolver()
                                                      });
            json.Should().NotBeEmpty();
            File.WriteAllText(Path.Combine(Path.GetTempPath(), "checklist.json"), json);
        }

        [Fact]
        public void Can_deserialize_checklist()
        {
            var json = File.ReadAllText("./Data/checklist.json");
            var dto = JsonConvert.DeserializeObject<ChecklistDeserializationDto>(json);
            dto.Should().NotBeNull();

            dto.Rubrics.Should().NotBeEmpty();
            dto.Rubrics.Count.Should().Be(2);
            dto.Rubrics["R1"].Should().NotBeNull();
            dto.Rubrics["R2"].Should().NotBeNull();
            dto.Rubrics["R1"].Children.Count.Should().Be(2);
            dto.Rubrics["R1"].Children.Should().ContainKeys("R1,P1", "R1,P2");
            dto.Rubrics["R2"].Children.Count.Should().Be(2);
            dto.Rubrics["R2"].Children.Should().ContainKeys("R2,G1", "R2,G2");

            dto.Rubrics["R1"].Children["R1,P1"].ConjunctElementCode.Should().Be("R1,P1");
            dto.Rubrics["R1"].Children["R1,P1"].ElementCode.Should().Be("P1");
            dto.Rubrics["R1"].Children["R1,P1"].ShortName.Should().Be("P1");
            dto.Rubrics["R1"].Children["R1,P2"].ConjunctElementCode.Should().Be("R1,P2");
            dto.Rubrics["R1"].Children["R1,P2"].ElementCode.Should().Be("P2");
            dto.Rubrics["R1"].Children["R1,P2"].ShortName.Should().Be("P2");

            dto.Rubrics["R2"].Children["R2,G1"].ConjunctElementCode.Should().Be("R2,G1");
            dto.Rubrics["R2"].Children["R2,G1"].ElementCode.Should().Be("G1");
            dto.Rubrics["R2"].Children["R2,G1"].ShortName.Should().Be("G1");
            dto.Rubrics["R2"].Children["R2,G1"].Children.Count.Should().Be(3);
            dto.Rubrics["R2"].Children["R2,G1"].Children.Should().ContainKeys("R2,G1,P1", "R2,G1,P2", "R2,G1,P3");

            dto.Rubrics["R2"].Children["R2,G2"].ConjunctElementCode.Should().Be("R2,G2");
            dto.Rubrics["R2"].Children["R2,G2"].ElementCode.Should().Be("G2");
            dto.Rubrics["R2"].Children["R2,G2"].ShortName.Should().Be("G2");
            dto.Rubrics["R2"].Children["R2,G2"].Children.Count.Should().Be(2);
            dto.Rubrics["R2"].Children["R2,G2"].Children.Should().ContainKeys("R2,G2,SG1", "R2,G2,SG2");

            dto.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Count.Should().Be(4);
            dto.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Should().ContainKeys("R2,G2,SG1,P1", "R2,G2,SG1,P2", "R2,G2,SG1,P3", "R2,G2,SG1,P4");
            dto.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Count.Should().Be(2);
            dto.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Should().ContainKeys("R2,G2,SG2,P1", "R2,G2,SG2,P2");
        }

        [Fact]
        public void Can_parse_result()
        {
            var json = File.ReadAllText("./Data/checklist.json");
            var dto = JsonConvert.DeserializeObject<ChecklistDeserializationDto>(json);
            var r1 = ResultFactory.Parse(dto.Rubrics["R1"]);
            r1.Should().NotBeNull();
            r1.Children.Count.Should().Be(2);
            r1.Children.Should().ContainKeys("R1,P1", "R1,P2");
            
            r1.Children["R1,P1"].ConjunctElementCode.Should().Be("R1,P1");
            r1.Children["R1,P1"].ElementCode.Should().Be("P1");
            r1.Children["R1,P1"].ShortName.Should().Be("P1");
            r1.Children["R1,P1"].Parent.Should().NotBeNull();
            r1.Children["R1,P1"].Parent.ConjunctElementCode.Should().Be("R1");
            r1.Children["R1,P1"].Parent.ElementCode.Should().Be("R1");
            r1.Children["R1,P1"].Parent.ShortName.Should().Be("R1");
            r1.Children["R1,P2"].ConjunctElementCode.Should().Be("R1,P2");
            r1.Children["R1,P2"].ElementCode.Should().Be("P2");
            r1.Children["R1,P2"].ShortName.Should().Be("P2");
            r1.Children["R1,P2"].Parent.Should().NotBeNull();
            r1.Children["R1,P2"].Parent.ConjunctElementCode.Should().Be("R1");
            r1.Children["R1,P2"].Parent.ElementCode.Should().Be("R1");
            r1.Children["R1,P2"].Parent.ShortName.Should().Be("R1");

            var r2 = ResultFactory.Parse(dto.Rubrics["R2"]);
            r2.Should().NotBeNull();
            r2.Children.Count.Should().Be(2);
            r2.Children.Should().ContainKeys("R2,G1", "R2,G2");
            
            r2.Children["R2,G1"].ConjunctElementCode.Should().Be("R2,G1");
            r2.Children["R2,G1"].ElementCode.Should().Be("G1");
            r2.Children["R2,G1"].ShortName.Should().Be("G1");
            r2.Children["R2,G1"].Parent.Should().NotBeNull();
            r2.Children["R2,G1"].Parent.ConjunctElementCode.Should().Be("R2");
            r2.Children["R2,G1"].Parent.ElementCode.Should().Be("R2");
            r2.Children["R2,G1"].Parent.ShortName.Should().Be("R2");
            r2.Children["R2,G1"].Children.Count.Should().Be(3);
            r2.Children["R2,G1"].Children.Should().ContainKeys("R2,G1,P1", "R2,G1,P2", "R2,G1,P3");

            r2.Children["R2,G2"].ConjunctElementCode.Should().Be("R2,G2");
            r2.Children["R2,G2"].ElementCode.Should().Be("G2");
            r2.Children["R2,G2"].ShortName.Should().Be("G2");
            r2.Children["R2,G2"].Parent.Should().NotBeNull();
            r2.Children["R2,G2"].Parent.ConjunctElementCode.Should().Be("R2");
            r2.Children["R2,G2"].Parent.ElementCode.Should().Be("R2");
            r2.Children["R2,G2"].Parent.ShortName.Should().Be("R2");
            r2.Children["R2,G2"].Children.Count.Should().Be(2);
            r2.Children["R2,G2"].Children.Should().ContainKeys("R2,G2,SG1", "R2,G2,SG2");

            r2.Children["R2,G2"].Children["R2,G2,SG1"].Children.Count.Should().Be(4);
            r2.Children["R2,G2"].Children["R2,G2,SG1"].Children.Should().ContainKeys("R2,G2,SG1,P1", "R2,G2,SG1,P2", "R2,G2,SG1,P3", "R2,G2,SG1,P4");
            r2.Children["R2,G2"].Children["R2,G2,SG2"].Children.Count.Should().Be(2);
            r2.Children["R2,G2"].Children["R2,G2,SG2"].Children.Should().ContainKeys("R2,G2,SG2,P1", "R2,G2,SG2,P2");
        }

        [Fact]
        public void Can_parse_checklist()
        {
            var json = File.ReadAllText("./Data/checklist.json");
            var dto = JsonConvert.DeserializeObject<ChecklistDeserializationDto>(json);
            var checklist = ChecklistFactory.Parse(dto);
            ChecklistTestHelper.ChecklistTreeStructureShouldBeConsistent(checklist);
        }
    }

    class Converter : TextWriter
    {
        readonly ITestOutputHelper output_;
        public Converter(ITestOutputHelper output)
        {
            output_ = output;
        }
        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string message)
        {
            output_.WriteLine(message);
        }
        public override void WriteLine(string format, params object[] args)
        {
            output_.WriteLine(format, args);
        }
    }

    public class EntityContractResolver : DefaultContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p =>
                                   p.PropertyName != nameof(Entity.CreatedBy) &&
                                   p.PropertyName != nameof(Entity.CreationDate) &&
                                   p.PropertyName != nameof(Entity.ModifiedBy) &&
                                   p.PropertyName != nameof(Entity.ModificationDate)
                        )
                        .ToList();
        }
    }

    public class ChecklistContractResolver : EntityContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> props = base.CreateProperties(type, memberSerialization);
            return props.Where(p =>
                                   p.PropertyName != nameof(Checklist.DomainEvents) &&
                                   p.PropertyName != nameof(Result.Parent)
                        )
                        .ToList();
        }
    }
}
