using System.Collections.Generic;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Checklist : AggregateRoot, IProgressable, IOutcomable
    {
        public int FarmInspectionId { get; }
        public SortedList<string, RubricResult> Rubrics { get; } = new SortedList<string, RubricResult>();
        protected int NumChildren => Rubrics?.Count ?? 0;
        public double Percent => NumChildren == 0 ? 0.0 :
                                 (Rubrics?.Sum(x => x.Value?.Percent ?? 0.0) ?? 0.0) / NumChildren;
        public InspectionOutcome OutcomeComputed => NumChildren == 0 ? InspectionOutcome.NotInspected :
                                            Rubrics?.Any(x => (x.Value?.OutcomeComputed ?? InspectionOutcome.NotInspected) == InspectionOutcome.NotOk) ?? false ? InspectionOutcome.NotOk :
                                            Rubrics?.Any(x => (x.Value?.OutcomeComputed ?? InspectionOutcome.NotInspected) == InspectionOutcome.PartiallyOk) ?? false ? InspectionOutcome.PartiallyOk :
                                            Rubrics?.Any(x => (x.Value?.OutcomeComputed ?? InspectionOutcome.NotInspected) == InspectionOutcome.Ok) ?? false ? InspectionOutcome.Ok :
                                            Rubrics?.Any(x => (x.Value?.OutcomeComputed ?? InspectionOutcome.NotInspected) == InspectionOutcome.NotApplicable) ?? false ? InspectionOutcome.NotApplicable :
                                            InspectionOutcome.NotInspected;

        public Checklist(long id, int farmInspectionId) : base(id)
        {
            FarmInspectionId = farmInspectionId;
        }
        public Checklist(int farmInspectionId)
        {
            FarmInspectionId = farmInspectionId;
        }

        public Checklist AddRubric(string key, RubricResult rubricResult)
        {
            Rubrics.TryAdd(key, rubricResult);
            return this;
        }

        public ITreeNode<Result> Find(string conjunctElementCode)
        {
            return Rubrics.Select(rubric => rubric.Value.Find(conjunctElementCode))
                          .FirstOrDefault(found => found != null);
        }

        public void SetOutcome(string conjunctElementCode, InspectionOutcome outcome)
        {
            var result = Find(conjunctElementCode);
            result.SetOutcome(outcome);
            RaiseDomainEvent(new OutcomeSetEvent(outcome, result));
        }

        public class OutcomeSetEvent : ValueObject, IDomainEvent
        {
            public OutcomeSetEvent(InspectionOutcome outcome, IResult result)
            {
                Outcome = outcome;
                Result = result;
            }
            public IResult Result { get; }
            public InspectionOutcome Outcome { get; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Outcome;
                yield return Result.ConjunctElementCode;
            }
        }
    }
}
