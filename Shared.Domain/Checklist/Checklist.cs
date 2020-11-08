using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Checklist : AggregateRoot
    {
        public Checklist(SortedList<string, RubricResult> rubricResults)
        {
            RubricResults = rubricResults;
        }

        public SortedList<string, RubricResult> RubricResults { get; }
    }
}
