using System;
using System.Linq.Expressions;
using Agridea.SpecificationPattern;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class CanReopen : Specification<Inspection>
    {
        public override Expression<Func<Inspection, bool>> ToExpression()
        {
            return x => x.CloseStatus.IsClosed;
        }
    }
}
