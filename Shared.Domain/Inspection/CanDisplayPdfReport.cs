using System;
using System.Linq.Expressions;
using Agridea.SpecificationPattern;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class CanDisplayPdfReport : Specification<Inspection>
    {
        public override Expression<Func<Inspection, bool>> ToExpression()
        {
            return x => x.PdfReport != PdfReport.None;
        }
    }
}
