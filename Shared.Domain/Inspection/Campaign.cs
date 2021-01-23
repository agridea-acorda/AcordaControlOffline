using System;
using System.Collections.Generic;
using Agridea.DomainDrivenDesign;

namespace Agridea.Acorda.AcordaControlOffline.Shared.Domain.Inspection {
    public class Campaign : ValueObject
    {
        public Campaign(int id, string name)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), $"{nameof(id)} must be > 0");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException($"{nameof(name)} must be non-empty");

            Id = id;
            Name = name;
        }
        public int Id { get; }
        public string Name { get; }
        public int Year { get; }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
            yield return Name;
        }
    }
}