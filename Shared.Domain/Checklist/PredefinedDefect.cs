using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Checklist
{
    public class PredefinedDefect : ValueObject
    {
        public static PredefinedDefect None => new PredefinedDefect(0, "", "");
        
        public PredefinedDefect(int id, string name, string conjunctElementCode)
        {
            bool IsEmpty() => id == 0 && 
                              string.IsNullOrWhiteSpace(name) &&
                              string.IsNullOrWhiteSpace(conjunctElementCode); 
            
            if (!IsEmpty() && id <= 0)
                throw new ArgumentOutOfRangeException($"{nameof(id)} must be positive.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(ConjunctElementCode))
                throw new ArgumentNullException($"{nameof(conjunctElementCode)} cannot be empty.");

            if (!IsEmpty() && string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException($"{nameof(name)} cannot be empty.");

            Id = id;
            Name = name;
            ConjunctElementCode = conjunctElementCode;
        }

        public int Id { get; }
        public string Name { get; }
        public string ConjunctElementCode { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return ConjunctElementCode;
        }
    }

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