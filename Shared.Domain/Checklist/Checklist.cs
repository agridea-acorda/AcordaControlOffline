using System.Collections.Generic;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Mandate;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Checklist : AggregateRoot
    {
        private readonly SortedList<string, RubricResult> rubrics_ = new SortedList<string, RubricResult>();

        public SortedList<string, RubricResult> Rubrics => rubrics_;

        public Checklist AddRubric(string key, RubricResult rubricResult)
        {
            rubrics_.TryAdd(key, rubricResult);
            return this;
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
