using System;
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
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            var pdfReport = (PdfReport)obj;
            return ByteArrayCompare(Bytes, pdfReport.Bytes);
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return (current * 23) + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(PdfReport a, PdfReport b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;
            return a.Equals(b);
        }

        public static bool operator !=(PdfReport a, PdfReport b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Uses Span<T> for efficient comparison, see https://stackoverflow.com/questions/43289/comparing-two-byte-arrays-in-net
        /// </summary>
        static bool ByteArrayCompare(ReadOnlySpan<byte> a1, ReadOnlySpan<byte> a2)
        {
            return a1.SequenceEqual(a2);
        }
    }
}
