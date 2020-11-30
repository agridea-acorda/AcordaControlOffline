using System;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;
using FluentAssertions;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class TestDataHelper
    {
        public static Checklist.Checklist BuildChecklist()
        {
            var checklist = new Checklist.Checklist()
                            .AddRubric("R1", new RubricResult("R1", "R1", "R1")
                                             .AddChild("R1,P1", new PointResult("R1,P1", "P1", "P1"))
                                             .AddChild("R1,P2", new PointResult("R1,P2", "P2", "P2")))
                            .AddRubric("R2", new RubricResult("R2", "R2", "R2")
                                             .AddChild("R2,G1", new GroupResult("R2,G1", "G1", "G1")
                                                                .AddChild("R2,G1,P1", new PointResult("R2,G1,P1", "P1", "P1"))
                                                                .AddChild("R2,G1,P2", new PointResult("R2,G1,P2", "P2", "P2"))
                                                                .AddChild("R2,G1,P3", new PointResult("R2,G1,P3", "P3", "P3")))
                                             .AddChild("R2,G2", new GroupResult("R2,G2", "G2", "G2")
                                                                .AddChild("R2,G2,SG1", new GroupResult("R2,G2,SG1", "SG1", "SG1")
                                                                                       .AddChild("R2,G2,SG1,P1", new PointResult("R2,G2,SG1,P1", "P1", "P1"))
                                                                                       .AddChild("R2,G2,SG1,P2", new PointResult("R2,G2,SG1,P2", "P2", "P2"))
                                                                                       .AddChild("R2,G2,SG1,P3", new PointResult("R2,G2,SG1,P3", "P3", "P3"))
                                                                                       .AddChild("R2,G2,SG1,P4", new PointResult("R2,G2,SG1,P4", "P4", "P4")))
                                                                .AddChild("R2,G2,SG2", new GroupResult("R2,G2,SG2", "SG2", "SG2")
                                                                                       .AddChild("R2,G2,SG2,P1", new PointResult("R2,G2,SG2,P1", "P1", "P1"))
                                                                                       .AddChild("R2,G2,SG2,P2", new PointResult("R2,G2,SG2,P2", "P2", "P2")))));
            return checklist;
        }

        public static void ChecklistTreeStructureShouldBeConsistent(Checklist.Checklist checklist)
        {
            checklist.Should().NotBeNull();
            
            checklist.Rubrics.Count.Should().Be(2);
            checklist.Rubrics["R1"].Children.Count.Should().Be(2);
            checklist.Rubrics["R1"].Children.Should().ContainKeys("R1,P1", "R1,P2");
            checklist.Rubrics["R2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children.Should().ContainKeys("R2,G1", "R2,G2");

            checklist.Rubrics["R1"].Children["R1,P1"].ConjunctElementCode.Should().Be("R1,P1");
            checklist.Rubrics["R1"].Children["R1,P1"].ElementCode.Should().Be("P1");
            checklist.Rubrics["R1"].Children["R1,P1"].ShortName.Should().Be("P1");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.Should().NotBeNull();
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.ConjunctElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.ElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.ShortName.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P2"].ConjunctElementCode.Should().Be("R1,P2");
            checklist.Rubrics["R1"].Children["R1,P2"].ElementCode.Should().Be("P2");
            checklist.Rubrics["R1"].Children["R1,P2"].ShortName.Should().Be("P2");
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.Should().NotBeNull();
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.ConjunctElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.ElementCode.Should().Be("R1");
            checklist.Rubrics["R1"].Children["R1,P2"].Parent.ShortName.Should().Be("R1");

            checklist.Rubrics["R2"].Children["R2,G1"].ConjunctElementCode.Should().Be("R2,G1");
            checklist.Rubrics["R2"].Children["R2,G1"].ElementCode.Should().Be("G1");
            checklist.Rubrics["R2"].Children["R2,G1"].ShortName.Should().Be("G1");
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.Should().NotBeNull();
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.ConjunctElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.ElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G1"].Parent.ShortName.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G1"].Children.Count.Should().Be(3);
            checklist.Rubrics["R2"].Children["R2,G1"].Children.Should().ContainKeys("R2,G1,P1", "R2,G1,P2", "R2,G1,P3");

            checklist.Rubrics["R2"].Children["R2,G2"].ConjunctElementCode.Should().Be("R2,G2");
            checklist.Rubrics["R2"].Children["R2,G2"].ElementCode.Should().Be("G2");
            checklist.Rubrics["R2"].Children["R2,G2"].ShortName.Should().Be("G2");
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.Should().NotBeNull();
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.ConjunctElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.ElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G2"].Parent.ShortName.Should().Be("R2");
            checklist.Rubrics["R2"].Children["R2,G2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children.Should().ContainKeys("R2,G2,SG1", "R2,G2,SG2");

            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Count.Should().Be(4);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Should().ContainKeys("R2,G2,SG1,P1", "R2,G2,SG1,P2", "R2,G2,SG1,P3", "R2,G2,SG1,P4");
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Should().ContainKeys("R2,G2,SG2,P1", "R2,G2,SG2,P2");
        }

        public const int FarmInspectionId = 1;
        public static readonly Guid InspectionId = Guid.NewGuid();
        public const long ChecklistId = 1;
        public static readonly Mandate.Domain Domaine_PER_Grandes_Cultures = new Mandate.Domain(1, "PER grandes cultures");
        public static readonly Campaign Campagne_été_2020 = new Campaign(1, "Campagne été 2020");
        public const string EmptyComment = "";
        
        public static Inspection ConstructInspection()
        {
            return new Inspection(FarmInspectionId, InspectionId, Domaine_PER_Grandes_Cultures, Campagne_été_2020, InspectionReason.Routine, EmptyComment, ChecklistId);
        }

        public static void InspectionShouldBeSuchAsConstructed(Inspection inspection)
        {
            inspection.Should().NotBeNull();
            inspection.FarmInspectionId.Should().Be(FarmInspectionId);
            inspection.Domain.Should().Be(Domaine_PER_Grandes_Cultures);
            inspection.Campaign.Should().Be(Campagne_été_2020);
            inspection.Reason.Should().Be(InspectionReason.Routine);
            inspection.ChecklistId.Should().Be(ChecklistId);
        }
    }
}