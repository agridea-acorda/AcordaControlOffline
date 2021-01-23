using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
    public class Domain : ValueObject
    {
        public Domain(int id, string shortName)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), $"{nameof(id)} must be > 0");

            if (string.IsNullOrWhiteSpace(shortName))
                throw new ArgumentException($"{nameof(shortName)} must be non-empty");

            Id = id;
            ShortName = shortName;
        }
        public int Id { get; }
        public string ShortName { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return ShortName;
        }
    }
}