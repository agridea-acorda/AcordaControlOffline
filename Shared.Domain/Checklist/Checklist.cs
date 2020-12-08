using System.Collections.Generic;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Checklist : AggregateRoot
    {
        public SortedList<string, RubricResult> Rubrics { get; } = new SortedList<string, RubricResult>();

        public Checklist(long id) : base(id) { }
        public Checklist() { }

        public Checklist AddRubric(string key, RubricResult rubricResult)
        {
            Rubrics.TryAdd(key, rubricResult);
            return this;
        }

        public IResult Find(string conjunctElementCode)
        {
            return Rubrics.Select(rubric => rubric.Value.Find(conjunctElementCode))
                          .FirstOrDefault(found => found != null);
        }

        public void SetOutcome<T>(InspectionOutcome outcome, T result) where T: Result
        {
            result.SetOutcome(outcome);
            RaiseDomainEvent(new OutcomeSetEvent<T>(outcome, result));
        }

        public class OutcomeSetEvent<T> : ValueObject, IDomainEvent where T: Result
        {
            public OutcomeSetEvent(InspectionOutcome outcome, T result)
            {
                Outcome = outcome;
                Result = result;
            }
            public T Result { get; }
            public InspectionOutcome Outcome { get; }

            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Outcome;
                yield return Result.ConjunctElementCode;
            }
        }
    }
}
