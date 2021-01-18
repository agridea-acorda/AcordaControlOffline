using System.IO;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using FluentAssertions;
using Xunit;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Tests
{
    public class ChecklistTests
    {
        private readonly Checklist.Checklist checklist_;

        public ChecklistTests()
        {
            checklist_ = TestDataHelper.ConstructChecklist();
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

        [Fact]
        public void Empty_checklist_Percent_should_be_zero_by_convention()
        {
            var checklist = new Checklist.Checklist(1, 1);
            checklist.Percent.Should().Be(0);
        }

        [Fact]
        public void Percent_should_be_zero_when_no_top_level_node_has_an_outcome()
        {
            checklist_.Percent.Should().Be(0);
        }

        [Fact]
        public void Percent_should_be_one_when_all_top_level_nodes_have_an_outcome()
        {
            foreach (var kvp in checklist_.Rubrics)
            {
                ITreeNode<Result> node = kvp.Value;
                node.SetOutcome(InspectionOutcome.Ok);
            }

            checklist_.Percent.Should().Be(1);
        }

        [Fact]
        public void Percent_regular_test_case_1()
        {
            checklist_.Find("R1").SetOutcome(InspectionOutcome.Ok);
            checklist_.Percent.Should().Be(0.5);
        }

        [Fact]
        public void Percent_regular_test_case_2()
        {
            checklist_.Find("R1,P1").SetOutcome(InspectionOutcome.Ok);
            checklist_.Percent.Should().Be(0.25);
        }

        [Fact]
        public void Percent_regular_test_case_3()
        {
            checklist_.Find("R1,P1").SetOutcome(InspectionOutcome.Ok);
            checklist_.Percent.Should().Be(0.25);
        }

        [Fact]
        public void Percent_regular_test_case_4()
        {
            checklist_.SetOutcome("R2,G2,SG1,P1", InspectionOutcome.Ok);
            checklist_.Find("R2,G2,SG1,P1").Percent.Should().Be(1);
            checklist_.Find("R2,G2,SG1").Percent.Should().Be(1.0 / 4);
            checklist_.Find("R2,G2").Percent.Should().Be(1.0 / 4 / 2);
            checklist_.Find("R2").Percent.Should().Be(1.0 / 4 / 2 / 2);
            checklist_.Percent.Should().Be(1.0 / 4 / 2 / 2 / 2);
        }

        [Fact]
        public void Outcome_is_nc_by_convention_when_checklist_is_empty()
        {
            var checklist = new Checklist.Checklist(1, 1);
            checklist.Outcome.Should().Be(InspectionOutcome.NotInspected);
        }

        [Fact]
        public void Point_outcome_is_nc_when_no_outcome_is_set()
        {
            checklist_.Find("R1,P1").ComputedOutcome.Should().Be(InspectionOutcome.NotInspected);
        }

        [Fact]
        public void Group_outcome_is_regular_outcome_when_set()
        {
            checklist_.SetOutcome("R2,G2,SG1", InspectionOutcome.Ok);
            checklist_.Find("R2,G2,SG1").ComputedOutcome.Should().Be(InspectionOutcome.Ok);
        }

        [Fact]
        public void Outcome_na_takes_precedence_over_nc()
        {
            checklist_.SetOutcome("R2,G2,SG1,P1", InspectionOutcome.NotApplicable);
            checklist_.SetOutcome("R2,G2,SG1,P2", InspectionOutcome.NotInspected);
            checklist_.SetOutcome("R2,G2,SG1,P3", InspectionOutcome.NotInspected);
            checklist_.SetOutcome("R2,G2,SG1,P4", InspectionOutcome.NotInspected);
            checklist_.Find("R2,G2,SG1").ComputedOutcome.Should().Be(InspectionOutcome.NotApplicable);
            checklist_.Find("R2,G2").ComputedOutcome.Should().Be(InspectionOutcome.NotApplicable);
            checklist_.Find("R2").ComputedOutcome.Should().Be(InspectionOutcome.NotApplicable);
            checklist_.Outcome.Should().Be(InspectionOutcome.NotApplicable);
        }

        [Fact]
        public void Outcome_ok_takes_precedence_over_na()
        {
            checklist_.SetOutcome("R2,G2,SG1,P1", InspectionOutcome.Ok);
            checklist_.SetOutcome("R2,G2,SG1,P2", InspectionOutcome.NotApplicable);
            checklist_.SetOutcome("R2,G2,SG1,P3", InspectionOutcome.NotInspected);
            checklist_.SetOutcome("R2,G2,SG1,P4", InspectionOutcome.NotInspected);
            checklist_.Find("R2,G2,SG1").ComputedOutcome.Should().Be(InspectionOutcome.Ok);
            checklist_.Outcome.Should().Be(InspectionOutcome.Ok);
        }

        [Fact]
        public void Outcome_pok_takes_precedence_over_ok()
        {
            checklist_.SetOutcome("R2,G2,SG1,P1", InspectionOutcome.PartiallyOk);
            checklist_.SetOutcome("R2,G2,SG1,P2", InspectionOutcome.Ok);
            checklist_.SetOutcome("R2,G2,SG1,P3", InspectionOutcome.NotApplicable);
            checklist_.SetOutcome("R2,G2,SG1,P4", InspectionOutcome.NotInspected);
            checklist_.Find("R2,G2,SG1").ComputedOutcome.Should().Be(InspectionOutcome.PartiallyOk);
            checklist_.Outcome.Should().Be(InspectionOutcome.PartiallyOk);
        }

        [Fact]
        public void Outcome_nok_takes_precedence_over_pok()
        {
            checklist_.SetOutcome("R2,G2,SG1,P1", InspectionOutcome.NotOk);
            checklist_.SetOutcome("R2,G2,SG1,P2", InspectionOutcome.PartiallyOk);
            checklist_.SetOutcome("R2,G2,SG1,P3", InspectionOutcome.Ok);
            checklist_.SetOutcome("R2,G2,SG1,P4", InspectionOutcome.NotApplicable);
            checklist_.Find("R2,G2,SG1").ComputedOutcome.Should().Be(InspectionOutcome.NotOk);
            checklist_.Outcome.Should().Be(InspectionOutcome.NotOk);
        }
    }
}