using FluentAssertions;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class ChecklistTests
    {
        private readonly Checklist.Checklist checklist_;

        public ChecklistTests()
        {
            checklist_ = TestDataHelper.BuildChecklist();
        }

        [Fact]
        public void Checklist_tree_structure_should_be_consistent()
        {
            TestDataHelper.ChecklistTreeStructureShouldBeConsistent(checklist_);
        }

        [Fact]
        public void Can_find_nodes_in_checklist()
        {
            checklist_.Rubrics["R2"].Find(x => x.ConjunctElementCode == "R2").Should().NotBeNull();
            checklist_.Rubrics["R2"].Find(x => x.ConjunctElementCode == "R2").ConjunctElementCode.Should().Be("R2");
            checklist_.Rubrics["R2"].Find(x => x.ConjunctElementCode == "R2,G2").Should().NotBeNull();
            checklist_.Rubrics["R2"].Find(x => x.ConjunctElementCode == "R2,G2").ConjunctElementCode.Should().Be("R2,G2");
            checklist_.Rubrics["R2"].Find(x => x.ConjunctElementCode == "R2,G1,P2").ConjunctElementCode.Should().Be("R2,G1,P2");
            checklist_.Rubrics["R2"].Find(x => x.ConjunctElementCode == "R2,G2,SG1,P4").ConjunctElementCode.Should().Be("R2,G2,SG1,P4");

            checklist_.Rubrics["R2"].Find("R2").Should().NotBeNull();
            checklist_.Rubrics["R2"].Find("R2").ConjunctElementCode.Should().Be("R2");
            checklist_.Rubrics["R2"].Find("R2,G2").Should().NotBeNull();
            checklist_.Rubrics["R2"].Find("R2,G2").ConjunctElementCode.Should().Be("R2,G2");
            checklist_.Rubrics["R2"].Find("R2,G1,P2").ConjunctElementCode.Should().Be("R2,G1,P2");
            checklist_.Rubrics["R2"].Find("R2,G2,SG1,P4").ConjunctElementCode.Should().Be("R2,G2,SG1,P4");

            checklist_.Find("R2").Should().NotBeNull();
            checklist_.Find("R2").ConjunctElementCode.Should().Be("R2");
            checklist_.Find("R2,G2").Should().NotBeNull();
            checklist_.Find("R2,G2").ConjunctElementCode.Should().Be("R2,G2");
            checklist_.Find("R2,G1,P2").ConjunctElementCode.Should().Be("R2,G1,P2");
            checklist_.Find("R2,G2,SG1,P4").ConjunctElementCode.Should().Be("R2,G2,SG1,P4");
        }
    }
}