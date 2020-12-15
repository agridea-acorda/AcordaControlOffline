using System;
using System.Collections.Generic;
using System.Linq;
using Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Checklist : AggregateRoot, IProgressable
    {
        public int FarmInspectionId { get; }
        public SortedList<string, RubricResult> Rubrics { get; } = new SortedList<string, RubricResult>();
        public double Percent { get; private set; }

        public Checklist(long id, int farmInspectionId) : base(id)
        {
            FarmInspectionId = farmInspectionId;
        }
        public Checklist(int farmInspectionId)
        {
            FarmInspectionId = farmInspectionId;
        }

        public Checklist ProgressTo(double percent)
        {
            if (percent < 0.0 || percent > 1.0) throw new ArgumentOutOfRangeException($"{nameof(percent)} must be between 0.0 and 1.0");
            Percent = percent;
            return this;
        }

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
