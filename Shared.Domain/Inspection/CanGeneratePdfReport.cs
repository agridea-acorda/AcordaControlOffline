using System;
using System.Linq.Expressions;
using Agridea.SpecificationPattern;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class CanGeneratePdfReport : Specification<Inspection>
    {
        public override Expression<Func<Inspection, bool>> ToExpression()
        {
            return x =>
                x.PercentComputed >= 1.0 - double.Epsilon && // inspection is 100% finished
                x.InspectorSignature != Signature.None &&
                !string.IsNullOrWhiteSpace(x.InspectorSignature.DataUrl) && // inspector has signed
                x.PdfReport == PdfReport.None; // no pdf already exists
        }
    }
}
