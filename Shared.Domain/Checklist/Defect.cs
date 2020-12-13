using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class Defect : ValueObject
    {
        public string Description { get; }
        public Measurement Size { get; }
        public Defect(string description, Measurement size = null)
        {
            if (size == null)
                size = Measurement.Unspecified;

            Description = description;
            Size = size;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
            yield return Size;
        }

        public class Measurement : ValueObject
        {
            public double Size { get; }
            public string Unit { get; }
            public static Measurement Unspecified => new Measurement(0, "");
            public Measurement(double size, string unit)
            {
                Size = size;
                Unit = unit;
            }
            protected override IEnumerable<object> GetEqualityComponents()
            {
                yield return Size;
                yield return Unit;
            }
        }
    }
}