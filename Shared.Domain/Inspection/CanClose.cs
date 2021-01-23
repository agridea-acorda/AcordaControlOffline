using System;
using System.Linq.Expressions;
using Agridea.SpecificationPattern;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class CanClose : Specification<Inspection>
    {
        public override Expression<Func<Inspection, bool>> ToExpression()
        {
            return x =>
                !x.CloseStatus.IsClosed && // not already closed
                x.PdfReport != PdfReport.None; // pdf exists
        }
    }
}
