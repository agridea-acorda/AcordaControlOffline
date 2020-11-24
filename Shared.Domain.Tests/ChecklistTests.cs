using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using FluentAssertions;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class ChecklistTests
    {
        private readonly Checklist.Checklist checklist_;

        public ChecklistTests()
        {
            checklist_ = ChecklistTestHelper.BuildChecklist();
        }

        [Fact]
        public void Checklist_tree_structure_should_be_consistent()
        {
            ChecklistTestHelper.ChecklistTreeStructureShouldBeConsistent(checklist_);
        }

        [Fact]
        public void Can_find_nodes_in_checklist()
        {
            checklist_.Rubrics["R2"].Find("R2").Should().NotBeNull();
            checklist_.Rubrics["R2"].Find("R2").ConjunctElementCode.Should().Be("R2");
            checklist_.Rubrics["R2"].Find("R2,G2").Should().NotBeNull();
            checklist_.Rubrics["R2"].Find("R2,G2").ConjunctElementCode.Should().Be("R2,G2");
        }
    }
}