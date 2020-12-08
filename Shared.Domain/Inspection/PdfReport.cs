using System.Collections.Generic;
using System.Linq;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection
{
    public class PdfReport : ValueObject
    {
        public static PdfReport None => new PdfReport(Enumerable.Empty<byte>().ToArray());
        public PdfReport(byte[] bytes)
        {
            Bytes = bytes;
        }

        public byte[] Bytes { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Bytes;
        }
    }
}
