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
        [Fact]
        public void Can_Construct_Checklist()
        {
            var checklist = new Checklist.Checklist()
                .AddRubric("R1", new RubricResult("R1", "R1", "R1")
                                    .AddChild("R1,P1", new PointResult("R1,P1", "P1", "P1"))
                                    .AddChild("R1,P2", new PointResult("R1,P2", "P2", "P2")))
                .AddRubric("R2", new RubricResult("R2", "R2", "R2")
                                  .AddChild("R2,G1", new GroupResult("R2,G1", "G1", "G1")
                                                   .AddChild("R2,G1,P1", new PointResult("R2,G1,P1","P1", "P1"))
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
            
            checklist.Rubrics.Count.Should().Be(2);
            checklist.Rubrics["R1"].Children.Count.Should().Be(2);
            checklist.Rubrics["R1"].Children.Should().ContainKeys("R1,P1", "R1,P2");
            checklist.Rubrics["R1"].Children["R1,P1"].Parent.Should().NotBeNull();
            //checklist.Rubrics["R1"].Children["R1,P1"].Parent.ConjunctElementCode.Should().Be("R1");
            checklist.Rubrics["R2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children.Should().ContainKeys("R2,G1", "R2,G2");
            checklist.Rubrics["R2"].Children["R2,G1"].Children.Count.Should().Be(3);
            checklist.Rubrics["R2"].Children["R2,G1"].Children.Should().ContainKeys("R2,G1,P1", "R2,G1,P2", "R2,G1,P3");
            checklist.Rubrics["R2"].Children["R2,G2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children.Should().ContainKeys("R2,G2,SG1", "R2,G2,SG2");
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Count.Should().Be(4);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG1"].Children.Should().ContainKeys("R2,G2,SG1,P1", "R2,G2,SG1,P2", "R2,G2,SG1,P3", "R2,G2,SG1,P4");
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Count.Should().Be(2);
            checklist.Rubrics["R2"].Children["R2,G2"].Children["R2,G2,SG2"].Children.Should().ContainKeys("R2,G2,SG2,P1", "R2,G2,SG2,P2");
        }
    }
}
