using System;
using System.Collections.Generic;
using System.Text;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using FluentAssertions;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class ChecklistTests
    {
        private static Checklist.Checklist BuildChecklist()
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

        [Fact]
        public void Checklist_tree_structure_should_be_consistent()
        {
            var checklist = BuildChecklist();
            
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

        [Fact]
        public void Can_find_nodes_in_checklist()
        {
            var checklist = BuildChecklist();

            checklist.Rubrics["R2"].Find("R2").Should().NotBeNull();
            checklist.Rubrics["R2"].Find("R2").ConjunctElementCode.Should().Be("R2");
            checklist.Rubrics["R2"].Find("R2,G2").Should().NotBeNull();
            checklist.Rubrics["R2"].Find("R2,G2").ConjunctElementCode.Should().Be("R2,G2");
        }
    }
}
