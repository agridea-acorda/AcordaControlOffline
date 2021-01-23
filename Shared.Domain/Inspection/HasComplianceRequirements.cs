using System;
using System.Linq.Expressions;
using Agridea.SpecificationPattern;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class HasComplianceRequirements : Specification<Inspection>
    {
        public override Expression<Func<Inspection, bool>> ToExpression()
        {
            return x => x.Compliance != Compliance.Empty &&
                        x.Compliance.DueDate.HasValue &&
                        !string.IsNullOrWhiteSpace(x.Compliance.ActionsOrDocuments);
        }
    }
}
